using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;

namespace Heroes.Models
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            //string Fight(ICollection<IHero> heroes)
            // Separates all heroes into two types - knights and barbarians. The battle continues until one of the teams is completely dead (all heroes have 0 health).
            // The knights attack first and after that the barbarians. 
            // The attack happens like so: 
            // •	Each knight (if he is alive) attacks each barbarian once and inflicts damage equal to the damage of his weapon.
            // •	Next, each barbarian (if he is alive) attacks each knight and inflicts damage equal to the damage of his weapon.
            // The method returns a string with the winning team:
            // •	When knights win, print: "The knights took { number of death knights } casualties but won the battle."
            // •	When barbarians win, print: "The barbarians took { number of death barbarians } casualties but won the battle."
            List<IHero> knights = new List<IHero>();
            List<IHero> barbarians = new List<IHero>();

            foreach (var hero in players)
            {
                if (hero.GetType() == typeof(Knight))
                    knights.Add(hero);
                else
                {
                    barbarians.Add(hero);
                }
            }

            bool knightTurn = true;

            while (knights.Any(x => x.IsAlive) && barbarians.Any(x => x.IsAlive))
            {
                if (knightTurn)
                {
                    MakeTheRoundFight(knights, barbarians);
                }
                MakeTheRoundFight(barbarians, knights);
            }
            int knightAlive = knights.Where(x => !x.IsAlive).Count(); // мъртви
            int barbarianAlive = barbarians.Where(x => !x.IsAlive).Count(); // мъртви

            if (knights.Any(x => x.IsAlive))
                return string.Format(OutputMessages.MapFightKnightsWin, knightAlive);
            else

                return string.Format(OutputMessages.MapFigthBarbariansWin, barbarianAlive);
        }


        private void MakeTheRoundFight(List<IHero> attackers, List<IHero> defenders)
        {
            foreach (var attacker in attackers)
            {
                foreach (var defender in defenders)
                {
                    defender.TakeDamage(attacker.Weapon.DoDamage());
                }
            }
        }

    }
}
