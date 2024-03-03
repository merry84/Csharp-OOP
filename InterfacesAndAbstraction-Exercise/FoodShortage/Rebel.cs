using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShortage
{
    public class Rebel : IBuyer
    {//a Rebel buys food his Food increases by 5
        private const int foodIncreases = 5;
        public Rebel(string name, int age, string group)
        {
            Name = name;
            Age = age;
            Group = group;
        }

        public string Name { get; }
        public int Age { get; }
        public int Food { get; private set; }
        public string Group { get; }

       public void BuyFood() => Food += foodIncreases;
    }
}
