using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Theatre.DataProcessor.ImportDto;

public class ImportTicketDto
{
    [JsonProperty("Price")]
    [Required]
    [Range(typeof(decimal), ValidationConstraints.TicketMinPrice, ValidationConstraints.TicketMaxPrice)]
    public decimal Price { get; set; }

    [JsonProperty("RowNumber")]
    [Required]
    [Range(ValidationConstraints.TicketMinRowNumber, ValidationConstraints.TicketMaxRowNumber)]
    public sbyte RowNumber { get; set; }

    [JsonProperty("PlayId")]
    [Required]
    public int PlayId { get; set; }
}
