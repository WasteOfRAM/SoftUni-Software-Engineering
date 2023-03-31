using System.ComponentModel.DataAnnotations;

namespace Theatre.DataProcessor.ImportDto;

public class ImportTheatherDto
{
    [Required]
    [MinLength(ValidationConstraints.TheatreNameMinLength)]
    [MaxLength(ValidationConstraints.TheatreNameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(ValidationConstraints.TicketMinRowNumber, ValidationConstraints.TicketMaxRowNumber)]
    public sbyte NumberOfHalls { get; set; }

    [Required]
    [MinLength(ValidationConstraints.TheatreDirectorMinLength)]
    [MaxLength(ValidationConstraints.TheatreDirectorMaxLength)]
    public string Director { get; set; } = null!;

    public ImportTicketDto[] Tickets { get; set; } = null!;
}
