using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto;

[XmlType("Shell")]
public class ImportShellDto
{
    [XmlElement("ShellWeight")]
    [Required]
    [Range(ValidationConstraints.ShellWeightMin, ValidationConstraints.ShellWeightMax)]
    public double ShellWeight { get; set; }

    [XmlElement("Caliber")]
    [Required]
    [MinLength(ValidationConstraints.CaliberMin)]
    [MaxLength(ValidationConstraints.CaliberMax)]
    public string Caliber { get; set; } = null!;
}
