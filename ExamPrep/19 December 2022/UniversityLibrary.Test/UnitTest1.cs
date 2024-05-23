using System.Text;

namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TextBookConstructorWorkCorrectly()
        {
            TextBook book = new TextBook("Koko", "Iliev", "Horor");
            TextBook book1 = new TextBook("Momo", "Ilkov", "Horor");

            UniversityLibrary library = new UniversityLibrary();

            library.AddTextBookToLibrary(book);
            library.AddTextBookToLibrary(book1);
            Assert.AreEqual(2,library.Catalogue.Count);
        }

        [Test]
        public void MethodOverrideToStringTexBook()
        {
            TextBook book = new TextBook("Koko", "Iliev", "Horor");
            UniversityLibrary library = new UniversityLibrary();
            var actualResult = library.AddTextBookToLibrary(book);
            var sb = new StringBuilder();
            sb.AppendLine($"Book: Koko - 1");
            sb.AppendLine($"Category: Horor");
            sb.AppendLine($"Author: Iliev");
            var expectedResult = sb.ToString().TrimEnd();
            Assert.AreEqual(expectedResult,actualResult);
        }

        [Test]
        public void LoanTextBookIsNotReturned()
        {
            TextBook book = new TextBook("Koko", "Iliev", "Horor");
            UniversityLibrary library = new UniversityLibrary();
            library.AddTextBookToLibrary(book);
            library.LoanTextBook(1, "Papazov");
            var actualResult = library.LoanTextBook(1, "Papazov");
            var expectedResult = $"Papazov still hasn't returned Koko!";
            Assert.AreEqual(expectedResult,actualResult);
        }
        [Test]
        public void BookIsReturned()
        {
            TextBook book = new TextBook("Koko", "Iliev", "Horor");
            UniversityLibrary library = new UniversityLibrary();
            library.AddTextBookToLibrary(book);
            library.LoanTextBook(1, "Papazov");

            var actualResult = library.ReturnTextBook(1);
            var expectedResult = "Koko is returned to the library.";

            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(book.Holder,string.Empty);
        }
        [Test]
        public void LoanTextBook()
        {
            TextBook book = new TextBook("Koko", "Iliev", "Horor");
            TextBook book1 = new TextBook("Momo", "Ilkov", "Horor");

            UniversityLibrary library = new UniversityLibrary();

            library.AddTextBookToLibrary(book);
            library.AddTextBookToLibrary(book1);
            
            var actualResult = library.LoanTextBook(1, "Papazov");
            var expectedResult = "Koko loaned to Papazov.";
           
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(book.Holder, "Papazov");
        }

        [Test]
        public void InventoryNumberWorkCorrectly()
        {
            TextBook book = new TextBook("Koko", "Iliev", "Horor");
            TextBook book1 = new TextBook("Momo", "Ilkov", "Horor");

            UniversityLibrary library = new UniversityLibrary();

            library.AddTextBookToLibrary(book);
            library.AddTextBookToLibrary(book1);
            var ecpectedResult = book1.InventoryNumber;
            Assert.AreEqual(ecpectedResult, 2);
        }


    }
}