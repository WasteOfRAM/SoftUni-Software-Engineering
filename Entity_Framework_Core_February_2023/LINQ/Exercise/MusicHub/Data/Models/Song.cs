using MusicHub.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Data.Models;

[Table("Songs")]
public class Song
{
    public Song()
    {
        this.SongPerformers= new HashSet<SongPerformer>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.SongNameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    public TimeSpan Duration { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [Required]
    public Genre Genre { get; set; }

    [ForeignKey(nameof(Album))]
    public int? AlbumId { get; set; }
    public Album? Album { get; set; }

    [Required]
    [ForeignKey(nameof(Writer))]
    public int WriterId { get; set; }
    public Writer Writer { get; set; } = null!;

    [Required]
    [Column(TypeName = "money")]
    public decimal Price { get; set; }


    public ICollection<SongPerformer> SongPerformers { get; set; }
}