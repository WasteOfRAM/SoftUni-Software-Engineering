using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.ImportDto;

public class ImportGameDto
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), ValidationConstraints.GamePriceMinValue, ValidationConstraints.GamePriceMaxValue)]
    public decimal Price { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string ReleaseDate { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Developer { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Genre { get; set; } = null!;

    public string[] Tags { get; set; } = null!;
}
