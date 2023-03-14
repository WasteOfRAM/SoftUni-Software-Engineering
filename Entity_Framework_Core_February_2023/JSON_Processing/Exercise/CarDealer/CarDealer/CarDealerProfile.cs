using AutoMapper;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.DTOs.Converters;
using CarDealer.Models;
using System.Globalization;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {

            // Supplier
            this.CreateMap<ImportSupplierDto, Supplier>();

            this.CreateMap<Supplier, ExportLocalSuppliersDto>()
                .ForMember(d => d.PartsCount, opt => opt.MapFrom(s => s.Parts.Count));

            // Part
            this.CreateMap<ImportPartDto, Part>();

            // Car
            this.CreateMap<ImportCarDto, Car>()
                .ConvertUsing(new CarDtoToCarConverter());

            this.CreateMap<Car, ExportCarsDto>();

            // Customers
            this.CreateMap<ImportCustomerDto, Customer>();

            this.CreateMap<Customer, ExportOrderedCustomersDto>()
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => s.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));


            // Sale
            this.CreateMap<ImportSaleDto, Sale>();
        }
    }
}
