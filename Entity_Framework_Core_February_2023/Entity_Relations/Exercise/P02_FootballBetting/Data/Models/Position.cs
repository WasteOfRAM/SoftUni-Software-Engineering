using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

[Table("Positions")]
public class Position
{
    public Position()
    {
        this.Players = new HashSet<Player>();
    }

    [Key]
    public int PositionId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Player> Players { get; set; }
}
