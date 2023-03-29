using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TeisterMask.DataProcessor.ImportDto;

public class ImportEmpoyeeDto
{
    [JsonProperty("UserName")]
    [Required]
    [MaxLength(ValidationConstraints.EmployeeUsernameMaxLength)]
    [MinLength(ValidationConstraints.EmployeeUsernameMinLength)]
    [RegularExpression(ValidationConstraints.EmployeeUsernameRegexPatern)]
    public string Username { get; set; } = null!;

    [JsonProperty("Email")]
    [EmailAddress]
    [Required]
    public string Email { get; set; } = null!;

    [JsonProperty("Phone")]
    [Required]
    [RegularExpression(ValidationConstraints.EmployeePhoneRegexPatern)]
    public string Phone { get; set; } = null!;

    [JsonProperty("Tasks")]
    public int[] Tasks { get; set; } = null!;
}
