using System.Text.Json;
using AV.AvA.Data;
using AV.AvA.StorageBackend;
using AV.AvA.StorageBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddNpgsql<AvADbContext>(
    connStr,
    pgOpts =>
    {
        pgOpts.UseNodaTime();
        pgOpts.MigrationsHistoryTable("__ef_migrations_history");
    },
    ctxOpts =>
    {
        ctxOpts.UseSnakeCaseNamingConvention();
#if DEBUG
        ctxOpts.EnableDetailedErrors();
        ctxOpts.EnableSensitiveDataLogging();
#endif
    });

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddGrpc();

var jwtSecret = builder.Configuration.GetValue<string>("JwtSecret")
    ?? throw new InvalidOperationException("A JwtSecret needs to be configured.");
var key = JwtKeyDerivation.DeriveKey(jwtSecret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddTransient(sp => AV.AvA.Common.Json.CreateSTJOptions());

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseGrpcWeb();
app.UseCors();

// Configure the HTTP request pipeline.
app.MapGrpcService<PersonVersionRepositoryService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<LoginService>().EnableGrpcWeb().RequireCors("AllowAll");

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
