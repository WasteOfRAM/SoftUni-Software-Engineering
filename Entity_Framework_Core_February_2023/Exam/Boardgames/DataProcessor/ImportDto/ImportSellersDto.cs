using System.ComponentModel.DataAnnotations;

namespace Boardgames.DataProcessor.ImportDto;

public class ImportSellersDto
{
    [Required]
    [MinLength(ValidationConstraints.SellerNameMinLength)]
    [MaxLength(ValidationConstraints.SellerNameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(ValidationConstraints.SellerAddressMinLength)]
    [MaxLength(ValidationConstraints.SellerAddressMaxLength)]
    public string Address { get; set; } = null!;

    [Required]
    public string Country { get; set; } = null!;

    [Required]
    [RegularExpression(ValidationConstraints.SellerWebsiteRegexPatern)]
    public string Website { get; set; } = null!;

    public int[] Boardgames { get; set; } = null!;
}
