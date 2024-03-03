namespace FoodShortage
{
    public class Program
    {
        static void Main(string[] args)
        {
            int numberOfPeople = int.Parse(Console.ReadLine());

            List<IBuyer> buyers = new List<IBuyer>();

            for (int i = 0; i < numberOfPeople; i++)
            {
                string[] peopleInfo = Console.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries);
                if(peopleInfo.Length == 4)
                {
                    buyers.Add(new Citizens(peopleInfo[0], int.Parse(peopleInfo[1]), peopleInfo[2], peopleInfo[3]));
                }
                else
                {
                    buyers.Add(new Rebel(peopleInfo[0], int.Parse(peopleInfo[1]), peopleInfo[2]));
                }
            }
            string input;
            while((input = Console.ReadLine())!= "End")
            {
               var buyer = buyers.FirstOrDefault(n=>n.Name == input);
                if(buyer != null) { buyer.BuyFood(); }
            }
            //The output consists of only one line on which you should print the total amount of food purchased.
            Console.WriteLine(buyers.Sum(s=>s.Food));
        }
    }
}
