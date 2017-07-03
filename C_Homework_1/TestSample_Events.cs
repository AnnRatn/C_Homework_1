using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace C_Homework_1
{
    [TestFixture]
    class TestSample_Events
    {
        [Test]
        public void Test_Ev_BookAdded()
        {
            Library library = new Library();
            string s = "";
            EventHandler<Book> eventHandler1 = (sender, b) => s = b.Name;
            library.BookAdded += eventHandler1;
            Book book = new Book(library, "Достоевский Ф. М.", "Преступление и наказание", false);
            library.Add_book(book);
            Assert.AreEqual(s, "Преступление и наказание");
        }

        [Test]
        public void Test_Ev_StateChanged()
        {
            Library library = new Library();
            string s = "";
            EventHandler<Book_State> eventHandler = (sender, state) => s = state.ToString();
            library.StateChanged += eventHandler;
            EventHandler<Book> eventHandler1 = (sender, b) => s = b.Name;
            library.BookAdded += eventHandler1;
            Book book = new Book(library, "Достоевский Ф. М.", "Преступление и наказание", false);
            Subscriber sub = new Subscriber(library, "Иванов Федор", "8(911)5555555");
            library.Add_book(book);
            library.Give_book(sub, book);
            Assert.AreEqual("notinLib", s);
            library.Return_book(book);
            Assert.AreEqual("inLib", s);
        }
    }
}
