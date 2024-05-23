using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Models
{
    public class OxygenClimber :Climber
    {
        private const int initialStamina = 10;//Will have an initial Stamina of 10 units.
        public OxygenClimber(string name) : base(name, initialStamina)
        {
        }

        public override void Rest(int daysCount) => Stamina += daysCount;//Will recover 1 unit of Stamina for every day of rest in the base camp.
    }
}
