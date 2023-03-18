using AutoMapper;
using CarDealer.DTOs;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using System.Globalization;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            // Suppliers

            this.CreateMap<ImportSuppliersDto, Supplier>();

            // Parts

            this.CreateMap<ImportPartDto, Part>();

            this.CreateMap<ImportCarPartDto, PartCar>()
                .ForMember(d => d.PartId, opt => opt.MapFrom(s => s.PartId));



            CreateMap<PartCar, ExportPartDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Part.Name))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Part.Price));

            // Cars

            this.CreateMap<ImportCarDto,  Car>();

            this.CreateMap<Car, ExportCarWithDistanceDto>();


            this.CreateMap<Car, ExportBmwCarsDto>();


            this.CreateMap<Car, ExportCarWithPartsDto>()
                .ForMember(d => d.Parts, opt => opt.MapFrom(s => s.PartsCars.OrderByDescending(p => p.Part.Price)));

            this.CreateMap<Car, ExportCarDto>();

            // Customer

            this.CreateMap<ImportCustomerDto, Customer>()
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => DateTime.Parse(s.BirthDate, CultureInfo.InvariantCulture)));

            this.CreateMap<Supplier, ExportLocalSupplierDto>()
                .ForMember(d => d.PartsCount, opt => opt.MapFrom(s => s.Parts.Count));

            this.CreateMap<Customer, ExportCustomersTotalSalesDto>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.BoughtCars, opt => opt.MapFrom(s => s.Sales.Count))
                .ForMember(d => d.SpentMoney, opt => opt.Ignore());

            // Sale

            this.CreateMap<ImportSaleDto, Sale>();

            this.CreateMap<Sale, ExportSalesWithDiscountDto>()
                .ForMember(d => d.Car, opt => opt.MapFrom(s => s.Car))
                .ForMember(d => d.Discount, opt => opt.MapFrom(s => s.Discount))
                .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer.Name))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Car.PartsCars.Sum(pc => pc.Part.Price)))
                .ForMember(d => d.PriceWithDiscount, opt => opt.MapFrom(s =>
                    decimal.Parse(
                        (s.Car.PartsCars.Sum(pc => pc.Part.Price) - (s.Car.PartsCars.Sum(pc => pc.Part.Price) * (s.Discount / 100)))
                        .ToString("0.####"))
                    )
                );
        }
    }
}
