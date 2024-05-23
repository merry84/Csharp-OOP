using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handball.Models.Contracts;
using Handball.Repositories.Contracts;

namespace Handball.Repositories
{
    public class PlayerRepository :IRepository<IPlayer>
    {
        private List<IPlayer> players;

        public PlayerRepository()
        {
            players = new List<IPlayer>();
        }
        public IReadOnlyCollection<IPlayer> Models => players.AsReadOnly();
        public void AddModel(IPlayer model) => players.Add(model);


        public bool RemoveModel(string name) => players.Remove(players.FirstOrDefault(x=>x.Name == name));


        public bool ExistsModel(string name) => players.Any(x => x.Name == name);

        public IPlayer GetModel(string name) => players.FirstOrDefault(x => x.Name == name);

    }
}
