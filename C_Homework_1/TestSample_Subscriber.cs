using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace C_Homework_1
{
    [TestFixture]
    class TestSample_Subscriber
    {
        Library library;
        Book book1;
        Book book2;
        Subscriber sub1;

        [SetUp]
        public void new_Subs()
        {
            library = new Library();
            book1 = new Book(library, "Достоевский Ф. М.", "Преступление и наказание", false);
            library.Add_book(book1);

            book2 = new Book(library, "Данте А.", "Божественная комедия", true);
            library.Add_book(book2);
            sub1 = new Subscriber(library, "Иванов Федор", "8(911)5555555");
            library.Give_book(sub1, book1);
        }

        //Основные свойства абонента(имя, телефон)
        [Test]
        public void Test_Sub_describe()
        {
            Assert.AreEqual(sub1.Name, "Иванов Федор");
            Assert.AreEqual(sub1.Phone, "8(911)5555555");
        }

        //Проверим взятые книги
        [Test]
        public void Test_Sub_having()
        {
            Assert.AreEqual(sub1.Having_book()[0].Name, "Преступление и наказание");
        }

        //Проверим просроченные книги
        [Test]
        public void Test_Sub_missing()
        {
            Book_Subscr newbook = new Book_Subscr(book1, sub1, Convert.ToDateTime("11.05.2017"));
            library.Books_in_Lib.RemoveAt(library.Book_Index(book1));
            library.Books_in_Lib.Add(newbook);
            Assert.AreEqual(sub1.Missing_book()[0].Name, "Преступление и наказание");
        }

        //Проверяем обращение к книге по индексу
        [Test]
        public void Test_Sub_BookforIndex()
        {
            Assert.AreEqual(sub1[0].Name, "Преступление и наказание");
        }
    }
}
