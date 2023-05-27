namespace Boardgames;

public static class ValidationConstraints
{
    // Boardgame

    public const int BoardgameNameMinLength = 10;
    public const int BoardgameNameMaxLength = 20;

    public const double BoardgameRatingMinValue = 1.00;
    public const double BoardgameRatingMaxValue = 10.00;

    public const int BoardgameYearPublishedMinValue = 2018;
    public const int BoardgameYearPublishedMaxValue = 2023;

    // Seller

    public const int SellerNameMinLength = 5;
    public const int SellerNameMaxLength = 20;

    public const int SellerAddressMinLength = 2;
    public const int SellerAddressMaxLength = 30;

    public const string SellerWebsiteRegexPatern = @"^www.[\w\d-]+\.com$";

    // Creator

    public const int CreatorFirstNameMinLength = 2;
    public const int CreatorFirstNameMaxLength = 7;

    public const int CreatorLastNameMinLength = 2;
    public const int CreatorLastNameMaxLength = 7;
}
