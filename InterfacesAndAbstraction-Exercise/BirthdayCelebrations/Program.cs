namespace BirthdayCelebrations
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input;
            List<IBirthing> output = new();

            while ((input = Console.ReadLine()) != "End")
            {
                string[] elements = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (elements[0] == "Citizen")
                {
                    output.Add(new Citizens(elements[1], int.Parse(elements[2]), elements[3], elements[4]));
                }
                if (elements[0] == "Pet")
                {
                    output.Add(new Pet(elements[1], elements[2]));
                }


            }

            string year = Console.ReadLine();

            var currentYear = output.Where(y => y.Birthdate.EndsWith(year));
            foreach(var element in currentYear) 
            {
                Console.WriteLine(element.Birthdate);
            }



        }

    }
}
