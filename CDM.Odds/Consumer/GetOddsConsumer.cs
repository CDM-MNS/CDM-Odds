using CDM.Match.DTO;
using CDM.Match.Repository;
using CDM.Odds.Models;
using CMD.Odds.Messaging;
using MassTransit;

namespace CDM.Match.Consumer;

public class GetOddsConsumer : IConsumer<GetOddsDTO>
{
    private readonly IOddsRepository _oddsRepository;
    private readonly IRabbitMQService _rabbitMQService;
    
    public GetOddsConsumer(IOddsRepository oddsRepository, IRabbitMQService rabbitMQService)
    {
        _oddsRepository = oddsRepository;
        _rabbitMQService = rabbitMQService;
    }
    
    
    public async Task Consume(ConsumeContext<GetOddsDTO> context)
    {
        Console.WriteLine(context.Message.OddId);
        var id = context.Message.OddId;
        var result = _oddsRepository.GetOddsById(id);
        await _rabbitMQService.PublishAsync("get-odds-out", result, "get.oods");
    }
}