namespace Footballers;

public static class ValidationConstraints
{
    // Footballer

    public const int FootballerNameMinLength = 2;
    public const int FootballerNameMaxLength = 40;

    // Team

    public const int TeamNameMinLength = 3;
    public const int TeamNameMaxLength = 40;

    public const string TeamNameRegexPatern = @"^[\w\d\s\.-]+$";

    public const int TeamNationalityMinLength = 2;
    public const int TeamNationalityMaxLength = 40;

    // Coach
    public const int CoachNameMinLength = 2;
    public const int CoachNameMaxLength = 40;
}
