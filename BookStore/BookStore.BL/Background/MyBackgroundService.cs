using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace BookStore.BL.Background
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly ILogger<MyBackgroundService> _logger;
        private Timer _timer;

        public MyBackgroundService(ILogger<MyBackgroundService> logger)
        {
            _logger = logger;
           // _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Hello from {nameof(StartAsync)}");
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            Thread.Sleep(2);
            _logger.LogInformation(
                $"Hello from {nameof(StartAsync)} {DateTime.Now.ToString()}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Stopped");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer periodicTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000));
            while (!stoppingToken.IsCancellationRequested)
            {
                 Task.Delay(999, stoppingToken);
                _logger.LogInformation($"{nameof(MyBackgroundService)} {DateTime.Now.ToString()}");
            }

            return  Task.CompletedTask;
        }
    }
}
