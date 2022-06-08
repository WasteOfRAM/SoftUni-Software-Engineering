using System;
using System.Collections.Generic;
using System.Text;

namespace P07.Tuple
{
    public class Tuple<ItemOne, ItemTwo>
    {
        public Tuple()
        {

        }
        public Tuple(ItemOne item1, ItemTwo item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        public ItemOne Item1 { get; set; }
        public ItemTwo Item2 { get; set; }
    }
}
