namespace Database.Tests
{
    using System;
    using System.Reflection;
    using NUnit.Framework;

    [TestFixture]
    public class DatabaseTests
    {
        [TestCase(new int[] { })]
        [TestCase(new int[] { 5 })]
        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void ConstructorDataSizeParameters(int[] elements)
        {
            // Arrange
            Database database = new Database(elements);

            // Act
            int[] actualData = database.Fetch();
            int[] expectedData = elements;

            int actualCount = database.Count;
            int expectedCount = elements.Length;

            // Assert
            CollectionAssert.AreEqual(expectedData, actualData);
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void ArrayMaxStoringCapacityOnCreation()
        {
            // Arrange
            int[] arr = new int[17];

            // Assert
            Assert.That(() =>
            {
                // Act
                Database database = new Database(arr);
            }, Throws.Exception.TypeOf<InvalidOperationException>(), "Array's capacity must be exactly 16 integers!");
        }

        [Test]
        public void AddingElementsOverMaxCapacity()
        {
            // Arrange
            Database database = new Database(new int[16]);

            // Assert
            Assert.That(() =>
            {
                database.Add(5);
            }, Throws.Exception.TypeOf<InvalidOperationException>(), "Array's capacity must be exactly 16 integers!");
        }

        [Test]
        public void CountOnElementRemove()
        {
            // Arrange
            Database database = new Database(1, 2, 3);

            // Act
            database.Remove();

            int actualCount = database.Count;
            int expectedCount = 2;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void RemoveElementFromEmptyCollection()
        {
            // Arrange
            Database database = new Database(new int[0]);

            // Assert
            Assert.That(() =>
            {
                database.Remove();
            }, Throws.Exception.TypeOf<InvalidOperationException>(), "The collection is empty!");
        }

        [Test]
        public void FetchMethodReturnValue()
        {
            // Arrange
            int[] data = new int[] { 1, 2, 3 };
            Database database = new Database(data);

            // Act
            var actualData = database.Fetch();
            var expectedData = data;

            // Assert
            CollectionAssert.AreEqual(expectedData, actualData);
        }
    }
}
