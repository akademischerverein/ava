using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AV.AvA.Data;
using AV.AvA.Model;
using AV.AvA.StorageBackend;
using Grpc.Core;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NodaTime;

namespace AV.AvA.StorageBackend.Services;

public class LoginService : Login.LoginBase
{
    private readonly IClock _clock;
    private readonly AvADbContext _dbContext;
    private readonly ILoginTokenSender _tokenSender;
    private readonly ILogger<LoginService> _l;

    public LoginService(IClock clock, AvADbContext dbContext, ILoginTokenSender loginTokenSender, ILogger<LoginService> logger)
    {
        _clock = clock;
        _dbContext = dbContext;
        _tokenSender = loginTokenSender;
        _l = logger;
    }

    public override async Task<CreateLoginTokenReply> CreateLoginToken(CreateLoginTokenRequest request, ServerCallContext context)
    {
        var people = await _dbContext.GetCurrentPersonVersions()
            .AsNoTracking()
            .ToListAsync();

        var avid = people.SelectMany(p => p.DeserializePerson().Emails.Select(em => (em.Adresse, (int?)p.AvId)))
            .Where(x => StringComparer.OrdinalIgnoreCase.Equals(x.Adresse, request.EMail))
            .Select(x => x.Item2)
            .FirstOrDefault();
        if (!avid.HasValue)
        {
            throw new RpcException(new Grpc.Core.Status(StatusCode.NotFound, "email not found"));
        }

        var token = WebEncoders.Base64UrlEncode(RandomNumberGenerator.GetBytes(16));
        var loginToken = new StorageModel.LoginToken
        {
            Token = token,
            ValidUntil = _clock.GetCurrentInstant() + Duration.FromMinutes(15),
            AvId = avid.Value,
        };
        _dbContext.LoginTokens.Add(loginToken);
        await _dbContext.SaveChangesAsync();
        await _tokenSender.SendLoginTokenAsync(token);

        return new CreateLoginTokenReply { };
    }

    public override async Task<LoginByTokenRequest> LoginByToken(LoginByTokenRequest request, ServerCallContext context)
    {
        var now = _clock.GetCurrentInstant();
        var loginToken = await _dbContext.LoginTokens
            .FirstOrDefaultAsync(lt => lt.Token == request.Token && lt.ValidUntil >= now);

        if (loginToken == null)
        {
            throw new RpcException(new Grpc.Core.Status(StatusCode.NotFound, "token not found"));
        }

        var person = await _dbContext.GetCurrentPersonVersions()
            .FirstOrDefaultAsync(pv => pv.AvId == loginToken.AvId);

        if (person == null)
        {
            throw new RpcException(new Grpc.Core.Status(StatusCode.NotFound, "person not found"));
        }

        loginToken.ValidUntil = null;
        loginToken.UsedAt = now;

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes("very-secure")),
                SecurityAlgorithms.HmacSha256Signature),
        };
        tokenDescriptor.Expires = (now + Duration.FromDays(7)).InUtc().ToDateTimeUtc();

        // TODO Verstorbene und Ausgetretene komplett ausschließen
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginToken.AvId.ToString()),
            new Claim(ClaimTypes.Role, "Reader"),
        };
        if (person.DeserializePerson().AvASchreibzugriff)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Writer"));
        }

        tokenDescriptor.Subject = new ClaimsIdentity(claims);

        var token = tokenHandler.CreateToken(tokenDescriptor);

        await _dbContext.SaveChangesAsync();
        return new LoginByTokenRequest
        {
            Token = tokenHandler.WriteToken(token),
        };
    }
}
