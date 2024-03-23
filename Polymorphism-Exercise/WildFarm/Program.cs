using WildFarm.Models;

namespace WildFarm;

public class StartUp
{
    static void Main()
    {
        List<Animal> animals = new List<Animal>();
        string input;
        while ((input = Console.ReadLine())!= "End")
        {            

            string[] animalInfo = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] foodInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string animalType = animalInfo[0];
            string nameAnimal = animalInfo[1];
            double weight = double.Parse(animalInfo[2]);
            string foodType = foodInfo[0];
            int foodQuantity = int.Parse(foodInfo[1]);

            try
            {
                if (animalType == "Cat")
                {
                    Cat cat = new Cat(nameAnimal, weight, animalInfo[3], animalInfo[4]);
                    Console.WriteLine(cat.ProduceSound());
                    animals.Add(cat);
                    cat.Eaten(foodType, foodQuantity);
                }
                else if (animalType == "Tiger")
                {
                    Tiger tiger = new(nameAnimal, weight, animalInfo[3], animalInfo[4]);
                    Console.WriteLine(tiger.ProduceSound());
                    animals.Add(tiger);
                    tiger.Eaten(foodType, foodQuantity);
                }
                else if (animalType == "Dog")
                {
                    Dog dog = new(nameAnimal, weight, animalInfo[3]);
                    Console.WriteLine(dog.ProduceSound());
                    animals.Add(dog);
                    dog.Eaten(foodType, foodQuantity);
                }
                else if (animalType == "Mouse")
                {
                    Mouse mouse = new(nameAnimal, weight, animalInfo[3]);
                    Console.WriteLine(mouse.ProduceSound());
                    animals.Add(mouse);
                    mouse.Eaten(foodType, foodQuantity);
                }
                else if (animalType == "Owl")
                {
                    Owl owl = new(nameAnimal, weight, double.Parse(animalInfo[3]));
                    Console.WriteLine(owl.ProduceSound());
                    animals.Add(owl);
                    owl.Eaten(foodType, foodQuantity);
                }
                else if (animalType == "Hen")
                {
                    Hen hen = new(nameAnimal, weight, double.Parse(animalInfo[3]));
                    Console.WriteLine(hen.ProduceSound());
                    animals.Add(hen);
                    hen.Eaten(foodType, foodQuantity);
                }
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }

        }

        foreach (var animal in animals)
        {
            Console.WriteLine(animal);
        }
    }
}
