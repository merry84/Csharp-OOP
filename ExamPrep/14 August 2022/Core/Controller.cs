using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private readonly PlanetRepository planets;

        public Controller()
        {
            planets = new PlanetRepository();
        }
        public string CreatePlanet(string name, double budget)
        {
            //Creates a planet with the provided name and budget. 
            // •	If a Planet with the same name is already created, return the following message: "Planet {planetName} is already added!" 
            // •	If the planet is valid, keep it in the repository of planets and return the following message: "Successfully added Planet: {planetName}"
            IPlanet planet = new Planet(name, budget);
            if (planets.FindByName(name) != default)//ExistingPlanet
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }
            else
            {
                planets.AddItem(planet);
                return string.Format(OutputMessages.NewPlanet, name);
            }
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            // 	If a Planet with the given name doesn’t exist in the PlanetReposotiry,
            // throw an InvalidOperationException with the following message: "Planet {planetName} does not exist!"

            // 	If the MilitaryUnit is not available in our application (no such type of MilitaryUnit exists in the child classes),
            // throw an InvalidOperationException with the following message: "{unitTypeName} still not available!"

            // 	If the same MilitaryUnit is already added, throw an InvalidOperationException with the following message
            // "{unitTypeName} already added to the Army of {planetName}!"

            // 	If the MilitaryUnit is valid, add it to the UnitRepository of the planet.
            // Planet’s Budget is reduced with the price of the unit and the following message is returned:
            // "{unitTypeName} added successfully to the Army of {planetName}!"
            IPlanet planet = planets.FindByName(planetName);
            if (planet == default)//not exist!
            {
                return string.Format(ExceptionMessages.UnexistingPlanet, planetName);
            }
            if (unitTypeName != nameof(AnonymousImpactUnit)
                && unitTypeName != nameof(SpaceForces)
                && planetName != nameof(StormTroopers))
            {
                return string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName);
            }

            if (planet.Army.Any(x => x.GetType().Name == unitTypeName))
            {
                return string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName);
            }

            IMilitaryUnit unit;
            if (unitTypeName == nameof(SpaceForces))
            {
                unit = new SpaceForces();
            }
            else if (unitTypeName == nameof(StormTroopers))
            {
                unit = new StormTroopers();
            }
            else
            {
                unit = new AnonymousImpactUnit();
            }
            planet.Spend(unit.Cost);
            planet.AddUnit(unit);
            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            //Creates a Weapon from the given type and adds it to the Weapons of the Planet with the given name.
            //Every weapon is unique. A Planet can have only one Weapon from a specific type:
            // 	If a Planet with the given name doesn’t exist in the PlanetRepository,
            // throw an InvalidOperationException with the following message: "Planet {planetName} does not exist!"

            // If the same Weapon is already added, throw an InvalidOperationException with the following message:
            // "{weaponTypeName} already added to the Weapons of {planetName}!"

            // If the Weapon is not available in our application (no such type of Weapon exists in the child classes),
            // throw an InvalidOperationException with the following message: "{weaponTypeName} still not available!"

            // If the Weapon is valid, add it to the WeaponRepository of the planet.
            // Planet’s Budget is reduced with the price of the weapon and the following message is returned: "{planetName} purchased {weaponTypeName}!"
            IPlanet planet = planets.FindByName(planetName);
            if (planet == default)//not exist!
            {
                return string.Format(ExceptionMessages.UnexistingPlanet, planetName);
            }

            if (planet.Weapons.Any(x => x.GetType().Name == weaponTypeName))
            {
                return string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName);
            }

            if (weaponTypeName != nameof(SpaceMissiles)
                && weaponTypeName != nameof(NuclearWeapon)
                && weaponTypeName != nameof(BioChemicalWeapon))
            {
                return string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName);

            }
            IWeapon weapon;
            if (weaponTypeName == nameof(SpaceMissiles))
            {
                weapon = new SpaceMissiles(destructionLevel);
            }
            else if (weaponTypeName == nameof(NuclearWeapon))
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }

            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);
            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string SpecializeForces(string planetName)
        {
            //Increases the EnduranceLevel of the Army of the specific Planet:
            //If a Planet with the given name doesn't exist, throw an InvalidOperationException with the following message:
            //"Planet {planetName} does not exist!" 

            //If there are no Military units added still, throw an InvalidOperationException with the following message
            //"No units available for upgrade!"

            //If the action is completed successfully, reduce the Budget by 1.25 billion QUID,
            //train the army of the given Planet and return the following message: "{planetName} has upgraded its forces!".
            IPlanet planet = planets.FindByName(planetName);
            if (planet == default)//not exist!
            {
                return string.Format(ExceptionMessages.UnexistingPlanet, planetName);
            }

            if (planet.Army.Count == 0)
            {
                return string.Format(ExceptionMessages.NoUnitsFound);
            }

            double cost = 1.25;
            planet.TrainArmy();
            planet.Spend(cost);
            return string.Format(OutputMessages.ForcesUpgraded, planetName);

        }

        public string SpaceCombat(string planetOne, string planetTwo)
        { // If both have the same MilitaryPower the winner is the Planet that owns NuclearWeapon
            // If both have the same MilitaryPower and both own NuclearWeapon OR both do NOT own NuclearWeapon,
            // no one wins the war and both lose half of their Budget. The following message should be returned:
            // "The only winners from the war are the ones who supply the bullets and the bandages!"

            // The winner loses half of its own Budget. Then takes half of the Budget left from the losing planet.

            // The winner also adds the sum of all forces costs and weapons prices possessed by the losing planet to its Budget.

            // The losing planet is deleted from the PlanetRepository of the Universe.

            // The following message is returned: "{winningPlanetName} destructed {losingPlanetName}!" 
            IPlanet planet1 = planets.FindByName(planetOne);
            IPlanet planet2 = planets.FindByName(planetTwo);

            double halfBudgetPlanet1 = planet1.Budget / 2;
            double halfBudgetPlanet2 = planet2.Budget / 2;

            double calculateValuePlanet1 = planet1.Army.Sum(x => x.Cost) + planet1.Weapons.Sum(x => x.Price);   
            double calculateValuePlanet2 = planet2.Army.Sum(x => x.Cost) + planet2.Weapons.Sum(x => x.Price);

            double planet1Ratio = planet1.MilitaryPower;
            double planet2Ratio = planet2.MilitaryPower;

            bool hasNuclearPlanet1 = planet1.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon));
            bool hasNuclearPlanet2 = planet2.Weapons.Any(x=>x.GetType().Name == nameof(NuclearWeapon));

            var NuclearPlanet1 = planet1.Weapons.FirstOrDefault(x => x.GetType().Name == nameof(NuclearWeapon));
            var NuclearPlanet2 = planet2.Weapons.FirstOrDefault(x => x.GetType().Name == nameof(NuclearWeapon));

            if (planet1Ratio > planet2Ratio)
            {
                planet1.Spend(halfBudgetPlanet1);
                planet1.Profit(halfBudgetPlanet2);
                planet1.Profit(calculateValuePlanet1);
                planets.RemoveItem(planet1.Name);
                return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
            }
            else if (planet2Ratio > planet1Ratio)
            {
                planet2.Spend(halfBudgetPlanet2);
                planet2.Profit(halfBudgetPlanet2);
                planet2.Profit(calculateValuePlanet2);
                planets.RemoveItem(planet2.Name);
                return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
            }
            else
            {
                if (NuclearPlanet1 != null && NuclearPlanet2 != null)
                {

                    planet1.Spend(halfBudgetPlanet1);
                    planet2.Spend(halfBudgetPlanet2);
                    return string.Format(OutputMessages.NoWinner);
                }
                else if (NuclearPlanet2 != null)
                {
                    planet2.Profit(halfBudgetPlanet1);
                    planet2.Profit(calculateValuePlanet1);

                    planets.RemoveItem(planet1.Name);
                    return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);

                }
                else if (NuclearPlanet1 != null)
                {
                    planet1.Spend(halfBudgetPlanet1);
                    planet1.Profit(halfBudgetPlanet2);
                    planet1.Profit(calculateValuePlanet2);

                    planets.RemoveItem(planet2.Name);
                    return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);

                }
                else
                {
                    planet1.Spend(halfBudgetPlanet1);
                    planet2.Spend(halfBudgetPlanet2);
                    return string.Format(OutputMessages.NoWinner);

                }
            }
        }

        public string ForcesReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");
            foreach (var planet in planets.Models.OrderByDescending(x=>x.MilitaryPower).ThenBy(x=>x.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
