namespace CarManager.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;

    using NUnit.Framework;

    [TestFixture]
    public class CarManagerTests
    {
        private Car defaultCar;

        [SetUp]
        public void SetUp()
        {
            string expectedMake = "Honda";
            string expectedModel = "Civic";
            double expectedFuelConsumption = 1.7;
            double expectedFuelCapacity = 50;

            defaultCar = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
        }

        [Test]
        public void BothConstuctorsTestAndSettersWithValidData()
        {
            //Arrange
            string expectedMake = "Honda";
            string expectedModel = "Civic";
            double expectedFuelConsumption = 1.7;
            double expectedFuelAmount = 0;
            double expectedFuelCapacity = 50;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
            //Assert.IsNotNull(car);
            //Assert.AreEqual(expectedMake, car.Make);
            //Assert.AreEqual(expectedModel, car.Model);
            //Assert.AreEqual(expectedFuelConsumption, car.FuelConsumption);
            //Assert.AreEqual(expectedFuelCapacity, car.FuelCapacity);
            //Assert.AreEqual(expectedFuelAmount, car.FuelAmount);

            FieldInfo makeField = this.GetField("make");
            string actualMake = (string)makeField.GetValue(car);

            FieldInfo modelField = this.GetField("model");
            string actualModel = (string)modelField.GetValue(car);

            FieldInfo consumptionField = this.GetField("fuelConsumption");
            double actualConsumption = (double)consumptionField.GetValue(car);

            FieldInfo fuelAmountField = this.GetField("fuelAmount");
            double actualFuelAmount = (double)fuelAmountField.GetValue(car);

            FieldInfo fuelCapacityField = this.GetField("fuelCapacity");
            double actualFuelCapacity = (double)fuelCapacityField.GetValue(car);

            // Assert

            Assert.AreEqual(expectedMake, actualMake, "Constructor should initialize the make of the Car!");
            Assert.AreEqual(expectedModel, actualModel, "Constructor should initialize the model of the Car!");
            Assert.AreEqual(expectedFuelConsumption, actualConsumption, "Constructor should initialize the fuel consumption of the Car!");
            Assert.AreEqual(expectedFuelAmount, actualFuelAmount, "Constructor should initialize the fuel amount of the Car!");
            Assert.AreEqual(expectedFuelCapacity, actualFuelCapacity, "Constructor should initialize the fuel capacity of the Car!");
        }

        [Test]
        public void TestMakeGetter()
        {
            // Arrange
            string expectedMake = "Honda";

            // Act
            string actualMake = defaultCar.Make;

            // Assert
            Assert.AreEqual(expectedMake, actualMake);
        }

        [TestCase(null)]
        [TestCase("")]
        public void TestMakeSetterValidation(string make)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var car = new Car(make, "Civic", 1.7, 50);
            }, "Make cannot be null or empty!");
        }

        [Test]
        public void TestModelGetter()
        {
            // Arrange
            string expectedModel = "Civic";

            // Act
            string actualModel = defaultCar.Model;

            // Assert
            Assert.AreEqual(expectedModel, actualModel);
        }

        [TestCase(null)]
        [TestCase("")]
        public void TestModelSetterValidation(string model)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var car = new Car("Honda", model, 1.7, 50);
            }, "Model cannot be null or empty!");
        }

        [Test]
        public void TestFuelConsumptionGetter()
        {
            // Arrange
            double expectedFuelConsumption = 1.7;

            // Act
            double actualFuelConsumption = defaultCar.FuelConsumption;

            // Assert
            Assert.AreEqual(expectedFuelConsumption, actualFuelConsumption);
        }

        [TestCase(0)]
        [TestCase(-17)]
        public void TestFuelConsumptionSetterValidation(double fuelConsumption)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var car = new Car("Honda", "Civic", fuelConsumption, 50);
            }, "Fuel consumption cannot be zero or negative!");
        }

        [Test]
        public void TestFuelAmountGetter()
        {
            // Arrange
            double expectedFuelAmount = 0;

            // Act
            double actualFuelAmount = defaultCar.FuelAmount;

            // Assert
            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }

        [TestCase(-1)]
        [TestCase(-17)]
        public void TestFuelAmountSetterValidation(double fuelAmount)
        {
            FieldInfo fuelCapacity = this.GetField("fuelCapacity");
            fuelCapacity.SetValue(defaultCar, fuelAmount);

            Assert.Throws<ArgumentException>(() =>
            {
                defaultCar.Refuel(5);
            }, "Fuel amount cannot be negative!");
        }

        [Test]
        public void TestFuelCapacityGetter()
        {
            // Arrange
            double expectedFuelCapacity = 50;

            // Act
            double actualFuelAmount = defaultCar.FuelCapacity;

            // Assert
            Assert.AreEqual(expectedFuelCapacity, actualFuelAmount);
        }

        [TestCase(0)]
        [TestCase(-17)]
        public void TestFuelCapacitySetterValidation(double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var car = new Car("Honda", "Civic", 1.7, fuelCapacity);
            }, "Fuel capacity cannot be zero or negative!");
        }

        [TestCase(1)]
        [TestCase(49)]
        [TestCase(50)]
        public void RefuleMethodWithValidFuelUnderFuelCapacity(double fuelToRefule)
        {
            // Act
            defaultCar.Refuel(fuelToRefule);

            double expectedFuelAmount = fuelToRefule;
            double actualFuelAmount = defaultCar.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }

        [TestCase(51)]
        [TestCase(300)]
        public void RefuleMethodWithValidFuelOverFuelCapacity(double fuelToRefule)
        {
            // Act
            defaultCar.Refuel(fuelToRefule);

            double expectedFuelAmount = defaultCar.FuelCapacity;
            double actualFuelAmount = defaultCar.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }

        [TestCase(0)]
        [TestCase(-15)]
        public void RefuleMethodWithZeroOrNegativeFuel(double fuelToRefule)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                defaultCar.Refuel(fuelToRefule);
            }, "Fuel amount cannot be zero or negative!");
        }

        [TestCase(50, 50)]
        [TestCase(0.85, 50)]
        public void DriveMethodWithFuelThatIsEnoughForTheDistance(double fuelAmount, double distance)
        {
            defaultCar.Refuel(fuelAmount);

            double fuelNeeded = (distance / 100) * defaultCar.FuelConsumption;

            double expectedFuelAmount = defaultCar.FuelAmount - fuelNeeded;

            defaultCar.Drive(distance);

            double actualFuelamount = defaultCar.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelamount);
        }

        [TestCase(20, 1000000)]
        [TestCase(0.84, 50)]
        public void DriveMethodWithFuelThatIsNotEnoughForTheDistance(double fuelAmount, double distance)
        {
            defaultCar.Refuel(fuelAmount);

            Assert.Throws<InvalidOperationException>(() =>
            {
                defaultCar.Drive(distance);
            }, "You don't have enough fuel to drive!");
        }


        private FieldInfo GetField(string fieldName)
            => typeof(Car)
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
            .FirstOrDefault(fi => fi.Name == fieldName);
    }
}