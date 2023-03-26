namespace SoftJail.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data";

        private const string SuccessfullyImportedDepartment = "Imported {0} with {1} cells";

        private const string SuccessfullyImportedPrisoner = "Imported {0} {1} years old";

        private const string SuccessfullyImportedOfficer = "Imported {0} ({1} prisoners)";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var departmentsCellsDto = JsonConvert.DeserializeObject<ImportDepartmentCellsDto[]>(jsonString);

            var cells = new List<Department>();

            foreach (var deparmentDto in departmentsCellsDto)
            {
                if (!IsValid(deparmentDto) || deparmentDto.Cells.Length == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool invalidCell = false;

                foreach (var cell in deparmentDto.Cells)
                {
                    if (!IsValid(cell))
                    {
                        sb.AppendLine(ErrorMessage);
                        invalidCell = true;
                        break;
                    }
                }

                if (invalidCell)
                    continue;

                var department = new Department { Name =  deparmentDto.Name };

                foreach (var cellDto in deparmentDto.Cells)
                {
                    department.Cells.Add(new Cell
                    {
                        CellNumber = cellDto.CellNumber,
                        HasWindow = cellDto.HasWindow
                    });
                }

                cells.Add(department);
                sb.AppendLine(string.Format(SuccessfullyImportedDepartment, deparmentDto.Name, deparmentDto.Cells.Count()));
            }

            context.Departments.AddRange(cells);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var prisonersWithMailDtos = JsonConvert.DeserializeObject<ImportPrisonersAndMailDto[]>(jsonString);

            var prisoners = new List<Prisoner>();

            foreach (var prisonerMailDto in prisonersWithMailDtos)
            {
                if (!IsValid(prisonerMailDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool invalidMail = false;

                foreach (var mail in prisonerMailDto.Mails)
                {
                    if (!IsValid(mail))
                    {
                        sb.AppendLine(ErrorMessage);
                        invalidMail = true;
                        break;
                    }
                }

                if (invalidMail)
                    continue;


                var prisoner = new Prisoner
                {
                    FullName = prisonerMailDto.FullName,
                    Nickname = prisonerMailDto.Nickname,
                    Age = prisonerMailDto.Age,
                    IncarcerationDate = DateTime.ParseExact(prisonerMailDto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    ReleaseDate = prisonerMailDto.ReleaseDate != null ? DateTime.ParseExact(prisonerMailDto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : null,
                    Bail = prisonerMailDto.Bail,
                    CellId = prisonerMailDto.CellId,
                };

                foreach (var mail in prisonerMailDto.Mails)
                {
                    prisoner.Mails.Add(new Mail
                    {
                        Description = mail.Description,
                        Sender = mail.Sender,
                        Address = mail.Address
                    });
                }

                sb.AppendLine(string.Format(SuccessfullyImportedPrisoner, prisoner.FullName, prisoner.Age));
                prisoners.Add(prisoner);
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportOfficersPrisonersDto[]), new XmlRootAttribute("Officers"));

            using StringReader reader = new StringReader(xmlString);

            var officersDtos = (ImportOfficersPrisonersDto[])serializer.Deserialize(reader)!;

            var officers = new List<Officer>();

            foreach (var officerDto in officersDtos)
            {
                if (!IsValid(officerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!Enum.IsDefined(typeof(Position), officerDto.Position) ||
                    !Enum.IsDefined(typeof(Weapon), officerDto.Weapon))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var officer = new Officer
                {
                    FullName = officerDto.FullName,
                    Salary = officerDto.Salary,
                    Position = (Position)Enum.Parse(typeof(Position), officerDto.Position),
                    Weapon = (Weapon)Enum.Parse(typeof(Weapon), officerDto.Weapon),
                    DepartmentId = officerDto.DepartmentId
                };

                foreach (var prisoner in officerDto.Prisoners)
                {
                    officer.OfficerPrisoners.Add(new OfficerPrisoner
                    {
                        Officer = officer,
                        PrisonerId = prisoner.PrisonerId
                    });
                }

                officers.Add(officer);

                sb.AppendLine(string.Format(SuccessfullyImportedOfficer, officer.FullName, officer.OfficerPrisoners.Count));
            }

            context.Officers.AddRange(officers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}