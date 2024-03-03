﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedForSpeed
{
    public class RaceMotorcycle :  Motorcycle
    {//DefaultFuelConsumption = 8
        private double DefaultFuelConsumption = 8;
        public override double FuelConsumption => DefaultFuelConsumption;
        public RaceMotorcycle(int horsePower,double fuel) : base(horsePower, fuel)
        {
            
        }
    }
}
