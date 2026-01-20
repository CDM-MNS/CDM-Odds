using CDM.Match.DTO;
using CDM.Odds.Models;

namespace CDM.Match.Repository;

public class OddsRepository(OddDbContext context) : IOddsRepository
{

    public async Task CreateOddsAsync(CreateOddsDto oddsDto)
    {
        OddsEntity odds = new OddsEntity()
        {
            MatchId = oddsDto.MatchId,
            OddFirstTeamWinning = oddsDto.OddFirstTeamWinning,
            OddSecondTeamWinning = oddsDto.OddSecondTeamWinning,
            OddDraw = oddsDto.OddDraw,
            OddCreated = oddsDto.OddCreated
        };
        
        await context.Odds.AddAsync(odds);
        await context.SaveChangesAsync();
    }

    public List<OddsEntity> GetAllOdds()
    {
        return context.Odds.ToList();
    }
    
    public OddsEntity GetOddsById(int id)
    {
        return context.Odds.Find(id);
    }

    public async Task UpdateOdds(UpdateOddsDto oddsDto, int id)
    {
        var odd = GetOddsById(id);
        odd.OddFirstTeamWinning = oddsDto.OddFirstTeamWinning;
        odd.OddSecondTeamWinning = oddsDto.OddSecondTeamWinning;
        odd.OddDraw = oddsDto.OddDraw;
        odd.OddUpdated = DateOnly.FromDateTime(DateTime.Now);
        await context.SaveChangesAsync();
    }

}