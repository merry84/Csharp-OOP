using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplicitInterfaces
{
    public class Citizen : IPerson, IResident
    {
        public Citizen(string name, int age, string country)
        {
            Name = name;
            Age = age;
            Country = country;
        }

        public string Name { get; }

        public int Age { get; }

        public string Country { get; }

         string IResident.GetName()=> $"Mr/Ms/Mrs {Name}";
        
         string IPerson.GetName() => Name;
    }
}
