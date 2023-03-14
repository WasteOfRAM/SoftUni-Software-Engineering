namespace ProductShop.DTOs.Export
{
    public class UserSoldProductsDto
    {
        public UserSoldProductsDto()
        {
            this.SoldProducts = new List<ProductDto>();
        }

        public string? FirstName { get; set; }

        public string LastName { get; set; } = null!;

        public ICollection<ProductDto> SoldProducts { get; set; }
    }
}
