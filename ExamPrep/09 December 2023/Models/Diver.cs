using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Utilities.Messages;

namespace NauticalCatchChallenge.Models
{
    public abstract class Diver : IDiver
    {
        private string name;
        private int oxygenLevel;
        private List<string> @catch;
        private bool hasHealthIssues;
        private double competitionPoints;


        public Diver(string name, int oxygenLevel)
        {
            Name = name;
            OxygenLevel = oxygenLevel;
            @catch = new List<string>();
            competitionPoints = 0;//Set the initial value of the property to zero.
            hasHealthIssues = false;//Its initial value is False, representing that the diver starts in a healthy state.
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(ExceptionMessages.DiversNameNull)) ;
                name = value;
            }
        }
        public int OxygenLevel
        {
            get => oxygenLevel;
            protected set
            {
                if (oxygenLevel < 0) oxygenLevel = 0;
                else
                {
                    oxygenLevel = value;
                    
                }
            }

        }
        public IReadOnlyCollection<string> Catch => @catch;
        //o	Set the initial value of the property to zero. Returns a floating-point number rounded to the first decimal place.
        //Represents the total points accumulated by a diver, based on the type of fish caught during the competition. 
        public double CompetitionPoints => competitionPoints;
        public bool HasHealthIssues => hasHealthIssues;
        public void Hit(IFish fish)
        {
            OxygenLevel -= fish.TimeToCatch;
            @catch.Add(fish.Name);
            competitionPoints += fish.Points;
        }

        public abstract void Miss(int TimeToCatch);
        public abstract void RenewOxy();

        public void UpdateHealthStatus()
        {
            if (!HasHealthIssues) { hasHealthIssues = true; }
            else
            {
                hasHealthIssues = false;
            }
        }

        public override string ToString()
        {
            return $"Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {Catch.Count}, Points earned: {CompetitionPoints} ]";
        }
    }
}
