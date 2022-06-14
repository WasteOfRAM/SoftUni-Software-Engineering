using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace P06.Equality_Logic
{
    public class Person : IComparable<Person>
    {
        private string name;
        private int age;

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public int CompareTo([AllowNull] Person other)
        {
            int result = this.name.CompareTo(other.name);
            if(result == 0)
                result = this.age.CompareTo(other.age);

            return result;
        }

        public override bool Equals(object obj)
        {
            Person other = obj as Person;

            if(other == null)
                return false;

            return this.name == other.name && this.age == other.age;
        }

        public override int GetHashCode() => this.name.GetHashCode() ^ this.age.GetHashCode();
    }
}
