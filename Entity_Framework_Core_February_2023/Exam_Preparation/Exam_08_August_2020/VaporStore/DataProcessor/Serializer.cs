namespace VaporStore.DataProcessor
{ 
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.ExportDto;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var gamesByGenres = context.Genres
                .Where(gen => genreNames.Any(gn => gn == gen.Name))
                .Include(gen => gen.Games)
                .ToArray()
                .Select(gen => new
                {
                    Id = gen.Id,
                    Genre = gen.Name,
                    Games = gen.Games
                        .Where(game => game.Purchases.Count >= 1)
                        .Select(game => new
                        {
                            Id = game.Id,
                            Title = game.Name,
                            Developer = game.Developer.Name,
                            Tags = string.Join(", ", game.GameTags.Select(gt => gt.Tag.Name)),
                            Players = game.Purchases.Count
                        })
                        .OrderByDescending(g => g.Players)
                        .ThenBy(g => g.Id)
                        .ToArray(),
                    TotalPlayers = gen.Games.Sum(g => g.Purchases.Count)
                })
                .OrderByDescending(gen => gen.TotalPlayers)
                .ThenBy(gen => gen.Id)
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(gamesByGenres, Formatting.Indented);

            return jsonString;
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string purchaseType)
        {
            StringBuilder sb = new StringBuilder();

            var usersAndPurchesesByTypeDtos = context.Users
                .AsNoTracking()
                .Where(u => u.Cards.Any(p => p.Purchases.Any()))
                .Select(u => new ExportUserPurchasesDto
                {
                    Username = u.Username,
                    Purchases = context.Purchases
                        .AsNoTracking()
                        .Where(p => p.Card.User.Username == u.Username && p.Type == Enum.Parse<PurchaseType>(purchaseType))
                        .OrderBy(p => p.Date)
                        .Select(p => new ExportPurcheseDto
                        {
                            Card = p.Card.Number,
                            Cvc = p.Card.Cvc,
                            Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                            Game = new ExportGameDto
                            {
                                Title = p.Game.Name,
                                Genre = p.Game.Genre.Name,
                                Price = p.Game.Price
                            }
                        })
                        .ToArray(),
                    TotalSpent = context.Purchases
                        .AsNoTracking()
                        .Where(p => p.Card.User.Username == u.Username && p.Type == Enum.Parse<PurchaseType>(purchaseType))
                        .Sum(p => p.Game.Price)
                })
                .Where(u => u.Purchases.Length > 0)
                .OrderByDescending(u => u.TotalSpent)
                .ThenBy(u => u.Username)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportUserPurchasesDto[]), new XmlRootAttribute("Users"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(sb);

            serializer.Serialize(writer, usersAndPurchesesByTypeDtos, ns);

            return sb.ToString().TrimEnd();
        }
    }
}