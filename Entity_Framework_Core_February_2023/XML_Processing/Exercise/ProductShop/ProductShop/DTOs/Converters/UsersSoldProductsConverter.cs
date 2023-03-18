using AutoMapper;
using ProductShop.DTOs.Export;
using ProductShop.Models;

namespace ProductShop.DTOs.Converters
{
    public class UsersSoldProductsConverter : ITypeConverter<User, ExportUserSoldProductsDto>
    {
        public ExportUserSoldProductsDto Convert(User source, ExportUserSoldProductsDto destination, ResolutionContext context)
        {
            destination = new ExportUserSoldProductsDto
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
                SoldProducts = context.Mapper.Map<List<ExportSoldProductsDto>>(source.ProductsSold)
            };

            return destination;
        }
    }
}
