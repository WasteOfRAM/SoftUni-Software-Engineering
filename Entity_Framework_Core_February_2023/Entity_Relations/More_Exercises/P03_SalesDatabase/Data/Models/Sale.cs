using System.ComponentModel.DataAnnotations.Schema;

namespace P03_SalesDatabase.Data.Models;

[Table("Sales")]
public class Sale
{
    public int SaleId { get; set; }

    public DateTime Date { get; set; }

    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; } = null!;

    public int CustomerId { get; set; }
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    public int StoreId { get; set; }
    [ForeignKey(nameof(StoreId))]
    public Store Store { get; set; } = null!;
}
