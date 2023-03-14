namespace ProductShop.DTOs.Export
{
    public class CategoriesByProductsDto
    {
        private decimal averagePrice;
        private decimal totalRevenue;

        public string Category { get; set; } = null!;

        public int ProductsCount { get; set; }

        public decimal AveragePrice 
        { 
            get { return Math.Round(this.averagePrice, 2); } 
            set { this.averagePrice = value; } 
        }

        public decimal TotalRevenue 
        { 
            get { return Math.Round(this.totalRevenue, 2); } 
            set { this.totalRevenue = value; } 
        }
    }
}
