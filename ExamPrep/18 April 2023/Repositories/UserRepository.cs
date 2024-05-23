using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;

namespace EDriveRent.Repositories
{
    public class UserRepository :IRepository<IUser>
    {
        private List<IUser> users;

        public UserRepository()
        {
            users = new List<IUser>();
        }
        public void AddModel(IUser model)
        {
           users.Add(model);
        }

        public bool RemoveById(string identifier)
        {
            //Removes the first IUser from the collection, which has the same DrivingLicenseNumber as the given
            // identifier. Returns true if the removal was successful, otherwise returns false.
            var user = users.FirstOrDefault(x => x.DrivingLicenseNumber == identifier);
          return  users.Remove(user);
            
        }

        public IUser FindById(string identifier)
        {
            var user = users.FirstOrDefault(x => x.DrivingLicenseNumber == identifier);
            return user;
        }

        public IReadOnlyCollection<IUser> GetAll()=> users.AsReadOnly();
      
    }
}
