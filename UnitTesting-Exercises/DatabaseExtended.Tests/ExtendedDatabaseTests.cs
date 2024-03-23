using System;
using System.Data;
using ExtendedDatabase;

namespace DatabaseExtended.Tests
{
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Person[] people;
       
        private Database database;
        [SetUp]
        public void SetUp()
        {
            people = new Person[]
            {
                new(1, "Henry1"),
                new(2, "Henry2"),
                new(3, "Henry3"),
                new(4, "Henry4"),
                new(5, "Henry5"),
                new(6, "Henry6"),
                new(7, "Henry7"),
                new(8, "Henry8"),
                new(9, "Henry9"),
                new(10, "Henry10"),
                new(11, "Henry11"),
                new(12, "Henry12"),
                new(13, "Henry13"),
                new(14, "Henry14"),
                new(15, "Henry15"),
            };
        }
        [Test]
        public void ConstructorShouldWorkCorrectly()
        {
            Database db = new Database();
            Person person = new Person(1, "Dobcho");
            db.Add(person);
            Assert.AreEqual(1, db.Count);
        }
        [Test]
        public void Adding_More_Than_16_People_Through_The_Constructor_Should_Throw_An_Exception()
        {
            Person[] extraPeople = new Person[] { new(16, "Henry16"), new(17, "Henry17") };
            Person[] tooManyPeople = Enumerable.ToArray(Enumerable.Concat(people, extraPeople));

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () =>
                {
                    database = new Database(tooManyPeople);
                }, "Adding more than 16 people through the constructor did not throw an exception");
            Assert.AreEqual("Provided data length should be in range [0..16]!", exception.Message);
        }

        [Test]
        public void AddMethodThrowExceptionIfCountIs16()
        {
            Person[] people = new Person[16];

            for (int i = 0; i < people.Length; i++)
            {
                people[i] = new Person(i, $"Dobchi{i}");
            }

            Database db = new Database(people);

            Assert.Throws<InvalidOperationException>
            (() =>
                {
                    Person person = new(19, "Ivan");
                    db.Add(person);
                });
        }
        [Test]
        public void AddMethodThrowExceptionIfExistUserName()
        {
            Database database = new Database();
            Person person = new Person(1, "Liolio");
            database.Add(person);
            Assert.Throws<InvalidOperationException>(() => database.Add(person));

        }
        [Test]
        public void AddMethodThrowExceptionIfExistId()
        {
            Database database = new Database();
            Person person = new Person(1, "Liolio");
            
            database.Add(person);
            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(1, "Iliya")));
        }

        [Test]
        public void RemoveMethodWorkCorrectly()
        {
            Database db = new Database();
            Person people = new Person(1,"Lili");
            Person person = new Person(2, "Mila");
            db.Add(people);
            db.Add(person);
            db.Remove();
            Assert.AreEqual(1,db.Count);
        }

        [Test]
        public void RemoveMethodThrowExceptionIfCountIsZero()
        {
            Database database = new Database();
            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void FindByNameCorrectly()
        {
            Database db = new Database();
            Person person = new Person(1, "Niki");
            db.Add(person);
            db.FindByUsername("Niki");
            Assert.AreEqual("Niki","Niki");
        }

        [Test]
        public void FindByNameThrowsExceptionUsernameIsNullOrEmpty()
        {
            Database db = new Database();
            Assert.Throws<ArgumentNullException>(() => db.FindByUsername(null));
        }

        [Test]
        public void FindByNameThrowExceptionNonExistUserName()
        {
            Database db = new Database();
            Assert.Throws<InvalidOperationException>(() => db.FindByUsername("Gorgona"));
        }

        [Test]
        public void FindByNameThrowsExceptionNonExistId()
        {
            Database db = new Database();
            Assert.Throws<InvalidOperationException>(() => db.FindById(5));
        }

        [Test]
        public void FindByNameThrowsExceptionIdIsNotPositiveNumber()
        {
            Database db = new Database();
            Assert.Throws<ArgumentOutOfRangeException>(() => db.FindById(-3));
        }

        [Test]
        public void FindByNameMethodWorkCorrectly()
        {
            Database db = new Database();
            Person people = new Person(1, "Mitko");
            db.Add(people);
            db.FindById(1);
            Assert.AreEqual(1,1);
        }

    }
}