using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.ImportDto;

public class ImportUserDto
{
    [Required]
    [MinLength(ValidationConstraints.UsernameMinLength)]
    [MaxLength(ValidationConstraints.UsernameMaxLength)]
    public string Username { get; set; } = null!;

    [Required]
    [RegularExpression(ValidationConstraints.UserFullNameRegexPatern)]
    public string FullName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Range(ValidationConstraints.UserAgeMinValue, ValidationConstraints.UserAgeMaxValue)]
    public int Age { get; set; }

    public ImportCardDto[] Cards { get; set; } = null!;
}
