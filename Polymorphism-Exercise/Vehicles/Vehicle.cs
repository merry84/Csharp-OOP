using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Contracts;

namespace Vehicles
{
    public abstract class Vehicle : IDrivable, IRefuellable
    {
        private double fuelQuantity;
        private double consumptionPerKm;

        protected Vehicle(double fuelQuantity, double consumptionPerKm)
        {
            FuelQuantity = fuelQuantity;
            ConsumptionPerKm = consumptionPerKm;
        }

        public double FuelQuantity
        {
            get => fuelQuantity;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Fuel must be positive number");
                }
                fuelQuantity = value;
            }
        }
        public double ConsumptionPerKm
        {
            get => consumptionPerKm;
            private set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Consumption must be positive number");
                }
                consumptionPerKm = value;
            } 
        }

        public virtual void Drive(double distance)
        {
            double totalConsumption = distance * ConsumptionPerKm;
            if (FuelQuantity< totalConsumption)
            {
                //: "Car/Truck needs refueling"
                throw new ArgumentException($"{this.GetType().Name} needs refueling");
            }
            //	"Car/Truck travelled {distance} km"
            FuelQuantity -= totalConsumption;

            Console.WriteLine($"{GetType().Name} travelled {distance} km");
        }

        public virtual void Refuel(double fuelAmount)
        {
            if(fuelAmount < 0) throw new ArgumentException("FuelAmount must be positive number");
            FuelQuantity += fuelAmount;
        }
        public override string ToString() => $"{GetType().Name}: {FuelQuantity:f2}".ToString();
    }
}
