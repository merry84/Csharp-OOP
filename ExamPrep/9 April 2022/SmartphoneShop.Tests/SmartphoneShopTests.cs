using NUnit.Framework;
using System;
using System.Reflection;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        [Test]
        public void ShopConstructorWorkCorrectly()
        {
            Shop shop = new Shop(2);
            Assert.IsNotNull(shop);
            Assert.AreEqual(shop.Capacity,2);
            Assert.AreEqual(shop.Count,0);
        }

        [Test]
        public void ShopCapacityIsNegativeNumber()
        {
            Assert.Throws<ArgumentException> (() => new Shop(-3));
        }

        [Test]
        public void ShopAddPhoneCorrectly()
        {
            Shop shop = new Shop(2);
            Smartphone phone = new Smartphone("nokia", 600);
            shop.Add(phone);
            Assert.AreEqual(shop.Capacity, 2);
            Assert.AreEqual(shop.Count, 1);
        }
        [Test]
        public void ShopAddPhoneExistPhone()
        {
            Shop shop = new Shop(2);
            Smartphone phone = new Smartphone("nokia", 600);
            shop.Add(phone);
            
           Assert.Throws<InvalidOperationException>(() => shop.Add(phone));
        }
        [Test]
        public void ShopThrowShopIsFullCapacity()
        {
            Shop shop = new Shop(2);
            Smartphone phone = new Smartphone("nokia", 600);
            Smartphone phone1 = new Smartphone("lg", 600);
            Smartphone phone2= new Smartphone("koko", 600);
            shop.Add(phone);
            shop.Add(phone1);

            Assert.Throws<InvalidOperationException>(() => shop.Add(phone2));
        }

        [Test]
        public void ShopRemovePhoneCorrectly()
        {
            Shop shop = new Shop(2);
            Smartphone phone = new Smartphone("nokia", 600);
            Smartphone phone1 = new Smartphone("lg", 600);
            Smartphone phone2 = new Smartphone("koko", 600);
            shop.Add(phone);
            shop.Add(phone1);
            shop.Remove("nokia");
            Assert.AreEqual(shop.Count, 1);
        }
        [Test]
        public void ShopRemovePhoneThrowRException()
        {
            Shop shop = new Shop(2);
            Smartphone phone = new Smartphone("nokia", 600);
            Smartphone phone1 = new Smartphone("lg", 600);
            Smartphone phone2 = new Smartphone("koko", 600);
            shop.Add(phone);
            shop.Add(phone1);
            shop.Remove("nokia");
            Assert.Throws<InvalidOperationException>(() => shop.Remove("samsung"));
        }

        [Test]
        public void TestPhoneMethod()
        {
            Shop shop = new Shop(2);
            Smartphone phone = new Smartphone("nokia", 600);
            Smartphone phone1 = new Smartphone("lg", 600);
            Smartphone phone2 = new Smartphone("koko", 600);
            shop.Add(phone);
            shop.Add(phone1);
            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("koko", 600));
        }



    }



}