using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ImportDto;

[XmlType("Project")]
public class ImportProjectDto
{
    [XmlElement("Name")]
    [Required]
    [MaxLength(ValidationConstraints.ProjectNameMaxLength)]
    [MinLength(ValidationConstraints.ProjectNameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [XmlElement("OpenDate")]
    public string OpenDate { get; set; } = null!;

    [XmlElement("DueDate")]
    public string? DueDate { get; set; }

    [XmlArray("Tasks")]
    public ImportTaskDto[] Tasks { get; set; } = null!;
}
