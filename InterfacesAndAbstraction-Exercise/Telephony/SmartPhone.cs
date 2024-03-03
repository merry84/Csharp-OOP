using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telephony
{
    public class SmartPhone : ICalling, IBrowsing
    {
        public SmartPhone() { }
        public void BrowseURL(string url)
        {
            if (!ValidBrowseUrl(url))
            {
                Console.WriteLine("Invalid URL!");
                return;
            }
            Console.WriteLine($"Browsing: {url}!");
            
        }
        

        public void Call(string phoneNumber)
        {
            if(!ValidNumber(phoneNumber) )
            {
                Console.WriteLine("Invalid number!");
                return;
            }
            Console.WriteLine($"Calling... {phoneNumber}");
        }
        
        private bool ValidNumber(string number) => number.All(n => char.IsDigit(n));
        private bool ValidBrowseUrl(string url) => url.All(u => !char.IsDigit(u));
    }
    
}
