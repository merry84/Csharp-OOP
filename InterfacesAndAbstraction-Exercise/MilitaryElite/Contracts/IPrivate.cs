using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Contracts
{
    public interface IPrivate : ISoldier //Private - lowest base Soldier type, holding the salary(decimal). 
    {
        decimal Salary { get; }
    }
}
