using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VaporStore.Data.Models.Enums;

namespace VaporStore.Data.Models;

public class Card
{
    public Card()
    {
        this.Purchases = new HashSet<Purchase>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstraints.CardNumberMaxLength)]
    public string Number { get; set; } = null!;

    [Required]
    [MaxLength(ValidationConstraints.CardCvcMaxLength)]
    public string Cvc { get; set; } = null!;

    [Required]
    public CardType Type { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public virtual ICollection<Purchase> Purchases { get; set; }
}
