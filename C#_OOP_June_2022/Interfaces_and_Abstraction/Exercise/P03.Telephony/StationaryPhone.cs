namespace P03.Telephony
{
    using System;
    public class StationaryPhone : ICalling
    {
        public string MakeCall(string phoneNumber)
        {
            if (!int.TryParse(phoneNumber, out _))
                throw new FormatException("Invalid number!");

            return $"Dialing... {phoneNumber}";
        }
    }
}
