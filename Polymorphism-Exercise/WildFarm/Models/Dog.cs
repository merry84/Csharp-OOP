﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models
{
    public class Dog : Mammal

    {
        private const double increase = 0.40;
        public Dog(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }

        public override void Eaten(string food, int quantity)
        {
            if(food != "Meat")
            {
                throw new ArgumentException($"{GetType().Name} does not eat {food}!");
            }
            Weight += quantity * increase;
            FoodEaten = quantity;
        }

        public override string ProduceSound()
        {
            return "Woof!";
        }
    }
}
