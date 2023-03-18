using AutoMapper;
using ProductShop.DTOs.Export;
using ProductShop.Models;

namespace ProductShop.DTOs.Converters
{
    public class UsersAndProductsConverter : ITypeConverter<User[], ExportUsersAndProductsDto>
    {
        public ExportUsersAndProductsDto Convert(User[] source, ExportUsersAndProductsDto destination, ResolutionContext context)
        {
            var users = new List<ExportUserDto>();

            foreach (var userWithProducts in source.Take(10))
            {
                var product = context.Mapper.Map<List<ExportSoldProductsDto>>(userWithProducts.ProductsSold).OrderByDescending(p => p.Price).ToList();

                var products = new ExportSoldProductsAndCopuntDto 
                {
                    Count = product.Count,
                    Products = product
                };

                var user = new ExportUserDto { FirstName = userWithProducts.FirstName, LastName = userWithProducts.LastName, Age = userWithProducts.Age, SoldProducts = products };

                users.Add(user);
            }

            destination = new ExportUsersAndProductsDto
            {
                Count = source.Length,
                Users = users
            };

            return destination;
        }
    }
}
