using Create_Custom_Data_Structures;

var list = new MyList();

list.Add(1);
list.Add(2);
list.Add(3);
list.Add(4);
list.Add(5);

Console.WriteLine(list.Count);

Console.WriteLine(list.RemoveAt(0));
Console.WriteLine(list.RemoveAt(0));
Console.WriteLine(list.RemoveAt(0));
Console.WriteLine(list.RemoveAt(0));
Console.WriteLine(list.RemoveAt(0));

for (int i = 1; i <= 10; i++)
{
    list.Add(i);
}

list.Insert(7, 16);

list.Swap(7, 1);

var contains = list.Constains(654);

for (int i = 0; i < list.Count; i++)
{
    Console.WriteLine(list[i]);
}

Console.WriteLine();

Console.WriteLine(contains);