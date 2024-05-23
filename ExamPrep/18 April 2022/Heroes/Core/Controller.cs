using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Heroes.Core.Contracts;
using Heroes.Models;
using Heroes.Models.Contracts;
using Heroes.Repositories;
using Heroes.Utilities.Messages;

namespace Heroes.Core
{
    public class Controller : IController
    {
        //•	heroes - HeroRepository
        // •	weapons - WeaponRepository
        private readonly HeroRepository heroes;
        private readonly WeaponRepository weapons;
        private IMap map;

        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
            map = new Map();
        }
        public string CreateWeapon(string type, string name, int durability)
        {
            if (weapons.FindByName(name) != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponAlreadyExists, name));
            }

            IWeapon weapon;

            switch (type)
            {
                case "Mace":
                    weapon = new Mace(name, durability);
                    break;
                case "Claymore":
                    weapon = new Claymore(name, durability);
                    break;
                default:
                    throw new InvalidOperationException(string.Format(OutputMessages.WeaponTypeIsInvalid));
            }

            weapons.Add(weapon);
            return string.Format(OutputMessages.WeaponAddedSuccessfully, weapon.GetType().Name.ToLower(), weapon.Name);

        }

        public string CreateHero(string type, string name, int health, int armour)
        {

            if (heroes.FindByName(name) != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyExist, name));
            }

            IHero hero;

            switch (type)
            {
                case "Barbarian":
                    hero = new Barbarian(name, health, armour);
                    heroes.Add(hero);
                    return string.Format(OutputMessages.SuccessfullyAddedBarbarian, name);
                case "Knight":
                    hero = new Knight(name, health, armour);
                    heroes.Add(hero);
                    return string.Format(OutputMessages.SuccessfullyAddedKnight, name);
                default:
                    throw new InvalidOperationException(string.Format(OutputMessages.HeroTypeIsInvalid));
            }

        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {

            IHero hero = heroes.FindByName(heroName);

            if (hero is null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroDoesNotExist, heroName));
            }

            IWeapon weapon = weapons.FindByName(weaponName);

            if (weapon is null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponDoesNotExist, weaponName));
            }

            if (hero.Weapon != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyHasWeapon, heroName));
            }

            hero.AddWeapon(weapon);
            weapons.Remove(weapon);
            return string.Format(OutputMessages.WeaponAddedToHero, heroName, weapon.GetType().Name.ToLower());

        }
        // A map is created and a fight starts with all the heroes who are alive and have weapons! Returns the result from the Fight() method.
        public string StartBattle()
            => map.Fight(heroes.Models.Where(x => x.IsAlive && x.Weapon != null).ToList());

        public string HeroReport()
        {
            //Returns information about each hero separated with a new line.
            //Order them by hero type alphabetically,
            //then by health descending, then by hero name alphabetically:
            // "{ hero type }: { hero name }
            // --Health: { hero health }
            // --Armour: { hero armour }
            // --Weapon: { weapon name }/Unarmed
            // { hero type }: { hero name }
            // --Health: { hero health }
            // --Armour: { hero armour }
            // --Weapon: { weapon }/Unarmed
            // (…)"
            var sb = new StringBuilder();
            foreach (var hero in heroes.Models
                         .OrderBy(x => x.GetType().Name)
                         .ThenByDescending(x => x.Health)
                         .ThenBy(x => x.Name))

            {
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                if (hero.Weapon == null)
                {
                    sb.AppendLine("Unarmed");
                }
                else
                {
                    sb.AppendLine($"--Weapon: {hero.Weapon.Name}");
                }

            }
            return sb.ToString().TrimEnd();

        }
    }
}
