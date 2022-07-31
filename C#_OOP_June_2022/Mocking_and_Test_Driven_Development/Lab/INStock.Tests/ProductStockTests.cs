namespace INStock.Tests
{
    using INStock.Contracts;
    using INStock.Models;
    using NUnit.Framework;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using System;
    using System.Collections;

    [TestFixture]
    public class ProductStockTests
    {
        private static ProductStock defaultProducktStock;

        [SetUp]
        public void Setup()
        {
            defaultProducktStock = new ProductStock();
        }

        [Test]
        public void ConstructorShouldInitializeEmptyCollection()
        {
            FieldInfo collectionField = typeof(ProductStock).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(f => f.Name == "products");
            var actualCollection = (List<IProduct>)collectionField.GetValue(defaultProducktStock);

            Assert.NotNull(actualCollection);
        }

        [Test]
        public void IndexerReturningProductAtIndex()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);

            defaultProducktStock.Add(product1);
            defaultProducktStock.Add(product2);

            IProduct actualProductAtIndex = defaultProducktStock[1];

            Assert.AreEqual(product2, actualProductAtIndex);
        }

        [Test]
        public void IndexerSetsNewValueAtExistingIndex()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);

            defaultProducktStock.Add(product1);

            defaultProducktStock[0] = product2;

            Assert.AreEqual(product2, defaultProducktStock[0]);
        }

        [Test]
        public void IndexerGetterThrowsExeptionIfIndexDoesNotExist()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);

            defaultProducktStock.Add(product1);
            defaultProducktStock.Add(product2);

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                IProduct actualProductAtIndex = defaultProducktStock[3];
            });
        }

        [Test]
        public void IndexerSetterThrowsExeptionIfIndexDoesNotExist()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);

            defaultProducktStock.Add(product1);

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                defaultProducktStock[3] = product2;
            });
        }

        [Test]
        public void CountPropertyReturnsCollectionCountCorectly()
        {
            var actualCount = defaultProducktStock.Count;

            Assert.AreEqual(0, actualCount);
        }

        [Test]
        public void AddingProductsWithDiferentLablesToCollection()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);

            defaultProducktStock.Add(product1);
            defaultProducktStock.Add(product2);

            Assert.AreEqual(2, defaultProducktStock.Count);
        }

        [Test]
        public void AddingProductsWithSameLableShouldThrowExeption()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test", 34.3m, 99);

            defaultProducktStock.Add(product1);

            Assert.Throws<ArgumentException>(() =>
            {
                defaultProducktStock.Add(product2);
            }, "Cant have 2 products with same lable");
        }

        [Test]
        public void TestIfCollectionContainsProductWhenItIs()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test", 34.3m, 99);

            defaultProducktStock.Add(product1);

            bool result = defaultProducktStock.Contains(product2);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestIfCollectionContainsProductWhenItIsNot()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);

            defaultProducktStock.Add(product1);

            bool result = defaultProducktStock.Contains(product2);

            Assert.IsFalse(result);
        }

        [Test]
        public void FindProductByExistinIndex()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);

            defaultProducktStock.Add(product1);
            defaultProducktStock.Add(product2);

            var productResult = defaultProducktStock.Find(1);

            Assert.AreEqual(product2, productResult);
        }

        [Test]
        public void FindProductByIndexWithInExistingIndexShouldThrowExeption()
        {
            var product1 = new Product("Test", 54.33m, 4);
            defaultProducktStock.Add(product1);

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                defaultProducktStock.Find(5);
            });
        }

        [Test]
        public void FindProductByLable()
        {
            var product1 = new Product("Test", 54.33m, 4);
            defaultProducktStock.Add(product1);

            var actualProduct = defaultProducktStock.FindByLabel("Test");

            Assert.AreEqual(product1, actualProduct);
        }

        [Test]
        public void FindByLableThrowsExeptionIfProductWithThatLableDoesNotExist()
        {
            var product1 = new Product("Test", 54.33m, 4);
            defaultProducktStock.Add(product1);

            Assert.Throws<ArgumentException>(() =>
            {
                var product = defaultProducktStock.FindByLabel("Does not Exist");
            }, "No such product is in stock");
        }

        [Test]
        public void FindAllInPriceRangeWithValidProducts()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);
            var product3 = new Product("Test3", 24.3m, 99);
            var product4 = new Product("Test4", 30.3m, 99);

            defaultProducktStock.Add(product1);
            defaultProducktStock.Add(product2);
            defaultProducktStock.Add(product3);
            defaultProducktStock.Add(product4);

            IEnumerable<IProduct> expectedCollection = new List<IProduct> { product4, product2 }.OrderByDescending(p => p.Price);

            var actualEnumeration = defaultProducktStock.FindAllInRange(30, 40);

            CollectionAssert.AreEqual(expectedCollection, actualEnumeration);
        }

        [Test]
        public void FindAllInRangeWithNoMatchShouldReturnEmptyCollection()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);
            var product3 = new Product("Test3", 24.3m, 99);
            var product4 = new Product("Test5", 30.3m, 99);

            defaultProducktStock.Add(product1);
            defaultProducktStock.Add(product2);
            defaultProducktStock.Add(product3);
            defaultProducktStock.Add(product4);

            var actualCollection = defaultProducktStock.FindAllInRange(100, 500);

            CollectionAssert.IsEmpty(actualCollection);
        }

        [Test]
        public void FindAllByPriceWithValidProducts()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 54.33m, 99);
            var product3 = new Product("Test3", 24.3m, 99);

            defaultProducktStock.Add(product1);
            defaultProducktStock.Add(product2);
            defaultProducktStock.Add(product3);

            var actualCollection = defaultProducktStock.FindAllByPrice(54.33m);

            Assert.AreEqual(2, actualCollection.ToList().Count);
        }

        [Test]
        public void FindAllByPriceShouldReturnEmptyCollection()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 54.33m, 99);
            var product3 = new Product("Test3", 24.3m, 99);

            defaultProducktStock.Add(product1);
            defaultProducktStock.Add(product2);
            defaultProducktStock.Add(product3);

            var actualCollection = defaultProducktStock.FindAllByPrice(154.33m);

            CollectionAssert.IsEmpty(actualCollection);
        }

        [Test]
        public void FindTheMostExpenciveProductInStock()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);
            var product3 = new Product("Test3", 24.3m, 99);
            var product4 = new Product("Test5", 30.3m, 99);

            defaultProducktStock.Add(product1);
            defaultProducktStock.Add(product2);
            defaultProducktStock.Add(product3);
            defaultProducktStock.Add(product4);

            var actualProduct = defaultProducktStock.FindMostExpensiveProduct();

            Assert.AreEqual(product1, actualProduct);
        }

        [Test]
        public void FindTheMostExpensiveProductIfCollectionIsemptyReturnNull()
        {
            var actualProduct = defaultProducktStock.FindMostExpensiveProduct();

            Assert.IsNull(actualProduct);
        }

        [Test]
        public void FindAllByQuantityWithValidProducts()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);
            var product3 = new Product("Test3", 24.3m, 99);
            var product4 = new Product("Test5", 30.3m, 99);

            defaultProducktStock.Add(product1);
            defaultProducktStock.Add(product2);
            defaultProducktStock.Add(product3);
            defaultProducktStock.Add(product4);

            var expectedCollection = new List<IProduct> { product2, product3, product4 };
            var actualCollection = defaultProducktStock.FindAllByQuantity(99);

            CollectionAssert.AreEqual(expectedCollection, actualCollection);
        }

        [Test]
        public void FindAllByQuantityWithNoMacheShoudReturnEmptyCollection()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);
            var product3 = new Product("Test3", 24.3m, 99);
            var product4 = new Product("Test5", 30.3m, 99);

            defaultProducktStock.Add(product1);
            defaultProducktStock.Add(product2);
            defaultProducktStock.Add(product3);
            defaultProducktStock.Add(product4);

            var actualCollection = defaultProducktStock.FindAllByQuantity(98);

            CollectionAssert.IsEmpty(actualCollection);
        }

        [Test]
        public void GetEnumeratorReturnsAllProductsInStock()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);
            var product3 = new Product("Test3", 24.3m, 99);
            var product4 = new Product("Test5", 30.3m, 99);

            defaultProducktStock.Add(product1);
            defaultProducktStock.Add(product2);
            defaultProducktStock.Add(product3);
            defaultProducktStock.Add(product4);

            ICollection<IProduct> actualCollection = new List<IProduct>();
            foreach (var product in defaultProducktStock)
            {
                actualCollection.Add(product);
            }

            var expectedCollection = new List<IProduct> { product1, product2, product3, product4 };

            CollectionAssert.AreEqual(expectedCollection, actualCollection);
        }

        [Test]
        public void RemoveItemThatExistsInTheCollection()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);

            defaultProducktStock.Add(product1);
            defaultProducktStock.Add(product2);

            Assert.IsTrue(defaultProducktStock.Remove(product2));
        }

        [Test]
        public void RemoveItemThatDoesNotExistInTheCollection()
        {
            var product1 = new Product("Test", 54.33m, 4);
            var product2 = new Product("Test2", 34.3m, 99);

            defaultProducktStock.Add(product1);

            Assert.IsFalse(defaultProducktStock.Remove(product2));
        }

        
    }
}
