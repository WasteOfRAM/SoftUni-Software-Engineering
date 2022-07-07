namespace P03.Telephony
{
    using System;
    using System.Linq;

    public class Smartphone : ICalling, IBrowsing
    {
        public string Browsing(string webSite)
        {
            if (webSite.Any(char.IsDigit))
                throw new FormatException("Invalid URL!");

            return $"Browsing: {webSite}!";
        }

        public string MakeCall(string phoneNumber)
        {
            if (!int.TryParse(phoneNumber, out _))
                throw new FormatException("Invalid number!");

            return $"Calling... {phoneNumber}";
        }
    }
}
