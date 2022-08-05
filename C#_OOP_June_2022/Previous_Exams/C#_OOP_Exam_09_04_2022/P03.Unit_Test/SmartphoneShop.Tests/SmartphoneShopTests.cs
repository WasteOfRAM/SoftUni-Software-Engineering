using NUnit.Framework;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        [Test]
        public void ConstructorShouldInitializeEmptyListAndCapacity()
        {
            var shop = new Shop(4);

            FieldInfo collectionField = typeof(Shop).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(l => l.Name == "phones");
            var actualList = (List<Smartphone>)collectionField.GetValue(shop);
            var expectedList = new List<Smartphone>();

            Assert.AreEqual(4, shop.Capacity);
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestCase(-1)]
        [TestCase(-453)]
        public void CapacitySetterShouldThrowExceptionIfValueIsLessThanZero(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var shop = new Shop(capacity);
            }, "Invalid capacity.");
        }

        [Test]
        public void CountGetterReturnsClasCollectionCount()
        {
            var phone1 = new Smartphone("Phone1", 10);
            var phone2 = new Smartphone("Phone2", 23);
            var shop = new Shop(3);

            var expectedCollectionCount = 2;
            shop.Add(phone1);
            shop.Add(phone2);

            Assert.That(expectedCollectionCount, Is.EqualTo(shop.Count));
        }

        [Test]
        public void AddMethodAddsItemsToCollectionWithValidData()
        {
            var phone1 = new Smartphone("Phone1", 10);
            var phone2 = new Smartphone("Phone2", 23);
            var shop = new Shop(3);

            var expectedCollection = new List<Smartphone> { phone1, phone2 };
            shop.Add(phone1);
            shop.Add(phone2);

            FieldInfo collectionField = typeof(Shop).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(l => l.Name == "phones");
            var actualList = (List<Smartphone>)collectionField.GetValue(shop);

            CollectionAssert.AreEqual(expectedCollection, actualList);
        }

        [Test]
        public void AddingExistingPhoneToCollectionShouldThrowException()
        {
            var phone1 = new Smartphone("Phone1", 10);
            var phone2 = new Smartphone("Phone1", 23);
            var shop = new Shop(3);

            shop.Add(phone1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(phone2);
            }, "The phone model Phone1 already exist.");
            
        }

        [Test]
        public void AddingPhoneToShopWithFullCapacityShouldThrowException()
        {
            var phone1 = new Smartphone("Phone1", 10);
            var phone2 = new Smartphone("Phone2", 23);
            var shop = new Shop(1);

            shop.Add(phone1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(phone2);
            }, "The shop is full.");
        }

        [Test]
        public void RemovingPhoneFormShopWithValidData()
        {
            var phone1 = new Smartphone("Phone1", 10);
            var phone2 = new Smartphone("Phone2", 23);
            var shop = new Shop(3);

            shop.Add(phone1);
            shop.Add(phone2);

            shop.Remove("Phone1");

            Assert.That(1, Is.EqualTo(shop.Count));
        }

        [Test]
        public void RemovingInExistingPhoneShouldThrowException()
        {
            var phone1 = new Smartphone("Phone1", 10);
            var phone2 = new Smartphone("Phone2", 23);
            var shop = new Shop(3);

            shop.Add(phone1);
            shop.Add(phone2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Remove("Invalid");
            }, "The phone model Invalid doesn't exist.");
        }

        [Test]
        public void TestingPhoneWithValidDataShouldDecreaseCurrentBateryCharge()
        {
            var phone1 = new Smartphone("Phone1", 10);
            var shop = new Shop(3);
            shop.Add(phone1);

            shop.TestPhone("Phone1", 5);

            Assert.That(5, Is.EqualTo(phone1.CurrentBateryCharge));
        }

        [Test]
        public void TestingInExistingPhoneShouldThrowException()
        {
            var phone1 = new Smartphone("Phone1", 10);
            var phone2 = new Smartphone("Phone2", 23);
            var shop = new Shop(3);

            shop.Add(phone1);
            shop.Add(phone2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone("Invalid", 2);
            }, "The phone model Invalid doesn't exist.");
        }

        [Test]
        public void TestingPhoneWithCurrentBateryChargeLowerThanTheBatteryUsage()
        {
            var phone1 = new Smartphone("Phone1", 10);
            var phone2 = new Smartphone("Phone2", 23);
            var shop = new Shop(3);

            shop.Add(phone1);
            shop.Add(phone2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone("Phone1", 30);
            }, "The phone model Phone1 is low on batery.");
        }

        [Test]
        public void ChargingPhoneWithValidDataShouldChargeItToMax()
        {
            var phone1 = new Smartphone("Phone1", 10);
            var phone2 = new Smartphone("Phone2", 23);
            var shop = new Shop(3);

            shop.Add(phone1);
            shop.Add(phone2);

            shop.TestPhone("Phone1", 5);
            shop.ChargePhone("Phone1");

            Assert.That(10, Is.EqualTo(phone1.CurrentBateryCharge));
        }

        [Test]
        public void ChargingInExistingPhoneWhouldThrowException()
        {
            var phone1 = new Smartphone("Phone1", 10);
            var phone2 = new Smartphone("Phone2", 23);
            var shop = new Shop(3);

            shop.Add(phone1);
            shop.Add(phone2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.ChargePhone("Invalid");
            }, "The phone model Invalid doesn't exist.");
        }
    }
}