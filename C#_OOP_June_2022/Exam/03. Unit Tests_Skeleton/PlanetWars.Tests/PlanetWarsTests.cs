using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            // Weapon clas tests
            [Test]
            public void ConstructorInitializesPropertiesWithCorectValues()
            {
                var weapon = new Weapon("Valid", 4.43, 4);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("Valid", weapon.Name);
                    Assert.AreEqual(4.43, weapon.Price);
                    Assert.AreEqual(4, weapon.DestructionLevel);
                });
            }

            [Test]
            public void NamePropertyGetter()
            {
                var expectedName = "Valid";
                var weapon = new Weapon(expectedName, 4.43, 4);

                var acturlaName = weapon.Name;

                Assert.AreEqual(expectedName, acturlaName);
            }

            [Test]
            public void NamePropertySetter()
            {
                var weapon = new Weapon("Valid", 4.43, 4);
                var expectedName = "AlsoValid";

                weapon.Name = expectedName;

                Assert.AreEqual(expectedName, weapon.Name);
            }

            [Test]
            public void PricePropertyGetter()
            {
                var expectedPrice = 4.85;
                var weapon = new Weapon("Valid", expectedPrice, 4);

                var acturlaPrice = weapon.Price;

                Assert.AreEqual(expectedPrice, acturlaPrice);
            }

            [Test]
            public void PriceSetterWithValidValue()
            {
                var expectedPrice = 4.85;
                var weapon = new Weapon("Valid", 55.5, 4);

                weapon.Price = expectedPrice;
                var acturlaPrice = weapon.Price;

                Assert.AreEqual(expectedPrice, acturlaPrice);
            }

            [TestCase(-1)]
            [TestCase(-1394.34)]
            public void PriceSetterWithInvalidValueThrowsException(double price)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    var weapon = new Weapon("Valid", price, 4);
                }, "Price cannot be negative.");
            }

            [Test]
            public void DestructionLevelGetter()
            {
                var expectedDestructionLevel = 4;
                var weapon = new Weapon("Valid", 88.25, expectedDestructionLevel);

                var destructionLevel = weapon.DestructionLevel;

                Assert.AreEqual(expectedDestructionLevel, destructionLevel);
            }

            [Test]
            public void DestructionLevelSetter()
            {
                var expectedDestructionLevel = 7;
                var weapon = new Weapon("Valid", 55.5, 4);

                weapon.DestructionLevel = expectedDestructionLevel;
                var acturlaDestructionLevel = weapon.DestructionLevel;

                Assert.AreEqual(expectedDestructionLevel, acturlaDestructionLevel);
            }

            [Test]
            public void IncreaseDestructionLevelMethodIncrimentsDestructionLevel()
            {
                var weapon = new Weapon("Valid", 55.5, 4);

                weapon.IncreaseDestructionLevel();

                Assert.AreEqual(5, weapon.DestructionLevel);
            }

            [Test]
            public void IsNuclearFalseTest()
            {
                var weapon = new Weapon("Valid", 55.5, 4);

                Assert.IsFalse(weapon.IsNuclear);
            }

            [Test]
            public void IsNuclearTrueTest()
            {
                var weapon = new Weapon("Valid", 55.5, 10);

                Assert.IsTrue(weapon.IsNuclear);
            }

            // Planet class tests

            [Test]
            public void ConstructorSetsCorectValues()
            {
                var planet = new Planet("Valid", 45.5);
                var expectedColection = new List<Weapon>();

                Assert.Multiple(() =>
                {
                    Assert.AreEqual("Valid", planet.Name);
                    Assert.AreEqual(45.5, planet.Budget);
                    CollectionAssert.AreEqual(expectedColection, planet.Weapons);
                });
            }

            [TestCase(null)]
            [TestCase("")]
            public void NameSetterWithNullOrEnmptyThrowsExeption(string name)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    var planet = new Planet(name, 45.5);
                }, "Invalid planet Name");
            }

            [TestCase(-1)]
            [TestCase(-2424.2)]
            public void BudgetSetterWithNegativeValueThrowsException(double budget)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    var planet = new Planet("Valid", budget);
                }, "Budget cannot drop below Zero!");
            }

            [Test]
            public void MilitaryPowerRatio()
            {
                var expectedPower = 11;
                var planet = new Planet("Valid", 1000);
                var weapontOne = new Weapon("WeaponOne", 50, 5);
                var weapontTwo = new Weapon("WeaponTwo", 50, 6);

                planet.AddWeapon(weapontTwo);
                planet.AddWeapon(weapontOne);

                var actualPower = planet.MilitaryPowerRatio;

                Assert.AreEqual(expectedPower, actualPower);
            }

            [Test]
            public void ProfitMethodIncrecesBudget()
            {
                var expectedBudget = 1010.3;
                var planet = new Planet("Valid", 1000);

                planet.Profit(10.3);

                Assert.AreEqual(expectedBudget, planet.Budget);
            }

            [Test]
            public void SpentMethodDecresesBudget()
            {
                var expectedBudget = 1000;
                var planet = new Planet("Valid", 1010.3);

                planet.SpendFunds(10.3);

                Assert.AreEqual(expectedBudget, planet.Budget);
            }

            [Test]
            public void SpendMethodWithAmounthBigerThanBudgetThrowsException()
            {
                var planet = new Planet("Valid", 2);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.SpendFunds(10.3);
                }, "Not enough funds to finalize the deal.");
            }

            [Test]
            public void AddWeaponWithExistingNameThrowsException()
            {
                var planet = new Planet("Valid", 1000);
                var weapontOne = new Weapon("Weapon", 50, 5);
                var weapontTwo = new Weapon("Weapon", 50, 6);

                planet.AddWeapon(weapontOne);
                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.AddWeapon(weapontTwo);
                }, $"There is already a {weapontTwo.Name} weapon.");
            }

            [Test]
            public void RemoveWeaponByName()
            {
                var planet = new Planet("Valid", 1000);
                var weapontOne = new Weapon("WeaponOne", 50, 5);
                var weapontTwo = new Weapon("WeaponTwo", 50, 6);

                planet.AddWeapon(weapontOne);
                planet.AddWeapon(weapontTwo);

                planet.RemoveWeapon("WeaponOne");

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(1, planet.Weapons.Count);
                    Assert.IsFalse(planet.Weapons.Any(p => p.Name == "WeaponOne"));
                });

            }

            [Test]
            public void UpgradeWeaponWithExistingWeapon()
            {
                var planet = new Planet("Valid", 1000);
                var weapontOne = new Weapon("WeaponOne", 50, 5);
                var weapontTwo = new Weapon("WeaponTwo", 50, 6);

                planet.AddWeapon(weapontOne);
                planet.AddWeapon(weapontTwo);

                planet.UpgradeWeapon("WeaponTwo");

                Assert.AreEqual(7, weapontTwo.DestructionLevel);
            }

            [Test]
            public void UpgradeWeaponWithInExistingWeapon()
            {
                var planet = new Planet("Valid", 1000);
                var weapontOne = new Weapon("WeaponOne", 50, 5);
                var weapontTwo = new Weapon("WeaponTwo", 50, 6);

                planet.AddWeapon(weapontOne);
                planet.AddWeapon(weapontTwo);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.UpgradeWeapon("InvalidWeapon");
                },$"InvalidWeapon does not exist in the weapon repository of Valid");
            }

            [Test]
            public void DestroyOponentWithSmalerPowerRatio()
            {
                var planetOne = new Planet("Attaker", 1000);
                var planetTwo = new Planet("Defender", 1);

                var weapontOne = new Weapon("WeaponOne", 50, 5);
                var weapontTwo = new Weapon("WeaponTwo", 50, 6);

                planetOne.AddWeapon(weapontOne);
                planetOne.AddWeapon(weapontTwo);
                var expectedResult = $"{planetTwo.Name} is destructed!";
                var actualResult = planetOne.DestructOpponent(planetTwo);

                Assert.AreEqual(expectedResult, actualResult);
            }

            [TestCase(5)]
            [TestCase(7)]
            public void DestructOpponentWithEqualOrHigerPower(int weaponPower)
            {
                var planetOne = new Planet("Attaker", 1000);
                var planetTwo = new Planet("Defender", 1000);

                var weapontOne = new Weapon("WeaponOne", 50, 5);
                var weapontTwo = new Weapon("WeaponTwo", 50, weaponPower);

                planetOne.AddWeapon(weapontOne);
                planetTwo.AddWeapon(weapontTwo);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    planetOne.DestructOpponent(planetTwo);
                }, $"{planetTwo.Name} is too strong to declare war to!");
            }
        }
    }
}
