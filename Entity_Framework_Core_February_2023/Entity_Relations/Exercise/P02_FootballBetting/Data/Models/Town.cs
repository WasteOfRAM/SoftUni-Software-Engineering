using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

[Table("Towns")]
public class Town
{
    public Town()
    {
        this.Teams = new HashSet<Team>();
        this.Players = new HashSet<Player>();
    }

    [Key]
    public int TownId { get; set; }

    public string Name { get; set; } = null!;

    public int CountryId { get; set; }
    [ForeignKey(nameof(CountryId))]
    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Team> Teams { get; set; }

    public virtual ICollection<Player> Players { get; set; }
}
