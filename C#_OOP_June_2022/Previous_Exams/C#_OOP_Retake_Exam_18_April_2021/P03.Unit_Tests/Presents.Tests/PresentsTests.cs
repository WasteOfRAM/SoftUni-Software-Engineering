namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class PresentsTests
    {
        // Present class tests
        [Test]
        public void PresentClassConstructorInitializingProperties()
        {
            var expectedName = "Name";
            var expectedMagic = 15.55;

            var present = new Present(expectedName, expectedMagic);

            Assert.Multiple(() =>
            {
                Assert.That(expectedName, Is.EqualTo(present.Name));
                Assert.That(expectedMagic, Is.EqualTo(present.Magic));
            });
        }

        [Test]
        public void PresentNamePropGetter()
        {
            var expectedName = "Name";
            var expectedMagic = 15.55;

            var present = new Present(expectedName, expectedMagic);
            var actual = present.Name;

            Assert.That(actual, Is.EqualTo(expectedName));
        }

        [Test]
        public void PresentNamePropSetter()
        {
            var expectedName = "Name";
            var expectedMagic = 15.55;

            var present = new Present("To be changed", expectedMagic);
            present.Name = expectedName;

            Assert.That(present.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void PresentMagicPropGetter()
        {
            var expectedName = "Name";
            var expectedMagic = 15.55;

            var present = new Present(expectedName, expectedMagic);
            var actual = present.Magic;

            Assert.That(actual, Is.EqualTo(expectedMagic));
        }

        [Test]
        public void PresentMagicPropSetter()
        {
            var expectedName = "Name";
            var expectedMagic = 15.55;

            var present = new Present(expectedName, 12);
            present.Magic = expectedMagic;

            Assert.That(present.Magic, Is.EqualTo(expectedMagic));
        }

        // Bag class tests

        [Test]
        public void ConstructorInitializesWithEmptyList()
        {
            var bag = new Bag();

            var expectedCollection = new List<Present>();
            var actualCollection = bag.GetPresents();

            CollectionAssert.AreEqual(expectedCollection, actualCollection);
        }

        [Test]
        public void CreateMethodGettsValidPresentAndAddsItToTheCollectionAndReturnsCorectString()
        {
            var present = new Present("Valid", 15.55);
            var bag = new Bag();

            var actualString = bag.Create(present);
            var actualCount = bag.GetPresents().Count;
            var expectedCount = 1;
            var expectedString = "Successfully added present Valid.";
            

            Assert.Multiple(() =>
            {
                Assert.That(expectedCount, Is.EqualTo(actualCount));
                Assert.That(expectedString, Is.EqualTo(actualString));
            });

        }

        [Test]
        public void CreateMethodWithNullPresentThrowsExeption()
        {
            Present present = null;
            var bag = new Bag();

            Assert.Throws<ArgumentNullException>(() =>
            {
                bag.Create(present);
            }, "Present is null");
        }

        [Test]
        public void CreateMethodAddingExistingItemThrowException()
        {
            Present present = new Present("Valid", 15.55);
            var bag = new Bag();
            bag.Create(present);

            Assert.Throws<InvalidOperationException>(() =>
            {
                bag.Create(present);
            }, "This present already exists!");
        }

        [Test]
        public void GetPresentReturnsCorectExistingPresent()
        {
            Present present = new Present("Valid", 15.55);
            Present present2 = new Present("Valid2", 87);
            var bag = new Bag();
            bag.Create(present);
            bag.Create(present2);

            var expectedPesent = present;
            var actualPresent = bag.GetPresent("Valid");

            Assert.That(expectedPesent, Is.EqualTo(actualPresent));
        }

        [Test]
        public void GetPresentReturnsNullWithInexistingPresentName()
        {
            Present present = new Present("Valid", 15.55);
            Present present2 = new Present("Valid2", 87);
            var bag = new Bag();
            bag.Create(present);
            bag.Create(present2);

            Present expectedPesent = null;
            var actualPresent = bag.GetPresent("Inexisting");

            Assert.That(expectedPesent, Is.EqualTo(actualPresent));
        }

        [Test]
        public void RemoveMethodReturnsTrueRemovingExistinPresentAndDecrementsCollection()
        {
            Present present = new Present("Valid", 15.55);
            Present present2 = new Present("Valid2", 87);
            var bag = new Bag();
            bag.Create(present);
            bag.Create(present2);

            bool actualBool = bag.Remove(present);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(actualBool);
                Assert.That(1, Is.EqualTo(bag.GetPresents().Count));
            });
        }

        [Test]
        public void RemoveMethodReturnsFalseWithInexistingPresentAndDoesNotChangeTheColectionCount()
        {
            Present present = new Present("Valid", 15.55);
            Present present2 = new Present("Valid2", 87);
            var bag = new Bag();
            bag.Create(present2);

            bool actualBool = bag.Remove(present);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(actualBool);
                Assert.That(1, Is.EqualTo(bag.GetPresents().Count));
            });
        }

        [Test]
        public void GetPresentWithLeastMagicReturnsCorectPresent()
        {
            Present present = new Present("Valid", 15.55);
            Present present2 = new Present("Valid2", 87);
            Present present3 = new Present("Valid3", 15.54);
            var bag = new Bag();
            bag.Create(present);
            bag.Create(present2);
            bag.Create(present3);

            var expectedPresent = present3;
            var actualPresent = bag.GetPresentWithLeastMagic();

            Assert.That(expectedPresent, Is.EqualTo(actualPresent));
        }
    }
}
