using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Models
{
    public class Druid : BaseHero
    {
        private const int druidPower = 80;
        public Druid(string name) : base(name, druidPower) { }


        public override string CastAbility()
        {
            //Druid - "{Type} - {Name} healed for {Power}"
            return $"{GetType().Name} - {Name} healed for {druidPower}";
        }
    }
}

