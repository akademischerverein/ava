using AV.AvA.Data;
using AV.AvA.StorageBackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var conf = builder.Configuration;

// Add services to the container.
builder.Services.AddGrpc();

services.AddDbContextFactory<AvADbContext>(options =>
{
    options.UseNpgsql(conf.GetConnectionString("DefaultConnection"), o =>
    {
        o.UseNodaTime();
        o.MigrationsHistoryTable("__ef_migrations_history");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
