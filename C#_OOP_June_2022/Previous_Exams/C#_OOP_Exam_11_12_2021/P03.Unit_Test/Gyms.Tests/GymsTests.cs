namespace Gyms.Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Reflection;
    using System;
    using System.Linq;

    public class GymsTests
    {
        // Athlete class tests

        [Test]
        public void AthleteClassConstructorTest()
        {
            var athlete = new Athlete("Test");

            var expectedFullName = "Test";
            var expectedIsInjured = false;

            Assert.AreEqual(expectedFullName, athlete.FullName);
            Assert.IsFalse(expectedIsInjured);
        }

        [Test]
        public void AthleteClassFullNamePropGetter()
        {
            var athlete = new Athlete("Test");

            var expectedFullName = "Test";
            var actualFullName = athlete.FullName;

            Assert.AreEqual(expectedFullName, actualFullName);
        }

        [Test]
        public void AthleteClassFullNamePropSetter()
        {
            var athlete = new Athlete("Test");

            var expectedFullName = "Valid";

            athlete.FullName = expectedFullName;

            var actualFullName = athlete.FullName;

            Assert.AreEqual(expectedFullName, actualFullName);
        }

        [Test]
        public void AthleteClassIsInjuredPropGetter()
        {
            var athlete = new Athlete("Test");

            var expectedIsInjured = false;
            var actualIsInjured = athlete.IsInjured;

            Assert.AreEqual(expectedIsInjured, actualIsInjured);
        }

        [Test]
        public void AthleteClassIsInjuredPropSetter()
        {
            var athlete = new Athlete("Test");

            var expectedIsInjured = true;

            athlete.IsInjured = expectedIsInjured;

            var actualIsInjured = athlete.IsInjured;

            Assert.AreEqual(expectedIsInjured, actualIsInjured);
        }

        // Gym class tests

        //[Test]
        //public void ConstructorInitializingFields()
        //{
        //    var expectedName = "Name";
        //    var expectedSize = 10;
        //    var expecredCollection = new List<Athlete>();

        //    var gym = new Gym(expectedName, expectedSize);

        //    FieldInfo collectionFiled = typeof(Gym).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(fn => fn.Name == "athletes");
        //    var actualCollection = (List<Athlete>)collectionFiled.GetValue(gym);
        //    FieldInfo nameFiled = typeof(Gym).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(fn => fn.Name == "name");
        //    var actualName = (string)nameFiled.GetValue(gym);
        //    FieldInfo sizeFiled = typeof(Gym).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(fn => fn.Name == "size");
        //    var actualSize = (int)sizeFiled.GetValue(gym);

        //    Assert.AreEqual(expectedName, actualName);
        //    Assert.AreEqual(expectedSize, actualSize);
        //    CollectionAssert.AreEqual(expecredCollection, actualCollection);
        //}

        [Test]
        public void ConstructorInitializingFields()
        {
            var expectedName = "Name";
            var expectedSize = 10;
            var expecredCollection = new List<Athlete>();

            var gym = new Gym(expectedName, expectedSize);

            FieldInfo collectionFiled = typeof(Gym).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(fn => fn.Name == "athletes");
            var actualCollection = (List<Athlete>)collectionFiled.GetValue(gym);

            Assert.AreEqual(expectedName, gym.Name);
            Assert.AreEqual(expectedSize, gym.Capacity);
            CollectionAssert.AreEqual(expecredCollection, actualCollection);
        }

        [Test]
        public void NamePropGetterReturnsCorectValue()
        {
            var expectedName = "Name";
            var expectedSize = 10;

            var gym = new Gym(expectedName, expectedSize);

            var actualName = gym.Name;

            Assert.AreEqual(expectedName, actualName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void NameSetterWithNullOrEmptyShouldthrwoException(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var gym = new Gym(name, 10);
            }, "Invalid gym name.");
        }

        [Test]
        public void CapacityGetterReturnsCorectValue()
        {
            var expectedName = "Name";
            var expectedSize = 10;

            var gym = new Gym(expectedName, expectedSize);

            var actualCapacity = gym.Capacity;

            Assert.AreEqual(expectedSize, actualCapacity);
        }

        [TestCase(-1)]
        [TestCase(-10000)]
        public void CapacitySetterShouldThrowExceptionWithNegativeValue(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var gym = new Gym("Valid", capacity);
            }, "Invalid gym capacity.");
        }

        [Test]
        public void CountShouldReturnCollectionCount()
        {
            var gym = new Gym("Valid", 10);

            var actualCount = gym.Count;

            Assert.AreEqual(0, actualCount);
        }

        [Test]
        public void AddAthleteMethodShouldIncrimentTheCount()
        {
            var gym = new Gym("Valid", 10);
            var athlete = new Athlete("Valid");

            gym.AddAthlete(athlete);

            Assert.AreEqual(1, gym.Count);
        }

        [Test]
        public void AddingAthleateWithCountEqualToSizeShouldThrowException()
        {
            var gym = new Gym("Valid", 2);
            var athlete = new Athlete("Valid");
            var athlete2 = new Athlete("Valid2");
            var athlete3 = new Athlete("Valid3");

            gym.AddAthlete(athlete);
            gym.AddAthlete(athlete2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.AddAthlete(athlete3);
            }, "The gym is full.");
        }

        [Test]
        public void RemoveAthleteWithExistingAthleteShouldDecreseTheCount()
        {
            var gym = new Gym("Valid", 3);
            var athlete = new Athlete("Valid");
            var athlete2 = new Athlete("Valid2");
            var athlete3 = new Athlete("Valid3");

            gym.AddAthlete(athlete);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);

            gym.RemoveAthlete("Valid2");

            Assert.AreEqual(2, gym.Count);
        }

        [Test]
        public void RemovingAthleteThatDoesNoeExistThrowsException()
        {
            var gym = new Gym("Valid", 3);
            var athlete = new Athlete("Valid");
            var athlete2 = new Athlete("Valid2");
            var athlete3 = new Athlete("Valid3");

            gym.AddAthlete(athlete);
            gym.AddAthlete(athlete2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.RemoveAthlete("Valid3");
            }, "The athlete Valid3 doesn't exist.");
        }

        [Test]
        public void InjureAthleteShoudSetIsInjuredToTrue()
        {
            var gym = new Gym("Valid", 3);
            var athlete = new Athlete("Valid");
            gym.AddAthlete(athlete);

            var injuredAthlete = gym.InjureAthlete("Valid");

            Assert.IsTrue(injuredAthlete.IsInjured);
        }

        [Test]
        public void InjureAthleteWithInExistingAthleteThrowsException()
        {
            var gym = new Gym("Valid", 3);
            var athlete = new Athlete("Valid");
            var athlete2 = new Athlete("Valid2");

            gym.AddAthlete(athlete);

            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.InjureAthlete("Valid2");
            }, "The athlete Valid2 doesn't exist.");
        }

        [Test]
        public void ReportReturnsCorectString()
        {
            var gym = new Gym("ValidGym", 3);
            var athlete = new Athlete("Valid");
            var athlete2 = new Athlete("Valid2");
            var athlete3 = new Athlete("Valid3");

            gym.AddAthlete(athlete);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);

            gym.InjureAthlete("Valid2");

            string expectedReport = "Active athletes at ValidGym: Valid, Valid3";
            string actualReport = gym.Report();

            Assert.AreEqual(expectedReport, actualReport);
        }
    }
}
