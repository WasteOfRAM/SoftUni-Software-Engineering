namespace VaporStore.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.ImportDto;

    public static class Deserializer
    {
        public const string ErrorMessage = "Invalid Data";

        public const string SuccessfullyImportedGame = "Added {0} ({1}) with {2} tags";

        public const string SuccessfullyImportedUser = "Imported {0} with {1} cards";

        public const string SuccessfullyImportedPurchase = "Imported {0} for {1}";

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var importGamesDtos = JsonConvert.DeserializeObject<ImportGameDto[]>(jsonString);

            var games = new List<Game>();
            var developers = new List<Developer>();
            var genres = new List<Genre>();
            var tags = new List<Tag>();

            foreach (var gameDto in importGamesDtos!)
            {
                if (!IsValid(gameDto) || gameDto.Tags.Length == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool invalidTag = false;

                var validTags = new List<string>();

                foreach (var tagName in gameDto.Tags)
                {
                    if (string.IsNullOrWhiteSpace(tagName))
                    {
                        sb.AppendLine(ErrorMessage);
                        invalidTag = true;
                        break;
                    }

                    validTags.Add(tagName);
                }

                if (invalidTag)
                    continue;

                foreach (var tagName in validTags)
                {
                    Tag? tag = tags.FirstOrDefault(t => t.Name == tagName);

                    if (tag == null)
                    {
                        tag = new Tag { Name = tagName };
                        tags.Add(tag);
                    }
                }

                Developer? developer = developers.FirstOrDefault(d => d.Name == gameDto.Developer);
                Genre? genre = genres.FirstOrDefault(g => g.Name == gameDto.Genre);

                if (developer == null)
                {
                    developer = new Developer { Name = gameDto.Developer };
                    developers.Add(developer);
                }

                if (genre == null)
                {
                    genre = new Genre { Name = gameDto.Genre };
                    genres.Add(genre);
                }

                Game game = new Game
                {
                    Name = gameDto.Name,
                    Price = gameDto.Price,
                    ReleaseDate = DateTime.ParseExact(gameDto.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Developer = developer,
                    Genre = genre
                };

                foreach (var tagName in validTags)
                {
                    Tag gameTag = tags.First(t => t.Name == tagName);
                    game.GameTags.Add(new GameTag { Game = game, Tag = gameTag });
                }

                games.Add(game);

                sb.AppendLine(string.Format(SuccessfullyImportedGame, game.Name, game.Genre.Name, game.GameTags.Count));
            }

            context.Developers.AddRange(developers);
            context.Genres.AddRange(genres);
            context.Games.AddRange(games);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var usersAndCardsDtos = JsonConvert.DeserializeObject<ImportUserDto[]>(jsonString);

            var users = new List<User>();

            foreach (var userDto in usersAndCardsDtos!)
            {
                if (!IsValid(userDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool invalidCard = false;

                foreach (var cardDto in userDto.Cards)
                {
                    if (!IsValid(cardDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        invalidCard = true;
                        break;
                    }
                }

                if (invalidCard)
                    continue;

                var user = new User 
                {
                    FullName = userDto.FullName,
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Age = userDto.Age
                };

                foreach (var carDto in userDto.Cards)
                {
                    user.Cards.Add(new Card
                    {
                        Number = carDto.Number,
                        Cvc = carDto.Cvc,
                        Type = (CardType)Enum.Parse(typeof(CardType), carDto.Type)
                    });
                }

                users.Add(user);

                sb.AppendLine(string.Format(SuccessfullyImportedUser, user.Username, user.Cards.Count));
            }

            context.AddRange(users);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportPurchaseDto[]), new XmlRootAttribute("Purchases"));

            using StringReader reader = new StringReader(xmlString);

            var purchasesDtos = (ImportPurchaseDto[])serializer.Deserialize(reader)!;

            var purchases = new List<Purchase>();

            foreach (var purchaseDto in purchasesDtos)
            {
                if (!IsValid(purchaseDto) || !Enum.IsDefined(typeof(PurchaseType), purchaseDto.Type))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Game? game = context.Games.FirstOrDefault(g => g.Name == purchaseDto.Title);
                Card? card = context.Cards.FirstOrDefault(c => c.Number == purchaseDto.Card);

                if (game == null || card == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var purchase = new Purchase
                {
                    Type = (PurchaseType)Enum.Parse(typeof(PurchaseType), purchaseDto.Type),
                    ProductKey = purchaseDto.ProductKey,
                    Date = DateTime.ParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    CardId = card.Id,
                    Card = card,
                    GameId = game.Id,
                    Game = game
                };

                purchases.Add(purchase);

                sb.AppendLine(string.Format(SuccessfullyImportedPurchase, purchase.Game.Name, purchase.Card.User.Username));
            }

            context.Purchases.AddRange(purchases);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}