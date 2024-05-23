using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Formula1.Models.Contracts;
using Formula1.Utilities;

namespace Formula1.Models
{
    public class Pilot : IPilot
    {
        private string fullName;
        
        public Pilot(string fullName)
        {
            FullName = fullName;
            CanRace = false;

        }

        public string FullName
        {
            get => fullName;
            private set
            {
                //If the pilot's full name is null, white space or the length is less than 5 symbols,
                //throw an ArgumentException with the  message: "Invalid pilot name: { fullName }."
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    string.Format(ExceptionMessages.InvalidPilot, value);
                }
                fullName = value;
            }
        }
        //	If the car is null throw a NullReferenceException with the message: "Pilot car can not be null."
        public IFormulaOneCar Car
        {
            get ;
            private set
            ;

        }

        public int NumberOfWins
        {
            get ;
            private set ;
        }
        public bool CanRace { get; private set; }

        public void AddCar(IFormulaOneCar car)
        {
            //Sets a car to the pilot, and sets CanRace to true.
            if (car == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidCarForPilot);
            }

            Car = car;
            CanRace = true;
            
        }

        public void WinRace()
        {
            //The WinRace method increases the NumberOfWins by one (1) every time a pilot wins a race.
            NumberOfWins++;
        }

        public override string ToString()=> $"Pilot {FullName} has {NumberOfWins} wins.";
        
    }
}
