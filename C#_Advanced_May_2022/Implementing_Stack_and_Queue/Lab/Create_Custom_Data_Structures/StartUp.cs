using Create_Custom_Data_Structures;

var queue = new CustomQueue();

queue.Enqueue(1);
queue.Enqueue(2);
queue.Enqueue(3);
queue.Enqueue(4);
queue.Enqueue(5);

int test = queue.Dequeue();
queue.Dequeue();
queue.Dequeue();
queue.Dequeue();

Console.WriteLine(test);