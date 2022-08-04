namespace Aquariums.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using NUnit.Framework;

    public class AquariumsTests
    {
        [Test]
        public void ConstructorCreatingFishObject()
        {
            string expectedName = "Test";
            bool expectedAvailable = true;

            var fish = new Fish(expectedName);

            Assert.That(expectedName, Is.EqualTo(fish.Name));
            Assert.That(expectedAvailable, Is.True);
        }

        [Test]
        public void AquariumConstructorTest()
        {
            var expectedName = "Test";
            var expectedCapacity = 20;
            var expectedCollection = new List<Fish>();

            var aquarium = new Aquarium(expectedName, expectedCapacity);
            var collectionField = typeof(Aquarium).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(fn => fn.Name == "fish");
            var actualColection = (List<Fish>)collectionField.GetValue(aquarium);

            Assert.That(expectedName, Is.EqualTo(aquarium.Name));
            Assert.That(expectedCapacity, Is.EqualTo(aquarium.Capacity));
            CollectionAssert.AreEqual(expectedCollection, actualColection);
        }

        [TestCase(null)]
        [TestCase("")]
        public void NameSetterShouldThrowExceptionIfNullOrEmpty(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var aquarium = new Aquarium(name, 20);
            }, "Invalid aquarium name!");
        }

        [TestCase(-1)]
        [TestCase(-23452)]
        public void CapacitySetterShouldThrowExceptionIfLesThanZero(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var aquarium = new Aquarium("Valid", capacity);
            }, "Invalid aquarium capacity!");
        }

        [Test]
        public void CountGetterShouldReturnCollectionCount()
        {
            var aquarium = new Aquarium("Valid", 20);

            Assert.That(aquarium.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddMethodWithAvailableCapacityShouldAddAFishToCollection()
        {
            var aquarium = new Aquarium("Valid", 2);
            var fish1 = new Fish("Fish1");
            var fish2 = new Fish("Fish2");

            aquarium.Add(fish1);
            aquarium.Add(fish2);

            Assert.That(2, Is.EqualTo(aquarium.Count));
        }

        [Test]
        public void AddingFishWithAquariumFullCapacityShouldThrowException()
        {
            var aquarium = new Aquarium("Valid", 1);
            var fish1 = new Fish("Fish1");
            var fish2 = new Fish("Fish2");

            aquarium.Add(fish1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.Add(fish2);
            }, $"Fish with the name {fish2.Name} doesn't exist!");
        }

        [Test]
        public void RemoveAnExistingFishByName()
        {
            var aquarium = new Aquarium("Valid", 2);
            var fish1 = new Fish("Fish1");
            var fish2 = new Fish("Fish2");

            aquarium.Add(fish1);
            aquarium.Add(fish2);

            aquarium.RemoveFish("Fish2");

            Assert.That(1, Is.EqualTo(aquarium.Count));
        }

        [Test]
        public void RemoveAnInExistingFishByNameShouldThrowException()
        {
            var aquarium = new Aquarium("Valid", 2);
            var fish1 = new Fish("Fish1");
            var fish2 = new Fish("Fish2");

            aquarium.Add(fish1);
            aquarium.Add(fish2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.RemoveFish("Invalid");
            }, $"Fish with the name Invalid doesn't exist!");
        }

        [Test]
        public void SellingInExistingFishShouldThrowException()
        {
            var aquarium = new Aquarium("Valid", 2);
            var fish1 = new Fish("Fish1");
            var fish2 = new Fish("Fish2");

            aquarium.Add(fish1);
            aquarium.Add(fish2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.SellFish("Invalid");
            }, $"Fish with the name Invalid doesn't exist!");
        }

        [Test]
        public void SellingExistingFishShouldReturnTheFish()
        {
            var aquarium = new Aquarium("Valid", 2);
            var fish1 = new Fish("Fish1");

            aquarium.Add(fish1);

            var actualFish = aquarium.SellFish("Fish1");

            Assert.That(fish1, Is.EqualTo(actualFish));
        }

        [Test]
        public void SellingAFishSettsAvailablePropToFalse()
        {
            var aquarium = new Aquarium("Valid", 2);
            var fish1 = new Fish("Fish1");

            aquarium.Add(fish1);

            var actualFish = aquarium.SellFish("Fish1");

            Assert.AreEqual(false, actualFish.Available);
        }

        [Test]
        public void ReportReturnsCorrectString()
        {
            var aquarium = new Aquarium("Valid", 2);
            var fish1 = new Fish("Fish1");
            var fish2 = new Fish("Fish2");

            aquarium.Add(fish1);
            aquarium.Add(fish2);

            string expectedString = $"Fish available at Valid: Fish1, Fish2";
            string actualString = aquarium.Report();

            Assert.That(expectedString, Is.EqualTo(actualString));
        }
    }
}
