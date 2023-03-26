using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto;

[XmlType("Officer")]
public class ImportOfficersPrisonersDto
{
    [XmlElement("Name")]
    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string FullName { get; set; } = null!;

    [XmlElement("Money")]
    [Required]
    [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
    public decimal Salary { get; set; }

    [XmlElement("Position")]
    [Required]
    public string Position { get; set; } = null!;

    [XmlElement("Weapon")]
    [Required]
    public string Weapon { get; set; } = null!;

    [XmlElement("DepartmentId")]
    [Required]
    public int DepartmentId { get; set; }

    [XmlArray("Prisoners")]
    [XmlArrayItem("Prisoner")]
    public ImportPrisonerByIdDto[] Prisoners { get; set; } = null!;
}
