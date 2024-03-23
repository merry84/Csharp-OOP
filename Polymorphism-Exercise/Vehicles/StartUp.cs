namespace Vehicles
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            //"Car {fuel quantity} {liters per km}"
            string[] carInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            double fuelQuantity = double.Parse(carInfo[1]);
            double consumptionPerKm = double.Parse(carInfo[2]);
            Car car = new Car(fuelQuantity, consumptionPerKm);

            //"Truck {fuel quantity} {liters per km}"
            string[] truckInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            double truckQuantity = double.Parse(truckInfo[1]);
            double truckConumption = double.Parse(truckInfo[2]);
            Truck truck = new Truck(truckQuantity,truckConumption);

            //the number of commands N that will be given on the next N lines
            int numberOfCommands = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfCommands; i++)
            {
                try
                {
                    string[] commands = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string action = commands[0];
                    string typeVehicle = commands[1];
                    if(action == "Drive")
                    {
                        double distance = double.Parse(commands[2]);
                        if(typeVehicle == "Car")
                        {
                            car.Drive(distance);
                        }
                        else if(typeVehicle == "Truck")
                        {
                            truck.Drive(distance);
                        }
                    }
                    else if(action == "Refuel")
                    {
                        double liters = double.Parse(commands[2]);
                        if(typeVehicle == "Car")
                        {
                            car.Refuel(liters);
                        }
                        else if(typeVehicle == "Truck")
                        {
                            truck.Refuel(liters);
                        }
                    }
                }
                catch (Exception ae)
                {
                    Console.WriteLine(ae.Message);
                   
                }
                
                
            }
            Console.WriteLine(car.ToString());
            Console.WriteLine(truck.ToString());
            /*•	On the next N lines – commands in the format:
            	"Drive Car {distance}"
            	"Drive Truck {distance}"
            	"Refuel Car {liters}"
            	"Refuel Truck {liters}"
            */
        }
    }
}
