using System.Diagnostics.CodeAnalysis;
using System;

namespace P05_06.Generic_Count_Method
{
    public class Box<T> : IComparable<T>
    {
        private IComparable<T> value;

        public Box(IComparable<T> value)
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
