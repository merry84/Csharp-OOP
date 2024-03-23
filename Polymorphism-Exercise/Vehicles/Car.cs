using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles
{
    public class Car : Vehicle
    {
        private const double increasedfuel = 0.9;
        public Car(double fuelQuantity, double consumptionPerKm) : base(fuelQuantity, consumptionPerKm + increasedfuel)
        {
        }
    }
}
