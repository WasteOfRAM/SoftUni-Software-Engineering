using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Data.Models;

[Table("Producers")]
public class Producer
{
    public Producer()
    {
        this.Albums = new HashSet<Album>();
    }

    [Key] 
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.ProducerNameMaxLength)]
    public string Name { get; set; } = null!;

    public string? Pseudonym { get; set; }

    public string? PhoneNumber { get; set; }

    public ICollection<Album> Albums { get; set; }
}
