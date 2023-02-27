using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_SalesDatabase.Data.Models;

[Table("Products")]
public class Product
{
    public Product()
    {
        this.Sales = new HashSet<Sale>();
    }

    public int ProductId { get; set; }

    [Unicode(true)]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public double Quantity { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public virtual ICollection<Sale> Sales { get; set; }
}
