using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedForSpeed
{
    public class Vehicle
    {
        //Create a base class Vehicle. It should contain the following members:
        //•	a constructor that accepts the following parameters: int horsepower, double fuel
        //•	defaultfuelconsumption – double 
        //•	fuelconsumption – virtual double
        //•	fuel – double
        //•	horsepower – int
        //•	virtual void drive(double kilometers)
        //o the drive method should have a functionality to reduce the fuel based on the traveled kilometers.
        //the default fuel consumption for vehicle is 1.25.
        //some of the classes have different default fuel consumption values:
        //•	sportcar – defaultfuelconsumption = 10
        //•	racemotorcycle – defaultfuelconsumption = 8
        //•	car – defaultfuelconsumption = 3

        private double DefaultFuelConsumption = 1.25;
        public virtual double FuelConsumption => DefaultFuelConsumption;
        public double Fuel { get; set; }
        public int HorsePower  { get; set; }
        public virtual void Drive(double kilometers)
        {
            Fuel -= kilometers * FuelConsumption;

        }
        public Vehicle(int horsePower,double fuel)
        {
            HorsePower = horsePower;
            Fuel = fuel;
        }
    }
}
