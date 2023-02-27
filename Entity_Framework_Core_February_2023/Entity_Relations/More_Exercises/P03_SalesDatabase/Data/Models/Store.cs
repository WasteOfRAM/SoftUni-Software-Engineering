using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_SalesDatabase.Data.Models;

[Table("Stores")]
public class Store
{
    public Store()
    {
        this.Sales = new HashSet<Sale>();
    }

    public int StoreId { get; set; }

    [Unicode(true)]
    [MaxLength(80)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; }
}
