namespace ExplicitInterfaces
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input;
            while((input = Console.ReadLine())!="End")
            {
                string[] elements = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = elements[0];
                string country = elements[1];
                int age = int.Parse(elements[2]);
                IPerson person = new Citizen(name,age,country);
                IResident resident = new Citizen(name, age, country);

                Console.WriteLine(person.GetName());
                Console.WriteLine(resident.GetName());

            }
        }
    }
}
