using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Data.Models;

[Table("Performers")]
public class Performer
{
    public Performer()
    {
        this.PerformerSongs = new HashSet<SongPerformer>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.PerformerFirstNameMaxLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(ValidationConstants.PerformerLastNameMaxLength)]
    public string LastName { get; set; } = null!;

    [Required]
    public int Age { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal NetWorth { get; set; }

    public ICollection<SongPerformer> PerformerSongs { get; set; }
}
