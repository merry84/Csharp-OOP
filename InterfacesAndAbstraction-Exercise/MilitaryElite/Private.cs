using MilitaryElite.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class Private : Soldiers, IPrivate
    {
        protected Private(int id, string firstName, string lastName,decimal salary) : base(id, firstName, lastName)
        {
            Salary = salary;
        }

        public decimal Salary {get; private set;}
        public override string ToString()
        {
            return $" Salary: {Salary:f2}";
        }
    }
}
