using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

public enum Prediction
{
    Win,
    Lose,
    Draw
}

[Table("Bets")]
public class Bet
{
    [Key]
    public int BetId { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    public Prediction Prediction { get; set; }

    public DateTime DateTime { get; set; }

    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;

    public int GameId { get; set; }
    [ForeignKey(nameof(GameId))]
    public virtual Game Game { get; set; } = null!;
}
