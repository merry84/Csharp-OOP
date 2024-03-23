using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationAttributes.Attributes;

namespace ValidationAttributes.Models
{
    public class Person
    {
        private const int MinAge = 12;
        private const int MaxAge = 90;
        [MyRequired]
        public string Name { get; set; }
        [MyRange(MinAge,MaxAge)]
        public int Age { get; set; }
        public Person(string name,int age)
        {
            Name = name;
            Age = age;
        }
    }
}
