using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Runtime.Serialization;
using System.Xml;

namespace C_Homework_1
{
    [TestFixture]
    class TestSample_Library
    {
        Library library = new Library();
        Book book1;
        Subscriber sub;
        [SetUp]
        public void New_lib()
        {
            library = new Library();
            book1 = new Book(library, "Достоевский Ф. М.", "Преступление и наказание", false);
            sub = new Subscriber(library, "Иванов Федор", "8(911)5555555");
            library.Add_book(book1);
        }

        //тестируем добавление книг
        [Test]
        public void Test_Lib_Add()
        {
            Assert.AreEqual(library.Books_in_Lib[0].book, book1);
        }

        //книги на руках у абонентов
        [Test]
        public void Test_Lib_BooksAbon()
        {
            library.Give_book(sub, book1);
            Assert.AreEqual(library.Books_abon().Count, 1);
        }

        //книги на руках в библиотеке
        [Test]
        public void Test_Lib_BooksinLib()
        {
            Assert.AreEqual(library.Books_lib().Count, 1);
        }

        //поиск по названию и по автору
        [Test]
        public void Test_Lib_Search()
        {
            Assert.AreEqual(library.Name_search("Преступление и наказание").book.Author, "Достоевский Ф. М.");
            Assert.AreEqual(library.Author_search("Достоевский Ф. М.").book.Name, "Преступление и наказание");
        }

        //возврат книг
        [Test]
        public void Test_Lib_Return()
        {
            library.Give_book(sub, book1);
            library.Return_book(book1);
            Assert.Null(library.Books_in_Lib[0].subscr);
        }

        //выдача книг
        [Test]
        public void Test_Lib_Give()
        {
            library.Give_book(sub, book1);
            Assert.NotNull(library.Books_in_Lib[0].subscr);
        }

        //обращение к книге по автору и названию
        [Test]
        public void Test_Lib_BookforIndex()
        {
            Assert.AreEqual(library["Достоевский Ф. М.", "Преступление и наказание"].book.Rare, false);
        }
    }
}
