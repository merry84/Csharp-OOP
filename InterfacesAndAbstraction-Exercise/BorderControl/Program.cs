namespace BorderControl
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input;
            List<IIdentavable> output =  new ();

            while ((input = Console.ReadLine()) != "End")
            {
                string[] elements =  input.Split(" ",StringSplitOptions.RemoveEmptyEntries);
                 
                if(elements.Length == 3)//Citizen
                {
                    output.Add(new Citizens(elements[0], int.Parse(elements[1]), elements[2]));
                }
                else { output.Add(new Robots(elements[0], elements[1])); }
            }
            
            string fakeId = Console.ReadLine();

            foreach (var element in output)
            {
                if (!element.IsValidId(fakeId))
                {
                    Console.WriteLine(element.Id);
                }
            }
        }
    }
}
