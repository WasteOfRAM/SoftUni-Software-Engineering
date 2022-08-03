using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            [Test]
            public void ConstructorTest()
            {
                string expectedName = "booo";
                int expectedMechanics = 3;
                List<Car> expectedList = new List<Car>();

                Garage garage = new Garage(expectedName, expectedMechanics);
                FieldInfo listField = typeof(Garage).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(fn => fn.Name == "cars");

                Assert.AreEqual(expectedName, garage.Name);
                Assert.AreEqual(expectedMechanics, garage.MechanicsAvailable);
                CollectionAssert.AreEqual(expectedList, (List<Car>)listField.GetValue(garage));
            }

            [TestCase("")]
            [TestCase(null)]
            public void NameSetterValidationTest(string name)
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    var garage = new Garage(name, 5);
                }, nameof(name), "Invalid garage name.");
            }

            [TestCase(0)]
            [TestCase(-13)]
            public void MechanicsAvailableSetterValidationTest(int mechanics)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    var garage = new Garage("Valid", mechanics);
                }, "At least one mechanic must work in the garage.");
            }

            [Test]
            public void CarsInGarageCountGetterTest()
            {
                var garage = new Garage("Valid", 1);

                Assert.AreEqual(0, garage.CarsInGarage);
            }

            [Test]
            public void AddingACarWithAvailableMechanics()
            {
                var garage = new Garage("Valid", 1);

                garage.AddCar(new Car("Model", 3));

                Assert.AreEqual(1, garage.CarsInGarage);
            }

            [Test]
            public void AddingACarWithNoAvailableMechanics()
            {
                var garage = new Garage("Valid", 1);

                garage.AddCar(new Car("Model", 3));

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.AddCar(new Car("Model2", 4));
                }, "No mechanic available.");
            }

            [Test]
            public void FixingCarWithExistingCarShouldReturnACarWithZeroNumberOfIssues()
            {
                var garage = new Garage("Valid", 7);

                var expectedCar = new Car("Model", 0);

                garage.AddCar(new Car("Model", 3));

                var actualCar = garage.FixCar("Model");

                Assert.AreEqual(expectedCar.NumberOfIssues, actualCar.NumberOfIssues);
            }

            [Test]
            public void FixingCarWithInExistingModelWhouldThrowExeption()
            {
                var garage = new Garage("Valid", 7);
                garage.AddCar(new Car("Model", 3));

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.FixCar("Modell");
                }, "The car Modell doesn't exist.");
            }

            [Test]
            public void RemoveFixedCarsWithCarsInTheGarage()
            {
                var garage = new Garage("Valid", 7);

                garage.AddCar(new Car("Model", 3));
                garage.AddCar(new Car("Model2", 33));
                garage.AddCar(new Car("Model3", 233));

                garage.FixCar("Model");
                garage.FixCar("Model2");

                garage.RemoveFixedCar();

                Assert.AreEqual(1, garage.CarsInGarage);
            }

            [Test]
            public void RemoveFixetCarWithNoFixedCarsShouldThrowExeption()
            {
                var garage = new Garage("Valid", 7);

                garage.AddCar(new Car("Model", 3));
                garage.AddCar(new Car("Model2", 33));
                garage.AddCar(new Car("Model3", 233));

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.RemoveFixedCar();
                },"No fixed cars available.");
            }

            [Test]
            public void ReportCarsThatAreNotFixed()
            {
                var garage = new Garage("Valid", 7);

                garage.AddCar(new Car("Model", 3));
                garage.AddCar(new Car("Model2", 33));
                garage.AddCar(new Car("Model3", 233));

                garage.FixCar("Model3");

                string expectedMsg = $"There are {2} which are not fixed: Model, Model2.";
                string actualMsg = garage.Report();

                Assert.AreEqual(expectedMsg, actualMsg);
            }
        }
    }
}