using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models
{
    public abstract class Animal :ISound
    {
		//string Name, double Weight, int FoodEaten
		private string name;
		private double weight;
		private int foodEaten;

        protected Animal(string name, double weight)
        {
            Name = name;
            Weight = weight;
           
        }

        public string Name
		{
			get { return name; }
			set { name = value; }
		}
        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public int FoodEaten
        {
            get { return foodEaten; }
            set { foodEaten = value; }
        }

        public abstract string ProduceSound();
        public abstract void Eaten(string food, int quantity);
    }
}
