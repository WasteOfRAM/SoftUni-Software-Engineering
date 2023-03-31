
namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ExportDto;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            var shells = context.Shells
                .Where(s => s.ShellWeight > shellWeight)
                .Include(s => s.Guns)
                .AsNoTracking()
                .Select(s => new
                {
                    ShellWeight = s.ShellWeight,
                    Caliber = s.Caliber,
                    Guns = s.Guns
                        .Where(g => g.GunType == (GunType)3)
                        .OrderByDescending(g => g.GunWeight)
                        .Select(g => new
                        {
                            GunType = g.GunType.ToString(),
                            GunWeight = g.GunWeight,
                            BarrelLength = g.BarrelLength,
                            Range = g.Range > 3000 ? "Long-range" : "Regular range"
                        })
                        .ToArray()
                })
                .OrderBy(s => s.ShellWeight)
                .ToArray();

            string jsonString = JsonConvert.SerializeObject(shells, Formatting.Indented);

            return JsonConvert.SerializeObject(shells);
        }

        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            var gunsDto = context.Guns
                .Where(g => g.Manufacturer.ManufacturerName == manufacturer)
                .Include(g => g.CountriesGuns)
                .AsNoTracking()
                .Select(g => new ExportGunDto
                {
                    Manufacturer = g.Manufacturer.ManufacturerName,
                    GunType = g.GunType.ToString(),
                    GunWeight = g.GunWeight,
                    BarrelLength = g.BarrelLength,
                    Range = g.Range,
                    Countries = g.CountriesGuns
                        .Where(cg => cg.Country.ArmySize > 4500000)
                        .OrderBy(cg => cg.Country.ArmySize)
                        .Select(cg => new ExportCountryDto
                        {
                            Country = cg.Country.CountryName,
                            ArmySize = cg.Country.ArmySize
                        })
                        .ToArray()
                })
                .OrderBy(g => g.BarrelLength)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportGunDto[]), new XmlRootAttribute("Guns"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(sb);

            serializer.Serialize(writer, gunsDto, ns);

            return sb.ToString().TrimEnd();
        }
    }
}
