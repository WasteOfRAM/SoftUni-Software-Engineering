using System.ComponentModel.DataAnnotations;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.ImportDto;

public class ImportCardDto
{
    [Required]
    [RegularExpression(ValidationConstraints.CardNumberRegecPatern)]
    public string Number { get; set; } = null!;

    [Required]
    [RegularExpression(ValidationConstraints.CardCvcRegexPatern)]
    public string Cvc { get; set; } = null!;

    [Required]
    public string Type { get; set; } = null!;
}
