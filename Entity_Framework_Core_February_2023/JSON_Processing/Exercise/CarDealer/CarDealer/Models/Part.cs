using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Models
{
    public class Part
    {
        public Part()
        {
            this.PartsCars= new List<PartCar>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        [Column(TypeName = "money")]
        public virtual decimal Price { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(nameof(Supplier))]
        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; } = null!;

        public virtual ICollection<PartCar> PartsCars { get; set; }
    }
}
