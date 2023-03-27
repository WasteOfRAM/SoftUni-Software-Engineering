using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.ImportDto;

public class ImportTagDto
{

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = null!;
}
