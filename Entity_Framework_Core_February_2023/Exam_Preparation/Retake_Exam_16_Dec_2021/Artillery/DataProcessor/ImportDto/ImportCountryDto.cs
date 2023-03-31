using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto;

[XmlType("Country")]
public class ImportCountryDto
{
    [XmlIgnore]
    public int Id { get; set; }

    [JsonIgnore]
    [XmlElement("CountryName")]
    [Required]
    [MinLength(ValidationConstraints.CountryNameMinLength)]
    [MaxLength(ValidationConstraints.CountryNameMaxLength)]
    public string CountryName { get; set; } = null!;

    [JsonIgnore]
    [XmlElement("ArmySize")]
    [Required]
    [Range(ValidationConstraints.ArmySizeMin, ValidationConstraints.ArmySizeMax)]
    public int ArmySize { get; set; }
}
