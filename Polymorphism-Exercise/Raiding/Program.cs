using Raiding.Models;

namespace Raiding
{
    public class Program
    {
        static void Main(string[] args)
        {
           List<BaseHero> list = new List<BaseHero>();
            int n = int.Parse(Console.ReadLine());
            while (list.Count < n)
            {
                string nameHero = Console.ReadLine();
                string typeHero = Console.ReadLine();
                if (typeHero == "Druid")
                {
                    Druid druid = new Druid(nameHero);
                    list.Add(druid);
                }
                else if (typeHero == "Paladin")
                {
                    Paladin paladin = new Paladin(nameHero);
                    list.Add(paladin);
                }
                else if (typeHero == "Rogue")
                {
                    Rogue rogue = new Rogue(nameHero);
                    list.Add(rogue);
                }
                else if (typeHero == "Warrior")
                {
                    Warrior warrior = new Warrior(nameHero);
                    list.Add(warrior);
                }
                else { Console.WriteLine("Invalid hero!"); }
            }
            int powerOfBoss = int.Parse(Console.ReadLine());
            //each of the heroes in the raid group should cast his ability once
            foreach (var hero in list)
            {
                Console.WriteLine(hero.CastAbility());
            }
            //You should sum the power of all of the heroes and if the total power is greater or equal to the boss’s power you have defeated him and you should print:
            //"Victory!"
            //Else print:
            //"Defeat..."
            int sumPowerAllHero = list.Sum(s=> s.Power);
            if(sumPowerAllHero >= powerOfBoss)
            {
                Console.WriteLine("Victory!");
            }
            else { Console.WriteLine("Defeat..."); }

        }
    }
}
