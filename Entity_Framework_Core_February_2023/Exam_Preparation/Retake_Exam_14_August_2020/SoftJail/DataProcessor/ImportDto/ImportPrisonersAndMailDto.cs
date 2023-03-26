using SoftJail.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto;

public class ImportPrisonersAndMailDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string FullName { get; set; } = null!;

    [Required]
    [RegularExpression(@"^(The )([A-Z]{1}[a-z]*)$")]
    public string Nickname { get; set; } = null!;

    [Required]
    [Range(18, 65)]
    public int Age { get; set; }

    [Required]
    public string IncarcerationDate { get; set; } = null!;

    public string? ReleaseDate { get; set; }

    [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
    public decimal? Bail { get; set; }

    public int? CellId { get; set; }

    public ImportMailDto[] Mails { get; set; } = null!;
}
