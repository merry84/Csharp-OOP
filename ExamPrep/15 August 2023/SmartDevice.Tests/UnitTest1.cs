namespace SmartDevice.Tests
{
    using NUnit.Framework;
    using System;
    using System.Text;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
           
        }
        [Test]
        [TestCase(2000)]
        public void ConstructorWorkCorrectly(int memoryCapacity)
        {
            
            Device device = new Device(memoryCapacity);
            Assert.AreEqual(memoryCapacity, device.MemoryCapacity);
            Assert.AreEqual(memoryCapacity,device.AvailableMemory);
            Assert.AreEqual(0,device.Photos);
            Assert.AreEqual(0,device.Applications.Count);
        }

        [Test]
        [TestCase(2000, 100)]
        public void TakePhotoWorkCorrectly(int memoryCapacity,int photoSize)
        {
            Device device = new Device(memoryCapacity);
            device.TakePhoto(photoSize);
            Assert.AreEqual(memoryCapacity- photoSize,device.AvailableMemory);
            Assert.AreEqual(1,device.Photos);
        }

        [Test]
        [TestCase(2000, 2100)]
        public void TakePhotoWithBigPhotoSize(int memoryCapacity, int photoSize)
        {
            Device device = new Device(memoryCapacity);
            bool photoTaken = device.TakePhoto(photoSize);

            Assert.IsFalse(photoTaken);
            Assert.AreEqual(memoryCapacity, device.AvailableMemory);
            Assert.AreEqual(0, device.Photos);
        }

        [Test]
        [TestCase(2000, 100,"App")]
        public void InstallAppWorkCorrectly(int memoryCapacity,int appSize,string appName)
        {
            Device device = new Device(memoryCapacity);
            string actualResult= device.InstallApp(appName, appSize);
            Assert.AreEqual($"{appName} is installed successfully. Run application?", actualResult);

            Assert.AreEqual(memoryCapacity- appSize,device.AvailableMemory);
            Assert.AreEqual(1,device.Applications.Count);
            Assert.IsTrue(device.Applications.Contains(appName));
        }

        [Test]
        [TestCase(2000, 3100, "App")]
        public void InstallAppThrowException(int memoryCapacity, int appSize, string appName)
        {
            Device device = new Device(memoryCapacity);

            Assert.Throws<InvalidOperationException>(() => device.InstallApp(appName, appSize));
            Assert.AreEqual(memoryCapacity,device.AvailableMemory);
            Assert.AreEqual(0,device.Applications.Count);
        }

        [Test]
        [TestCase(2000, 300, "App",200)]
        public void FormatDeviceWorkCorrectly(int memoryCapacity,int photoSize,string appName,int appSize)
        {
            Device device = new Device(memoryCapacity);
            device.TakePhoto(photoSize);
            device.InstallApp(appName, appSize);
            device.FormatDevice();
            Assert.AreEqual(memoryCapacity, device.AvailableMemory);
            Assert.AreEqual(0, device.Photos);
            Assert.AreEqual(0, device.Applications.Count);
        }

        [Test]
        [TestCase(2000, 300, "App", 200, "app", 100)]
        public void GetDeviceStatusWorkCorrectly(int memoryCapacity, int photoSize, string appName1, int appSize1,
            string appName2, int appSize2)
        {
            Device device = new Device(memoryCapacity);
            device.TakePhoto(photoSize);
            device.InstallApp(appName1, appSize1);
            device.InstallApp(appName2, appSize2);

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Memory Capacity: {memoryCapacity} MB, Available Memory: {memoryCapacity-photoSize-appSize1-appSize2} MB");
            stringBuilder.AppendLine($"Photos Count: {1}");
            stringBuilder.AppendLine($"Applications Installed: App, app");

            string actualResult = stringBuilder.ToString().TrimEnd();
            string statusDevace = device.GetDeviceStatus();
            Assert.AreEqual(actualResult,statusDevace);

        }


    }
}