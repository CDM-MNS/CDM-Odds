using CDM.Match.DTO;
using CDM.Match.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CDM.Match.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OddsController(IOddsRepository oddsRepository) : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetOddsById(int id)
    {
        var match = oddsRepository.GetOddsById(id);
        return Ok(match);
    }
    
    [HttpGet("all")]
    public IActionResult GetAllOdds()
    {
        var matches = oddsRepository.GetAllOdds();
        return Ok(matches);
    }

    [HttpPost]
    public async Task<IActionResult> AddOdds(CreateOddsDto odds)
    {
        await oddsRepository.CreateOddsAsync(odds);
        return Ok("Odds added");
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateOdds(UpdateOddsDto oddsDto, int id)
    {
        await oddsRepository.UpdateOdds(oddsDto, id);
        return Ok($"Odds {id} updated");
    }
}