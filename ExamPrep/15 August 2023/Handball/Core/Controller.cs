using Handball.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Repositories.Contracts;
using Handball.Utilities.Messages;

namespace Handball.Core
{
    public class Controller : IController
    {
        private IRepository<IPlayer> players;
        private IRepository<ITeam> teams;

        public Controller()
        {
            players = new PlayerRepository();
            teams = new TeamRepository();
        }
        public string NewTeam(string name)
        {
            if (teams.ExistsModel(name))
                return string.Format(OutputMessages.TeamAlreadyExists, name, nameof(TeamRepository));

            teams.AddModel(new Team(name));
            return string.Format(OutputMessages.TeamSuccessfullyAdded, name, nameof(TeamRepository));
        }


        public string NewPlayer(string typeName, string name)
        {
            
            if (typeName != nameof(Goalkeeper) 
                && typeName != nameof(CenterBack) 
                && typeName != nameof(ForwardWing))
               return string.Format(OutputMessages.InvalidTypeOfPosition, typeName);

           
            if (players.ExistsModel(name))
            {
                string position = this.players.GetModel(name).GetType().Name;

                return string.Format(OutputMessages.PlayerIsAlreadyAdded, name, nameof(PlayerRepository), position);
            }
            
            IPlayer player;
            if (typeName == nameof(Goalkeeper)) player = new Goalkeeper(name);
            else if (typeName == nameof(CenterBack)) player = new CenterBack(name);
            else player = new ForwardWing(name);

            players.AddModel(player);
            return string.Format(OutputMessages.PlayerAddedSuccessfully, name);
        }

        public string NewContract(string playerName, string teamName)
        {
            if (!players.ExistsModel(playerName))
                return string.Format(OutputMessages.PlayerNotExisting, playerName, nameof(PlayerRepository));
            if (!teams.ExistsModel(teamName))
                return string.Format(OutputMessages.TeamNotExisting, teamName, nameof(TeamRepository));

            IPlayer player = players.GetModel(playerName);
            ITeam team = teams.GetModel(teamName);
            if (player.Team != null)
                return string.Format(OutputMessages.PlayerAlreadySignedContract, playerName, player.Team);
            player.JoinTeam(playerName);
            team.SignContract(player);
            return string.Format(OutputMessages.SignContract, playerName, teamName);
        }

        public string NewGame(string firstTeamName, string secondTeamName)
        {
            ITeam firtsTeam = teams.GetModel(firstTeamName);
            ITeam secondTeam = teams.GetModel(secondTeamName);
            if (firtsTeam.OverallRating != secondTeam.OverallRating)
            {
                ITeam win;
                ITeam lose;
                if (firtsTeam.OverallRating > secondTeam.OverallRating)
                {
                    win = firtsTeam;
                    lose = secondTeam;
                }
                else
                {
                    win = secondTeam;
                    lose = firtsTeam;
                }
                win.Win();
                lose.Lose();

                return string.Format(OutputMessages.GameHasWinner, win.Name, lose.Name);
            }
            else
            {
                firtsTeam.Draw();
                secondTeam.Draw();
                return string.Format(OutputMessages.GameIsDraw, firstTeamName, secondTeamName);
            }
        }

        public string PlayerStatistics(string teamName)
        {
           
           var sb = new StringBuilder();
           sb.AppendLine($"***{teamName}***");
           ITeam team = teams.GetModel(teamName);
           foreach (var player in team.Players.OrderByDescending(r=>r.Rating).ThenBy(n=>n.Name))
           {
               sb.AppendLine(player.ToString());
           }
           return sb.ToString().TrimEnd();  
        }

        public string LeagueStandings()
        {
            var sb = new StringBuilder();
            sb.AppendLine("***League Standings***");

            foreach (var team in teams.Models
                         .OrderByDescending(p=>p.PointsEarned)
                         .ThenByDescending(o=>o.OverallRating)
                         .ThenBy(n=>n.Name))
            {
                sb.AppendLine(team.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
