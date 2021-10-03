using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NodaTime;

namespace AV.AvA.BackupTool.Services
{
    // see https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-5.0#ihostapplicationlifetime
    internal class MeasureRuntimeService : IHostedService
    {
        private readonly ILogger<MeasureRuntimeService> _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IClock _clock;
        private Instant _started;

        public MeasureRuntimeService(
            ILogger<MeasureRuntimeService> logger,
            IHostApplicationLifetime appLifetime,
            IClock clock)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _clock = clock;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(() =>
            {
                _started = _clock.GetCurrentInstant();
            });
            _appLifetime.ApplicationStopping.Register(() =>
            {
                var totalRuntime = _clock.GetCurrentInstant() - _started;
                _logger.LogInformation("Total runtime: {s:#,0.00} seconds", totalRuntime.TotalSeconds);
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
