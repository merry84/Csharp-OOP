using NUnit.Framework;
using System.Xml.Linq;

namespace VendingRetail.Tests
{
    public class Tests
    {

        [Test]
        [TestCase(2000, 5)]
        public void CoffeeMatWorkCorrectly(int waterCapacity, int buttonCount)
        {
            CoffeeMat autoMat = new CoffeeMat(waterCapacity, buttonCount);
            Assert.AreEqual(waterCapacity, autoMat.WaterCapacity);
            Assert.AreEqual(buttonCount, autoMat.ButtonsCount);
            Assert.IsNotNull(autoMat);
        }

        [Test]
        [TestCase(2000, 5)]
        public void CoffeeMatFillEmptyTank(int waterCapacity, int buttonCount)
        {
            CoffeeMat autoMat = new CoffeeMat(waterCapacity, buttonCount);
            string result = autoMat.FillWaterTank();
            string expectedResult = $"Water tank is filled with {waterCapacity}ml";
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(2000, 5)]
        public void CoffeeMatFillFullTank(int waterCapacity, int buttonCount)
        {
            CoffeeMat autoMat = new CoffeeMat(waterCapacity, buttonCount);
            autoMat.FillWaterTank();
            string result = autoMat.FillWaterTank();
            string expectedResult = "Water tank is already full!";
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(2000, 5)]
        public void CoffeeMatAddDrink(int waterCapacity, int buttonCount)
        {
            CoffeeMat autoMat = new CoffeeMat(waterCapacity, buttonCount);
            bool result = autoMat.AddDrink("Coffee", 1.50);

            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(2000, 5)]
        public void CoffeeMatAddDrinksToExceedCapacity(int waterCapacity, int buttonCount)
        {
            CoffeeMat autoMat = new CoffeeMat(waterCapacity, buttonCount);
            autoMat.AddDrink("Milk", 1.40);
            autoMat.AddDrink("Cola", 4.50);
            autoMat.AddDrink("Cappuccino", 1.50);
            autoMat.AddDrink("AceCoffee", 3.50);
            autoMat.AddDrink("Latte", 2.50);
            autoMat.AddDrink("Coffee", 1.20);
            bool isAdded = autoMat.AddDrink("Tea", 1.35);

            Assert.IsFalse(isAdded);

        }

        [Test]
        [TestCase(2000, 5)]
        public void CoffeeMatByeExistDrink(int waterCapacity, int buttonCount)
        {
            CoffeeMat autoMat = new CoffeeMat(waterCapacity, buttonCount);
            autoMat.AddDrink("Milk", 1.40);
            autoMat.AddDrink("Cola", 4.50);
            autoMat.AddDrink("Cappuccino", 1.50);
            autoMat.AddDrink("AceCoffee", 3.50);

            autoMat.FillWaterTank();
            string result = autoMat.BuyDrink("Milk");
            string expectedResult = $"Your bill is 1.40$";
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(2000, 5)]
        public void CoffeeMatOutOfWater(int waterCapacity, int buttonCount)
        {
            CoffeeMat autoMat = new CoffeeMat(waterCapacity, buttonCount);
            autoMat.AddDrink("Milk", 1.40);
            autoMat.AddDrink("Cola", 4.50);
            autoMat.AddDrink("Cappuccino", 1.50);
            autoMat.AddDrink("AceCoffee", 3.50);
            string result = autoMat.BuyDrink("Milk");
            string expectedResult = $"CoffeeMat is out of water!";
            Assert.AreEqual(expectedResult, result);

        }

        [Test]
        [TestCase(2000, 5)]
        public void CoffeeMatByeNotExistDrink(int waterCapacity, int buttonCount)
        {
            CoffeeMat autoMat = new CoffeeMat(waterCapacity, buttonCount);
            autoMat.AddDrink("Milk", 1.40);
            autoMat.AddDrink("Cola", 4.50);
            autoMat.AddDrink("Cappuccino", 1.50);
            autoMat.AddDrink("AceCoffee", 3.50);

            autoMat.FillWaterTank();
            string result = autoMat.BuyDrink("Coffee");
            string expectedResult = "Coffee is not available!";
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(2000, 5)]
        [TestCase(1000, 5)]
        public void CoffeeMatCheckWaterConsumption(int waterCapacity, int buttonCount)
        {
            CoffeeMat autoMat = new CoffeeMat(waterCapacity, buttonCount);
            autoMat.AddDrink("Milk", 1.40);
            autoMat.AddDrink("Cola", 4.50);
            autoMat.AddDrink("Cappuccino", 1.50);
            autoMat.AddDrink("AceCoffee", 3.50);
            autoMat.AddDrink("Latte", 2.50);
            autoMat.AddDrink("Coffee", 1.20);
            autoMat.BuyDrink("Coffee");
            autoMat.BuyDrink("Coffee");
            autoMat.BuyDrink("Coffee");
            autoMat.BuyDrink("Coffee");
            autoMat.BuyDrink("Latte");
            autoMat.BuyDrink("Latte");
            autoMat.BuyDrink("Latte");
            autoMat.BuyDrink("Latte");
            autoMat.BuyDrink("Cappuccino");
            autoMat.BuyDrink("Cappuccino");
            autoMat.BuyDrink("Cappuccino");
            autoMat.BuyDrink("Cappuccino");
            autoMat.BuyDrink("Cappuccino");
            string result = autoMat.BuyDrink("Cappuccino");
            string exceptionResult = "CoffeeMat is out of water!";
            Assert.AreEqual(result, exceptionResult);
        }

        [Test]
        [TestCase(2000, 5)]
        public void CoffeeMatCollectIncome(int waterCapacity, int buttonCount)
        {
            CoffeeMat autoMat = new CoffeeMat(waterCapacity, buttonCount);
            autoMat.FillWaterTank();
            autoMat.AddDrink("Milk", 1.40);
            autoMat.AddDrink("Cola", 4.50);
            autoMat.AddDrink("Cappuccino", 1.50);
            autoMat.AddDrink("AceCoffee", 3.50);
            autoMat.AddDrink("Latte", 2.50);
           

            autoMat.BuyDrink("AceCoffee");
            autoMat.BuyDrink("Cola");
            autoMat.BuyDrink("Cappuccino");
           

            double result = autoMat.Income;
            double income = autoMat.CollectIncome();
            double incomeAfterCollect = autoMat.Income;
            Assert.AreEqual((double)income, result);
            Assert.That(9.50, Is.EqualTo(income));
            Assert.That(0, Is.EqualTo(incomeAfterCollect));



        }
    }
}