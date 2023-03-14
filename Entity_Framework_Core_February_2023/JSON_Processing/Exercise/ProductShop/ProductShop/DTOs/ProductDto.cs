namespace ProductShop.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string? BuyerFirstName { get; set; }

        public string BuyerLastName { get; set; } = null!;
    }
}
