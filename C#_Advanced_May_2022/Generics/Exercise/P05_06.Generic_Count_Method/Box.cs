using System.Diagnostics.CodeAnalysis;
using System;

namespace P05_06.Generic_Count_Method
{
    public class Box<T> : IComparable<T> where T : IComparable<T>
    {
        private T value;

        public Box(T value)
        {
            this.value = value;
        }

        public int CompareTo([AllowNull] T other)
        {
            return this.value.CompareTo(other);
        }

        public override string ToString()
        {
            return $"{this.value.GetType().FullName}: {this.value}";
        }
    }
}
