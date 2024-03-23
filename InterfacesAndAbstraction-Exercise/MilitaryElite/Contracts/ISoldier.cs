using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Contracts
{
    public interface ISoldier//ISoldier should hold id, first name, and last name
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }

    }
}
