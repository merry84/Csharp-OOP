using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telephony
{
    public class StationaryPhone :ICalling
    {
        public StationaryPhone() { }
        public void Call(string number)
        {
            if (!Calling(number) )
            {
                Console.WriteLine("Invalid number!");
                return;
            }
            Console.WriteLine($"Dialing... {number}");
        }
        private bool Calling(string number) => number.All(n => char.IsDigit(n));
    }
}
