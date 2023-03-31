using Artillery.Data.Models.Enums;
using Artillery.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artillery.DataProcessor.ImportDto;

public class ImportGunDto
{
    [Required]
    public int ManufacturerId { get; set; }

    [Required]
    [Range(ValidationConstraints.GunWeigthMinValue, ValidationConstraints.GunWeigthMaxValue)]
    public int GunWeight { get; set; }

    [Required]
    [Range(ValidationConstraints.BarrelLengthMinValue, ValidationConstraints.BarrelLengthMaxValue)]
    public double BarrelLength { get; set; }

    public int? NumberBuild { get; set; }

    [Required]
    [Range(ValidationConstraints.GunRangeMin, ValidationConstraints.GunRangeMax)]
    public int Range { get; set; }

    [Required]
    public string GunType { get; set; } = null!;

    [Required]
    public int ShellId { get; set; }

    public ImportCountryDto[] Countries { get; set; } = null!;
}
