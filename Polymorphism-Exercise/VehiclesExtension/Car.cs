using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles
{
    public class Car : Vehicle
    {
        private const double increasedFuel = 0.9;
        public Car(double fuelQuantity, double consumptionPerKm,double tankCapacity) : base(fuelQuantity, consumptionPerKm + increasedFuel,tankCapacity)
        {
        }

    }
}
