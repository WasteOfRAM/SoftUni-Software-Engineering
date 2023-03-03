namespace BookShop;

using BookShop.Models.Enums;
using Data;
using Initializer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Text;

public class StartUp
{
    public static void Main()
    {
        using var db = new BookShopContext();
        DbInitializer.ResetDatabase(db);

        int result = RemoveBooks(db);

        Console.WriteLine(result);
    }


    // 02. Age Restriction

    public static string GetBooksByAgeRestriction(BookShopContext context, string command)
    {
        AgeRestriction ageRestriction = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), command, true);

        var books = context.Books
            .AsNoTracking()
            .Where(b => b.AgeRestriction == ageRestriction)
            .OrderBy(b => b.Title)
            .Select(b => b.Title)
            .ToArray();

        return string.Join(Environment.NewLine, books);
    }

    // 03. Golden Books

    public static string GetGoldenBooks(BookShopContext context)
    {
        var books = context.Books
            .AsNoTracking()
            .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
            .OrderBy(b => b.BookId)
            .Select(b => b.Title)
            .ToArray();

        return string.Join(Environment.NewLine, books);
    }

    // 04. Books by Price

    public static string GetBooksByPrice(BookShopContext context)
    {
        var books = context.Books
            .AsNoTracking()
            .Where(b => b.Price > 40)
            .OrderByDescending(b => b.Price)
            .Select(b => $"{b.Title} - ${b.Price:f2}")
            .ToArray();

        return string.Join(Environment.NewLine, books);
    }

    // 05. Not Released In

    public static string GetBooksNotReleasedIn(BookShopContext context, int year)
    {
        var books = context.Books
            .AsNoTracking()
            .Where(b => b.ReleaseDate!.Value.Year != year)
            .OrderBy(b => b.BookId)
            .Select(b => b.Title)
            .ToArray();

        return string.Join(Environment.NewLine, books);
    }

    // 06. Book Titles by Category

    public static string GetBooksByCategory(BookShopContext context, string input)
    {
        string[] categoryList = input.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var books = context.Books
            .AsNoTracking()
            .Where(b => b.BookCategories.Any(bc => categoryList.Contains(bc.Category.Name.ToLower())))
            .OrderBy(b => b.Title)
            .Select(b => b.Title)
            .ToArray();

        return string.Join(Environment.NewLine, books);
    }

    // 07. Released Before Date

    public static string GetBooksReleasedBefore(BookShopContext context, string date)
    {
        DateTime targetDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        var books = context.Books
            .AsNoTracking()
            .Where(b => b.ReleaseDate < targetDate)
            .OrderByDescending(b => b.ReleaseDate)
            .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}")
            .ToArray();

        return string.Join(Environment.NewLine, books);
    }

    // 08. Author Search

    public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
    {
        var authors = context.Authors
            .AsNoTracking()
            .Where(a => a.FirstName.EndsWith(input))
            .AsEnumerable()
            .Select(a => $"{a.FirstName} {a.LastName}")
            .OrderBy(a => a)
            .ToArray();

        return string.Join(Environment.NewLine, authors);
    }

    // 09. Book Search

    public static string GetBookTitlesContaining(BookShopContext context, string input)
    {
        var books = context.Books
            .AsNoTracking()
            .Where(b => b.Title.ToLower().Contains(input.ToLower()))
            .AsEnumerable()
            .Select(b => b.Title)
            .OrderBy(b => b)
            .ToArray();


        return string.Join(Environment.NewLine, books);
    }

    // 10. Book Search by Author

    public static string GetBooksByAuthor(BookShopContext context, string input)
    {
        var books = context.Books
            .AsNoTracking()
            .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
            .OrderBy(b => b.BookId)
            .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
            .ToArray();

        return string.Join(Environment.NewLine, books);
    }

    // 11. Count Books

    public static int CountBooks(BookShopContext context, int lengthCheck)
    {
        int count = context.Books
            .AsNoTracking()
            .Where(b => b.Title.Length > lengthCheck)
            .ToArray()
            .Count();

        return count;
    }

    // 12. Total Book Copies

    public static string CountCopiesByAuthor(BookShopContext context)
    {
        var copiesByAuthor = context.Authors
            .AsNoTracking()
            .OrderByDescending(a => a.Books.Sum(b => b.Copies))
            .Select(a => $"{a.FirstName} {a.LastName} - {a.Books.Sum(b => b.Copies)}")
            .ToArray();

        return string.Join(Environment.NewLine, copiesByAuthor);
    }

    // 13. Profit by Category

    public static string GetTotalProfitByCategory(BookShopContext context)
    {
        var sb = new StringBuilder();

        var categorysProfit = context.Categories
            .AsNoTracking()
            .Select(c => new
            {
                c.Name,
                Total = c.CategoryBooks.Sum(b => b.Book.Price * b.Book.Copies)
            })
            .OrderByDescending(c => c.Total)
            .ThenBy(c => c.Name)
            .ToArray();

        foreach (var c in categorysProfit)
        {
            sb.AppendLine($"{c.Name} ${c.Total:f2}");
        }

        return sb.ToString().TrimEnd();
    }

    // 14. Most Recent Books

    public static string GetMostRecentBooks(BookShopContext context)
    {
        var sb = new StringBuilder();

        var booksByCategory = context.Categories
            .AsNoTracking()
            .Select(c => new
            {
                CategoryName = c.Name,
                RecentBooks = c.CategoryBooks.OrderByDescending(b => b.Book.ReleaseDate).Select(b => new { b.Book.Title, b.Book.ReleaseDate!.Value.Year } ).Take(3)
            })
            .OrderBy(c => c.CategoryName)
            .ToArray();

        foreach (var category in booksByCategory)
        {
            sb.AppendLine($"--{category.CategoryName}");

            foreach (var book in category.RecentBooks)
            {
                sb.AppendLine($"{book.Title} ({book.Year})");
            }
        }

        return sb.ToString().TrimEnd();
    }

    // 15. Increase Prices

    public static void IncreasePrices(BookShopContext context)
    {
        var books = context.Books
            .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year < 2010)
            .ToArray();

        foreach (var book in books)
        {
            book.Price += 5;
        }

        context.SaveChanges();
    }

    // 16. Remove Books

    public static int RemoveBooks(BookShopContext context)
    {
        var booksToRemove = context.Books.Where(b => b.Copies < 4200).ToArray();

        context.RemoveRange(booksToRemove);

        context.SaveChanges();

        return booksToRemove.Length;
    }
}


