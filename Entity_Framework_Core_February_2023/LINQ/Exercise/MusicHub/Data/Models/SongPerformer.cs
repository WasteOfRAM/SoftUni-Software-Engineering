using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Data.Models;

[Table("SongsPerformers")]
public class SongPerformer
{
    public int SongId { get; set; }
    public Song Song { get; set; } = null!;

    public int PerformerId { get; set; }
    public Performer Performer { get; set; } = null!;
}
