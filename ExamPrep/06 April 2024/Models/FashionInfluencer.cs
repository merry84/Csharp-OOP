using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Models
{
    public class FashionInfluencer : Influencer
    {
        private const double EngagementRate = 4.0;
        private double factor = 0.1;
        public FashionInfluencer(string username, int followers) : base(username, followers, EngagementRate)
        {
        }
        public override int CalculateCampaignPrice()
        {
            int result = (int)Math.Ceiling(Followers * EngagementRate * factor);
            return result;
        }
    }
}
