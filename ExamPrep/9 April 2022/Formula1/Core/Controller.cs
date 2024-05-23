using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Repositories.Contracts;
using Formula1.Utilities;

namespace Formula1.Core
{
    public class Controller : IController
    {
        //•	pilotRepository - PilotRepository 
        // •	raceRepository - RaceRepository 
        // •	carRepository - FormulaOneCarRepository 
        private IRepository<IPilot> pilotRepository;
        private IRepository<IRace> raceRepository;
        private IRepository<IFormulaOneCar> formulaOneCarRepository;

        public Controller()
        {
            pilotRepository = new PilotRepository();
            raceRepository = new RaceRepository();
            formulaOneCarRepository = new FormulaOneCarRepository();
        }
        public string CreatePilot(string fullName)
        {
            //Adds a Pilot to the PilotRepository.
            // •	If a pilot with the given full name exists, throw an InvalidOperationException with the following message: "Pilot { full name } is already created."
            // •	If the Pilot is added successfully to the repository, return the following message: "Pilot { full name } is created."
            IPilot pilot = pilotRepository.FindByName(fullName);
            if (pilot != null)
                return string.Format(ExceptionMessages.PilotExistErrorMessage, fullName);
            pilotRepository.Add(new Pilot(fullName));
            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            //Creates a formula one car with the given parameters and adds it to the FormulaOneCarRepository. Valid types are: "Ferrari" and "Williams":
            // •	If a car with the given model exists, throw an InvalidOperationException with the  message: "Formula one car { model } is already created."
            // •	If the car type is invalid, throw an InvalidOperationException with the  message: "Formula one car type { type } is not valid."
            // •	If no errors are thrown, return a string with the following message: "Car { type }, model { model } is created."
            IFormulaOneCar car1 = formulaOneCarRepository.FindByName(model);
            if (car1 != null)
                return string.Format(ExceptionMessages.CarExistErrorMessage, model);
            IFormulaOneCar car;
            if (type == nameof(Ferrari))
                car = new Ferrari(model, horsepower, engineDisplacement);
            else if (type == nameof(Williams))
                car = new Williams(model, horsepower, engineDisplacement);
            else
            {
                return string.Format(ExceptionMessages.InvalidTypeCar, type);
            }
            formulaOneCarRepository.Add(car);
            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            //Creates a race with the given name, number of laps and adds it to the RaceRepository:
            // •	If a race with the given race name exists, throw an InvalidOperationException with the following message: "Race { race name } is already created."
            // •	If no errors are thrown, return a string with the following message: "Race { race name } is created."
            IRace race = raceRepository.FindByName(raceName);
            if (race != null)
                return string.Format(ExceptionMessages.RaceExistErrorMessage, raceName);
            raceRepository.Add(new Race(raceName, numberOfLaps));
            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            //Adds a car with the given car model to a pilot with the given name. After successfully adding a car to a pilot, remove the car from the FormulaOneCarRepository:
            // •	If the pilot does not exist, or the pilot already has a car, throw an InvalidOperationException with the following message: "Pilot { pilot name } does not exist or has a car."

            // •	If the car model does not exist, throw a NullReferenceException with the following message: "Car { model } does not exist."

            // •	If no errors are thrown, return a string with the following message: "Pilot { pilot name } will drive a {type of car} { model } car."
            IPilot pilot = pilotRepository.FindByName(pilotName);
            if (pilot == null || pilot.Car != null)
                return string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName);

            IFormulaOneCar car = formulaOneCarRepository.FindByName(carModel);
            if (car == null)
                return string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel);
            pilot.AddCar(car);
            formulaOneCarRepository.Remove(car);
            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            //Adds a pilot with the given name, to the race with the given race name.
            // •	If the race does not exist, throw a NullReferenceException with the following message: "Race { race name } does not exist."
            // •	If the pilot does not exist, or the pilot can not race, or the pilot is already in the race,
            // throw an InvalidOperationException with the following message: "Can not add pilot { pilot full name } to the race."
            // •	If no errors are thrown, return a string with the following message: "Pilot { pilot full name } is added to the { race name } race."
            IRace race = raceRepository.FindByName(raceName);
            if (race == null)
                return string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName);
            IPilot pilot = pilotRepository.FindByName(pilotFullName);
            if (pilot == null || pilot.CanRace == false || race.Pilots.Any(x => x.FullName == pilotFullName))
                //If the pilot does not exist, or the pilot can not race, or the pilot is already in the race

                return string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName);
            race.AddPilot(pilot);
            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);

        }

        public string StartRace(string raceName)
        {
           // •	If the race does not exist, throw a NullReferenceException with the following message: "Race { race name } does not exist."

           // •	If the race has less than 3 pilots, throw an InvalidOperationException with the following message: "Race { race name } cannot start with less than three participants."

           // •	If the race has been already executed, throw an InvalidOperationException with the following message: "Can not execute race { race name }."

           // •	If no errors are thrown, return a string with the following message: 
           // "Pilot { pilot full name } wins the { race name } race.
           // Pilot { pilot full name } is second in the { race name } race.
           // Pilot { pilot full name } is third in the { race name } race."
           IRace race = raceRepository.FindByName(raceName);
           if (race == null)
               return string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName);
           if (race.Pilots.Count < 3)
               return string.Format(ExceptionMessages.InvalidRaceParticipants, raceName);
           if (race.TookPlace)
               return string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName);
           //If everything is valid, you should arrange for all pilots in the given race to start racing. As a result, this method returns the three fastest pilots.
           //To execute the race you should sort all riders in descending order by the result of the RaceScoreCalculator method in FormulaOneCar object.
           //, increase the winner's score, and return the corresponding message.
           var selectedPilots = race.Pilots
               .OrderByDescending(x => x.Car.RaceScoreCalculator(race.NumberOfLaps))
               .Take(3)//three fastest pilots.
               .ToList();
           //In the end, if everything is valid set the race's TookPlace property to true
           race.TookPlace = true;
            //increase the winner's score
            selectedPilots.First().WinRace();

            var sb = new StringBuilder();
            for (int i = 1; i <= selectedPilots.Count; i++)
            {
                if (i == 1)
                {
                    sb.AppendLine($"Pilot {selectedPilots[i].FullName} wins the {race.RaceName} race.");
                }
                else if (i == 2)
                {
                    sb.AppendLine($"Pilot {selectedPilots[i].FullName} is second in the {race.RaceName} race.");
                }
                else
                {
                    sb.AppendLine($"Pilot {selectedPilots[i].FullName} is third in the {race.RaceName} race.");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string RaceReport()
        {
            //Returns information about each race that has been executed. You can use the RaceInfo method in the Race class.
            // "The { race name } race has:
            // Participants: { number of participants }
            // Number of laps: { number of laps }
            // Took place: Yes
            // The { race name } race has:
            // Participants: { number of participants }
            // Number of laps: { number of laps }
            // Took place: Yes
            var sb = new StringBuilder();
            foreach (var race in raceRepository.Models.Where(x=>x.TookPlace))
            {
                sb.AppendLine(race.RaceInfo());
            }
            return sb.ToString().TrimEnd();
        }

        public string PilotReport()
        {
            //Returns information about each pilot, ordered by the number of wins descending. You can use the override ToString method in the Pilot class.
            // "Pilot {FullName} has {NumberOfWins} wins.
            // Pilot {FullName} has {NumberOfWins} wins.
            // (…)"
            var sb = new StringBuilder();
            foreach (var pilot in pilotRepository.Models.OrderByDescending(x=>x.NumberOfWins))
            {
                sb.AppendLine($"Pilot {pilot.FullName} has {pilot.NumberOfWins} wins.");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
