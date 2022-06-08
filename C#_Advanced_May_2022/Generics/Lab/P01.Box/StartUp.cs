using System;

namespace BoxOfT
{
    public class StartUp
    {
        static void Main()
        {
            Box<int> box = new Box<int>();
            box.Add(1);
            box.Add(2);
            box.Add(3);
            Console.WriteLine(box.Remove());
            box.Add(4);
            box.Add(5);
            Console.WriteLine(box.Remove());

            var strBox = new Box<string>();
            strBox.Add("Test1");
            strBox.Add("Test2");
            strBox.Add("Test3");
            Console.WriteLine(strBox.Remove());
            strBox.Add("Test4");
            strBox.Add("Test5");
            Console.WriteLine(strBox.Remove());
        }
    }
}
