using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handball.Models.Contracts;
using Handball.Utilities.Messages;

namespace Handball.Models
{
    public class Team : ITeam

    {
        private string name;
        private int pointsEarned;
        private List<IPlayer> players;

        public Team(string name)
        {
            Name = name;
            pointsEarned =0;
            players = new List<IPlayer>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) string.Format(ExceptionMessages.TeamNameNull);
                name = value;
            }
        }

        public int PointsEarned => pointsEarned;

        public double OverallRating 
            => this.Players.Count == 0 ? 0// aко няма играчи върни нула
                : Math.Round(this.players.Average(p => p.Rating), 2);//иначе върни рейтинга закръглен до 2 знак
            

        public IReadOnlyCollection<IPlayer> Players => players.AsReadOnly();

        public void Draw()
        {//The Draw() method increases the PointsEarned property by 1 point.
         //It also increases the rating of the goalkeeper player competing for the team. Each team will always have only one goalkeeper filed.
            pointsEarned += 1;
            Players.FirstOrDefault
                (x=>x.GetType().Name == nameof(Goalkeeper))
                .IncreaseRating();
        }

        public void Lose()
        {
            foreach (var player in Players)
            {
             player.DecreaseRating();
            }
        }

        public void SignContract(IPlayer player)
        {
            players.Add(player);
        }

        public void Win()
        {
            pointsEarned += 3;
            foreach (var player in Players)
            {
                player.IncreaseRating();
            }
        }

        public override string ToString()
        {
            
            var sb = new StringBuilder();
            sb.AppendLine($"Team: {Name} Points: {PointsEarned}");
            sb.AppendLine($"--Overall rating: {OverallRating}");
            sb.Append("--Players: ");
            if (players.Any())
            {
                var names = Players.Select(x => x.Name);
                sb.Append(string.Join(", ", names));
            }
            else
            {
                sb.Append("none");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
