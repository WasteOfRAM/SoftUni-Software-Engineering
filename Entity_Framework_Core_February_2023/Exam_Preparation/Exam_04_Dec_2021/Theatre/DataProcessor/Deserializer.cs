namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.Data.Models.Enums;
    using Theatre.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";



        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            var minTimeSpan = TimeSpan.ParseExact(ValidationConstraints.PlayDurationMinTimeLength, "c", CultureInfo.InvariantCulture);


            XmlSerializer serializer = new XmlSerializer(typeof(ImportPlayDto[]), new XmlRootAttribute("Plays"));

            using StringReader reader = new StringReader(xmlString);

            var importedPlaysDtos = (ImportPlayDto[])serializer.Deserialize(reader)!;

            var plays = new HashSet<Play>();

            foreach (var playDto in importedPlaysDtos)
            {
                if (!IsValid(playDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var currentPlayTimeSpan = TimeSpan.ParseExact(playDto.Duration, "c", CultureInfo.InvariantCulture);

                if (currentPlayTimeSpan < minTimeSpan ||
                    !Enum.IsDefined(typeof(Genre), playDto.Genre))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var play = new Play
                {
                    Title = playDto.Title,
                    Duration = currentPlayTimeSpan,
                    Rating = playDto.Raiting,
                    Genre = Enum.Parse<Genre>(playDto.Genre),
                    Description = playDto.Description,
                    Screenwriter = playDto.Screenwriter
                };

                plays.Add(play);

                sb.AppendLine(string.Format(SuccessfulImportPlay, playDto.Title, playDto.Genre, play.Rating));
            }

            context.Plays.AddRange(plays);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportCastDto[]), new XmlRootAttribute("Casts"));

            using StringReader reader = new StringReader(xmlString);

            var importedCastDtos = (ImportCastDto[])serializer.Deserialize(reader)!;

            var casts = new HashSet<Cast>();

            foreach (var castDro in importedCastDtos)
            {
                if (!IsValid(castDro))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var cast = new Cast
                {
                    FullName = castDro.FullName,
                    IsMainCharacter = castDro.IsMainCharacter,
                    PhoneNumber = castDro.PhoneNumber,
                    PlayId = castDro.PlayId
                };

                casts.Add(cast);

                string role = cast.IsMainCharacter ? "main" : "lesser";

                sb.AppendLine(string.Format(SuccessfulImportActor, cast.FullName, role));
            }

            context.Casts.AddRange(casts);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var importedTheatresDtos = JsonConvert.DeserializeObject<ImportTheatherDto[]>(jsonString);

            var theatres = new HashSet<Theatre>();

            foreach (var theatreDto in importedTheatresDtos!)
            {
                if (!IsValid(theatreDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var theatre = new Theatre
                {
                    Name = theatreDto.Name,
                    NumberOfHalls = theatreDto.NumberOfHalls,
                    Director = theatreDto.Director
                };

                foreach (var ticketDto in theatreDto.Tickets)
                {
                    if (!IsValid(ticketDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    theatre.Tickets.Add(new Ticket
                    {
                        Price = ticketDto.Price,
                        RowNumber = ticketDto.RowNumber,
                        PlayId = ticketDto.PlayId
                    });
                }

                theatres.Add(theatre);

                sb.AppendLine(string.Format(SuccessfulImportTheatre, theatre.Name, theatre.Tickets.Count));
            }

            context.Theatres.AddRange(theatres);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
