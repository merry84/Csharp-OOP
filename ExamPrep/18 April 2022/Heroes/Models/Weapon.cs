using System;
using System.Collections.Generic;
using System.Text;
using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;

namespace Heroes.Models
{
    public abstract class Weapon :IWeapon
    {
        private string name;
        private int durability;

        protected Weapon(string name,int durability)
        {
            Name = name;
            Durability = durability;
        }

        public string Name
        {
            get=> name;
            private set
            {
                //o	If the name is null or whitespace, throw an ArgumentException with the message: "Weapon type cannot be null or empty."
                // o	All names are unique.
                if (string.IsNullOrWhiteSpace(value))
                    string.Format(ExceptionMessages.WeaponTypeNull);
                name = value;
            }
        }

        public int Durability
        {
            get=> durability;
            protected set
            {
                //o	If the durability is below 0, throw an ArgumentException with the message: "Durability cannot be below 0."
                if (value < 0)
                    string.Format(ExceptionMessages.DurabilityBelowZero);
                durability = value;
            }
        }

        public abstract int DoDamage();
        
            //abstract int DoDamage()
           
    }
}
