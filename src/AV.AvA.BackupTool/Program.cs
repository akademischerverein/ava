using System.Text.Json;
using AV.AvA.BackupTool.Commands;
using AV.AvA.BackupTool.Services;
using AV.AvA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace AV.AvA.BackupTool;

public class Program
{
    public static Task<int> Main(string[] args)
            => CreateHostBuilder(args).RunCommandLineApplicationAsync<MainCommand>(args);

    public static IHostBuilder CreateHostBuilder(string[] args)
    => Host.CreateDefaultBuilder(args)
           .ConfigureServices((hostContext, services) =>
           {
               var startup = new Startup(hostContext);
               startup.ConfigureServices(services);
           });
}

internal class Startup
{
    public Startup(HostBuilderContext context)
    {
        Configuration = context.Configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging();
        services.AddSingleton<IClock>(SystemClock.Instance);
        ConfigureDatabases(services);

        services.AddTransient(sp =>
        {
            var x = new JsonSerializerOptions();
            x.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            return x;
        });

        services.AddHostedService<MeasureRuntimeService>();
    }

    private void ConfigureDatabases(IServiceCollection services)
    {
        var connStr = Configuration.GetConnectionString("DefaultConnection");

        services.AddNpgsql<AvADbContext>(
            connStr,
            pgOpts =>
            {
                pgOpts.UseNodaTime();
                pgOpts.MigrationsHistoryTable("__ef_migrations_history");
            },
            ctxOpts =>
            {
                ctxOpts.UseSnakeCaseNamingConvention();
                ctxOpts.EnableDetailedErrors();
                ctxOpts.EnableSensitiveDataLogging();
            });
    }
}
