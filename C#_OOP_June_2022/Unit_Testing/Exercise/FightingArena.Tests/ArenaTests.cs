namespace FightingArena.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class ArenaTests
    {
        [Test]
        public void ConstructorInitializesEmptyCollectionOfWarriors()
        {
            // Arrange
            var arena = new Arena();
            var expectedCollection = new List<Warrior>();
            var actualCollection = arena.Warriors.ToList();

            CollectionAssert.AreEqual(expectedCollection, actualCollection);
        }

        [Test]
        public void TestIfEnrollMethodAddsCorrectlyUniqueWarriors()
        {
            // Arrange
            var arena = new Arena();
            var warrior = new Warrior("warror1", 50, 50);
            var warrior2 = new Warrior("warror2", 50, 50);
            var warrior3 = new Warrior("warrior3", 50, 50);

            var expectedCollection = new List<Warrior> { warrior, warrior2, warrior3 };

            // Act
            arena.Enroll(warrior);
            arena.Enroll(warrior2);
            arena.Enroll(warrior3);

            var actualCollection = arena.Warriors.ToList();

            // Assert
            CollectionAssert.AreEqual(expectedCollection, actualCollection);
        }

        [Test]
        public void TestIfCountGetterReturnsCollectionSize()
        {
            // Arrange
            var arena = new Arena();
            for (int i = 1; i <= 3; i++)
            {
                arena.Enroll(new Warrior("name" + i, 50, 50));
            }

            int expectedCount = 3;

            // Act
            int actualCount = arena.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void EnrollMethodThrowsExeptionIfWarriorEnrollsSecondTime()
        {
            // Arrange
            var arena = new Arena();
            var warrior = new Warrior("warror", 50, 50);
            var warrior2 = new Warrior("warror", 50, 50);
            arena.Enroll(warrior);

            // Act & Asset
            Assert.That(() =>
            {
                arena.Enroll(warrior2);
            }, Throws.Exception.TypeOf<InvalidOperationException>(), "Warrior is already enrolled for the fights!");
        }

        [Test]
        public void FightMethodTestIfTheFightHappend()
        {
            // Arrange
            var arena = new Arena();
            var warriorA = new Warrior("warrorA", 50, 80);
            var warriorD = new Warrior("warrorD", 45, 80);

            arena.Enroll(warriorA);
            arena.Enroll(warriorD);

            // Act
            arena.Fight("warrorA", "warrorD");

            var warriorAExpectedHp = 80 - warriorD.Damage;
            var warriorDExpectedHp = 80 - warriorA.Damage;

            // Assert
            Assert.AreEqual(warriorAExpectedHp, warriorA.HP);
            Assert.AreEqual(warriorDExpectedHp, warriorD.HP);
        }

        [Test]
        public void FightMethodIfAttackerNameIsNull()
        {
            //Arange
            var arena = new Arena();
            var warriorA = new Warrior("warrorA", 50, 80);
            var warriorD = new Warrior("warrorD", 45, 80);
            var warriorInvalid = new Warrior("warriorInvalid", 45, 80);

            arena.Enroll(warriorA);
            arena.Enroll(warriorD);

            // Act & Assert
            Assert.That(() =>
            {
                arena.Fight(warriorInvalid.Name, warriorD.Name);
            }, Throws.Exception.TypeOf<InvalidOperationException>(), $"There is no fighter with name {warriorInvalid.Name} enrolled for the fights!");
        }

        [Test]
        public void FightMethodDefenderNameIsNull()
        {
            //Arange
            var arena = new Arena();
            var warriorA = new Warrior("warrorA", 50, 80);
            var warriorD = new Warrior("warrorD", 45, 80);
            var warriorInvalid = new Warrior("warriorInvalid", 45, 80);

            arena.Enroll(warriorA);
            arena.Enroll(warriorD);

            // Act & Assert
            Assert.That(() =>
            {
                arena.Fight(warriorA.Name, warriorInvalid.Name);
            }, Throws.Exception.TypeOf<InvalidOperationException>(), $"There is no fighter with name {warriorInvalid.Name} enrolled for the fights!");
        }
    }
}
