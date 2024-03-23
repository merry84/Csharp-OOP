using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles
{
    public class Truck : Vehicle
    {
        private const double increasedfuel = 1.6;
        private const double realFuel = 0.95;
        public Truck(double fuelQuantity, double consumptionPerKm) : base(fuelQuantity, consumptionPerKm + increasedfuel)
        {
        }
        public override void Refuel(double fuelAmount)
        {
            base.Refuel(fuelAmount * realFuel);
        }
    }
}
