using AV.AvA.Data;
using AV.AvA.StorageBackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.MapGrpcService<PersonVersion>();
app.MapGrpcService<LoginService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
