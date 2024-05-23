using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Repositories.Contracts;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Core
{
    //users – UserRepository
    //  vehicles – VehicleRepository
    //  routes – RouteRepository
    public class Controller :IController
    {
        private IRepository<IUser> users;
        private IRepository<IVehicle> vehicles;
        private IRepository<IRoute> routes;

        public Controller()
        {
            users = new UserRepository();
            vehicles = new VehicleRepository();
            routes = new RouteRepository();
        }
        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            //The method should create and add a new entity of IUser to the UserRepository.
            //  If there is already a user with the same drivingLicenseNumber, return the following message: 
            // "{drivingLicenseNumber} is already registered in our platform."
            //  If the above case is NOT reached, create a new User and add it to the UserRepository. Return the 
            // following message: "{firstName} {lastName} is registered successfully with DLN-
            // {drivingLicenseNumber}"
            IUser user = users.FindById(drivingLicenseNumber);
            if (user != null)
            {
                return string.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
            }

            user = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(user);
            return string.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
           //The method should create and add a new entity of IVehicle to the VehicleRepository.
           //  If the given vehicleTypeName is NOT presented as a valid Vehicle’s child class (PassengerCar or 
           // CargoVan), return the following message: "{typeName} is not accessible in our platform."
           //  If there is already a vehicle with the same licensePlateNumber, return the following message: 
           // "{licensePlateNumber} belongs to another vehicle."
           //  If none of the above cases is reached, create a correct type of IVehicle and add it to the 
           // VehicleRepository. Return the following message: "{brand} {model} is uploaded 
           // successfully with LPN-{licensePlaneNumber}"
           if (vehicleType != nameof(PassengerCar) && vehicleType != nameof(CargoVan))
           {
               return string.Format(OutputMessages.VehicleTypeNotAccessible, vehicleType);
           }
           IVehicle vehicle = vehicles.FindById(licensePlateNumber);
           if (vehicle != null)
           {
               return string.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
           }
           else
           {
               if (vehicleType == nameof(CargoVan))
               {
                   vehicle = new CargoVan(brand, model, licensePlateNumber);
               }
               else if (vehicleType == nameof(PassengerCar))
               {
                   vehicle = new PassengerCar(brand, model, licensePlateNumber);
               }
               vehicles.AddModel(vehicle);
               return string.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
           }
        }

        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            //The method should create and add a new entity of IRoute to the RouteRepository.
            // HINT: Route’s constructor will be expecting as the last parameter routeId.So it should be created by taking 
            // the count of already added routes in the RouteRepository + 1.
            //  If there is already added Route with the given startPoint, endPoint and length, return the following 
            // message: "{startPoint}/{endPoint} - {length} km is already added in our platform."
            //  If there is already added Route with the given startPoint, endPoint and Route.Length is less than 
            // the given length return the following message: "{startPoint}/{endPoint} shorter route is 
            // already added in our platform."
            int routeId = routes.GetAll().Count + 1;
            IRoute existRoute =
                routes.GetAll().FirstOrDefault(x => x.StartPoint == startPoint && x.EndPoint == endPoint);
            if (existRoute != null)
            {
                if (existRoute.Length == length)
                {
                    return string.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
                }
                else if (existRoute.Length < length)
                {
                    return string.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint, length);
                }
                else if (existRoute.Length > length)
                {
                    // o If there is already added Route with the given startPoint, endPoint and greater Length, lock 
                    // the longer Route.
                    existRoute.LockRoute();
                }
            }
            //  If the above case is not reached, create a new Route and add it to the RouteRepository. 
            // o Return the following message: "{startPoint}/{endPoint} - {length} km is unlocked in  our platform."
            IRoute route = new Route(startPoint, endPoint, length, routeId);
            routes.AddModel(route);
            return string.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);

        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser user = users.FindById(drivingLicenseNumber);
            IVehicle vehicle = vehicles.FindById(licensePlateNumber);
            IRoute route =routes.FindById(routeId);

            if (user.IsBlocked)
            {
                return string.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
            }

            if (vehicle.IsDamaged)
            {
                return string.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
            }

            if (route.IsLocked)
            {
                return string.Format(OutputMessages.RouteLocked, routeId);
            }
            //Drive the specific vehicle on the specific route (Use the Vehicle.Drive(route.Length) method). The 
            // trip should take effect to the BatteryLevel of the vehicle.
            vehicle.Drive(route.Length);
            //If the value of the parameter isAccidentHappened is true, the IsDamaged status of the vehicle should 
            // be changed to true. The Rating of the User who has rented the Vehicle should be decreased
            if (isAccidentHappened)
            {
                vehicle.ChangeStatus();
                user.DecreaseRating();
            }
            else
            {
                //Else increase the User’s Rating
                user.IncreaseRating();

            }

            return vehicle.ToString();
        }

        public string RepairVehicles(int count)
        {
           //The method should select only those vehicles from the VеhicleRepository, which are damaged. Order the 
           // selected vehicles alphabetically by their Brand, then alphabetically by their Model. Take the first {count}
           // vehicles, if there are as many damaged vehicles, else take all of the damaged vehicles.
           //  Each of the chosen vehicles will be repaired (IsDamaged == false) and recharged (battery level restored to 
           // 100%).
           //  Return the following message: "{countOfRepairedVehicles} vehicles are successfully 
           // repaired!
           var demagedVehicles = vehicles.GetAll()
               .Where(x => x.IsDamaged == true)
               .OrderBy(n => n.Brand)
               .ThenBy(x => x.Model);
           int countOfVehicle = 0;
           if (demagedVehicles.Count() < count)
           {
                countOfVehicle = demagedVehicles.Count();
           }
           else
           {
               countOfVehicle = count;
           }

           var vehicleSelected = demagedVehicles.ToArray()
               .Take(countOfVehicle);

           foreach (var vehicle in vehicleSelected)
           {
               vehicle.ChangeStatus();
               vehicle.Recharge();
           }

           return string.Format(OutputMessages.RepairedVehicles, countOfVehicle);
        }

        public string UsersReport()
        {
            //"*** E-Drive-Rent ***
            // {user1} 
            // {user2}
            // ...
            // {usern}
            var sb = new StringBuilder();
            sb.AppendLine("*** E-Drive-Rent ***");
            // Arrange the users by Rating, descending, then 
            // by LastName alphabetically, then by FirstName alphabetically
            var selectUsers = this.users.GetAll()
                .OrderByDescending(x => x.Rating)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToArray();
            foreach (var user in selectUsers)
            {
               sb.AppendLine(user.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
