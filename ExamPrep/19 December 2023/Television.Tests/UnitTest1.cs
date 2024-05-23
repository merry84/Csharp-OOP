namespace Television.Tests
{
    using System;
    using NUnit.Framework;
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConstructorWorkCorrectly()
        {
           
            string brand = "Neo";
            double price = 120.00;
            int screenWidth = 100;
            int screenHeight = 200;

            TelevisionDevice tv = new TelevisionDevice(brand, price, screenWidth, screenHeight);
            Assert.AreEqual(brand,tv.Brand);
            Assert.AreEqual(price, tv.Price);
            Assert.AreEqual(screenWidth,tv.ScreenWidth);
            Assert.AreEqual(screenHeight,tv.ScreenHeigth);
        }

        [Test]
        public void MethodSwitchOnWorkCorrectly()
        {
            var tv = new TelevisionDevice("Sharp", 199, 256, 1025);
            //return $"Cahnnel {CurrentChannel} - Volume {Volume} - Sound {sound}";
            string expectedOutput = "Cahnnel 0 - Volume 13 - Sound On";
            string output = tv.SwitchOn();

            Assert.AreEqual(expectedOutput,output);
        }

        [Test]
        public void MethodSwitchOffWorkCorrectlyLastMuted()
        {
            var tv = new TelevisionDevice("Sharp", 199, 256, 1025);
           tv.MuteDevice();
            string expectedOutput = "Cahnnel 0 - Volume 13 - Sound Off";
            string output = tv.SwitchOn();

            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void MethodChangeChannelToNegativeNumberThrowException()
        {
            var tv = new TelevisionDevice("Sharp", 199, 256, 1025);
            Assert.Throws<ArgumentException>(() => tv.ChangeChannel(-1));
        }

        [Test]
        public void MethodChangeChannelWorkCorrectly()
        {
            var tv = new TelevisionDevice("Sharp", 199, 256, 1025);
            int newChannel = 7;
            int realChannel= tv.ChangeChannel(newChannel);
            Assert.AreEqual(realChannel,newChannel);
        }

        [Test]
        public void MethodVolumeChangeUpChangeWorkCorrectly()
        {
            var tv = new TelevisionDevice("Sharp", 199, 256, 1025);
            string expectedVolume = "Volume: 23";
            string output = tv.VolumeChange("UP", 10);
            Assert.AreEqual(expectedVolume, output);
        }
        [Test]
        public void MethodVolumeChangeUpWith100OrMore()
        {
            var tv = new TelevisionDevice("Sharp", 199, 256, 1025);
            string expectedVolume = "Volume: 100";
            string output = tv.VolumeChange("UP", 100);
            Assert.AreEqual(expectedVolume, output);
        }

        [Test]
        public void MethodVolumeChangeDownChangeWorkCorrectly()
        {
            var tv = new TelevisionDevice("Sharp", 199, 256, 1025);
            string expectedVolume = "Volume: 3";
            string output = tv.VolumeChange("DOWN", 10);
            Assert.AreEqual(expectedVolume, output);
        }
        [Test]
        public void MethodVolumeChangeDownLessThanZeroVolume()
        {
            var tv = new TelevisionDevice("Sharp", 199, 256, 1025);
            string expectedVolume = "Volume: 0";
            string output = tv.VolumeChange("DOWN", 20);
            Assert.AreEqual(expectedVolume, output);
        }

        [Test]
        public void TvIsUnMuted()
        {
            var tv = new TelevisionDevice("Sharp", 199, 256, 1025);
            tv.MuteDevice();//mute first
            bool isMuted = tv.MuteDevice();// Is not muted
            Assert.IsFalse(isMuted);
        }
        [Test]
        public void TvIsMuted()
        {
            var tv = new TelevisionDevice("Sharp", 199, 256, 1025);
         
            bool isMuted = tv.MuteDevice();// Mute
            Assert.IsTrue(isMuted);
        }

        [Test]
        public void MethodOverrideToStrigWorkCorrectly()
        {
            var tv = new TelevisionDevice("Sharp", 199, 256, 1025);
            string expected = "TV Device: Sharp, Screen Resolution: 256x1025, Price 199$";
            string output = tv.ToString();
            Assert.AreEqual(expected, output);
        }

    }
}