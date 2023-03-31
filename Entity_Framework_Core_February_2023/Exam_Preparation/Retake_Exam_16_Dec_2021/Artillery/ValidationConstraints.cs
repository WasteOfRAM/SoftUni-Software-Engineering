namespace Artillery;

public static class ValidationConstraints
{
    // Country

    public const int CountryNameMinLength = 4;
    public const int CountryNameMaxLength = 60;

    public const int ArmySizeMin = 50000;
    public const int ArmySizeMax = 10000000;

    // Manufacturer

    public const int ManufacturerNameMinLength = 4;
    public const int ManufacturerNameMaxLength = 40;

    public const int ManufacturerFoundedMinLength = 10;
    public const int ManufacturerFoundedMaxLength = 100;

    // Shell

    public const int ShellWeightMin = 2;
    public const int ShellWeightMax = 1680;

    public const int CaliberMin = 4;
    public const int CaliberMax = 30;

    // Gun

    public const int GunWeigthMinValue = 100;
    public const int GunWeigthMaxValue = 1350000;

    public const double BarrelLengthMinValue = 2.00;
    public const double BarrelLengthMaxValue = 35.00;

    public const int GunRangeMin = 1;
    public const int GunRangeMax = 100000;
}
