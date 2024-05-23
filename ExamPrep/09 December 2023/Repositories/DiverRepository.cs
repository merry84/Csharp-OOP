﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories.Contracts;

namespace NauticalCatchChallenge.Repositories
{
    public class DiverRepository : IRepository<IDiver>
    {
        private List<IDiver> divers;

        public DiverRepository()
        {
            divers = new List<IDiver>();
        }
        public IReadOnlyCollection<IDiver> Models => divers.AsReadOnly();
        public void AddModel(IDiver model)
        {
            this.divers.Add(model);
        }

        public IDiver GetModel(string name)=>
            Models.FirstOrDefault(x=>x.Name == name);
        
    }
}
