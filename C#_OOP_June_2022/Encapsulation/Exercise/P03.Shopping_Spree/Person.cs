namespace P03.Shopping_Spree
{
    using System;
    using System.Collections.Generic;
    public class Person
    {
        private string name;
        private int money;
        private readonly List<Product> bag;

        public Person(string name, int money)
        {
            this.Name = name;
            this.Money = money;
            this.bag = new List<Product>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }

                this.name = value;
            }
        }

        public int Money
        {
            get
            {
                return this.money;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }

                this.money = value;
            }
        }

        public IReadOnlyCollection<Product> Bag
        {
            get
            {
                return this.bag.AsReadOnly();
            }
        }

        public bool BuyProduct(Product product)
        {
            if (product.Cost > this.Money)
            {
                return false;
            }

            this.Money -= product.Cost;
            this.bag.Add(product);

            return true;
        }
    }
}
