using NUnit.Framework;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckCapacityGarage()
        {
            Garage garage = new Garage(2);
            Vehicle car = new Vehicle("opel", "corsa", "55828557");
            Vehicle truck = new Vehicle("man", "ipon", "5888557");
            Vehicle bike = new Vehicle("lolo", "momo", "55811557");
            garage.AddVehicle(car);
            garage.AddVehicle(truck);

            bool result = garage.AddVehicle(bike);
            Assert.IsFalse(result);
        }

        [Test]
        public void AddVehicleWorkCorrectlyWithCapacityCount()
        {
            Garage garage = new Garage(2);
            Vehicle car = new Vehicle("opel", "corsa", "55828557");
            Vehicle truck = new Vehicle("man", "ipon", "5888557");
            garage.AddVehicle(car);
            garage.AddVehicle(truck);
            var result = garage.Capacity;
            Assert.AreEqual(2,result);
        }
        [Test]
        public void AddVehicleWorkCorrectlyWhitLicenseNumber()
        {
            Garage garage = new Garage(2);
            Vehicle car = new Vehicle("opel", "corsa", "55828557");
          
            garage.AddVehicle(car);
          
            var result = garage.AddVehicle(car);
            Assert.IsFalse(result);
        }

        [Test]
        public void ChargeVehiclesWorkCorrectly()
        {
            Garage garage = new Garage(4);
            Vehicle car = new Vehicle("opel", "corsa", "55828557");
            Vehicle truck = new Vehicle("man", "ipon", "5888557");
            Vehicle bike = new Vehicle("lolo", "momo", "55811557");
            garage.AddVehicle(car);
            garage.AddVehicle(truck);
            garage.AddVehicle(bike);

            garage.DriveVehicle("55828557", 35,false);
            garage.DriveVehicle("5888557", 30,false);
            garage.DriveVehicle("55811557",35,false);
            int result = garage.ChargeVehicles(65);
            Assert.AreEqual(2,result);

        }

        [Test]
        public void DriveVehicleIsDamaged()
        {
            Garage garage = new Garage(4);
            Vehicle car = new Vehicle("opel", "corsa", "55828557");
            Vehicle truck = new Vehicle("man", "ipon", "5888557");
            Vehicle bike = new Vehicle("lolo", "momo", "55811557");
            garage.AddVehicle(car);
            garage.AddVehicle(truck);
            garage.AddVehicle(bike);
            garage.DriveVehicle("55828557", 35, true);
            garage.DriveVehicle("55828557", 35, true);
            var result = car.BatteryLevel;
            Assert.AreEqual(65,result);
        }

        [Test]
       [ TestCase(40,60)]
       [ TestCase(120,100)]

        public void DriveVehicleBatteryLevel(int actualBatteryLevel,int batteryLevel)
        {
            Garage garage = new Garage(4);
            Vehicle car = new Vehicle("opel", "corsa", "55828557");
            Vehicle truck = new Vehicle("man", "ipon", "5888557");
            Vehicle bike = new Vehicle("lolo", "momo", "55811557");
            garage.AddVehicle(car);
            garage.AddVehicle(truck);
            garage.AddVehicle(bike);
            garage.DriveVehicle("55828557", actualBatteryLevel, false);
            
            var result = car.BatteryLevel;
            Assert.AreEqual(batteryLevel, result);
        }

        [Test]

        public void RepairVehiclesWorkCorrectly()
        {
            Garage garage = new Garage(4);
            Vehicle car = new Vehicle("opel", "corsa", "55828557");
            Vehicle truck = new Vehicle("man", "ipon", "5888557");
            Vehicle bike = new Vehicle("lolo", "momo", "55811557");
            garage.AddVehicle(car);
            garage.AddVehicle(truck);
            garage.AddVehicle(bike);

            garage.DriveVehicle("55828557", 52, false);
            garage.DriveVehicle("5888557",50,true);
            garage.DriveVehicle("55811557",50,true);

            var actualResult = garage.RepairVehicles();
            var expectedResult = $"Vehicles repaired: 2";
            Assert.AreEqual(expectedResult,actualResult);
            Assert.IsFalse(truck.IsDamaged);
            Assert.IsFalse(bike.IsDamaged);
        }
        [Test]
        public void DriveVehicleAccidentOccured()
        {
            Garage garage = new Garage(5);

            Vehicle car = new Vehicle("Peugoet", "208", "CT7006H");
            Vehicle van = new Vehicle("Mercedes-Benz", "Vito", "H7806AH");
            Vehicle truck = new Vehicle("Scania", "Citywide", "P7006XX");
            Vehicle scooter = new Vehicle("Yamaha", "Aerox", "PB6006PA");

            garage.AddVehicle(car);
            garage.AddVehicle(van);
            garage.AddVehicle(truck);
            garage.AddVehicle(scooter);

            garage.DriveVehicle("PB6006PA", 50, true);

            bool actualVehicleCondition = scooter.IsDamaged;

            Assert.IsTrue(actualVehicleCondition);
        }

    }
}