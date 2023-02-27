using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_SalesDatabase.Data.Models;

[Table("Customers")]
public class Customer
{
    public Customer()
    {
        this.Sales = new HashSet<Sale>();
    }

    public int CustomerId { get; set; }

    [Unicode(true)]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Unicode(false)]
    [MaxLength(80)]
    public string Email { get; set; } = null!;

    public string CreditCardNumber { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; }
}
