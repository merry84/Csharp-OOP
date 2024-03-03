using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class Animal
    {
        private string name;
        private int age;
        private string gender;
        public virtual string Type => default;
        public Animal(string name, int age, string gender)
        {
           Name = name;
           Age = age;
           Gender = gender;
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Invalid input");
                

                name = value;
            }
        }

        public int Age
        {
            get => age;
            private set
            {
                if (value <= 0) throw new ArgumentException("Invalid input!");
                
                age = value;
            }
        }
        public string Gender
        {
            get { return gender; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Invalid input");

                gender = value;
            }
        }

        public virtual string ProduceSound() => "";
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Type}");
            sb.AppendLine($"{Name} {Age} {Gender}");
            sb.AppendLine($"{ProduceSound()}");
            return sb.ToString().TrimEnd();
        }
    }
}
