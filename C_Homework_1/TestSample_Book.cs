using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace C_Homework_1
{
    [TestFixture]
    class TestSample_Book
    {
        Library library;
        Book book1;
        Book book2;
        Subscriber sub1;

       [SetUp]
        public void new_Books()
        {
            library = new Library();
            book1 = new Book(library, "Достоевский Ф. М.", "Преступление и наказание", false);
            library.Add_book(book1);

            book2 = new Book(library, "Данте А.", "Божественная комедия", true);
            library.Add_book(book2);
            sub1 = new Subscriber(library, "Иванов Федор", "8(911)5555555");
            library.Give_book(sub1, book1);
        }
        //основные свойства книги
        [Test]
        public void Test_Book_describe()
        {
            Assert.AreEqual(book1.Name, "Преступление и наказание");
            Assert.AreEqual(book1.Author, "Достоевский Ф. М.");
            Assert.IsFalse(book1.Rare);
        }

        //Находится книга в библиотеке или у абонента(у какого именно абонента)
        [Test]
        public void Test_Book_where()
        {
            Assert.AreEqual(book1.Where_book(), "Книга Преступление и наказание предоставлена: Иванов Федор");
            Assert.AreEqual(book2.Where_book(), "Книга в библиотеке");
        }

        //Когда книга была выдана, если она находится у абонента
        [Test]
        public void Test_Book_when()
        {
            Assert.AreEqual(book1.When_book(), "Книга Преступление и наказание выдана: 20.06.2017");
            Assert.AreEqual(book2.When_book(), "Книга в библиотеке");
        }
    }
}
