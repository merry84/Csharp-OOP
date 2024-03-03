namespace Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] numbers = Console.ReadLine()
                .Split();
            string[] url = Console.ReadLine()
                .Split();
            ICalling phone;

            foreach (string number in numbers)
            {
               if(number.Length == 7)
                {
                    phone = new StationaryPhone();
                }
               else
                {
                    phone = new SmartPhone();
                }
                phone.Call(number);
            }
            IBrowsing smartPhone = new SmartPhone();
            foreach (string currentUrl in url)
            {
                smartPhone.BrowseURL(currentUrl);

            }
        }
    }
}
