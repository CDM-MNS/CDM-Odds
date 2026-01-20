namespace CDM.Match.DTO;

public class UpdateOddsDto
{
    public float OddFirstTeamWinning { get; set; }
    public float OddSecondTeamWinning { get; set; }
    public float OddDraw { get; set; }
    public DateOnly OddCreated { get; set; }
}