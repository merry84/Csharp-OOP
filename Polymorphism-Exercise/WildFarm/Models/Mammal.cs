using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models
{
    public abstract class Mammal : Animal
    {
        public string  LivingRegion { get; set; }
        protected Mammal(string name, double weight,string livingRegion) : base(name, weight)
        {
            LivingRegion = livingRegion;
        }
        public override string ToString()
        {
            return $"{GetType().Name} [{Name}, {Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
