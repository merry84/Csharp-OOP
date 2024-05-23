using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;

namespace Formula1.Repositories
{
    public class FormulaOneCarRepository:IRepository<IFormulaOneCar>
    {
        private List<IFormulaOneCar> formulaOneCars;

        public FormulaOneCarRepository()
        {
            formulaOneCars = new List<IFormulaOneCar>();
        }
        public IReadOnlyCollection<IFormulaOneCar> Models => formulaOneCars.AsReadOnly();
        public void Add(IFormulaOneCar model)
            => formulaOneCars.Add(model);

        public bool Remove(IFormulaOneCar model)
            => formulaOneCars.Remove(model);

        IFormulaOneCar IRepository<IFormulaOneCar>.FindByName(string name)
            => formulaOneCars.FirstOrDefault(x => x.Model == name);
           
    }
}
