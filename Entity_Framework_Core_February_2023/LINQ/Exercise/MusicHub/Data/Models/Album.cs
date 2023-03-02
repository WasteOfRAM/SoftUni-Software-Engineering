using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Data.Models;

[Table("Albums")]
public class Album
{
    public Album()
    {
        this.Songs = new HashSet<Song>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.AlbumNameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    public DateTime ReleaseDate { get; set; }

    [NotMapped]
    public decimal Price => this.Songs.Sum(s => s.Price);

    [ForeignKey(nameof(Producer))]
    public int? ProducerId { get; set; }
    public Producer? Producer { get; set; }

    public ICollection<Song> Songs { get; set; }
}
