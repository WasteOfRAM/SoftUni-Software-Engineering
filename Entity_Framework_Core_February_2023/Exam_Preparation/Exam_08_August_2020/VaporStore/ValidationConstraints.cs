namespace VaporStore
{
    public static class ValidationConstraints
    {
        // Game

        public const string GamePriceMinValue = "0.0";
        public const string GamePriceMaxValue = "79228162514264337593543950335";

        // User

        public const int UsernameMinLength = 3;
        public const int UsernameMaxLength = 20;

        public const string UserFullNameRegexPatern = @"^([A-Z]{1}[a-z]*)\s{1}([A-Z]{1}[a-z]*)$";

        public const int UserAgeMinValue = 3;
        public const int UserAgeMaxValue = 103;

        // Card

        public const int CardNumberMinLength = 19;
        public const int CardNumberMaxLength = 19;
        public const string CardNumberRegecPatern = @"^\d{4} \d{4} \d{4} \d{4}$";

        public const int CardCvcMinLength = 3;
        public const int CardCvcMaxLength = 3;
        public const string CardCvcRegexPatern = @"^\d{3}$";

        // Purchase

        public const int PurchaseProductKeyMinLength = 14;
        public const int PurchaseProductKeyMaxLength = 14;
        public const string PurchaseProductKeyRegexPatern = @"^([A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4})$";
    }
}
