using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Utilities.Messages;

namespace HighwayToPeak.Models
{
    public abstract class Climber :IClimber
    {
        private string name;
        private int stamina;
        private List<string> conqueredPeaks;

        protected Climber(string name, int stamina)
        {
            Name = name;
            Stamina = stamina;
            this.conqueredPeaks = new List<string>();
        }

        public string Name
        {
            get=>name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ClimberNameNullOrWhiteSpace);
                }
                name = value;
            }
            
        }

        public int Stamina
            //The climber's stamina, in the mountains. Must be a value from 0 up to 10, both inclusive.
            // If it exceeds 10 during any operation, it should be reset to 10.
            // If it drops below zero during any operation, it should be reset to zero.
        {
            get=> stamina;
            protected set
            {
                if (value < 0)
                {
                    stamina = 0;
                }
                else if (value > 10)
                {
                    stamina = 10;
                }
                else
                {
                    stamina = value;
                }
            }
        }

        public IReadOnlyCollection<string> ConqueredPeaks => conqueredPeaks.AsReadOnly();
        public void Climb(IPeak peak)
            //The method will add the given peakName to the collection of ConqueredPeaks,
            //if the peak is already climbed by the specific climber, do NOT duplicate the peakName in the collection of ConqueredPeaks,
            //but climber can climb a peak unlimited times. The method will always decrease the climber’s Stamina by:
            //•	If the peak’s DifficultyLevel is "Extreme" - 6 units ;
            // •	If the peak’s DifficultyLevel is "Hard" - 4 units ;
            // •	If the peak’s DifficultyLevel is "Moderate" - 2 units ;
        {
            if(!conqueredPeaks.Contains(peak.Name)){conqueredPeaks.Add(peak.Name);}

            int tempStamina = 0;
            if (peak.DifficultyLevel == "Extreme")
            {
                tempStamina += 6;
            }
            else if (peak.DifficultyLevel == "Hard")
            {
                tempStamina += 4;
            }
            else
            {
                tempStamina += 2;
            }
            Stamina = tempStamina;
        }

        public abstract void Rest(int daysCount);
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{GetType().Name} - Name: {Name}, Stamina: {Stamina}");
            sb.AppendLine($"Peaks conquered: ");
            if (conqueredPeaks.Count > 0)
            {
                sb.AppendLine($"{ConqueredPeaks.Count}");

            }
            else
            {
                sb.AppendLine("no peaks conquered");}
            return sb.ToString().Trim();
        }
    }
}
