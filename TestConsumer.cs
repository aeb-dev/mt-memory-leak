using MassTransit;

namespace mt_memory_leak;

public class TestConsumer : IConsumer<TestMessage>
{

    public TestConsumer()
    {
    }

    public async Task Consume(ConsumeContext<TestMessage> context)
    {
        await context.Publish(context.Message);
    }
}
