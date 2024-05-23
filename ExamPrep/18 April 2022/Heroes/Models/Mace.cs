using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models
{
    public class Mace :Weapon
    {
        private const int damage = 25;
        public Mace(string name, int durability) : base(name, durability)
        {
        }

        public override int DoDamage()
        {
            Durability--;
            if (Durability == 0)
            {
                return 0;
            }
            
            return damage;
            // The DoDamage() method returns the damage of each weapon:
            //            // •	Mace - 25 damage
            //            // •	Claymore - 20 damage
            //            // Whenever the DoDamage method is invoked, the weapon's durability is decreased by 1. If the weapon's durability becomes 0, the method should return 0.
        }
    }
}
