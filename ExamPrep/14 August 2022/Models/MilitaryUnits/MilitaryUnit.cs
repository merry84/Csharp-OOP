using System;
using System.Collections.Generic;
using System.Text;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Utilities.Messages;

namespace PlanetWars.Models.MilitaryUnits
{
    public abstract class MilitaryUnit :IMilitaryUnit
    {
        private double cost;
        private int enduranceLevel;

        protected MilitaryUnit(double cost)
        {
            Cost = cost;
            this.enduranceLevel = 1;
        }

        public double Cost
        {
            get=>cost;
            private set=>cost = value;
        }
        public int EnduranceLevel => this.enduranceLevel;
        public void IncreaseEndurance()
        {
            if (enduranceLevel == 20)
            {
                 string.Format(ExceptionMessages.EnduranceLevelExceeded);
            }
            enduranceLevel++;
        }
    }
}
