using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handball.Models.Contracts;
using Handball.Utilities.Messages;

namespace Handball.Models
{
    public abstract class Player :IPlayer
    {
        private string name;
        private double rattng;
        private string team;

        protected Player(string name,double rating)
        {
            Name = name;
            Rating = rating;
        }

        public string Name
        {
            get=>name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) string.Format(ExceptionMessages.PlayerNameNull);
                name = value;
            }
        }
        public double Rating { get; protected set; }
        public string Team { get; private set; }
        public void JoinTeam(string name)
        {
          
            this.Team = name;
        }
        public abstract void IncreaseRating();
        public abstract void DecreaseRating();
        public override string ToString()
        {
            
            var sb = new StringBuilder();
            sb.AppendLine($"{GetType().Name}: {Name}");
            sb.AppendLine($"--Rating: {Rating}");
            return sb.ToString().TrimEnd();
        }
    }
}
