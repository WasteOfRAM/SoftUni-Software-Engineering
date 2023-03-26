namespace SoftJail.Data.Models;

public class OfficerPrisoner
{
    public int PrisonerId { get; set; }
    public Prisoner Prisoner { get; set; } = null!;

    public int OfficerId { get; set; }
    public Officer Officer { get; set; } = null!;
}
