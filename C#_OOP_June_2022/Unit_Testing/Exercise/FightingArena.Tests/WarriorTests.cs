namespace FightingArena.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using NUnit.Framework;

    [TestFixture]
    public class WarriorTests
    {
        [Test]
        public void ConstructorIsWorking()
        {
            // Arrange
            string expectedName = "Anbatoli";
            int expextedDamage = 50;
            int expectedHp = 100;

            // Act
            var warrior = new Warrior(expectedName, expextedDamage, expectedHp);

            // Assert
            //Assert.AreEqual(expectedName, warior.Name);
            //Assert.AreEqual(expextedDamage, warior.Damage);
            //Assert.AreEqual(expectedHp, warior.HP);

            FieldInfo nameField = this.GetField("name");
            string actualName = (string)nameField.GetValue(warrior);

            FieldInfo damageField = this.GetField("damage");
            int actualDamage = (int)damageField.GetValue(warrior);

            FieldInfo hpField = this.GetField("hp");
            int actualHp = (int)hpField.GetValue(warrior);

            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expextedDamage, actualDamage);
            Assert.AreEqual(expectedHp, actualHp);
        }

        [Test]
        public void TestNameGetter()
        {
            // Arrange
            string expectedName = "Anbatoli";
            int expextedDamage = 50;
            int expectedHp = 100;
            var warrior = new Warrior(expectedName, expextedDamage, expectedHp);

            // Act
            var actualName = warrior.Name;

            // Assert
            Assert.AreEqual(expectedName, actualName);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        public void TestWarriorNameSetterWithInvalidInput(string invalidName)
        {
            // Act & Assert
            Assert.That(() =>
            {
                var warior = new Warrior(invalidName, 10, 10);
            }, Throws.Exception.TypeOf<ArgumentException>(), "Name should not be empty or whitespace!");
        }

        [Test]
        public void TestWarriorDamageGetter()
        {
            // Arrange
            string expectedName = "Anbatoli";
            int expextedDamage = 50;
            int expectedHp = 100;
            var warrior = new Warrior(expectedName, expextedDamage, expectedHp);

            // Act
            var actualDamage = warrior.Damage;

            // Assert
            Assert.AreEqual(expextedDamage, actualDamage);
        }

        [TestCase(0)]
        [TestCase(-14)]
        public void TestWarriorDamageSetterWithNegativeNumberAndZero(int invalidDamage)
        {
            // Act & Assert
            Assert.That(() =>
            {
                var warior = new Warrior("Anbatoli", invalidDamage, 10);
            }, Throws.Exception.TypeOf<ArgumentException>(), "Damage value should be positive!");
        }

        [Test]
        public void TestWarriorHPGetter()
        {
            // Arrange
            string expectedName = "Anbatoli";
            int expextedDamage = 50;
            int expectedHp = 100;
            var warrior = new Warrior(expectedName, expextedDamage, expectedHp);

            // Act
            var actualHP = warrior.HP;

            // Assert
            Assert.AreEqual(expectedHp, actualHP);
        }

        [Test]
        public void TestWarriorHPSetterWithNegativeValue()
        {
            // Act & Assert
            Assert.That(() =>
            {
                var warior = new Warrior("Anbatoli", 10, -14);
            }, Throws.Exception.TypeOf<ArgumentException>(), "HP should not be negative!");
        }

        [TestCase(30)]
        [TestCase(1)]
        public void TestAttackWithHPBelowMinMinAttackHP(int attackingWariorHP)
        {
            // Arrange
            var attacingWarior = new Warrior("name", 50, attackingWariorHP);
            var defendingWarrior = new Warrior("name?", 1, 50);

            // Act & Assert
            Assert.That(() =>
            {
                attacingWarior.Attack(defendingWarrior);
            }, Throws.Exception.TypeOf<InvalidOperationException>(), "Your HP is too low in order to attack other warriors!");
        }

        [TestCase(30)]
        [TestCase(1)]
        public void TestAttackingAnEnemyWithHPBelowMinAttackHP(int deffendingWarriorHP)
        {
            // Arrange
            var attacingWarior = new Warrior("name", 50, 50);
            var defendingWarrior = new Warrior("name?", 1, deffendingWarriorHP);

            FieldInfo minAttackHPField = typeof(Warrior).GetFields(BindingFlags.Static | BindingFlags.NonPublic).First(field => field.Name == "MIN_ATTACK_HP");
            var minAttackHP = (int)minAttackHPField.GetValue(null);

            // Act & Assert
            Assert.That(() =>
            {
                attacingWarior.Attack(defendingWarrior);
            }, Throws.Exception.TypeOf<InvalidOperationException>(), $"Enemy HP must be greater than {minAttackHP} in order to attack him!");
        }

        [Test]
        public void TestAttackingAndDefendingWarriorWithDamageBigerThanAttackingWarriorHP()
        {
            // Arrange
            var attacingWarior = new Warrior("name", 50, 35);
            var defendingWarrior = new Warrior("name?", 40, 50);

            // Act & Assert
            Assert.That(() =>
            {
                attacingWarior.Attack(defendingWarrior);
            }, Throws.Exception.TypeOf<InvalidOperationException>(), $"You are trying to attack too strong enemy");
        }

        [Test]
        public void TestIfAttackingWarriorHPGoesDownOnAttack()
        {
            // Arrange
            var attacingWarior = new Warrior("name", 50, 55);
            var defendingWarrior = new Warrior("name?", 50, 50);

            var expectedHP = attacingWarior.HP - defendingWarrior.Damage;

            // Act
            attacingWarior.Attack(defendingWarrior);

            // Assert
            Assert.AreEqual(expectedHP, attacingWarior.HP);
        }

        [Test]
        public void TestIfDefendingWarriorHpIsSetToZeroIfKilled()
        {
            // Arrange
            var attacingWarior = new Warrior("name", 50, 55);
            var defendingWarrior = new Warrior("name?", 45, 45);

            // Act
            attacingWarior.Attack(defendingWarrior);

            // Assert
            Assert.AreEqual(0, defendingWarrior.HP);
        }

        private FieldInfo GetField(string fieldName) 
            => typeof(Warrior)
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
            .First(field => field.Name == fieldName);
    }
}