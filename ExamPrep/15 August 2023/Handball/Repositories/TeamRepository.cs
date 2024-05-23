using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handball.Models.Contracts;
using Handball.Repositories.Contracts;

namespace Handball.Repositories
{
    public class TeamRepository : IRepository<ITeam>
    {
        private List<ITeam> teams;

        public TeamRepository()
        {
            teams = new List<ITeam>();
        }
        public IReadOnlyCollection<ITeam> Models => teams.AsReadOnly();
        public void AddModel(ITeam model) => teams.Add(model);


        public bool RemoveModel(string name) => teams.Remove(teams.FirstOrDefault(n => n.Name == name));


        public bool ExistsModel(string name) => teams.Any(x => x.Name == name);


        public ITeam GetModel(string name) => teams.FirstOrDefault(x => x.Name == name);

    }
}
