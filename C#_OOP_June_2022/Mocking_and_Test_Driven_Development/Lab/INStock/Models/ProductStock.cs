namespace INStock.Models
{
    using System.Linq;

    using Contracts;
    using System.Collections;
    using System.Collections.Generic;
    using System;

    public class ProductStock : IProductStock
    {
        private IList<IProduct> products;

        public ProductStock()
        {
            this.products = new List<IProduct>();
        }

        public IProduct this[int index]
        {
            get
            {
                if (index < 0 || index >= this.products.Count)
                    throw new IndexOutOfRangeException();

                return this.products[index];
            }
            set
            {
                if (index < 0 || index >= this.products.Count)
                    throw new IndexOutOfRangeException();

                this.products[index] = value;
            }
        }

        public int Count => this.products.Count;

        public void Add(IProduct product)
        {
            if (this.Contains(product))
                throw new ArgumentException("Cant have 2 products with same lable");

            this.products.Add(product);
        }

        public bool Contains(IProduct product)
        {
            if (this.products.Any(p => p.CompareTo(product) == 0))
                return true;

            return false;
        }

        public IProduct Find(int index)
        {
            var product = this[index];

            return product;
        }

        public IEnumerable<IProduct> FindAllByPrice(decimal price)
        {
            IEnumerable<IProduct> productsWithPrice = new List<IProduct>();

            productsWithPrice = this.products.Where(p => p.Price == price);

            return productsWithPrice;
        }

        public IEnumerable<IProduct> FindAllByQuantity(int quantity)
        {
            IEnumerable<IProduct> allWithQuantity = new List<IProduct>();

            allWithQuantity = this.products.Where(p => p.Quantity == quantity);

            return allWithQuantity;
        }

        public IEnumerable<IProduct> FindAllInRange(decimal lo, decimal hi)
        {
            IEnumerable<IProduct> inPriceRange = new List<IProduct>();

            inPriceRange = this.products.Where(product => product.Price >= lo && product.Price <= hi).OrderByDescending(product => product.Price);

            return inPriceRange;
        }

        public IProduct FindByLabel(string label)
        {
            var result = this.products.FirstOrDefault(p => p.Label == label);

            if (result == null)
                throw new ArgumentException("No such product is in stock");

            return result;
        }

        public IProduct FindMostExpensiveProduct()
        {
            IProduct result = this.products.OrderByDescending(p => p.Price).FirstOrDefault();

            return result;
        }

        public IEnumerator<IProduct> GetEnumerator()
        {
            foreach (var item in this.products)
            {
                yield return item;
            }
        }

        public bool Remove(IProduct product) => this.products.Remove(product);

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
