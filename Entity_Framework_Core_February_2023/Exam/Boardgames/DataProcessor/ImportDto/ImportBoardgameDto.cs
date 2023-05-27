using Boardgames.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ImportDto;

[XmlType("Boardgame")]
public class ImportBoardgameDto
{
    [XmlElement("Name")]
    [Required]
    [MinLength(ValidationConstraints.BoardgameNameMinLength)]
    [MaxLength(ValidationConstraints.BoardgameNameMaxLength)]
    public string Name { get; set; } = null!;

    [XmlElement("Rating")]
    [Required]
    [Range(ValidationConstraints.BoardgameRatingMinValue, ValidationConstraints.BoardgameRatingMaxValue)]
    public double Rating { get; set; }

    [XmlElement("YearPublished")]
    [Required]
    [Range(ValidationConstraints.BoardgameYearPublishedMinValue, ValidationConstraints.BoardgameYearPublishedMaxValue)]
    public int YearPublished { get; set; }

    [XmlElement("CategoryType")]
    [Required]
    public int CategoryType { get; set; }

    [XmlElement("Mechanics")]
    [Required]
    public string Mechanics { get; set; } = null!;
}
