namespace CDM.Match.DTO;

public class CreateOddsDto
{
    public int MatchId { get; set; }
    public float OddFirstTeamWinning { get; set; }
    public float OddSecondTeamWinning { get; set; }
    public float OddDraw { get; set; }
    public DateOnly OddCreated { get; set; }
}