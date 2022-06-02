using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    public class DateModifier
    {
        private DateTime dateOne;
        private DateTime dateTwo;

        public DateModifier(DateTime dateOne, DateTime dateTwo)
        {
            this.dateOne = dateOne;
            this.dateTwo = dateTwo;
        }

        public int DateDifference()
        {
            return Math.Abs((this.dateOne - this.dateTwo).Days);
        }
    }
}
