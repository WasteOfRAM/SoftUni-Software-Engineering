using NUnit.Framework;
using System;

namespace Unit_Testing_Basics.Tests
{
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void LosesDurabilityAfterAttack()
        {
            // Arrange
            Axe axe = new Axe(10, 10);
            Dummy dummy = new Dummy(10, 10);

            // Act
            axe.Attack(dummy);

            // Assert
            Assert.That(axe.DurabilityPoints, Is.EqualTo(9), "Axe Durability doesn't change after attack");
        }

        [Test]
        public void AttackingWithBrokenWeapon()
        {
            // Arrange
            Axe axe = new Axe(10, 0);
            Dummy dummy = new Dummy(10, 10);

            // Assert
            Assert.That(() =>
            {
                // Act
                axe.Attack(dummy);
            }, Throws.Exception.TypeOf<InvalidOperationException>(), "Axe is broken.");

        }
    }
}