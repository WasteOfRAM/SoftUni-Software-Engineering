namespace P04.Wild_Farm.Exeptions
{
    using System;
    public class WrongFoodTypeExeption : Exception
    {
        public WrongFoodTypeExeption(string message) 
            : base(message)
        {
        }
    }
}
