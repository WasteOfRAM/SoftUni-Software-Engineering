using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new CarDealerContext();

            //var inputJson = File.ReadAllText(@"..\..\..\"Datasets\suppliers.json");
            //var inputJson = File.ReadAllText(@"..\..\..\Datasets\parts.json");
            //var inputJson = File.ReadAllText(@"..\..\..\Datasets\cars.json");
            //var inputJson = File.ReadAllText(@"..\..\..\Datasets\customers.json");
            //var inputJson = File.ReadAllText(@"..\..\..\Datasets\sales.json");

            var jsonResult = GetSalesWithAppliedDiscount(context);

            //File.WriteAllText(@"../../../Results/ordered-customers.json", jsonResult);
            //File.WriteAllText(@"../../../Results/toyota-cars.json", jsonResult);
            //File.WriteAllText(@"../../../Results/local-suppliers.json", jsonResult);
            //File.WriteAllText(@"../../../Results/cars-and-parts.json", jsonResult);
            //File.WriteAllText(@"../../../Results/customers-total-sales.json", jsonResult);
            File.WriteAllText(@"../../../Results/sales-discounts.json", jsonResult);
        }

        // Import Data

        // 09. Import Suppliers

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>()).CreateMapper();

            var suppliersDto = JsonConvert.DeserializeObject<List<ImportSupplierDto>>(inputJson);

            var suppliers = mapper.Map<List<Supplier>>(suppliersDto);

            context.Suppliers.AddRange(suppliers);

            var importedCount = context.SaveChanges();

            return $"Successfully imported {importedCount}.";
        }

        // 10. Import Parts

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>()).CreateMapper();

            var partsDto = JsonConvert
                .DeserializeObject<List<ImportPartDto>>(inputJson)!
                .Where(j => context.Suppliers.Any(s => s.Id == j.SupplierId));

            var parts = mapper.Map<List<Part>>(partsDto);

            context.Parts.AddRange(parts);

            var importedCount = context.SaveChanges();

            return $"Successfully imported {importedCount}.";
        }


        // 11. Import Cars

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carsDto = JsonConvert.DeserializeObject<List<ImportCarDto>>(inputJson);

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>()).CreateMapper();

            var cars = mapper.Map<List<Car>>(carsDto);

            context.Cars.AddRange(cars);

            var importedCount = context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }

        // 12. Import Customers

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customersDto = JsonConvert.DeserializeObject<List<ImportCustomerDto>>(inputJson);

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>()).CreateMapper();

            var customers = mapper.Map<List<Customer>>(customersDto);

            context.Customers.AddRange(customers);

            var importedCount = context.SaveChanges();

            return $"Successfully imported {importedCount}.";
        }

        // 13. Import Sales

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var salesDto = JsonConvert.DeserializeObject<List<ImportSaleDto>>(inputJson);

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>()).CreateMapper();

            var sales = mapper.Map<List<Sale>>(salesDto);

            context.Sales.AddRange(sales);

            var importedCount = context.SaveChanges();

            return $"Successfully imported {importedCount}.";
        }

        // Export Data

        // 14. Export Ordered Customers

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>()).CreateMapper();

            var orderedCustomers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .ProjectTo<ExportOrderedCustomersDto>(mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(orderedCustomers, Formatting.Indented);


            return jsonString;
        }

        // 15. Export Cars From Make Toyota

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>()).CreateMapper();

            var toyotaCars = context.Cars
                .AsNoTracking()
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .ProjectTo<ExportCarsDto>(mapper.ConfigurationProvider)
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(toyotaCars, Formatting.Indented);

            return jsonString;
        }

        // 16. Export Local Suppliers

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>()).CreateMapper();

            var localSuppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .ProjectTo<ExportLocalSuppliersDto>(mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(localSuppliers, Formatting.Indented);

            return jsonString;
        }

        // 17. Export Cars With Their List Of Parts

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsAndParts = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TraveledDistance
                    },
                    parts = c.PartsCars
                        .Select(p => new
                        {
                            p.Part.Name,
                            Price = p.Part.Price.ToString("f2")
                        }).ToArray()
                })
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(carsAndParts, Formatting.Indented);

            return jsonString;
        }

        // 18. Export Total Sales By Customer

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            //var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>()).CreateMapper();

            var customerSales = context.Customers
                .Where(c => c.Sales.Count >= 1)
                .Select(c => new
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney = Math.Round(SumCarPrices(c.Sales.Select(car => car.Car.PartsCars.Sum(p => p.Part.Price)).ToList()), 2)
                })
                .AsEnumerable()
                .OrderByDescending(c => c.SpentMoney)
                .ThenByDescending(c => c.BoughtCars)
                .ToList();

            var jsonString = JsonConvert.SerializeObject(customerSales, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy { OverrideSpecifiedNames = true }
                }
            });

            return jsonString;
        }

        public static decimal SumCarPrices(IEnumerable<decimal> carPrice)
        {
            return carPrice.Sum();
        }


        // 19. Export Sales With Applied Discount

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var salesSiscounts = context.Sales
                .Take(10)
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TraveledDistance
                    },
                    customerName = s.Customer.Name,
                    discount = s.Discount.ToString("f2"),
                    price = s.Car.PartsCars.Sum(p => p.Part.Price).ToString("f2"),
                    priceWithDiscount = (s.Car.PartsCars.Sum(p => p.Part.Price) - (s.Car.PartsCars.Sum(p => p.Part.Price) * (s.Discount / 100))).ToString("f2")
                })
                .ToArray();


            var jsonString = JsonConvert.SerializeObject(salesSiscounts, Formatting.Indented);

            return jsonString;
        }
    }
}