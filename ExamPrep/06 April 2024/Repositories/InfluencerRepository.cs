using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Repositories
{
    public class InfluencerRepository : IRepository<IInfluencer>
    {
        private readonly List<IInfluencer> influensers;
        public InfluencerRepository()
        {
            influensers = new List<IInfluencer>();
        }
        public IReadOnlyCollection<IInfluencer> Models => influensers;

        public void AddModel(IInfluencer model)
        {
            influensers.Add(model);
        }

        public IInfluencer FindByName(string name)
        {
            var nameOfInfluenser = influensers.FirstOrDefault(x=>x.Username == name);
            return nameOfInfluenser;
        }

        public bool RemoveModel(IInfluencer model)
        {
            if(model != null) influensers.Remove(model);
            return true;
        }
    }
}
