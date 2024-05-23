using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public class CargoVan :Vehicle
    {
        //CargoVan has a constant value for MaxMileage = 180
        // The constructor of the CargoVan should take the following parameters upon initialization:
        // string brand, string model, string licensePlateNumber
        private const double MaxMileage = 180;
        public CargoVan(string brand, string model, string licensePlateNumber) : base(brand, model, MaxMileage, licensePlateNumber)
        {
        }
    }
}
