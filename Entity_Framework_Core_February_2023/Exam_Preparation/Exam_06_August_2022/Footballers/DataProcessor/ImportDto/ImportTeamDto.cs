using System.ComponentModel.DataAnnotations;

namespace Footballers.DataProcessor.ImportDto;

public class ImportTeamDto
{
    [Required]
    [MinLength(ValidationConstraints.TeamNameMinLength)]
    [MaxLength(ValidationConstraints.TeamNameMaxLength)]
    [RegularExpression(ValidationConstraints.TeamNameRegexPatern)]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(ValidationConstraints.TeamNationalityMinLength)]
    [MaxLength(ValidationConstraints.TeamNationalityMaxLength)]
    public string Nationality { get; set; } = null!;

    [Required]
    public int Trophies { get; set; }

    public int[] Footballers { get; set; } = null!;
}
