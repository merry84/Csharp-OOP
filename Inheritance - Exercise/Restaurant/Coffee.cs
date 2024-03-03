using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Coffee : HotBeverage
    {
        //•	double CoffeeMilliliters = 50
        //•	decimal CoffeePrice = 3.50
        //•	Caffeine – double
        private const double CoffeeMilliliters = 50;
        private const decimal CoffeePrice = 3.5m;
        public double Caffeine { get; private set; }

        public Coffee(string name, double caffeine): base(name, CoffeePrice, CoffeeMilliliters)
        {
            Caffeine = caffeine;
        }
    }
}
