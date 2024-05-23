using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;

namespace EDriveRent.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        private List<IVehicle> vehicles;

        public VehicleRepository()
        {
            vehicles = new List<IVehicle>();
        }

        public void AddModel(IVehicle model) => vehicles.Add(model);
        

        public bool RemoveById(string identifier)
        {
            var vehicle = vehicles.FirstOrDefault(x => x.LicensePlateNumber == identifier);
            return vehicles.Remove(vehicle);
        }

        public IVehicle FindById(string identifier) =>  vehicles.FirstOrDefault(x => x.LicensePlateNumber == identifier);
        

        public IReadOnlyCollection<IVehicle> GetAll()=> vehicles.AsReadOnly();
        
    }
}
