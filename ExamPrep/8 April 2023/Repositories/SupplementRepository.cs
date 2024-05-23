using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;

namespace RobotService.Repositories
{
    public class SupplementRepository :IRepository<ISupplement>
    {
        private List<ISupplement> supplements;

        public SupplementRepository()
        {
            supplements = new List<ISupplement>();
        }

        public IReadOnlyCollection<ISupplement> Models()
            => supplements.AsReadOnly();

        public void AddNew(ISupplement model)
            => supplements.Add(model);

        public bool RemoveByName(string typeName)
            => supplements.Remove(supplements
                .FirstOrDefault(x => x.GetType().Name == typeName));

        public ISupplement FindByStandard(int interfaceStandard)
            => supplements.FirstOrDefault(x => x.InterfaceStandard == interfaceStandard);
    }
}
