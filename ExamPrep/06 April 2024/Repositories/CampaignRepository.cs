using InfluencerManagerApp.Models;
using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Repositories
{  
   
    public class CampaignRepository : IRepository<ICampaign>
    {
        private readonly List<ICampaign> campaigns;
        public CampaignRepository()
        {
            campaigns = new List<ICampaign>();
        }
        public IReadOnlyCollection<ICampaign> Models => campaigns;

        public void AddModel(ICampaign model)
        {
            campaigns.Add(model);
        }

        public ICampaign FindByName(string name)
        {
            var nameOfInfluenser = campaigns.FirstOrDefault(x=>x.Brand==name);
            return nameOfInfluenser;
        }

        public bool RemoveModel(ICampaign model)
        {
            if (model != null) campaigns.Remove(model);
            return true;
        }
    }
}
