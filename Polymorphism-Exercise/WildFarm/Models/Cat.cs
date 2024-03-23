using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models
{
    public class Cat : Feline
    {
        private const double increase = 0.30;
        public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        {
        }

        public override void Eaten(string food, int quantity)
        {
           if (food != "Vegetable" && food != "Meat") { throw new ArgumentException($"{GetType().Name} does not eat {food}!"); }
            Weight += quantity * increase;
            FoodEaten = quantity;
        }

        public override string ProduceSound() => "Meow";
    }
}
