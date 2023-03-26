namespace Trucks.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml.Serialization;
    using Trucks.Data.Models.Enums;
    using Trucks.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            StringBuilder xmlString = new StringBuilder();

            var despachersWithTrucks = context.Despatchers
                .Where(d => d.Trucks.Count >= 1)
                .Select(d => new ExportDespacherDto
                {
                    DespatcherName = d.Name,
                    Trucks = d.Trucks.Select(t => new ExportTruckDto
                    {
                        RegistrationNumber = t.RegistrationNumber,
                        Make = t.MakeType.ToString()
                    })
                    .OrderBy(t => t.RegistrationNumber)
                    .ToArray(),
                    TrucksCount = d.Trucks.Count

                })
                .OrderByDescending(d => d.Trucks.Count())
                .ThenBy(d => d.DespatcherName)
                .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportDespacherDto[]), new XmlRootAttribute("Despatchers"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(xmlString);

            xmlSerializer.Serialize(writer, despachersWithTrucks, ns);

            return xmlString.ToString().TrimEnd();
        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {

            var clientsWithMostTrycks = context.Clients
                .Where(c => c.ClientsTrucks.Count >= 1 && c.ClientsTrucks.Any(ct => ct.Truck.TankCapacity >= capacity))
                .ToList()
                .Select(c => new
                {
                    c.Name,
                    Trucks = c.ClientsTrucks
                        .Where(ct => ct.Truck.TankCapacity >= capacity)
                        .Select(ct => new
                        {
                            TruckRegistrationNumber = ct.Truck.RegistrationNumber,
                            ct.Truck.VinNumber,
                            ct.Truck.TankCapacity,
                            ct.Truck.CargoCapacity,
                            CategoryType = ((CategoryType)ct.Truck.CategoryType).ToString(),
                            MakeType = ((MakeType)ct.Truck.MakeType).ToString()
                        })
                        .OrderBy(t => t.MakeType)
                        .ThenByDescending(t => t.CargoCapacity)
                        .ToList()
                })
                .OrderByDescending(c => c.Trucks.Count)
                .ThenBy(c => c.Name)
                .Take(10)
                .ToList();

            var jsonString = JsonConvert.SerializeObject(clientsWithMostTrycks, Formatting.Indented);

            return jsonString;
        }
    }
}
