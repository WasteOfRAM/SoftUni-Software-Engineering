namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    public class RobotsTests
    {
        [Test]
        public void RobotClassCorectlySetsValuesInCOnstructor()
        {
            var expectedName = "Marvin";
            var expectedBattery = 20;
            var expectedMaxBattery = 20;

            var robot = new Robot(expectedName, expectedMaxBattery);

            Assert.Multiple(() =>
            {
                Assert.That(expectedName, Is.EqualTo(robot.Name));
                Assert.That(expectedMaxBattery, Is.EqualTo(robot.MaximumBattery));
                Assert.That(expectedBattery, Is.EqualTo(robot.Battery));
            });
        }

        // RobotManager tests

        [Test]
        public void RobotMangerConstructorInitializingWithCorectValues()
        {
            var expectedCapacity = 10;
            var expectedCount = 0;

            var robotManager = new RobotManager(expectedCapacity);

            Assert.Multiple(() =>
            {
                Assert.That(expectedCapacity, Is.EqualTo(robotManager.Capacity));
                Assert.That(expectedCount, Is.EqualTo(robotManager.Count));
            });
        }

        [Test]
        public void CapacityWhtNegativeValueShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var robotManager = new RobotManager(-1);
            }, "Invalid capacity!");
        }

        [Test]
        public void AddMethodShouldIncrimentCountWhrnAddingValidRobot()
        {
            RobotManager robotManager = new RobotManager(5);
            var robot = new Robot("Marvin", 80);

            robotManager.Add(robot);

            Assert.That(1, Is.EqualTo(robotManager.Count));
        }

        [Test]
        public void AddingRobotWithDuplicateNameShouldThrowException()
        {
            RobotManager robotManager = new RobotManager(5);
            var robot = new Robot("Marvin", 80);
            var robot2 = new Robot("Marvin", 4);

            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Add(robot2);
            }, "There is already a robot with name Marvin!");
        }

        [Test]
        public void AddingRobotOverTheCapacity()
        {
            RobotManager robotManager = new RobotManager(1);
            var robot = new Robot("Marvin", 80);
            var robot2 = new Robot("Not Marvin", 4);

            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Add(robot2);
            }, "Not enough capacity!");
        }

        [Test]
        public void RemovingRobotFromMangerShudeDecrementCount()
        {
            RobotManager robotManager = new RobotManager(5);
            var robot = new Robot("Marvin", 80);
            var robot2 = new Robot("Not Marvin", 4);
            var robot3 = new Robot("No", 14);

            robotManager.Add(robot);
            robotManager.Add(robot2);
            robotManager.Add(robot3);

            robotManager.Remove("Not Marvin");

            Assert.That(2, Is.EqualTo(robotManager.Count));
        }

        [Test]
        public void RemovingRobotThatDoesNotExistThrowsException()
        {
            RobotManager robotManager = new RobotManager(5);
            var robot = new Robot("Marvin", 80);
            var robot2 = new Robot("Not Marvin", 4);
            var robot3 = new Robot("No", 14);

            robotManager.Add(robot);
            robotManager.Add(robot2);
            robotManager.Add(robot3);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Remove("Google some robot names");
            }, "Robot with the name Google some robot names doesn't exist!");
        }

        [Test]
        public void WorkWithValidBatteryUsageShouldDecreseBattery()
        {
            var robotManager = new RobotManager(5);
            var robot = new Robot("Marvin", 80);
            var robot2 = new Robot("Not Marvin", 4);
            var robot3 = new Robot("No", 14);

            robotManager.Add(robot);
            robotManager.Add(robot2);
            robotManager.Add(robot3);

            robotManager.Work("Marvin", "BOO", 30);
            robotManager.Work("No", "???", 14);

            Assert.Multiple(() =>
            {
                Assert.That(50, Is.EqualTo(robot.Battery));
                Assert.That(0, Is.EqualTo(robot3.Battery));
                Assert.That(4, Is.EqualTo(robot2.Battery));
            });
        }

        [Test]
        public void WorkWithLowBatteryThrowsException()
        {
            var robotManager = new RobotManager(5);
            var robot = new Robot("Marvin", 80);
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Work("Marvin", ">>> mised", 81);
            }, "Marvin doesn't have enough battery!");
        }

        [Test]
        public void WorkWithNonExistingRobotThrowsException()
        {
            var robotManager = new RobotManager(5);
            var robot = new Robot("Marvin", 80);
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Work("NotValid", ">>> mised", 50);
            }, "Robot with the name NotValid doesn't exist!");
        }

        [Test]
        public void ChargingShouldSetBatteryToMax()
        {
            var robotManager = new RobotManager(5);
            var robot = new Robot("Marvin", 80);
            robotManager.Add(robot);
            robotManager.Work("Marvin", ">>> mised", 50);
            robotManager.Charge("Marvin");

            Assert.That(80, Is.EqualTo(robot.Battery));
        }

        [Test]
        public void ChargingWithNonExistingRobotThrowsException()
        {
            var robotManager = new RobotManager(5);
            var robot = new Robot("Marvin", 80);
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Charge("no");
            }, "Robot with the name no doesn't exist!");
        }
    }
}
