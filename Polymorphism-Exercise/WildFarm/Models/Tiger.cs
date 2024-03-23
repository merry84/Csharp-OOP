﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models
{
    public class Tiger : Feline
    {
        private const double increase = 1.00;
        public Tiger(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        {
        }

        public override void Eaten(string food, int quantity)
        {
            if (food != "Meat")
            {
                throw new ArgumentException($"{GetType().Name} does not eat {food}!");
            }
            Weight += quantity * increase;
            FoodEaten = quantity;
        }

        public override string ProduceSound() => "ROAR!!!";


    }
}
