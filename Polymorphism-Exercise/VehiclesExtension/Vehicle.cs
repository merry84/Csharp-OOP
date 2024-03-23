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
        private double tankCapacity;        

        protected Vehicle(double fuelQuantity, double consumptionPerKm, double tankCapacity)
        {
            FuelQuantity = fuelQuantity;
            ConsumptionPerKm = consumptionPerKm;
            TankCapacity = tankCapacity;   
        }

        public double FuelQuantity
        {
            get => fuelQuantity;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Fuel must be a positive number");
                }
                fuelQuantity = value;
            }
        }
        public double ConsumptionPerKm
        {
            get => consumptionPerKm;
            private set 
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Consumption must be a positive number");
                }
                consumptionPerKm = value;
            } 
        }

        public double TankCapacity
        {
            get => tankCapacity;
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Tank capacity must be a positive number");
                }
                tankCapacity = value;
                // If you try to create a vehicle with more fuel than its tank capacity, create it but start with an empty tank.
                if (FuelQuantity > TankCapacity) FuelQuantity = 0;
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
            if (fuelAmount <= 0) throw new ArgumentException("Fuel must be a positive number");
            //If you try to put more fuel in the tank than the available space
            if (TankCapacity < fuelAmount + FuelQuantity) throw new ArgumentException($"Cannot fit {fuelAmount} fuel in the tank");
            FuelQuantity += fuelAmount;
        }
        public override string ToString() => $"{GetType().Name}: {FuelQuantity:f2}".ToString();
    }
}
