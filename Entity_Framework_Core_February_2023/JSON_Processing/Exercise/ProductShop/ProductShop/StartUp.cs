using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            using var context = new ProductShopContext();

            //string inputJson = File.ReadAllText(@"..\..\..\Datasets\users.json");
            //string inputJson = File.ReadAllText(@"..\..\..\Datasets\products.json");
            //string inputJson = File.ReadAllText(@"..\..\..\Datasets\categories.json");
            //string inputJson = File.ReadAllText(@"..\..\..\Datasets\categories-products.json");

            string result = GetUsersWithProducts(context);

            //File.WriteAllText(@"..\..\..\Results\products-in-range.json", result);
            //File.WriteAllText(@"..\..\..\Results\users-sold-products.json", result);
            //File.WriteAllText(@"..\..\..\Results\categories-by-products.json", result);
            File.WriteAllText(@"..\..\..\Results\users-and-products.json", result);
        }

        // Import Data

        // 01. Import Users

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var usersDtos = JsonConvert.DeserializeObject<List<UserImportDto>>(inputJson);

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>()).CreateMapper();

            var users = mapper.Map<List<User>>(usersDtos);

            context.Users.AddRange(users);

            int usersAdded = context.SaveChanges();

            return $"Successfully imported {usersAdded}";
        }

        // 02. Import Products

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>()).CreateMapper();

            var productsDtos = JsonConvert.DeserializeObject<List<ProductImportDto>>(inputJson);

            var products = mapper.Map<List<Product>>(productsDtos);

            context.Products.AddRange(products);

            int productsAdded = context.SaveChanges();

            return $"Successfully imported {productsAdded}";
        }

        // 03. Import Categories

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>()).CreateMapper();

            var categoriesDto = JsonConvert.DeserializeObject<List<CategoryImportDto>>(inputJson)!.Where(c => c.Name != null);

            var categories = mapper.Map<List<Category>>(categoriesDto);

            context.Categories.AddRange(categories);

            int categoriesAdded = context.SaveChanges();

            return $"Successfully imported {categoriesAdded}";
        }

        // 04. Import Categories and Products

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>()).CreateMapper();

            var categoryProductsDto = JsonConvert.DeserializeObject<List<CategotyProductInportDto>>(inputJson);

            var categoryProducts = mapper.Map<List<CategoryProduct>>(categoryProductsDto);

            context.CategoriesProducts.AddRange(categoryProducts);

            int categoryProductsAdded = context.SaveChanges();

            return $"Successfully imported {categoryProductsAdded}";
        }

        // Export Data


        // 05. Export Products In Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>()).CreateMapper();

            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .ProjectTo<ProductInRangeDto>(mapper.ConfigurationProvider)
                .OrderBy(p => p.Price)
                .ToArray();

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    OverrideSpecifiedNames = false
                }
            };

            var jsonString = JsonConvert.SerializeObject(products, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            });

            return jsonString;
        }

        // 06. Export Sold Products

        public static string GetSoldProducts(ProductShopContext context)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>()).CreateMapper();

            var userSoldProducts = context.Users
                .ProjectTo<UserSoldProductsDto>(mapper.ConfigurationProvider)
                .Where(u => u.SoldProducts.Count >= 1)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToArray();


            string jsonString = JsonConvert.SerializeObject(userSoldProducts, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy { OverrideSpecifiedNames = false }
                }
            });

            return jsonString;
        }

        // 07. Export Categories By Products Count

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>()).CreateMapper();

            var categoriesByProducts = context.Categories
                .ProjectTo<CategoriesByProductsDto>(mapper.ConfigurationProvider)
                .OrderByDescending(c => c.ProductsCount)
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(categoriesByProducts, Formatting.Indented, new JsonSerializerSettings
            {

                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy { OverrideSpecifiedNames = false }
                }
            });

            return jsonString;
        }

        // 08. Export Users and Products

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = new
            {
                UsersCount = context.Users.Include(u => u.ProductsSold).Where(u => u.ProductsSold.Any(p => p.BuyerId.HasValue)).Count(),
                Users = context.Users
                    .Include(u => u.ProductsSold)
                    .Where(u => u.ProductsSold.Any(p => p.BuyerId.HasValue))
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,
                        u.Age,
                        SoldProducts = new
                        {
                            Count = u.ProductsSold.Where(p => p.BuyerId.HasValue).Count(),
                            Products = u.ProductsSold.Where(p => p.BuyerId.HasValue).Select(p => new
                            {
                                p.Name,
                                Price = decimal.Parse(p.Price.ToString("0.##")) // "G29" or 0.##
                            })
                            .ToArray()
                        }
                    })
                    .OrderByDescending(u => u.SoldProducts.Count)
                    .ToArray()
            };


            string jsonString = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy { OverrideSpecifiedNames = false }
                }
            });


            return jsonString;
        }
    }
}