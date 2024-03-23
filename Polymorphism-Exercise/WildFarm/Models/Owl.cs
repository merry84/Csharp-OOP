
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models
{
    public class Owl : Bird
    {//•	Owl - 0.25
        private const double increase = 0.25;

        public Owl(string name, double weight, double wingSize) : base(name, weight , wingSize)
        {
        }       

        public override string ProduceSound() => "Hoot Hoot";
       
        public override void Eaten(string food, int quantity)
        {
            if (food != "Meat ")
            {
                throw new ArgumentException($"{GetType().Name} does not eat {food}!");
            }
            Weight += quantity * increase;
            FoodEaten = quantity;
        }
    }
}
