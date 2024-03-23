using VehiclesExtension;

namespace Vehicles
{
    public class StartUp
    {
        static void Main(string[] args)
        {
          
        	//  "Vehicle {initial fuel quantity} {liters per km} {tank capacity}"

            string[] carInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            double fuelQuantity = double.Parse(carInfo[1]);
            double consumptionPerKm = double.Parse(carInfo[2]);
            double tankCapacity = double.Parse(carInfo[3]);
            Car car = new Car(fuelQuantity, consumptionPerKm,tankCapacity);

            
            string[] truckInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            double truckQuantity = double.Parse(truckInfo[1]);
            double truckConumption = double.Parse(truckInfo[2]);
            double truckTankCapacity = double.Parse(truckInfo[3]);
            Truck truck = new Truck(truckQuantity,truckConumption,truckTankCapacity);

            string[] busInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            double busQuantity = double.Parse(busInfo[1]);
            double busConsumption = double.Parse(busInfo[2]);
            double busCapacity = double.Parse(busInfo[3]);
            Bus bus = new Bus(busQuantity, busConsumption,busCapacity);

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
                        else if(typeVehicle == "Bus")
                        {
                            bus.Drive(distance);
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
                        else if(typeVehicle == "Bus")
                        {
                            bus.Refuel(liters);
                        }
                    }
                    else if( action == "DriveEmpty") 
                    {
                        double distance = double.Parse(commands[2]);
                        bus.DriveEmptyBus(distance);
                    }
                }
                catch (Exception ae)
                {
                    Console.WriteLine(ae.Message);
                }               
                
            }
            Console.WriteLine($"Car: {car.FuelQuantity:f2}");
            Console.WriteLine($"Truck: {truck.FuelQuantity:f2}");
            Console.WriteLine($"Bus: {bus.FuelQuantity:f2}");
           
        }
    }
}
