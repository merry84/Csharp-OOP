using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;

namespace RobotService.Core
{
    public class Controller :IController
    {
        private SupplementRepository supplements;
        private RobotRepository robots;
        public Controller()
        {
            supplements = new SupplementRepository();
            robots = new RobotRepository();

        }
        public string CreateRobot(string model, string typeName)
        {
            
            IRobot robot;
            if (typeName == nameof(DomesticAssistant))
            {
                robot = new DomesticAssistant(model);
            }
            else if (typeName == nameof(IndustrialAssistant))
            {
                robot = new IndustrialAssistant(model);
            }
            else
            {
                return string.Format(OutputMessages.RobotCannotBeCreated, typeName);
            }
            robots.AddNew(robot);
            return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);

        }

        public string CreateSupplement(string typeName)
        {
            ISupplement supplement;
            if (typeName == nameof(SpecializedArm))
            {
                supplement = new SpecializedArm();
            }
            else if (typeName == nameof(LaserRadar))
            {
                supplement = new LaserRadar();
            }
            else
            {
                return string.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }
            supplements.AddNew(supplement);
            return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            
            ISupplement supplement = supplements.Models()
                .FirstOrDefault(x => x.GetType().Name == supplementTypeName);
            
            var selectedModels = robots.Models().Where(x => x.Model == model);
            
            var robotNotUpgrade =
                selectedModels
                    .Where(x => x.InterfaceStandards
                        .All(y => y != supplement.InterfaceStandard));
            var robotsForUpgrade = robotNotUpgrade.FirstOrDefault();// ЗА ЪГРЕЙТ
            // всички са обновени
            if (robotsForUpgrade == null)
            {
                
                return string.Format(OutputMessages.AllModelsUpgraded, model);
            }
           
            robotsForUpgrade.InstallSupplement(supplement);
            supplements.RemoveByName(supplementTypeName);
            return string.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
        }

        public string RobotRecovery(string model, int minutes)
        {
          
            var selectedRobots = robots.Models()
                .Where(x => x.BatteryLevel * 2 < x.BatteryCapacity && x.Model == model);
            int robotsFed = 0;
            foreach (var robot in selectedRobots)
            {
                robot.Eating(minutes);
                
                robotsFed++;
            }

            return string.Format(OutputMessages.RobotsFed, robotsFed);
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            var selectedRobots = robots.Models()
                .Where(y => y.InterfaceStandards
                    .Any(i=>i == intefaceStandard))
                .OrderByDescending(x => x.BatteryLevel);//Order the selected robots by BatteryLevel descending.
          
            if (selectedRobots.Count() == 0)
            {
                return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
            }
            //Find the sum of the BatteryLevel of the selected robots
            int batteryLevelSelectedRobots = selectedRobots.Sum(x => x.BatteryLevel);
        
            // needed."
            if (batteryLevelSelectedRobots < totalPowerNeeded)
            {
                return string.Format(OutputMessages.MorePowerNeeded, serviceName,
                    totalPowerNeeded - batteryLevelSelectedRobots);
            }
         
            int countRobots = 0;
           
            foreach (var robot in selectedRobots)
            {
                    countRobots++;
                if (robot.BatteryLevel >= totalPowerNeeded)
                {
                    robot.ExecuteService(totalPowerNeeded);
                    break;
                }
                else
                {
                    totalPowerNeeded-=robot.BatteryLevel;
                    robot.ExecuteService(robot.BatteryLevel);
                }
            }

            return string.Format(OutputMessages.PerformedSuccessfully, serviceName, countRobots);
        }

        public string Report()
        {
        
            var selectedRobots = robots.Models()
                .OrderByDescending(x => x.BatteryLevel)
                .ThenBy(x => x.BatteryCapacity);
            var sb = new StringBuilder();

            foreach (var robot in selectedRobots)
            {
                sb.AppendLine(robot.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
