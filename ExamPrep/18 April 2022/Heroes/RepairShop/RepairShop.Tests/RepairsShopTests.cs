using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
           
        }

        [Test]
        public void GarageConstructorWorkCorrectly()
        {
            Garage garage = new Garage("popo",16);
            Car car = new Car("opel", 125);
            Assert.AreEqual(garage.Name,"popo");
            Assert.AreEqual(garage.MechanicsAvailable,16);
            garage.AddCar(car);

            Assert.AreEqual(garage.CarsInGarage, 1);
        }

        [Test]
        public void GarageNameIsNullOrEmpty()
        {
            Assert.Throws< ArgumentNullException > (()=> new Garage("",15));
            Assert.Throws< ArgumentNullException > (()=> new Garage(null,15));
        }

        [Test]
        public void MechanicsAvailableThrowExceptionIsZeroEndNegativeNumber()
        {
            Assert.Throws<ArgumentException>(() => new Garage("lolo", 0));
            Assert.Throws<ArgumentException>(() => new Garage("popo", -5));
        }
        [Test]
        public void MechanicsAvailableWithValidParameter()
        {
            Garage garage = new Garage("popo", 16);
            Assert.AreEqual(garage.MechanicsAvailable,16);
        }

        [Test]
        public void AddCarWorkCorrectly()
        {
            Garage garage = new Garage("popo", 16);
            Car car = new Car("opel", 125);
           
            garage.AddCar(car);
            Assert.AreEqual(garage.CarsInGarage, 1);

        }
        
        [Test]
        public void AddCarThrowException()
        {
            Garage garage = new Garage("popo", 1);
            Car car = new Car("opel", 125);
            garage.AddCar(car);
            Car car1 = new Car("nisan", 124);


            Assert.Throws<InvalidOperationException>(() => garage.AddCar(car1));
        }
        [Test]
        public void FixCarMethod()
        {
            Garage garage = new Garage("popo", 1);
            Car car = new Car("opel", 125);
            garage.AddCar(car);
           var fixedCar = garage.FixCar("opel");
            
            Assert.IsTrue(fixedCar.IsFixed);
            
            //                const string carModel = "Honda";
            //                var garage = new Garage("Test", 3);
            //                var firstCar = new Car("Toyota", 2);
            //                var secondCar = new Car(carModel, 2);
            //                //act
            //                garage.AddCar(secondCar);
            //                var fixedCar = garage.FixCar(carModel);
            //                //assert
            //                Assert.That(fixedCar.IsFixed, Is.True);
            //                Assert.That(fixedCar.CarModel, Is.EqualTo(carModel));
            //                Assert.That(fixedCar.NumberOfIssues, Is.EqualTo(0));
        }

        [Test]
        public void FixCarThrowException()
        {
            Garage garage = new Garage("popo", 1);
            Car car = new Car("opel", 125);
            garage.AddCar(car);


            Assert.Throws<InvalidOperationException>(() => garage.FixCar("nisan"));
        }

        [Test]
        public void RemoveFixedCarWorkCorrectly()
        {
            Garage garage = new Garage("popo", 1);
            Car car = new Car("opel", 125);
            garage.AddCar(car);
            garage.FixCar("opel");
            var fixedCar = garage.RemoveFixedCar();



            Assert.That(fixedCar, Is.EqualTo(1));
            Assert.That(garage.CarsInGarage, Is.EqualTo(0));

        }

        [Test]
        public void RemoveFixedThrowException()
        {
            Garage garage = new Garage("popo", 1);
            Car car = new Car("opel", 125);
            garage.AddCar(car);
           Assert.Throws<InvalidOperationException>(()=> garage.RemoveFixedCar());
           
        }

        [Test]
        public void ReportGarageWorkCorrectly()
        {
             //arrange
                const string carModel = "Honda";
                var garage = new Garage("Test", 3);
                var firstCar = new Car("Toyota", 2);
                var secondCar = new Car(carModel, 2);
                var thirdCar = new Car("BMW", 3);
                //act
                garage.AddCar(firstCar);
                garage.AddCar(secondCar);
                garage.AddCar(thirdCar);
                garage.FixCar(carModel);
                var carReport = garage.Report();
                //assert
                Assert.That(carReport, Is.EqualTo($"There are 2 which are not fixed: Toyota, BMW."));
        }


    }
}