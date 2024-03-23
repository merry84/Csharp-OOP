using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private Dummy dummy;
        private Axe axe;
        private const int health = 5;
        private const int experience = 2;
        private const int attack = 3;
        private const int durability = 2;

        [SetUp]
        public void SetUp()
        {
            dummy = new Dummy(health, experience);
            axe = new Axe(attack, durability);

        }
        [Test]
        public void DummyLosesHealthIfAttacked()
        {
            var healthBefore = dummy.Health;
            dummy.TakeAttack(axe.AttackPoints);
            var healthAfter = dummy.Health;
            Assert.That(healthAfter, Is.LessThan(healthBefore));

        }
        [Test]
        public void DeadDummyThrowsAnExceptionIfAttacked()
        {
            Dummy dummy = new Dummy(0, experience);
            Assert.IsTrue(dummy.IsDead());
            Assert.Throws<InvalidOperationException> (()
                => dummy.TakeAttack(axe.AttackPoints));

        }
        [Test]
        public void DeadDummyCanGiveXP()
        {
            Dummy dummy = new Dummy(0, experience);
            Assert.IsTrue(dummy.IsDead());
            Assert.AreEqual(dummy.GiveExperience(), 2);
        }
        [Test]
        public void AliveDummyCantGiveXP()
        {
            Assert.IsFalse(dummy.IsDead()); 
            Assert.Throws<InvalidOperationException>(()=>
            dummy.GiveExperience());
        }
    }
}