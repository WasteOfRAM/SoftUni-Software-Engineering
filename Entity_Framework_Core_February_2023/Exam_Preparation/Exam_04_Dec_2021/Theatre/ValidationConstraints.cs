using System.IO;

namespace Theatre;

public static class ValidationConstraints
{
    // Theatre

    public const int TheatreNameMinLength = 4;
    public const int TheatreNameMaxLength = 30;

    public const int TheatreMinHalls = 1;
    public const int TheatreMaxHalls = 10;

    public const int TheatreDirectorMinLength = 4;
    public const int TheatreDirectorMaxLength = 30;

    // PLay

    public const int PlayTitleMinLength = 4;
    public const int PlayTitleMaxLength = 50;

    public const string PlayDurationMinTimeLength = "01:00:00";

    public const float PlayRaitingMinValue = 0.0f;
    public const float PlayRaitingMaxValue = 10.0f;

    public const int PlayDescriptionMaxLength = 700;

    public const int PlayScreenwriterMinLength = 4;
    public const int PlayScreenwriterMaxLength = 30;

    // Cast

    public const int CastFullNameMinLength = 4;
    public const int CastFullNameMaxLength = 30;

    public const string CastPhoneNumberRegexPatern = @"^(\+44-\d{2}-\d{3}-\d{4})$";

    // Ticket

    public const string TicketMinPrice = "1.00";
    public const string TicketMaxPrice = "100.00";

    public const int TicketMinRowNumber = 1;
    public const int TicketMaxRowNumber = 10;
}
