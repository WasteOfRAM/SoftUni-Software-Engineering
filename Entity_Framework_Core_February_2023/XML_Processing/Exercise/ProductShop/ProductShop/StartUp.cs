using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            using ProductShopContext context = new ProductShopContext();

            //string inputXml = File.ReadAllText(@"../../../Datasets/users.xml");
            //string inputXml = File.ReadAllText(@"../../../Datasets/products.xml");
            //string inputXml = File.ReadAllText(@"../../../Datasets/categories.xml");
            //string inputXml = File.ReadAllText(@"../../../Datasets/categories-products.xml");

            var xmlString = GetUsersWithProducts(context);

            //File.WriteAllText(@"..\..\..\Results\products-in-range.xml", xmlString);
            //File.WriteAllText(@"..\..\..\Results\users-sold-products.xml", xmlString);
            //File.WriteAllText(@"..\..\..\Results\categories-by-products.xml", xmlString);
            File.WriteAllText(@"..\..\..\Results\users-and-products.xml", xmlString);
        }


        // Import Data

        // 01. Import Users

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(ImportUserDto[]), new XmlRootAttribute("Users"));

            var usersDto = serializer.Deserialize(new StringReader(inputXml));

            var mapper = CreateMapperCfg();

            var users = mapper.Map<User[]>(usersDto);

            context.Users.AddRange(users);

            var importedCount = context.SaveChanges();

            return $"Successfully imported {importedCount}";
        }

        // 02. Import Products

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(ImportProductDto[]), new XmlRootAttribute("Products"));

            var productsDto = serializer.Deserialize(new StringReader(inputXml));

            var mapper = CreateMapperCfg();

            var products = mapper.Map<Product[]>(productsDto);

            context.Products.AddRange(products);

            var importedCount = context.SaveChanges();

            return $"Successfully imported {importedCount}";
        }

        // 03. Import Categories

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var mapper = CreateMapperCfg();

            var serializer = new XmlSerializer(typeof(ImportCategorieDto[]), new XmlRootAttribute("Categories"));

            var categoriesDto = serializer.Deserialize(new StringReader(inputXml));

            var products = mapper.Map<Category[]>(categoriesDto);

            context.Categories.AddRange(products);

            var importedCount = context.SaveChanges();

            return $"Successfully imported {importedCount}";
        }

        // 04. Import Categories and Products

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(ImportCategoryProductDto[]), new XmlRootAttribute("CategoryProducts"));

            var categoryProductsDto = (ImportCategoryProductDto[])serializer.Deserialize(new StringReader(inputXml))!;

            foreach (var item in categoryProductsDto)
            {
                if (!context.Categories.AsNoTracking().Any(c => c.Id == item.CategoryId) &&
                    !context.Products.AsNoTracking().Any(p => p.Id == item.ProductId))
                {
                    continue;
                }

                context.CategoryProducts.Add(new CategoryProduct { CategoryId = item.CategoryId, ProductId = item.ProductId });
            }

            var importedCount = context.SaveChanges();

            return $"Successfully imported {importedCount}";
        }


        // Query and Export Data

        // 05. Export Products In Range

        public static string GetProductsInRange(ProductShopContext context)
        {
            var mapper = CreateMapperCfg();

            var productsInRange = context.Products
                .AsNoTracking()
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .ProjectTo<ExportProductsInRangeDto>(mapper.ConfigurationProvider)
                .OrderBy(p => p.Price)
                .Take(10)
                .ToArray();

            // Works with Judge
            StringBuilder sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ExportProductsInRangeDto[]), new XmlRootAttribute("Products"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using var writer = new StringWriter(sb);
            serializer.Serialize(writer, productsInRange, ns);

            // NOT WORKING WITH Judge ??? ---------------------
            //var serializer = new XmlSerializer(typeof(ExportProductsInRange[]), new XmlRootAttribute("Products"));

            //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //ns.Add(string.Empty, string.Empty);

            //using var writer = XmlWriter.Create(@"..\..\..\Results\products-in-range.xml", new XmlWriterSettings { Encoding = Encoding.Unicode, Indent = true });
            //serializer.Serialize(writer, productsInRange, ns);

            // NOT WORKING WITH Judge ??? ---------------------
            //XDocument doc = new XDocument(new XDeclaration("1.0", "utf-16", null), new XElement("Products"));

            //foreach (var product in productsInRange)
            //{
            //    var productElement = new XElement("Product");

            //    productElement.Add(new XElement("name", product.Name),
            //                       new XElement("price", product.Price),
            //                       product.Buyer != null ? new XElement("buyer", product.Buyer) : null);

            //    doc.Root!.Add(productElement);
            //}

            //doc.Save(@"..\..\..\Results\products-in-range.xml", SaveOptions.OmitDuplicateNamespaces);

            return sb.ToString().TrimEnd();
        }


        // 06. Export Sold Products

        public static string GetSoldProducts(ProductShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var mapper = CreateMapperCfg();

            var usersSoldProducts = context.Users
                .Include(u => u.ProductsSold)
                .AsNoTracking()
                .Where(u => u.ProductsSold.Count >= 1)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ToArray();

            var users = mapper.Map<ExportUserSoldProductsDto[]>(usersSoldProducts);

            var serializer = new XmlSerializer(typeof(ExportUserSoldProductsDto[]), new XmlRootAttribute("Users"));

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using var writer = new StringWriter(sb);
            serializer.Serialize(writer, users, ns);

            
            return sb.ToString().Trim();
        }

        // 07. Export Categories By Products Count

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var mapper = CreateMapperCfg();

            var categoryProducts = context.Categories
                .ProjectTo<ExportCategoriesByProductsDto>(mapper.ConfigurationProvider)
                .AsNoTracking()
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToArray();

            var serializer = new XmlSerializer(typeof(ExportCategoriesByProductsDto[]), new XmlRootAttribute("Categories"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using var writer = new StringWriter(sb);
            serializer.Serialize(writer, categoryProducts, ns);

            return sb.ToString().Trim();
        }


        // 08. Export Users and Products

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var mapper = CreateMapperCfg();

            var usersAndProducts = context.Users
                .Where(u => u.ProductsSold.Count >= 1)
                .Include(u => u.ProductsSold)
                .OrderByDescending(u => u.ProductsSold.Count)
                .AsNoTracking()
                .ToArray();


            var usersAndProductsDto = mapper.Map<ExportUsersAndProductsDto>(usersAndProducts);

            var serializer = new XmlSerializer(typeof(ExportUsersAndProductsDto), new XmlRootAttribute("Users"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using var writer = new StringWriter(sb);
            serializer.Serialize(writer, usersAndProductsDto, ns);

            return sb.ToString().Trim();
        }

        // ---------------

        public static IMapper CreateMapperCfg()
        {
            return new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>()).CreateMapper();
        }
    }
}