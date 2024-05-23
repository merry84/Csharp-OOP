using System;
using System.Collections.Generic;
using System.Text;
using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;

namespace Heroes.Models
{
    public abstract class Hero :IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;

        protected Hero(string name, int health, int armour)
        {
            Name = name;
            Health = health;
            Armour = armour;
        }
        public string Name
        {
            get => name;
            private set
            {
                //o	If the name is null or whitespace, throw an ArgumentException with the message: "Hero name cannot be null or empty."
                
                if (string.IsNullOrWhiteSpace(value))
                    string.Format(ExceptionMessages.HeroNameNull);
                name = value;
            }
        }

        public int Health
        {
            get=>health;
            private set
            {
                //o	If the health is below 0, throw an ArgumentException with the message: "Hero health cannot be below 0."
                if (value < 0)
                    string.Format(ExceptionMessages.HeroHealthBelowZero);
                health = value;
            }
        }

        public int Armour
        {
            get=>armour;
            private set
            {
                if (value < 0)
                    string.Format(ExceptionMessages.HeroArmourBelowZero);
                armour = value;
            }
        }

        public IWeapon Weapon
        {
            get => weapon;
            private set
            {
                //o	If the weapon is null, throw an ArgumentException with the message: "Weapon cannot be null."
                if (value == null)
                    string.Format(ExceptionMessages.WeaponNull);
                weapon = value;
               
            }
        } 
        public bool IsAlive => Health > 0;
        public void TakeDamage(int points)
        {
            //The TakeDamage() method decreases the Hero's health. First, the armour is reduced.
            //If the goes below or reaches zero set the armour to zero and transfer the damage to health points.
            //If the health points are less than or equal to zero, set the health to zero, the hero is dead. 
            if (Armour > points)
            {
                Armour -= points;
            }
            else
            {
                int transferPoints = points - Armour;
                Armour = 0;

                if (transferPoints < Health)
                {
                    Health -= transferPoints;
                }
                else
                {
                    Health = 0;
                }
            }
        }

        public void AddWeapon(IWeapon weapon)
        => Weapon = weapon;
    }
}
