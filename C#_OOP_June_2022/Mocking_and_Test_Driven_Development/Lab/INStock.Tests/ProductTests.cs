namespace INStock.Tests
{
    using INStock.Models;

    using NUnit.Framework;

    public class ProductTests
    {
        [Test]
        public void ConstructorSettingPropertties()
        {
            var expectedLable = "Test";
            var expectedPrice = 54.33m;
            var expectedQuantity = 5;

            var product = new Product(expectedLable, expectedPrice, expectedQuantity);

            Assert.AreEqual(expectedLable, product.Label);
            Assert.AreEqual(expectedPrice, product.Price);
            Assert.AreEqual(expectedQuantity, product.Quantity);
        }

        [Test]
        public void CompareTwoProductsWithSameLable()
        {
            var product1 = new Product("Test", 54.33m, 2);
            var product2 = new Product("Test", 4.33m, 15);

            var result = product1.CompareTo(product2);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void CompareTwoProductsWithDiferentLables()
        {
            var product1 = new Product("Test", 54.33m, 2);
            var product2 = new Product("Test2", 4.33m, 15);

            var result = product1.CompareTo(product2);

            Assert.AreEqual(-1, result);
        }
    }
}