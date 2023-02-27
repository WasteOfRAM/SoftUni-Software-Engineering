using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

[Table("Teams")]
public class Team
{
    public Team()
    {
        this.Players = new HashSet<Player>();
        this.HomeGames = new HashSet<Game>();
        this.AwayGames = new HashSet<Game>();
    }

    [Key]
    public int TeamId { get; set; }

    public string Name { get; set; } = null!;

    public string LogoUrl { get; set; } = null!;

    [MaxLength(3)]
    public string Initials { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Budget { get; set; }

    public int PrimaryKitColorId { get; set; }
    public virtual Color PrimaryKitColor { get; set; } = null!;

    public int SecondaryKitColorId { get; set; }
    public virtual Color SecondaryKitColor { get; set; } = null!;

    public int TownId { get; set; }
    [ForeignKey(nameof(TownId))]
    public virtual Town Town { get; set; } = null!;


    public virtual ICollection<Player> Players { get; set; }

    public virtual ICollection<Game> HomeGames { get; set; }
    public virtual ICollection<Game> AwayGames { get; set; }
}
