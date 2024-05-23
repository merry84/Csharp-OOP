using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public class ScubaDiver :Diver
    {
        private const int oxygenLevel = 540;
        private const double oxygenDecrease = 0.30;
        public ScubaDiver(string name) : base(name, oxygenLevel)
        {
        }

        public override void Miss(int TimeToCatch)
        {
            int UsedOxygen = (int)Math.Round(oxygenLevel * oxygenDecrease);
            base.OxygenLevel -= UsedOxygen;
        }

        public override void RenewOxy()
        {
            base.OxygenLevel = oxygenLevel;
        }
    }
}
