using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public class FreeDiver :Diver
    {
        //It has OxygenLevel value of 120 seconds.
        //FreeDiver will decrease the OxygenLevel property by 60% (using the Miss() method) of the TimeToCatch value of the missed fish. 
        // If the calculated value is not a whole number, round it to the nearest whole integer.
        private const int oxygenLevel = 120;
        private const double oxygenDecrease = 0.60;
        
        public FreeDiver(string name) : base(name, oxygenLevel)
        {
        }

        public override void Miss(int TimeToCatch)
        {
            //OxygenLevel -= (int)Math.Round(…, MidpointRounding.AwayFromZero);
            int UsedOxygen = (int)Math.Round(oxygenLevel * oxygenDecrease);
            base.OxygenLevel -= UsedOxygen;
        }

        public override void RenewOxy()
        {
            base.OxygenLevel = oxygenLevel;
        }
    }
}
