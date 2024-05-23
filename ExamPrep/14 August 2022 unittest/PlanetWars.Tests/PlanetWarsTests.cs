using NUnit.Framework;
using System;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            
        }

        [Test]
        public void PlanetConstructorWorkCorrectly()
        {
            Planet planet = new Planet("Mars", 15);
            Assert.AreEqual(planet.Budget,15);
            Assert.AreEqual(planet.Name,"Mars");
            Assert.AreEqual(planet.Weapons.Count,0);
        }

        [Test]
        public void PlanetNameThrowIsNullOrEmpty()
        {
            Assert.Throws<ArgumentException>(() => new Planet("", 15));
            Assert.Throws<ArgumentException>(() => new Planet(null, 15));
        }
        [Test]
        public void PlanetBudgetThrowBelowZero()
        {
            Assert.Throws<ArgumentException>(() => new Planet("Mars", -6));
            Assert.Throws<ArgumentException>(() => new Planet("mars", -1));
        }

        [Test]
        public void ProfitWorkCorrectly()
        {
            Planet planet = new Planet("Mars", 15);
           planet.Profit(45);
            Assert.AreEqual(planet.Budget,60);
        }

        [Test]
        public void SpendFundsWorkCorrectly()
        {
            Planet planet = new Planet("Mars", 15);
            planet.SpendFunds(5);
            Assert.AreEqual(planet.Budget,10);
        }

        [Test]
        public void SpendFundsThrowException()
        {
            Planet planet = new Planet("Mars", 15);
            
            Assert.Throws<InvalidOperationException>(()=> planet.SpendFunds(20));
        }

        [Test]
        public void WeaponWorkCorrectly()
        {
           
            Weapon weapon = new Weapon("kalin", 12.00, 5);
           Assert.AreEqual(weapon.Name,"kalin");
           Assert.AreEqual(weapon.DestructionLevel,5);
           Assert.AreEqual(weapon.Price,12.00);

        }
        [Test]
        public void AddWeaponWorkCorrectly()
        {
            Weapon weapon = new Weapon("kalin", 12.00, 5);
            Planet planet = new Planet("Mars", 15);
            planet.AddWeapon(weapon);
            Assert.AreEqual(planet.Weapons.Count,1);

        }
        [Test]
        public void AddWeaponThrowException()
        {
            Weapon weapon = new Weapon("kalin", 12.00, 5);
            Planet planet = new Planet("Mars", 15);
            planet.AddWeapon(weapon);
            Assert.Throws<InvalidOperationException>(() => planet.AddWeapon(weapon));
        }

        [Test]
        public void RemoveWeaponWorkCorrectly()
        {
            Weapon weapon = new Weapon("kalin", 12.00, 5);
            Planet planet = new Planet("Mars", 15);
            planet.AddWeapon(weapon);
            planet.RemoveWeapon(weapon.Name);
            Assert.AreEqual(planet.Weapons.Count, 0);

        }

        [Test]
        public void UpgradeWeaponWorkCorrectly()
        {
            Weapon weapon = new Weapon("kalin", 12.00, 5);
            Planet planet = new Planet("Mars", 15);
            planet.AddWeapon(weapon);
            planet.UpgradeWeapon(weapon.Name);
            Assert.AreEqual(planet.MilitaryPowerRatio,6);
        }
        [Test]
        public void UpgradeWeaponThrowException()
        {
            
            Planet planet = new Planet("Mars", 15);
            Assert.Throws<InvalidOperationException>(() => planet.UpgradeWeapon("momo"));
        }

        [Test]
        public void DestructOpponentWorkCorrectly()
        {
            Planet planet = new Planet("koko", 15);
            Planet planet1 = new Planet("polo", 17);

            Weapon weapon = new Weapon("mimi", 10.00, 6);
            Weapon weapon1 = new Weapon("gogo", 12.00, 8);
            Weapon weapon2 = new Weapon("toto", 14.00, 9);
            planet.AddWeapon(weapon);
            planet1.AddWeapon(weapon1);
            planet.AddWeapon(weapon2);
            Assert.AreEqual(planet.DestructOpponent(planet1),"polo is destructed!");
        }
        [Test]
        public void DestructOpponentThrowException()
        {
            Planet planet = new Planet("koko", 15);
            Planet planet1 = new Planet("polo", 17);

            Weapon weapon = new Weapon("mimi", 10.00, 2);
            Weapon weapon1 = new Weapon("gogo", 12.00, 8);
            Weapon weapon2 = new Weapon("toto", 14.00, 3);
            planet.AddWeapon(weapon);
            planet1.AddWeapon(weapon1);
            planet.AddWeapon(weapon2);
            Assert.Throws<InvalidOperationException>(()=> planet.DestructOpponent(planet1));
        }
        [Test]
        public void WeaponPriceIsNotNegativeNumber()
        {
            Assert.Throws<ArgumentException>(() => new Weapon("Mars", -6,4));
            Assert.Throws<ArgumentException>(() => new Weapon("mars", -1, 4));
        }
        [Test]
        public void Weapon_IsNuclear_WorksProperly()
        {
            var weaponNuclear = new Weapon("Nuclear", 1500, 11);
            var weaponGun = new Weapon("Gun", 20, 2);

            Assert.IsTrue(weaponNuclear.IsNuclear);
            Assert.IsFalse(weaponGun.IsNuclear);
        }

        [Test]
        public void IncreaseDestructionLevelWorkCorrectly()
        {
            Weapon weapon = new Weapon("koko", 120.00, 45);
            Planet planet = new Planet("polpi", 60);
            planet.AddWeapon(weapon);
            weapon.IncreaseDestructionLevel();
            Assert.AreEqual(weapon.DestructionLevel,46);
        }

    }
}
