﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;

namespace HighwayToPeak.Repositories
{
    public class PeakRepository : IRepository<IPeak>
    {
        private List<IPeak> peaks = new List<IPeak>();
        public IReadOnlyCollection<IPeak> All => peaks.AsReadOnly();
        public void Add(IPeak model) => peaks.Add(model);


        public IPeak Get(string name) => peaks.FirstOrDefault(n => n.Name == name);

    }
    
}
