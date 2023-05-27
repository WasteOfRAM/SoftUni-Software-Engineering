namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Boardgames.DataProcessor.ExportDto;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            StringBuilder sb = new StringBuilder();

            var creators = context.Creators
                .Where(c => c.Boardgames.Count >= 1)
                .Include(c => c.Boardgames)
                .AsNoTracking()
                .ToArray()
                .Select(c => new ExportCreatorDto
                {
                    BoardgamesCount = c.Boardgames.Count,
                    CreatorName = $"{c.FirstName} {c.LastName}",
                    Boardgames = c.Boardgames
                        .OrderBy(b => b.Name)
                        .Select(b => new ExportBoardgameDto
                        {
                            BoardgameName = b.Name,
                            BoardgameYearPublished = b.YearPublished
                        })
                        .ToArray()
                })
                .OrderByDescending(c => c.BoardgamesCount)
                .ThenBy(c => c.CreatorName)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportCreatorDto[]), new XmlRootAttribute("Creators"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(sb);

            serializer.Serialize(writer, creators, ns);

            return sb.ToString().TrimEnd();
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            var sellers = context.Sellers
                .Where(s => s.BoardgamesSellers.Any(bs => bs.Boardgame.YearPublished >= year && bs.Boardgame.Rating <= rating))
                .Include(s => s.BoardgamesSellers)
                .AsNoTracking()
                .Select(s => new
                {
                    Name = s.Name,
                    Website = s.Website,
                    Boardgames = s.BoardgamesSellers
                        .Where(bs => bs.Boardgame.YearPublished >= year && bs.Boardgame.Rating <= rating)
                        .OrderByDescending(bs => bs.Boardgame.Rating)
                        .ThenBy(bs => bs.Boardgame.Name)
                        .Select(bs => new
                        {
                            Name = bs.Boardgame.Name,
                            Rating = bs.Boardgame.Rating,
                            Mechanics = bs.Boardgame.Mechanics,
                            Category = bs.Boardgame.CategoryType.ToString()
                        })
                        .ToArray()
                })
                .OrderByDescending(s => s.Boardgames.Count())
                .ThenBy(s => s.Name)
                .Take(5)
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(sellers, Formatting.Indented);


            return jsonString;
        }
    }
}