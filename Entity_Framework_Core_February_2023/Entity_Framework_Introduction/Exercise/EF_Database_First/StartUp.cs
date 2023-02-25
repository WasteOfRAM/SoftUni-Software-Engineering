using Microsoft.EntityFrameworkCore;

using System.Globalization;
using System.Text;

using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni;

public class StartUp
{
    static void Main()
    {
        var context = new SoftUniContext();

        var result = RemoveTown(context);

        Console.WriteLine(result);
    }

    // 03. Employees Full Information

    public static string GetEmployeesFullInformation(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var employees = context.Employees
            .AsNoTracking()
            .OrderBy(e => e.EmployeeId)
            .Select(e => new { e.FirstName, e.LastName, e.MiddleName, e.JobTitle, e.Salary})
            .ToArray();

        foreach (var e in employees)
        {
            sb
                .AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");
        }

        return sb.ToString().TrimEnd();
    }

    //04. Employees with Salary Over 50 000

    public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var employees = context.Employees
            .AsNoTracking()
            .Where(e => e.Salary > 50000)
            .Select(e => new { e.FirstName, e.Salary })
            .OrderBy(e => e.FirstName)
            .ToArray();

        foreach (var e in employees)
        {
            sb
                .AppendLine($"{e.FirstName} - {e.Salary:f2}");
        }

        return sb.ToString().TrimEnd();
    }

    //05. Employees from Research and Development

    public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var employees = context.Employees
            .AsNoTracking()
            .Where(e => e.Department.Name == "Research and Development")
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                DepartmentName = e.Department.Name,
                e.Salary
            })
            .OrderBy(e => e.Salary)
            .ThenByDescending(e => e.FirstName)
            .ToArray();

        foreach (var e in employees)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:f2}");
        }

        return sb.ToString().TrimEnd();
    }

    //06. Adding a New Address and Updating Employee

    public static string AddNewAddressToEmployee(SoftUniContext context)
    {
        Address newAddress = new Address()
        {
            AddressText = "Vitoshka 15",
            TownId = 4
        };

        Employee employee = context.Employees.FirstOrDefault(e => e.LastName == "Nakov")!;

        employee.Address = newAddress;

        context.SaveChanges();

        var addresses = context.Employees
            .OrderByDescending(e => e.AddressId)
            .Take(10)
            .Select(e => e.Address!.AddressText)
            .ToArray();

        return string.Join(Environment.NewLine, addresses);
    }

    //07. Employees and Projects

    public static string GetEmployeesInPeriod(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var employeesProjects = context.Employees
            .AsNoTracking()
            .Take(10)
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                ManagerFirstName = e.Manager!.FirstName,
                ManagerLastName = e.Manager!.LastName,
                Projects = e.EmployeesProjects
                    .Where(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003)
                    .Select(ep => new
                    {
                        ProjectName = ep.Project.Name,
                        StartDate = ep.Project!.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                        EndDate = ep.Project!.EndDate.HasValue
                        ? ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                        : "not finished"
                    })
                    .ToArray()
            })
            .ToArray();


        foreach (var e in employeesProjects)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName}");

            foreach (var p in e.Projects)
            {
                sb.AppendLine($"--{p.ProjectName} - {p.StartDate} - {p.EndDate}");
            }
        }

        return sb.ToString().TrimEnd();
    }

    //08. Addresses by Town

    public static string GetAddressesByTown(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var addressesByTown = context.Addresses
            .AsNoTracking()
            .OrderByDescending(a => a.Employees.Count)
            .ThenBy(a => a.Town!.Name)
            .ThenBy(a => a.AddressText)
            .Take(10)
            .Select(a => new
            {
                a.AddressText,
                TownName = a.Town!.Name,
                EmployeeCount = a.Employees.Count
            })
            .ToArray();

        foreach (var a in addressesByTown)
        {
            sb.AppendLine($"{a.AddressText}, {a.TownName} - {a.EmployeeCount} employees");
        }

        return sb.ToString().TrimEnd();
    }

    //09. Employee 147

    public static string GetEmployee147(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var employee147 = context.Employees
            .AsNoTracking()
            .Include("EmployeesProjects.Project")
            .FirstOrDefault(e => e.EmployeeId == 147)!;

        sb.AppendLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");


        foreach (var p in employee147.EmployeesProjects.ToArray().OrderBy(p => p.Project.Name))
        {
            sb.AppendLine($"{p.Project.Name}");
        }

        return sb.ToString().TrimEnd();
    }

    //10. Departments with More Than 5 Employees

    public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var departments = context.Departments
            .Where(d => d.Employees.Count > 5)
            .OrderBy(d => d.Employees.Count)
            .ThenBy(d => d.Name)
            .Select(d => new
            {
                DepartmentName = d.Name,
                ManagerFirstName = d.Manager.FirstName,
                ManagerLastName = d.Manager.LastName,
                DepartmentEmployees = d.Employees
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .Select(e => new
                    {
                        EmployeeFirstName = e.FirstName,
                        EmployeeLastName = e.LastName,
                        e.JobTitle
                    })
                    .ToArray()
            })
            .ToArray();

        foreach (var d in departments)
        {
            sb.AppendLine($"{d.DepartmentName} - {d.ManagerFirstName} {d.ManagerLastName}");

            foreach (var e in d.DepartmentEmployees)
            {
                sb.AppendLine($"{e.EmployeeFirstName} {e.EmployeeLastName} - {e.JobTitle}");
            }
        }


        return sb.ToString().TrimEnd();
    }

    //11. Find Latest 10 Projects

    public static string GetLatestProjects(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var projects = context.Projects
            .AsNoTracking()
            .OrderByDescending(p => p.StartDate)
            .Take(10)
            .OrderBy(p => p.Name)
            .Select(p => new
            {
                p.Name,
                p.Description,
                StartDate = p.StartDate.ToString("M/d/yyyy h:mm:ss tt")
            })
            .ToArray();

        foreach (var p in projects)
        {
            sb.AppendLine($"{p.Name}").AppendLine($"{p.Description}").AppendLine($"{p.StartDate}");
        }
        

        return sb.ToString().TrimEnd();
    }

    //12. Increase Salaries

    public static string IncreaseSalaries(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var employees = context.Employees
            .Where(e => e.Department.Name == "Engineering" || e.Department.Name == "Tool Design" || e.Department.Name == "Marketing" || e.Department.Name == "Information Services")
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName)
            .ToArray();

        foreach (var e in employees)
        {
            e.Salary *= 1.12M;
        }

        context.SaveChanges();

        foreach (var e in employees)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
        }

        return sb.ToString().TrimEnd();
    }

    //13. Find Employees by First Name Starting With Sa

    public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var employees = context.Employees
            //.Where(e => EF.Functions.Collate(e.FirstName, "SQL_Latin1_General_CP1_CI_AS").StartsWith("Sa")) // -Judge does not like the EF.Functions.Collate
            .Where(e => e.FirstName.ToLower().StartsWith("Sa".ToLower()))
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.JobTitle,
                e.Salary
            })
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName);

        foreach (var e in employees)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
        }

        return sb.ToString().TrimEnd();
    }

    //14. Delete Project by Id

    public static string DeleteProjectById(SoftUniContext context)
    {
        var projectToRemove = context.Projects.Find(2)!;

        var epToRemove = context.EmployeesProjects.Where(ep => ep.ProjectId == 2);

        context.EmployeesProjects.RemoveRange(epToRemove);
        context.Projects.Remove(projectToRemove);

        context.SaveChanges();

        var projects = context.Projects
            .AsNoTracking()
            .Take(10)
            .Select(p => p.Name)
            .ToArray();

        return string.Join(Environment.NewLine, projects);
    }

    // 15. Remove Town

    public static string RemoveTown(SoftUniContext context)
    {
        var employeesInSeattle = context.Employees
            .Where(e => e.Address!.Town!.Name == "Seattle")
            .ToArray();

        foreach (var a in employeesInSeattle)
        {
            a.AddressId = null;
        }

        var addressesToRemove = context.Addresses
            .Where(a => a.Town!.Name == "Seattle")
            .ToArray();

        int removedAddressesCount = addressesToRemove.Count();

        context.Addresses.RemoveRange(addressesToRemove);

        var townToRemove = context.Towns.FirstOrDefault(t => t.Name == "Seattle")!;

        context.Towns.Remove(townToRemove);

        context.SaveChanges();

        return $"{removedAddressesCount} addresses in Seattle were deleted";
    }
}