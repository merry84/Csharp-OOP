using InfluencerManagerApp.Models;
using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Repositories;
using InfluencerManagerApp.Repositories.Contracts;
using InfluencerManagerApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Core.Contracts
{
    public class Controller : IController
    {

        private readonly IRepository<IInfluencer> influencers;
        private readonly IRepository<ICampaign> campaigns;
        public Controller()
        {
            influencers = new InfluencerRepository();
            campaigns = new CampaignRepository();
        }

        public string ApplicationReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var influencer in influencers.Models
                .OrderByDescending(i => i.Income)
                .ThenByDescending(i => i.Followers))
            {
                sb.AppendLine(influencer.ToString());

                if (influencer.Participations.Any())
                {
                    sb.AppendLine("Active Campaigns:");

                    foreach (var campaign in campaigns.Models
                        .Where(c => c.Contributors
                        .Contains(influencer.Username))
                        .OrderBy(c => c.Brand))
                    {
                        sb.AppendLine($"--{campaign.ToString()}");
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string AttractInfluencer(string brand, string username)
        {

            if (influencers.FindByName(username) == null)
            {
                string.Format(OutputMessages.InfluencerNotFound, influencers.GetType().Name, username);
            }

            if (campaigns.FindByName(brand) == null)
            {
                return string.Format(OutputMessages.CampaignNotFound, brand);
            }
            IInfluencer influencer = influencers.FindByName(username);
            ICampaign campaign = campaigns.FindByName(brand);
            if (campaign.Contributors.Contains(influencer.Username))
            {
                return string.Format(OutputMessages.InfluencerAlreadyEngaged, username, brand);
            }

            bool isEligible = true;
            if (campaign.GetType().Name == nameof(ProductCampaign)
                && influencer.GetType().Name == nameof(BloggerInfluencer))
            {
                isEligible = false;
            }
            if (campaign.GetType().Name == nameof(ServiceCampaign)
                && influencer.GetType().Name == nameof(FashionInfluencer))
            {
                isEligible = false;
            }

            if (!isEligible)
            {
                return string.Format(OutputMessages.InfluencerNotEligibleForCampaign, username, brand);
            }

            double profit = influencer.CalculateCampaignPrice();

            if (campaign.Budget < profit)
            {
                return string.Format(OutputMessages.UnsufficientBudget, brand, username);
            }

            influencer.EarnFee(profit);
            influencer.EnrollCampaign(brand);
            campaign.Engage(influencer);

            return string.Format(OutputMessages.InfluencerAttractedSuccessfully, username, brand);

        }

        public string BeginCampaign(string typeName, string brand)
        {

            if (typeName != nameof(ProductCampaign) && typeName != nameof(ServiceCampaign))
            {
               return string.Format(OutputMessages.CampaignTypeIsNotValid, typeName); 
            }
            if (campaigns.FindByName(brand) != null)
            {
                    return string.Format(OutputMessages.CampaignDuplicated, brand);
            }
            ICampaign campaign;
            if (typeName == nameof(ServiceCampaign))
            {
                    campaign = new ServiceCampaign(brand);
            }
            else
            {
                campaign = new ProductCampaign(brand);                                       
            }
            campaigns.AddModel(campaign);
            return string.Format(OutputMessages.CampaignStartedSuccessfully, brand, typeName);

        }
        public string CloseCampaign(string brand)
        {
           
            ICampaign campaign = campaigns.FindByName(brand);
            if (campaign == null)
            {
                return string.Format(OutputMessages.InvalidCampaignToClose);
            }
            
            if (campaign.Budget <= 10000)
            {
                return string.Format(OutputMessages.CampaignCannotBeClosed, brand);
            }
            
            
            foreach (var contributor in campaign.Contributors)
            {
                var influencer = influencers.FindByName(contributor);
                influencer.EarnFee(2000);
                influencer.EndParticipation(campaign.Brand);
            }
           
            campaigns.RemoveModel(campaign);
            
            return string.Format(OutputMessages.CampaignClosedSuccessfully, brand);
        }

        public string ConcludeAppContract(string username)
        {
           
            IInfluencer influencer = influencers.FindByName(username);
            if (influencer == null)
            {
                return string.Format(OutputMessages.InfluencerNotSigned, username);
            }
           
            if (influencer.Participations.Any())
            {
                return string.Format(OutputMessages.InfluencerHasActiveParticipations, username);
            }
            
            influencers.RemoveModel(influencer);
            return string.Format(OutputMessages.ContractConcludedSuccessfully, username);

        }

        public string FundCampaign(string brand, double amount)
        {
          
            ICampaign campaign = campaigns.FindByName(brand);
            if (campaign == null)
            {
                return string.Format(OutputMessages.InvalidCampaignToFund, brand);
            }
            
            
            if (amount <= 0)
            {
                return string.Format(OutputMessages.NotPositiveFundingAmount);
            }
            
            campaign.Gain(amount);
            return string.Format(OutputMessages.CampaignFundedSuccessfully, brand, amount);
        }

        public string RegisterInfluencer(string typeName, string username, int followers)
        {
          
            if (typeName != nameof(BusinessInfluencer) &&
                 typeName != nameof(FashionInfluencer) &&
                 typeName != nameof(BloggerInfluencer))
            {
                return string.Format(OutputMessages.InfluencerInvalidType, typeName);
            }
            
            IInfluencer influencer = influencers.FindByName(username);
            if (influencer != null)
            {
                return string.Format(OutputMessages.UsernameIsRegistered, username, influencers.GetType().Name);
            }
            if (typeName == nameof(BusinessInfluencer))
            {
                influencer = new BusinessInfluencer(username, followers);
            }
            else if (typeName == nameof(FashionInfluencer))
            {
                influencer = new FashionInfluencer(username, followers);
            }
            else
            {
                influencer = new BloggerInfluencer(username, followers);
            }
            
            influencers.AddModel(influencer);
            return string.Format(OutputMessages.InfluencerRegisteredSuccessfully, username);
        }
    }
}
