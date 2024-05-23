using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;

namespace RobotService.Models
{
    public abstract class Robot :IRobot
    {
        private string model;
        private int batteryCapacity;
        private int batteryLevel;
        private int converrtionCapacityIndex;
        private List<int> interfaceStandards;

        protected Robot(string model, int batteryCapacity, int conversionCapacityIndex)
        {
            Model = model;
            BatteryCapacity = batteryCapacity;
            this.batteryLevel = batteryCapacity;
            this.converrtionCapacityIndex = conversionCapacityIndex;
            interfaceStandards = new List<int>();
        }

        public string Model
        {
            get=>model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.ModelNullOrWhitespace);
                }
                model = value;
            }
        }

        public int BatteryCapacity
        {
            get=> batteryCapacity;
            private set
            {
                if (value < 0)
                {
                    string.Format(ExceptionMessages.BatteryCapacityBelowZero);
                }
                batteryCapacity = value;
            }
        }

        public int BatteryLevel => this.batteryLevel;
        public int ConvertionCapacityIndex => this.converrtionCapacityIndex;
        public IReadOnlyCollection<int> InterfaceStandards => interfaceStandards.AsReadOnly();
        public virtual void Eating(int minutes)
        {
            var totalCapacity = converrtionCapacityIndex * minutes;
            if (totalCapacity > BatteryCapacity - batteryLevel)
            {
                batteryLevel = batteryCapacity;
            }
            else
            {
                batteryLevel += totalCapacity;
            }
        }

        public void InstallSupplement(ISupplement supplement)
        {
            batteryCapacity -= supplement.BatteryUsage;
            batteryLevel -= supplement.BatteryUsage;
            interfaceStandards.Add(supplement.InterfaceStandard);
        }

        public bool ExecuteService(int consumedEnergy)
        {
            if (batteryLevel >= consumedEnergy)
            {
                batteryLevel-=consumedEnergy;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
           
            var sb = new StringBuilder();
            sb.AppendLine($"{GetType().Name} {Model}:");
            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");
            sb.Append($"--Supplements installed: ");
            if (this.InterfaceStandards.Count == 0)
            {
                sb.Append("none");
            }
            else
            {
                sb.Append(string.Join(" ", InterfaceStandards));
            }
            return sb.ToString().TrimEnd();
        }
    }
}
