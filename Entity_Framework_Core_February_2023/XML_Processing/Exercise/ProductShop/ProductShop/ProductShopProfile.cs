using AutoMapper;

using ProductShop.Models;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.DTOs.Converters;

namespace ProductShop;

public class ProductShopProfile : Profile
{
    public ProductShopProfile()
    {
        // Users

        this.CreateMap<ImportUserDto, User>();

        this.CreateMap<User, ExportUserSoldProductsDto>()
            .ConvertUsing<UsersSoldProductsConverter>();

        this.CreateMap<User, ExportUserDto>();

        this.CreateMap<User[], ExportUsersAndProductsDto>()
            .ConvertUsing<UsersAndProductsConverter>();

        // Products

        this.CreateMap<Product, ExportSoldProductsDto>()
            .ForMember(d => d.Price, opt => opt.MapFrom(s => (decimal)Math.Round(s.Price, 2)));

        this.CreateMap<ImportProductDto, Product>();

        this.CreateMap<Product, ExportProductsInRangeDto>()
            .ForMember(d => d.Buyer, opt => opt.MapFrom(s => s.BuyerId != null ? $"{s.Buyer!.FirstName} {s.Buyer.LastName}" : null));

        // Categories

        this.CreateMap<ImportCategorieDto, Category>();

        this.CreateMap<Category, ExportCategoriesByProductsDto>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Count, opt => opt.MapFrom(s => s.CategoryProducts.Count))
            .ForMember(d => d.AveragePrice, opt => opt.MapFrom(s => s.CategoryProducts.Average(p => p.Product.Price)))
            .ForMember(d => d.TotalRevenue, opt => opt.MapFrom(s => s.CategoryProducts.Sum(p => p.Product.Price)));
    }
}
