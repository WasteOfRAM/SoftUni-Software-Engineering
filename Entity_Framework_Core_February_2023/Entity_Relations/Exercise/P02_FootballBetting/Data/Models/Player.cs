using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

[Table("Players")]
public class Player
{
    public Player()
    {
        this.PlayersStatistics = new HashSet<PlayerStatistic>();
    }

    [Key]
    public int PlayerId { get; set; }

    public string Name { get; set; } = null!;

    public int SquadNumber { get; set; }

    public int Assists { get; set; }

    public int TownId { get; set; }
    [ForeignKey(nameof(TownId))]
    public virtual Town Town { get; set; } = null!;

    public int PositionId { get; set; }
    [ForeignKey(nameof(PositionId))]
    public virtual Position Position { get; set; } = null!;
    public bool IsInjured { get; set; }

    public int TeamId { get; set; }
    [ForeignKey(nameof(TeamId))]
    public virtual Team? Team { get; set; }

    public virtual ICollection<PlayerStatistic> PlayersStatistics { get; set; }
}
