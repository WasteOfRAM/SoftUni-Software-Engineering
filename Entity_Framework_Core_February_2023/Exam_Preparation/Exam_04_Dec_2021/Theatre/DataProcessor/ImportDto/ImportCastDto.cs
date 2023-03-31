using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Theatre.DataProcessor.ImportDto;

[XmlType("Cast")]
public class ImportCastDto
{
    [XmlElement("FullName")]
    [Required]
    [MinLength(ValidationConstraints.CastFullNameMinLength)]
    [MaxLength(ValidationConstraints.CastFullNameMaxLength)]
    public string FullName { get; set; } = null!;

    [XmlElement("IsMainCharacter")]
    [Required]
    public bool IsMainCharacter { get; set; }

    [XmlElement("PhoneNumber")]
    [Required]
    [RegularExpression(ValidationConstraints.CastPhoneNumberRegexPatern)]
    public string PhoneNumber { get; set; } = null!;

    [XmlElement("PlayId")]
    [Required]
    public int PlayId { get; set; }
}
