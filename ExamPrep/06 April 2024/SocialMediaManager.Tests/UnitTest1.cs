using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace SocialMediaManager.Tests
{
    public class Tests
    {
        private InfluencerRepository influencerRepository;
        [SetUp]
        public void Setup()
        {
            influencerRepository = new InfluencerRepository();
        }

        [Test]
       
        public void InfluencerRepositoryConstructorWorkCorrectly()
        {
           Assert.IsNotNull(influencerRepository.Influencers);
        }
        [Test]       
        public void InfluencerIsNullThrow()
        {
            Assert.Throws<ArgumentNullException>(()=> influencerRepository.RegisterInfluencer(null));
            
        }
        [Test]
        [TestCase("penko",45)]
        public void InfluencerExistWithUsernameThrow(string username,int followers)
        {
            Influencer influencer = new Influencer(username, followers);
            influencerRepository.RegisterInfluencer(influencer);

            Assert.Throws<InvalidOperationException>(() 
                => influencerRepository.RegisterInfluencer(influencer));

        }
        [Test]
        [TestCase("penko", 45)]
        public void InfluencerRegistrationValidate(string username, int followers)
        {
            Influencer influencer = new Influencer(username, followers);
            influencerRepository.RegisterInfluencer(influencer);

            Assert.AreEqual(username, influencer.Username);
            Assert.AreEqual(followers, influencer.Followers);
        }
        [Test]
        public void RemoveInfluencerThrowNullException()
        {
            Assert.Throws<ArgumentNullException>(() => influencerRepository.RemoveInfluencer(null));

        }
        [Test]
        [TestCase("penko", 45)]
        public void RemoveInfluencerCorrectly(string username, int followers)
        {
            Influencer influencer = new Influencer(username, followers);
            influencerRepository.RegisterInfluencer(influencer);
            influencerRepository.RemoveInfluencer(username);
            Assert.That(influencerRepository.RemoveInfluencer(username), Is.False);
        }
        [Test]
        [TestCase("penko", 45)]
        public void RemoveInfluencerCorrectly2(string username, int followers)
        {
            Influencer influencer = new Influencer(username, followers);
            influencerRepository.RegisterInfluencer(influencer);
            
            Assert.That(influencerRepository.RemoveInfluencer(username), Is.True);
        }
        [Test]
        [TestCase("penko", 45,"koko", 560)]       
        public void InfluencerGetMostFollowers(string username, int followers, string username1, int followers1)
        {
            Influencer influencer = new Influencer(username, followers);
            influencerRepository.RegisterInfluencer(influencer);
            Influencer influencer1 = new Influencer(username1, followers1);
            influencerRepository.RegisterInfluencer(influencer1);

            var excpectedResult = influencerRepository.GetInfluencerWithMostFollowers().Followers;
            Assert.AreEqual(excpectedResult, influencer1.Followers);
           
        }
        [Test]
        [TestCase("penko", 45, "koko", 560)]
        public void GetInfluencerCorrertly(string username, int followers, string username1, int followers1)
        {
            Influencer influencer = new Influencer(username, followers);
            influencerRepository.RegisterInfluencer(influencer);
            Influencer influencer1 = new Influencer(username1, followers1);
            influencerRepository.RegisterInfluencer(influencer1);

            var excpectedResult = influencerRepository.GetInfluencer(username1);
            Assert.That(excpectedResult, Is.SameAs(influencer1));

        }
        [Test]
        [TestCase("penko", 45, "koko", 560)]
        public void GetInfluencerIsNull(string username, int followers, string username1, int followers1)
        {
            Influencer influencer = new Influencer(username, followers);
            influencerRepository.RegisterInfluencer(influencer);
            Influencer influencer1 = new Influencer(username1, followers1);
            influencerRepository.RegisterInfluencer(influencer1);

            var excpectedResult = influencerRepository.GetInfluencer("milko");
            Assert.IsNull(excpectedResult);

        }
    }
}