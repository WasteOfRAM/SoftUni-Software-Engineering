using SoftJail.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto;

public class ImportDepartmentCellsDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(25)]
    public string Name { get; set; } = null!;

    public ImportCellDto[] Cells { get; set; } = null!;
}
