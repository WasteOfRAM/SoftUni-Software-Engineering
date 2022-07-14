namespace P01.Square_Root
{
    using System;
    internal class Program
    {
        static void Main()
        {
            int number = int.Parse(Console.ReadLine());

            try
            {
                if (number < 0)
                    throw new ArgumentException("Invalid number.");

                int sqereRoot = (int)Math.Sqrt(number);

                Console.WriteLine(sqereRoot);
            }
            catch (ArgumentException msg)
            {
                Console.WriteLine(msg.Message);
            }
            finally
            {
                Console.WriteLine("Goodbye.");
            }
        }
    }
}
