using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle

    {
        private string brand;
        private string model;
        private double maxMileage;
        private string licensePlateNumber;
        private int batteryLevel;
        private bool isDamaged;

        public Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            Brand = brand;
            Model = model;
             this.maxMileage = maxMileage;
            LicensePlateNumber = licensePlateNumber;
            isDamaged = false;
            batteryLevel = 100;
        }

        public string Brand
        {
            get => brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.BrandNull));
                }
                brand = value;
            }
        }

        public string Model {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.ModelNull));
                }
                model = value;
            }
        }

        public double MaxMileage => this.maxMileage;
        public string LicensePlateNumber {
            get => licensePlateNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.DrivingLicenseRequired));
                }
                licensePlateNumber = value;
            }
        }

        public int BatteryLevel => this.batteryLevel;
        public bool IsDamaged => this.isDamaged;
        public void Drive(double mileage)
        {
            double percentage = Math.Round((mileage / this.maxMileage) * 100);
            this.batteryLevel -= (int)percentage;

            if (this.GetType().Name == nameof(CargoVan))
            {
                this.batteryLevel -= 5;
            }

        }

        public void Recharge()
        {
            batteryLevel = 100;
        }

        public void ChangeStatus()
        {
            if (!isDamaged) isDamaged = true;
            else isDamaged =false;
        }

        public override string ToString()
        {
            string vehicleCondition;

            if (isDamaged)
            {
                vehicleCondition = "damaged";
            }
            else
            {
                vehicleCondition = "OK";
            }

            return $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: {vehicleCondition}";
        }
    }
}
