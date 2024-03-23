using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles;

namespace VehiclesExtension
{
    
    public class Bus : Vehicle
    {
        private const double increasedFuel = 1.4;//with air-conditioner
        public Bus(double fuelQuantity, double consumptionPerKm, double tankCapacity) : base(fuelQuantity, consumptionPerKm, tankCapacity)
        {
        }
        public override void Drive(double distance)
        {
            double consumptionWithPersons = distance * (ConsumptionPerKm + increasedFuel);
            if(FuelQuantity < consumptionWithPersons)
            {
                throw new ArgumentException("Bus needs refueling");
            }
            
            FuelQuantity -= consumptionWithPersons;
            Console.WriteLine($"Bus travelled {distance} km");
        }
        public void DriveEmptyBus(double distance)
        {
            double consumption = distance * ConsumptionPerKm ;
            if (FuelQuantity < consumption)
            {
                throw new ArgumentException("Bus needs refueling");
            }

            FuelQuantity -= consumption;
            Console.WriteLine($"Bus travelled {distance} km");
        }
    }
}
