using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ImportDto;

[XmlType("Task")]
public class ImportTaskDto
{
    [XmlElement("Name")]
    [Required]
    [MaxLength(ValidationConstraints.TaskNameMaxLength)]
    [MinLength(ValidationConstraints.TaskNameMinLength)]
    public string Name { get; set; } = null!;

    [XmlElement("OpenDate")]
    [Required]
    public string OpenDate { get; set; } = null!;

    [XmlElement("DueDate")]
    [Required]
    public string DueDate { get; set; } = null!;

    [XmlElement("ExecutionType")]
    [Required]
    public int ExecutionType { get; set; }

    [XmlElement("LabelType")]
    [Required]
    public int LabelType { get; set; }
}
