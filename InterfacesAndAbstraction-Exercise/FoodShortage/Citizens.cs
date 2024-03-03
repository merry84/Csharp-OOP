using BirthdayCelebrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShortage
{
    public class Citizens : IBuyer, IBirthing, IIdentavable
    {
        private const int foodIncreases = 10;
        public Citizens(string name, int age ,string id, string birthdate)
        {
            Name = name;
            Age = age;
            Birthdate = birthdate;
            Id = id;
        }

        public int Food { get; private set; }

        public string Name { get; }
        public int Age { get; }
        public string Birthdate { get;}

        string Id { get; }

        public void BuyFood() => Food += foodIncreases;
       

       

       
    }
}
