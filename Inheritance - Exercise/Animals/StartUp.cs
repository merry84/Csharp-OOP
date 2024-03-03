using System;
using System.Linq;
using System.Text;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var printResult = new StringBuilder();
            string command = string.Empty;
            while ((command = Console.ReadLine()) != "Beast!")
            {
                string[] elements = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();
                string name = elements[0];
                int age = int.Parse(elements[1]);
                string gender = null;
                if (elements.Length > 2) { gender = elements[2]; }
                try
                {

                    if (command == "Cat")
                    {
                        Cat cat = new Cat(name, age, gender);
                        printResult.AppendLine(cat.ToString());
                    }
                    if (command == "Dog")
                    {
                        Dog dog = new Dog(name, age, gender);
                        printResult.AppendLine(dog.ToString());
                    }
                    if (command == "Tomcat")
                    {
                        Tomcat tomCat = new Tomcat(name, age);
                        printResult.AppendLine(tomCat.ToString());
                    }
                    if (command == "Kitten")
                    {
                        Kitten kittens = new Kitten(name, age);
                        printResult.AppendLine(kittens.ToString());
                    }
                    if (command == "Frog")
                    {
                        Frog frog = new Frog(name, age, gender);
                        printResult.AppendLine(frog.ToString());
                    }
                }
                catch (Exception ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }            
                Console.WriteLine(printResult.ToString());
                        
        }
    }
}
