using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Cake : Dessert
    {
        //The Cake class must have the following default values:
        //•	Grams = 250
        //•	Calories = 1000
        //•	CakePrice = 5
        private const double CakeGrams = 250;
        private const double CakeCalories = 1000;
        private const decimal CakePrice = 5;
        public Cake(string name): base (name,CakePrice,CakeGrams,CakeCalories)
        {
            
        }

    }
}
