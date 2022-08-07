﻿namespace SpaceStation.Models.Bags
{
    using Contracts;
    using System.Collections.Generic;

    public class Backpack : IBag
    {
        private readonly List<string> items;

        public Backpack()
        {
            this.items = new List<string>();
        }

        public ICollection<string> Items => this.items;
    }
}
