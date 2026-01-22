using System.Text.Json;
using MassTransit;

namespace CDM.Match.Consumer;

public class PongConsumer : IConsumer<Pongv1>
{
    public PongConsumer()
    {
    }

    public Task Consume(ConsumeContext<Pongv1> context)
    {
        Console.WriteLine(context.CorrelationId);
        Console.WriteLine(context.Message.CorrelationId);
        Console.WriteLine(context.Message.Message);
        Console.WriteLine(context.Headers);
        Console.WriteLine("Message reçu : " + nameof(Pongv1));
        return Task.CompletedTask;
    }
}

public record Pongv1(string CorrelationId, string Message);
