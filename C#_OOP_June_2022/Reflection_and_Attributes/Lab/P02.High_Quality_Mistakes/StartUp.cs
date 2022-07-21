using System;

namespace Stealer
{
    public class StartUp
    {
        static void Main()
        {
            Spy spy = new Spy();
            string result = spy.AnalyzeAccessModifiers("Hacker");
            Console.WriteLine(result);
        }
    }
}
