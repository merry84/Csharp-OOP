using System.Runtime.Serialization.Formatters;

namespace Database.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]

    public class DatabaseTests
    {
        [SetUp]
        public void Setup()
        {
           
        }
        [Test]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(16)]
        [TestCase(0)]
        public void AddMethodElementsInDatabaseCorrectly(int count) //правилно ли работи конструктора
        {
            Database database = new Database();
            for (int i = 0; i < count; i++)
            {
                database.Add(i);
            }
            Assert.AreEqual(count,database.Count);
        }

        [Test]
        
        public void AddMethodShouldThrowExceptionItems()//дали хваща грешка при опит да добавим повече от капацитета
        {
            Database database = new Database();
            for (int i = 0; i < 16; i++)
            {
                database.Add(i);
            }
            Assert.Throws<InvalidOperationException>(()
                => database.Add(5));
        }

        [Test]
        [TestCase(1,14)]
        [TestCase(1, 15)]
        [TestCase(1, 16)]
        public void ConstructorShouldAllElementsTheyAreBelow16(int start ,int count)//по малко от капацитета
        {
            int[] elements = Enumerable.Range(start, count).ToArray();
            Database database = new Database(elements);
            Assert.AreEqual(count,database.Count);
        }

        [Test]
        [TestCase(1, 17)]
        [TestCase(1, 26)]
        public void ConstructorShouldWhenElementsAreAbove16(int start, int count)//повече от капацитета хварля кли грешка
        {
            int[] elements = Enumerable.Range(start, count).ToArray();
            Assert.Throws<InvalidOperationException>(() =>
                new Database(elements)); 

        }

        [Test]
        [TestCase(1,10,8,2)]
        [TestCase(1,5,3,2)]
        [TestCase(5,10,7,3)]
        public void RemoveMethodShouldBeWorkCorrectly(
            int start,int count ,int result,int toRemove)
        {
            int[] elements = Enumerable.Range(start, count).ToArray();
            Database db = new Database(elements);
            for (int i = 0; i < toRemove; i++)
            {
                db.Remove();
                
            }
            Assert.AreEqual(result,db.Count);

        }
        [Test]
        public void RemoveMethodThrowsException()
        {
            Database db = new Database();
            Assert.Throws<InvalidOperationException>(
                ()=> db.Remove());
        }

        [Test]
        public void FetchMethodCreateEndReturnArray()
        {
            Database db = new Database(1, 2, 3);
            db.Add(4);
            db.Add(5);
            db.Remove();
            db.Remove();
            db.Remove();
            int[] fetchData = db.Fetch();
            int[] expectedData = new[] { 1, 2 };

            Assert.AreEqual(expectedData , fetchData);
        }


    }
}
