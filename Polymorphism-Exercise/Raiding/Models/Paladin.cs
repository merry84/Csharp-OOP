
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Models
{
    public class Paladin : BaseHero
    {
        private const int paladinPower = 100;

        public Paladin(string name) : base(name, paladinPower)
        {
        }
        public override string CastAbility() => $"{GetType().Name} - {Name} healed for {paladinPower}";

    }
}
