using BirthdayCelebrations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShortage
{
    public interface IBuyer : IIdentavable
    {
        int Food { get; }
        void BuyFood();
    }
}
