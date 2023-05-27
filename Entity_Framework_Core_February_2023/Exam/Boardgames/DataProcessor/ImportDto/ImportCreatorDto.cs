using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ImportDto;

[XmlType("Creator")]
public class ImportCreatorDto
{
    [XmlElement("FirstName")]
    [Required]
    [MinLength(ValidationConstraints.CreatorFirstNameMinLength)]
    [MaxLength(ValidationConstraints.CreatorFirstNameMaxLength)]
    public string FirstName { get; set; } = null!;

    [XmlElement("LastName")]
    [Required]
    [MinLength(ValidationConstraints.CreatorLastNameMinLength)]
    [MaxLength(ValidationConstraints.CreatorLastNameMaxLength)]
    public string LastName { get; set; } = null!;

    [XmlArray("Boardgames")]
    public ImportBoardgameDto[] Boardgames { get; set; } = null!;
}
