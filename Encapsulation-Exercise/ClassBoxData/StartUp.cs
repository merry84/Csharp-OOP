using ClassBoxData;

namespace PersonsInfo
{
    public class StartUp
    {
        static void Main()
        {
            //•	On the first three lines, you will get the length, width, and height. 
            double length = double.Parse(Console.ReadLine());
            double width = double.Parse(Console.ReadLine());
            double height = double.Parse(Console.ReadLine());
            try
            {
                var box = new Box(length, width, height);
                Console.WriteLine(box);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
