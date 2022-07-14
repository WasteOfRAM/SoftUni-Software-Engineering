namespace P04.Wild_Farm.Exeptions
{
    using System;
    public class InvalidAnimalTypeExeption : Exception
    {
        public InvalidAnimalTypeExeption(string message) 
            : base(message)
        {
        }
    }
}
