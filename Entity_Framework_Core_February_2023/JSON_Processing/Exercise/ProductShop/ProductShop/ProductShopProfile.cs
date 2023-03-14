using AutoMapper;
using ProductShop.DTOs;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile() 
        {
            

            this.CreateMap<UserImportDto, User>();

            this.CreateMap<ProductImportDto, Product>();

            this.CreateMap<CategoryImportDto, Category>();

            this.CreateMap<CategotyProductInportDto, CategoryProduct>();

            this.CreateMap<Product, ProductDto>()
                .ForMember(d => d.BuyerFirstName, opt => opt.MapFrom(s => s.Buyer!.FirstName))
                .ForMember(d => d.BuyerLastName, opt => opt.MapFrom(s => s.Buyer!.LastName));

            this.CreateMap<Product, ProductInRangeDto>()
                .ForMember(d => d.Seller, opt => opt.MapFrom(s => s.Seller.FirstName + " " + s.Seller.LastName));

            this.CreateMap<User, UserSoldProductsDto>()
                .ForMember(d => d.SoldProducts, opt => opt.MapFrom(s => s.ProductsSold.Where(p => p.BuyerId != null)));

            this.CreateMap<Category, CategoriesByProductsDto>()
                .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.ProductsCount, opt => opt.MapFrom(s => s.CategoriesProducts.Count))
                .ForMember(d => d.AveragePrice, opt => opt.MapFrom(s => s.CategoriesProducts.Average(cp => cp.Product.Price)))
                .ForMember(d => d.TotalRevenue, opt => opt.MapFrom(s => s.CategoriesProducts.Sum(cp => cp.Product.Price)));


        }
    }
}
