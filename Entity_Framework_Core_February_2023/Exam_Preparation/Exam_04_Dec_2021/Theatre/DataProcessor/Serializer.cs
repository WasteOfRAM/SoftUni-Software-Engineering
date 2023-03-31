namespace Theatre.DataProcessor
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;
    using Theatre.Data;
    using Theatre.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var theaters = context.Theatres
                .Where(t => t.NumberOfHalls >= numbersOfHalls && t.Tickets.Count >= 20)
                .Include(t => t.Tickets)
                .AsNoTracking()
                .Select(t => new
                {
                    Name = t.Name,
                    Halls = t.NumberOfHalls,
                    TotalIncome = t.Tickets.Where(tic => tic.RowNumber >= 1 && tic.RowNumber <= 5).Sum(t => t.Price),
                    Tickets = t.Tickets
                        .Where(tic => tic.RowNumber >= 1 && tic.RowNumber <= 5)
                        .OrderByDescending(tic => tic.Price)
                        .Select(tic => new
                        {
                            Price = tic.Price,
                            RowNumber = tic.RowNumber
                        })
                        .ToArray()
                })
                .OrderByDescending(t => t.Halls)
                .ThenBy(t => t.Name)
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(theaters, Formatting.Indented);

            return jsonString;
        }

        public static string ExportPlays(TheatreContext context, double raiting)
        {
            StringBuilder sb = new StringBuilder();

            var plays = context.Plays
                .Where(p => p.Rating <= raiting)
                .Include(p => p.Casts)
                .AsNoTracking()
                .ToArray()
                .Select(p => new ExportPlayDto
                {
                    Title = p.Title,
                    Duration = p.Duration.ToString("c", CultureInfo.InvariantCulture),
                    Rating = p.Rating == 0 ? "Premier" : p.Rating.ToString(),
                    Genre = p.Genre.ToString(),
                    Actors = p.Casts
                        .Where(a => a.IsMainCharacter)
                        .OrderByDescending(a => a.FullName)
                        .Select(a => new ExportActorDto
                        {
                            FullName = a.FullName,
                            MainCharacter = $"Plays main character in '{p.Title}'."
                        })
                        .ToArray()
                })
                .OrderBy(p => p.Title)
                .ThenByDescending(p => p.Genre)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportPlayDto[]), new XmlRootAttribute("Plays"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(sb);

            serializer.Serialize(writer, plays, ns);

            return sb.ToString().TrimEnd();
        }
    }
}
