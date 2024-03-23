using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles
{
    public class Truck : Vehicle
    {
        private const double increasedFuel = 1.6;
        private const double realFuel = 0.95;
        public Truck(double fuelQuantity, double consumptionPerKm,double tankCapacity) : base(fuelQuantity, consumptionPerKm + increasedFuel,tankCapacity)
        {
        }
        public override void Refuel(double fuelAmount)
        {
            if (fuelAmount <= 0) throw new ArgumentException("Fuel must be a positive number");
            if (TankCapacity < fuelAmount + FuelQuantity) throw new ArgumentException($"Cannot fit {fuelAmount} fuel in the tank");
            base.Refuel(fuelAmount * realFuel);
        }
    }
}
