using NUnit.Framework;
using System.Linq;

namespace RobotFactory.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("koko",50)]
        public void FactoryConstructorWorkCorrectly(string name,int capacity)
        {
            Factory factory = new Factory(name,capacity);
            Assert.IsNotNull(factory);
        }
        [Test]        
        public void ProduceRobotCheckAdd()
        {
            Factory factory = new Factory("koko", 60);
            int expectedCountBefore = 0;
            int actualCounBefore = factory.Robots.Count;   
            factory.ProduceRobot("bobo", 2500, 56);
            int expectedCountAfter = 1;
            int actualCounAfter = factory.Robots.Count;
            Assert.AreEqual(actualCounAfter, expectedCountAfter);
            Assert.AreEqual(expectedCountBefore,actualCounBefore);

        }
        [Test]
        public void ProduceRobotCheckCapacityFull()
        {
            Factory factory = new Factory("koko", 2);
            factory.ProduceRobot("bobo", 2500, 56);
            factory.ProduceRobot("bobo", 2500, 56);
            string actualResult = factory.ProduceRobot("bobo", 2500, 56);
            string expectedResult = "The factory is unable to produce more robots for this production day!";
            
            Assert.AreEqual(expectedResult, actualResult);
            
        }
        [Test]
        public void ProduceSupplementMethodWorkCorrectly()
        {
            Factory factory = new Factory("koko", 60);
            int expectedCountBefore = 0;
            int actualCounBefore = factory.Supplements.Count;
            factory.ProduceSupplement("bobo", 5);
            int expectedCountAfter = 1;
            int actualCounAfter = factory.Supplements.Count;
            Assert.AreEqual(actualCounAfter, expectedCountAfter);
            Assert.AreEqual(expectedCountBefore, actualCounBefore);

        }
        [Test]
        public void ProduceSupplementValidparameters()
        {
            Factory factory = new Factory("koko", 60);
            
            string expectedResult = "Supplement: bobo IS: 5";
            string actualResult = factory.ProduceSupplement("bobo", 5);
            Assert.AreEqual(@expectedResult, actualResult);
        }
        [Test]
        public void UpgradeRobotIsTrue() 
        {
            Factory factory = new Factory("koko", 60);
            factory.ProduceRobot("bobo", 2500, 5);
            factory.ProduceSupplement("bobo", 5);
            var result =(factory.UpgradeRobot(factory.Robots.FirstOrDefault(), factory.Supplements.FirstOrDefault()));
            Assert.IsTrue(result);
        }
        [Test]
        public void UpgradeRobotIsFalse()
        {
            Factory factory = new Factory("koko", 60);
            factory.ProduceRobot("bobo", 2500, 5);
            factory.ProduceSupplement("bobo", 6);
            var result = (factory.UpgradeRobot(factory.Robots.FirstOrDefault(), factory.Supplements.FirstOrDefault()));
            Assert.IsFalse(result);
        }
        [Test]
        public void SellRobot_Successful()
        {
            Factory factory = new Factory("SpaceX", 10);

            factory.ProduceRobot("Robo-3", 2000, 22);
            factory.ProduceRobot("Robo-3", 2500, 22);
            factory.ProduceRobot("Robo-3", 30000, 22);

            Robot robot = factory.Robots.FirstOrDefault(r => r.Price == 2000);

            var robotSold = factory.SellRobot(2499);

            Assert.AreSame(robot, robotSold);
        }

    }
}