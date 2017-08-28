using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;

namespace C_Homework_1
{
    class Program
    {
        static void Main(string[] args)
        {
            EventHandler<Book_State> eventHandler = (sender, state) => Console.WriteLine("Состояние книги: {0}", state);
            EventHandler<Book> eventHandler1 = (sender, book) => Console.WriteLine("Добавлена новая книга " + book.Name);

            Console.WriteLine("Описание книг: ");
            Library library = new Library();
            library.BookAdded += eventHandler1;
            library.StateChanged += eventHandler;
            Book book1 = new Book(library, "Достоевский Ф. М.", "Преступление и наказание", false);
            book1.Describtion();
            library.Add_book(book1);

            Book book2 = new Book(library, "Данте А.", "Божественная комедия", true);
            book2.Describtion();
            library.Add_book(book2);

            Book book3 = new Book(library, "Митчел М.", "Унесенные ветром", false);
            book3.Describtion();
            library.Add_book(book3);

            Book book4 = new Book(library, "Купер Ф.", "Зверобой", false);
            book4.Describtion();
            library.Add_book(book4);

            Book book5 = new Book(library, "Шекспир В.", "Гамлет", true);
            book5.Describtion();
            library.Add_book(book5);

            Book book6 = new Book(library, "Гоголь Н. В.", "Шинель", false);
            book6.Describtion();
            library.Add_book(book6);

            Book book7 = new Book(library, "Пушкин А. С.", "Капитанская дочка", false);
            book7.Describtion();
            library.Add_book(book7);

            Console.WriteLine();
            Console.WriteLine("Описание абонентов:");
            Subscriber sub1 = new Subscriber(library, "Иванов Федор", "8(911)5555555");
            sub1.Describtion();

            Subscriber sub2 = new Subscriber(library, "Капитанова Ирина", "9(931)4444444");
            sub2.Describtion();
            Console.WriteLine();


            //проверка работы
            //Выдача более 5ти книг
            library.Give_book(sub1, book1);
            Console.WriteLine(book1.Where_book());
            Console.WriteLine(book1.When_book());
            library.Give_book(sub1, book2);
            Console.WriteLine(book2.Where_book());
            Console.WriteLine(book2.When_book());
            library.Give_book(sub1, book3);
            Console.WriteLine(book3.Where_book());
            Console.WriteLine(book3.When_book());
            //library.Give_book(sub1, book4);
            //Console.WriteLine(book4.Where_book());
            //Console.WriteLine(book4.When_book());
            //library.Give_book(sub1, book6);
            //Console.WriteLine(book6.Where_book());
            //Console.WriteLine(book6.When_book());
            //library.Give_book(sub1, book7);
            //Console.WriteLine("Книги на руках абонента {0}", sub1.Name);
            //foreach (Book book in sub1.Having_book())
            //{
            //    Console.WriteLine("Название: {0}; Автор: {1}", book.Name, book.Author);
            //}

            //Console.WriteLine();
            //Console.WriteLine("Книги в библиотеке");
            //foreach(Book book in library.Books_lib())
            //{
            //    Console.WriteLine("Название: {0}; Автор: {1}", book.Name, book.Author);
            //}
            //Console.WriteLine();
            //Console.WriteLine("Книги у абонентов");
            //foreach (Book book in library.Books_abon())
            //{
            //    Console.WriteLine("Название: {0}; Автор: {1}", book.Name, book.Author);
            //}

            ////Возврат книг
            library.Return_book(book1);
            //library.Return_book(book2);
            //library.Return_book(book3);
            //library.Return_book(book4);
            //library.Return_book(book6);
            //Console.WriteLine();
            //Console.WriteLine("Книги в библиотеке");
            //foreach (Book book in library.Books_lib())
            //{
            //    Console.WriteLine("Название: {0}; Автор: {1}", book.Name, book.Author);
            //}

            ////Выдача более 2х редких книг
            //Console.WriteLine();
            //library.Give_book(sub2, book2);
            //library.Give_book(sub2, book5);

            ////подменим дату у выданной книги на более раннюю
            //Book_Subscr newbook = new Book_Subscr(book1, sub2, Convert.ToDateTime("11.05.2017"));
            //library.Books_in_Lib.RemoveAt(library.Book_Index(book1));
            //library.Books_in_Lib.Add(newbook);
            //book1.When_book();

            ////выдача данному пользователю
            //library.Give_book(sub2, book1);
            //Console.WriteLine("Просроченные книги на руках абонента {0}", sub2.Name);
            //foreach (Book book in sub2.Missing_book())
            //{
            //    Console.WriteLine("Название: {0}; Автор: {1}", book.Name, book.Author);
            //}

            //Console.WriteLine();
            //Book_Subscr search = new Book_Subscr();
            //try {
            //    //Поиск по названию
            //    search = library.Name_search("Шинель");
            //    Console.WriteLine("Название: {0}; Автор: {1}", search.book.Name, search.book.Author);
            //    if (search.subscr == null) Console.WriteLine("В библиотеке");
            //    else Console.WriteLine("Взял в библиотеке: " + search.subscr.Name);
            //}
            //catch
            //{
            //    Console.WriteLine("такой книги в библиотеке нет");
            //}
            //try { 
            ////Поиск по автору
            //search = library.Author_search("Достоевский Ф. М.");
            //    Console.WriteLine("Название: {0}; Автор: {1}", search.book.Name, search.book.Author);
            //    if (search.subscr == null) Console.WriteLine("В библиотеке");
            //    else Console.WriteLine("Взял в библиотеке: " + search.subscr.Name);
            //}
            //catch
            //{
            //    Console.WriteLine("такой книги в библиотеке нет");
            //}

            //Book_State s = Book_State.notinLib;
            //Console.WriteLine(s.ToString());

            var serializer = new NetDataContractSerializer();
            var xmlWriterSettings = new XmlWriterSettings { Indent = true };

            using (var xmlWriter =
                            XmlWriter.Create("library.txt", xmlWriterSettings))
            {
                serializer.WriteObject(xmlWriter, library);
            }

            using (var xmlReader =
                            XmlReader.Create("library.txt"))
            {
                Library desLibrary = (Library)serializer.ReadObject(xmlReader);
                Console.WriteLine(desLibrary.Books_lib().Count());
            }


            Console.ReadKey();
        }
    }
}
