using System.Security.Cryptography;
using AV.AvA.Data;
using AV.AvA.Model;
using AV.AvA.StorageBackend;
using Grpc.Core;
using Microsoft.AspNetCore.WebUtilities;
using NodaTime;

namespace AV.AvA.StorageBackend.Services;

public class LoginService : AvA.Login.LoginBase
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
        var token = WebEncoders.Base64UrlEncode(RandomNumberGenerator.GetBytes(16));
        var loginToken = new LoginToken
        {
            Token = token,
            ValidUntil = _clock.GetCurrentInstant() + Duration.FromMinutes(15),
        };
        _dbContext.LoginTokens.Add(loginToken);
        await _dbContext.SaveChangesAsync();
        await _tokenSender.SendLoginTokenAsync(token);

        return new CreateLoginTokenReply { };
    }
}
