namespace SoftJail.DataProcessor
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisonersByCells = context.Prisoners
                .Where(p => ids.Any(i => i == p.Id))
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell!.CellNumber,
                    Officers = p.PrisonerOfficers
                        .Select(po => new
                        {
                            OfficerName = po.Officer.FullName,
                            Department = po.Officer.Department.Name
                        })
                        .OrderBy(o => o.OfficerName)
                        .ToArray(),
                    TotalOfficerSalary = Math.Round(p.PrisonerOfficers.Sum(o => o.Officer.Salary), 2)
                })
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToArray();


            var jsonString = JsonConvert.SerializeObject(prisonersByCells, Formatting.Indented);

            return jsonString;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            string[] prisonersNamesArr = prisonersNames.Split(',');

            var prisonersWithMessagesDtos = context.Prisoners
                .Where(p => prisonersNamesArr.Any(name => name == p.FullName))
                .Include(p => p.Mails)
                .AsNoTracking()
                .Select(p => new ExportPrisonersDto
                {
                    Id = p.Id,
                    Name = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd"),
                    EncryptedMessages = p.Mails
                        .Select(m => new ExportPrisonerMailDto
                        {
                            Description = m.Description
                        })
                        .ToArray()
                })
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportPrisonersDto[]), new XmlRootAttribute("Prisoners"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(sb);

            xmlSerializer.Serialize(writer, prisonersWithMessagesDtos, ns);

            return sb.ToString().TrimEnd();
        }
    }
}