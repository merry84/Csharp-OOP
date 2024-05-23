using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1.Models.Contracts;
using Formula1.Utilities;

namespace Formula1.Models
{
    public class Race : IRace
    {
        private string raceName;
        private int numberOfLaps;
        private readonly List<IPilot> pilots;

        public Race(string raceName, int numberOfLaps)
        {
            RaceName = raceName;
            NumberOfLaps = numberOfLaps;
            TookPlace = false;
            pilots = new List<IPilot>();
        }

        public string RaceName
        {
            get => raceName;
            private set
            {
                //o	If the race name is null, white space or the length is less than 5 symbols,
                //throw an ArgumentException with the  message: "Invalid race name: { race name }."
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    string.Format(ExceptionMessages.InvalidRaceName, value);
                }
                raceName = value;
            }
        }

        public int NumberOfLaps
        {
            get => numberOfLaps;
            private set
            {
                //	If the number of laps is less than 1, throw an ArgumentException with the message: "Invalid lap numbers: { number of laps }."
                if (value < 1)
                    string.Format(ExceptionMessages.InvalidLapNumbers, value);

                numberOfLaps = value;
            }
        }
        public bool TookPlace { get; set; }
        public ICollection<IPilot> Pilots => pilots.AsReadOnly();
        public void AddPilot(IPilot pilot)
        {
            pilots.Add(pilot);
        }

        public string RaceInfo()
        {
           //Returns a string with information about the race in the format below:
           // "The { race name } race has:
           // Participants: { number of participants }
           // Number of laps: { number of laps }
           // Took place: { Yes/No }"
           var sb = new StringBuilder();
           sb.AppendLine($"The {RaceName} race has:");
           sb.AppendLine($"Participants: {pilots.Count}");
           sb.AppendLine($"Number of laps: {NumberOfLaps}");
           string tookPlaceString = TookPlace ? "Yes" : "No";
           sb.AppendLine($"Took place: {tookPlaceString}");
            return sb.ToString().TrimEnd();
        }
    }
}
