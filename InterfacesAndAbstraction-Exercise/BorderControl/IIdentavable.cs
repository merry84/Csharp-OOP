using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl
{
    public interface IIdentavable
    {
       string Id { get; }
        bool IsValidId(string fakeId);
    }
}
