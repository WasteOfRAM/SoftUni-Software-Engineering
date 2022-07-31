namespace INStock.Models
{
    using Contracts;
    using System.Diagnostics.CodeAnalysis;

    public class Product : IProduct
    {
        public Product(string lable, decimal price, int quantity)
        {
            this.Label = lable;
            this.Price = price;
            this.Quantity = quantity;
        }

        public string Label {get; private set;}

        public decimal Price { get; private set; }

        public int Quantity { get; private set; }

        public int CompareTo([AllowNull] IProduct other)
        {
            return this.Label.CompareTo(other.Label);
        }
    }
}
