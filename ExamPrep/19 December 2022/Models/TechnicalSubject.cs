using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityCompetition.Models
{
    public class TechnicalSubject : Subject
    {
        //TechnicalSubject has a constant value for subjectRate = 1.3
        private const double rate = 1.30;
        public TechnicalSubject(int id, string name) : base(id, name, rate)
        {
        }
    }
}
