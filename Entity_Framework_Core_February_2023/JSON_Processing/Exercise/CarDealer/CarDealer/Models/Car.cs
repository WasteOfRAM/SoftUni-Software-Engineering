namespace CarDealer.Models
{
    public class Car
    {
        public Car()
        {
            this.Sales = new List<Sale>();
            this.PartsCars = new List<PartCar>();
        }

        public int Id { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public long TraveledDistance { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public virtual IEnumerable<PartCar> PartsCars { get; set; }
    }
}
