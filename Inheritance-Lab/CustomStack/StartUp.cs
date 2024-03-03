namespace CustomStack
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var stringStack = new StackOfStrings();
            Console.WriteLine(stringStack.IsEmpty());
            for (int i = 0; i < 3; i++)
            {
                stringStack.Push(i.ToString());
            }
            Console.WriteLine(stringStack.IsEmpty());
            Console.WriteLine(stringStack.Count);
            var stack = new StackOfStrings();
            stack.AddRange(stringStack);
        }
    }
}
