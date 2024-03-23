namespace Shapes
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Shape shape = new Circle(3);
            Console.WriteLine($"{shape.CalculateArea():f2}");
            Console.WriteLine($"{shape.CalculatePerimeter():f2}");
            Console.WriteLine(shape.Draw());

            Shape shape2 = new Rectangle(3, 4);
            Console.WriteLine($"{shape2.CalculateArea():f2}");
            Console.WriteLine($"{shape2.CalculatePerimeter():f2}");
            Console.WriteLine(shape2.Draw());

        }
    }
}
