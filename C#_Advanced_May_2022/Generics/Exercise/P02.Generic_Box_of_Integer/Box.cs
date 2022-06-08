
namespace P02.Generic_Box_of_Integer
{
    public class Box<T>
    {
        private T value;

        public Box(T value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return $"{typeof(T).FullName}: {this.value}";
        }
    }
}
