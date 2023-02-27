namespace P01_HospitalDatabase.Data.Models;

public class Diagnose
{
    public int DiagnoseId { get; set; }

    public string Name { get; set; } = null!;

    public string Comments { get; set; } = null!;

    public int PatientId { get; set; }
    public virtual Patient Patient { get; set; } = null!;
}
