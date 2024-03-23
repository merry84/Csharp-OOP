using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models
{
    public class Hen : Bird
    {

        private const double increase = 0.35;
        public Hen(string name, double weight, double wingSize) : base(name, weight, wingSize)
        {
        }

        public override void Eaten(string food, int quantity)
        {
            Weight += quantity * increase;
            FoodEaten = quantity;
        }

        public override string ProduceSound() => "Cluck";
        
        
    }
}
