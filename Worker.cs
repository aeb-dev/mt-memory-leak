using MassTransit;

namespace mt_memory_leak;

public class Worker : BackgroundService
{
    private readonly IBus _bus;
    private readonly ILogger<Worker> _logger;

    public Worker(
        IBus bus,
        ILogger<Worker> logger
    )
    {
        _bus = bus;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await _bus.Publish(
                    new TestMessage()
                    {
                        Value1 = "Hello",
                        Value2 = "World",
                        Value3 = "Memory",
                        Value4 = "Test"
                    },
                    stoppingToken
                );
                // await Task.Delay(100, stoppingToken);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
