using NUnit.Framework;
using System;

namespace Unit_Testing_Basics.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void LosesHealthIfAttacked()
        {
            // Arrange
            Dummy dummy = new Dummy(10, 10);

            // Act
            dummy.TakeAttack(5);

            // Assert
            Assert.That(dummy.Health, Is.EqualTo(5));
        }

        [Test]
        public void DeadDummyThrowsExeptionIfAttacked()
        {
            // Arrange
            Dummy dummy = new Dummy(-1, 10);

            // Assert
            Assert.That(() =>
            {
                // Act
                dummy.TakeAttack(10);
            }, Throws.Exception.TypeOf<InvalidOperationException>(), "Dummy is dead.");
        }

        [Test]
        public void DeadDummyCanGiveXP()
        {
            // Arrange
            Dummy dummy = new Dummy(-1, 10);

            // Act
            int expGaind = dummy.GiveExperience();

            // Assert
            Assert.That(expGaind == 10);
        }

        [Test]
        public void AliveDummyCantGiveXP()
        {
            // Arrange
            Dummy dummy = new Dummy(10, 10);

            // Assert
            Assert.That(() =>
            {
                // Act
                dummy.GiveExperience();
            }, Throws.Exception.TypeOf<InvalidOperationException>(), "Target is not dead.");
        }
    }
}