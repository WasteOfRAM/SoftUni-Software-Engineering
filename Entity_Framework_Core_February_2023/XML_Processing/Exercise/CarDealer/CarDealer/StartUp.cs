using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            using CarDealerContext context = new CarDealerContext();

            //var inputXml = File.ReadAllText(@"../../../Datasets/suppliers.xml");
            //var inputXml = File.ReadAllText(@"../../../Datasets/parts.xml");
            //var inputXml = File.ReadAllText(@"../../../Datasets/cars.xml");
            //var inputXml = File.ReadAllText(@"../../../Datasets/customers.xml");
            //var inputXml = File.ReadAllText(@"../../../Datasets/sales.xml");

            //var result = ImportSales(context, inputXml);

            //Console.WriteLine(result);

            var result = GetTotalSalesByCustomer(context);

            //File.WriteAllText(@"../../../Results/cars.xml", result);
            //File.WriteAllText(@"../../../Results/bmw-cars.xml", result);
            //File.WriteAllText(@"../../../Results/local-suppliers.xml", result);
            //File.WriteAllText(@"../../../Results/cars-and-parts.xml", result);
            File.WriteAllText(@"../../../Results/customers-total-sales.xml", result);
            //File.WriteAllText(@"../../../Results/sales-discounts.xml", result);

            Console.WriteLine(result);
        }

        // Import Data

        // Query 9. Import Suppliers

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var mapper = MapperInitializer();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportSuppliersDto[]), new XmlRootAttribute("Suppliers"));

            using StringReader reader = new StringReader(inputXml);

            var suppliersDto = (ImportSuppliersDto[])serializer.Deserialize(reader)!;

            var suppliers = mapper.Map<Supplier[]>(suppliersDto);

            context.AddRange(suppliers);

            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}";
        }

        // 10. Import Parts

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var mapper = MapperInitializer();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportPartDto[]), new XmlRootAttribute("Parts"));

            using StringReader reader = new StringReader(inputXml);

            var partsDto = (ImportPartDto[])serializer.Deserialize(reader)!;

            var suppliersIds = context.Suppliers.Select(p => p.Id).ToList();

            var validParts = new List<ImportPartDto>();

            foreach (var part in partsDto)
            {
                if (!suppliersIds.Any(id => id == part.SupplierId))
                {
                    continue;
                }

                validParts.Add(part);
            }

            var parts = mapper.Map<Part[]>(validParts);

            context.AddRange(parts);

            context.SaveChanges();

            return $"Successfully imported {parts.Length}";
        }

        // 11. Import Cars

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var mapper = MapperInitializer();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportCarDto[]), new XmlRootAttribute("Cars"));

            using StringReader reader = new StringReader(inputXml);

            ImportCarDto[] carDtos = (ImportCarDto[])serializer.Deserialize(reader)!;

            var validCars = new List<Car>();

            var existinParts = context.Parts
                .AsNoTracking()
                .ToArray();

            foreach (var carDto in carDtos)
            {
                Car car = mapper.Map<Car>(carDto);

                foreach (var part in carDto.Parts.DistinctBy(p => p.PartId))
                {
                    if (!existinParts.Any(p => p.Id == part.PartId))
                    {
                        continue;
                    }

                    PartCar partCar = new PartCar()
                    {
                        PartId = part.PartId
                    };

                    car.PartsCars.Add(partCar);
                }

                validCars.Add(car);
            }

            context.Cars.AddRange(validCars);

            context.SaveChanges();

            return $"Successfully imported {validCars.Count}";
        }

        // 12. Import Customers

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var mapper = MapperInitializer();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportCustomerDto[]), new XmlRootAttribute("Customers"));

            using StringReader reader = new StringReader(inputXml);

            ImportCustomerDto[] customerDtos = (ImportCustomerDto[])serializer.Deserialize(reader)!;

            var customers = mapper.Map<Customer[]>(customerDtos);

            context.Customers.AddRange(customers);

            context.SaveChanges();

            return $"Successfully imported {customers.Length}";
        }

        // 13. Import Sales

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var mapper = MapperInitializer();

            var existingCars = context.Cars
                .AsNoTracking()
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportSaleDto[]), new XmlRootAttribute("Sales"));

            using StringReader reader = new StringReader(inputXml);

            ImportSaleDto[] saleDtos = (ImportSaleDto[])serializer.Deserialize(reader)!;

            var validSales = new HashSet<ImportSaleDto>();
            foreach (var saleDto in saleDtos)
            {
                if (!existingCars.Any(c => c.Id == saleDto.CarId))
                {
                    continue;
                }

                validSales.Add(new ImportSaleDto()
                {
                    CarId = saleDto.CarId,
                    CustomerId = saleDto.CustomerId,
                    Discount = saleDto.Discount
                });
            }

            var sales = mapper.Map<Sale[]>(validSales);

            context.Sales.AddRange(sales);

            context.SaveChanges();

            return $"Successfully imported {sales.Length}";
        }

        // Query and Export Data

        // 14. Export Cars With Distance

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            var mapper = MapperInitializer();

            var carsDtos = context.Cars
                .AsNoTracking()
                .Where(c => c.TraveledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ProjectTo<ExportCarWithDistanceDto>(mapper.ConfigurationProvider)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportCarWithDistanceDto[]), new XmlRootAttribute("cars"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(sb);

            serializer.Serialize(writer, carsDtos, namespaces);

            return sb.ToString().TrimEnd();
        }

        // 15. Export Cars From Make BMW

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            var mapper = MapperInitializer();

            var bmwCarsDtos = context.Cars
                .AsNoTracking()
                .Where(c => c.Make == "BMW")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .ProjectTo<ExportBmwCarsDto>(mapper.ConfigurationProvider)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportBmwCarsDto[]), new XmlRootAttribute("cars"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(sb);

            serializer.Serialize(writer, bmwCarsDtos, namespaces);

            return sb.ToString().TrimEnd();
        }

        // 16. Export Local Suppliers

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            var mapper = MapperInitializer();

            var localSupplierDtos = context.Suppliers
                .AsNoTracking()
                .Where(s => s.IsImporter == false)
                .ProjectTo<ExportLocalSupplierDto>(mapper.ConfigurationProvider)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportLocalSupplierDto[]), new XmlRootAttribute("suppliers"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(sb);

            serializer.Serialize(writer, localSupplierDtos, namespaces);

            return sb.ToString().TrimEnd();
        }

        // 17. Export Cars With Their List Of Parts

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            var mapper = MapperInitializer();

            var carsAndPartsDtos = context.Cars
                .ProjectTo<ExportCarWithPartsDto>(mapper.ConfigurationProvider)
                .AsNoTracking()
                .OrderByDescending(c => c.TraveledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportCarWithPartsDto[]), new XmlRootAttribute("cars"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(sb);

            serializer.Serialize(writer, carsAndPartsDtos, namespaces);

            return sb.ToString().TrimEnd();
        }

        // 18. Export Total Sales By Customer

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            var customersTotalSale = context.Customers
                .Where(c => c.Sales.Count >= 1)
                .Include(c => c.Sales)
                .ToArray();

            var customersTotalSaleDtos = customersTotalSale
                .Select(c => new ExportCustomersTotalSalesDto
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney = SumAllCarsValues(c.Sales, c.IsYoungDriver)
                })
                .OrderByDescending(cDto => cDto.SpentMoney)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportCustomersTotalSalesDto[]), new XmlRootAttribute("customers"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(sb);

            serializer.Serialize(writer, customersTotalSaleDtos, ns);

            return sb.ToString().TrimEnd();
        }

        public static decimal SumAllCarsValues(ICollection<Sale> sales, bool isYoungDriver)
        {
            decimal totalMoneySpent = 0;

            foreach (var sale in sales)
            {
                decimal price = sale.Car.PartsCars.Sum(pc => pc.Part.Price);
                decimal discount = sale.Discount / 100;

                decimal carValue = !isYoungDriver ? price
                                                  : price - (price * discount);

                totalMoneySpent += carValue;
            }

            return totalMoneySpent;
        }

        // 19. Export Sales With Applied Discount

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            var mapper = MapperInitializer();

            var saleDtos = context.Sales
                .ProjectTo<ExportSalesWithDiscountDto>(mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportSalesWithDiscountDto[]), new XmlRootAttribute("sales"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(sb);

            serializer.Serialize(writer, saleDtos, ns);

            return sb.ToString().TrimEnd();
        }

        // -------------------
        public static IMapper MapperInitializer()
        {
            return new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>()).CreateMapper();
        }
    }
}