
namespace GenericArrayCreator
{
    public class ArrayCreator
    {
        public static T[] Create<T>(int lenght, T item)
        {
            var data = new T[lenght];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = item;
            }

            return data;
        }
    }
}
