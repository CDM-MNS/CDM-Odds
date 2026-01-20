using CDM.Match.DTO;
using CDM.Odds.Models;

namespace CDM.Match.Repository;

public interface IOddsRepository
{
    Task CreateOddsAsync(CreateOddsDto oddsDto);
    List<OddsEntity> GetAllOdds();
    OddsEntity GetOddsById(int id);
    Task UpdateOdds(UpdateOddsDto oddsDto, int id);
}