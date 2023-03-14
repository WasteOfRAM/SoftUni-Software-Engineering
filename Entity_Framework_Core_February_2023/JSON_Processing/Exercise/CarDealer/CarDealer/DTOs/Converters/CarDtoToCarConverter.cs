using AutoMapper;
using CarDealer.DTOs.Import;
using CarDealer.Models;

namespace CarDealer.DTOs.Converters
{
    public class CarDtoToCarConverter : ITypeConverter<ImportCarDto, Car>
    {
        public Car Convert(ImportCarDto source, Car destination, ResolutionContext context)
        {
            destination = new Car
            {
                Make = source.Make,
                Model = source.Model,
                TraveledDistance = source.TraveledDistance,
                PartsCars = source.PartsId.Select(p => new PartCar { PartId = p }).ToList()
            };

            return destination;
        }
    }
}
