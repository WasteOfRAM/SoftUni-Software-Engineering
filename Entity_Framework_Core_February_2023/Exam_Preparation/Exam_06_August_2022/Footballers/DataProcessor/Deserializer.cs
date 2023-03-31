namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using Footballers.Data.Models;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportCoachDto[]), new XmlRootAttribute("Coaches"));

            using StringReader reader = new StringReader(xmlString);

            var coachesDtos = (ImportCoachDto[])serializer.Deserialize(reader)!;

            var coaches = new HashSet<Coach>();

            foreach (var coachDto in coachesDtos)
            {
                if (!IsValid(coachDto) || string.IsNullOrEmpty(coachDto.Nationality))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var coach = new Coach
                {
                    Name = coachDto.Name,
                    Nationality = coachDto.Nationality
                };

                foreach (var footbalerDto in coachDto.Footballers)
                {
                    var IsValidStartDate = DateTime.TryParseExact(footbalerDto.ContractStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);
                    var IsValidEndDate = DateTime.TryParseExact(footbalerDto.ContractEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

                    if (!IsValid(footbalerDto) || !IsValidStartDate || !IsValidEndDate || startDate > endDate || 
                        !Enum.IsDefined((BestSkillType)footbalerDto.BestSkillType) ||
                        !Enum.IsDefined((PositionType)footbalerDto.PositionType))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var foorballer = new Footballer
                    {
                        Name = footbalerDto.Name,
                        ContractStartDate = startDate,
                        ContractEndDate = endDate,
                        BestSkillType = (BestSkillType)footbalerDto.BestSkillType,
                        PositionType = (PositionType)footbalerDto.PositionType
                    };

                    coach.Footballers.Add(foorballer);
                }

                coaches.Add(coach);

                sb.AppendLine(string.Format(SuccessfullyImportedCoach, coach.Name, coach.Footballers.Count));
            }

            context.Coaches.AddRange(coaches);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var teamsDtos = JsonConvert.DeserializeObject<ImportTeamDto[]>(jsonString);

            var teams = new HashSet<Team>();

            foreach (var teamDto in teamsDtos!)
            {
                if (!IsValid(teamDto) || teamDto.Trophies == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var team = new Team
                {
                    Name = teamDto.Name,
                    Nationality = teamDto.Nationality,
                    Trophies = teamDto.Trophies
                };

                foreach (var footballerId in teamDto.Footballers.Distinct())
                {
                    var footbaler = context.Footballers.FirstOrDefault(f => f.Id == footballerId);

                    if (footbaler == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    team.TeamsFootballers.Add(new TeamFootballer
                    {
                        Footballer = footbaler,
                        Team = team
                    });
                }

                teams.Add(team);

                sb.AppendLine(string.Format(SuccessfullyImportedTeam, team.Name, team.TeamsFootballers.Count));
            }

            context.Teams.AddRange(teams);
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
