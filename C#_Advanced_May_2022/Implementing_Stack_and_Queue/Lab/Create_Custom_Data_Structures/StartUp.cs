using Create_Custom_Data_Structures;

var stack = new CustomStack();

stack.Push(1);
stack.Push(2);
stack.Push(3);
stack.Push(4);
stack.Push(5);

stack.Pop();
stack.Pop();

Console.WriteLine(stack.Peek());

stack.ForEach(x => Console.WriteLine(x));