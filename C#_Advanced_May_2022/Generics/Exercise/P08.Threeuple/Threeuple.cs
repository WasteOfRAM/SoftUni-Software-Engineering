using System;
using System.Collections.Generic;
using System.Text;

namespace P08.Threeuple
{
    public class Threeuple<ItemOne, ItemTwo, ItemThree>
    {
        public Threeuple()
        {

        }
        public Threeuple(ItemOne item1, ItemTwo item2, ItemThree item3)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
        }

        public ItemOne Item1 { get; set; }
        public ItemTwo Item2 { get; set; }
        public ItemThree Item3 { get; set; }

        public override string ToString()
        {
            return $"{this.Item1} -> {this.Item2} -> {this.Item3}";
        }
    }
}
