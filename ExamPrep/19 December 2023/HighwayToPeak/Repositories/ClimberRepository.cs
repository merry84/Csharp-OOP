using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;

namespace HighwayToPeak.Repositories
{
    public class ClimberRepository :IRepository<IClimber>
    {
        private List<IClimber> climbers = new List<IClimber>();
        // // •	All – IReadOnlyCollection<IClimber>
        // // o	Returns a readonly collection of all climbers, added to the repository. 
        public IReadOnlyCollection<IClimber> All => climbers.AsReadOnly();

        public void Add(IClimber model)=> climbers.Add(model);// Adds a new IClimber to the ClimberRepository.

        // IClimber Get(string name)
        // Returns a climber with the given name from the collection, if there is any. Otherwise, it returns null.
        public IClimber Get(string name) => climbers.FirstOrDefault(n => n.Name == name);

    }
    
}
