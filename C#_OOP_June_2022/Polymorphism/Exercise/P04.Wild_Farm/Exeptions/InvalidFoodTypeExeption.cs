namespace P04.Wild_Farm.Exeptions
{
    using System;
    public class InvalidFoodTypeExeption : Exception
    {
        public InvalidFoodTypeExeption(string message) 
            : base(message)
        {
        }
    }
}
