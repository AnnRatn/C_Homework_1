using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Homework_1
{
    public struct Book_Subscr
    {
        public readonly Book book;
        public readonly Subscriber subscr;
        public readonly DateTime date;
        public Book_Subscr(Book b, Subscriber sub, DateTime dat)
        {
            book = b;
            subscr = sub;
            date = dat;
        }
    }

    public class Library
    {
        public List<Book_Subscr> Books_in_Lib = new List<Book_Subscr>();
        
        //Поиск по автору или названию
        public Book_Subscr this[string author, string name]
        {
            get
            {
                Book_Subscr b = new Book_Subscr();
                foreach(Book_Subscr book in Books_in_Lib)
                {
                    if(book.book.Name == name || book.book.Author == author)
                    {
                        b = book;
                        break;
                    }
                }
                if (b.book == null) throw new Exception();
                else return b;
            }
        }

        //добавление книг
        public void Add_book(Book book)
        {
            Book_Subscr newbook = new Book_Subscr(book, null, DateTime.Today.Date);
            Books_in_Lib.Add(newbook);
        }

        //книги на руках у абонентов
        public List<Book> Books_abon()
        {
            List<Book> books = new List<Book>();
            for(int i = 0; i < Books_in_Lib.Count(); i++)
            {
                if(Books_in_Lib[i].subscr != null)
                {
                    books.Add(Books_in_Lib[i].book);
                }
            }
            return books;
        }

        //книги в библиотеке
        public List<Book> Books_lib()
        {
            List<Book> books = new List<Book>();
            for (int i = 0; i < Books_in_Lib.Count(); i++)
            {
                if (Books_in_Lib[i].subscr == null)
                {
                    books.Add(Books_in_Lib[i].book);
                }
            }
            return books;
        }

        //поиск по автору
        public Book_Subscr Author_search(string aurth_name)
        {
            Book_Subscr book = new Book_Subscr();
            for(int i = 0; i < Books_in_Lib.Count(); i++)
            {
                if(Books_in_Lib[i].book.Author == aurth_name)
                {
                    book = Books_in_Lib[i];
                }
            }
            return book;
        }

        //поиск по названию
        public Book_Subscr Name_search(string book_name)
        {
            Book_Subscr book = new Book_Subscr();
            for (int i = 0; i < Books_in_Lib.Count(); i++)
            {
                if (Books_in_Lib[i].book.Name == book_name)
                {
                    book = Books_in_Lib[i];
                }
            }
            return book;
        }

        //поиск индекса книги
        public int Book_Index(Book book)
        {
            int current = -1;
            for (int i = 0; i < Books_in_Lib.Count(); i++)
            {
                if (Books_in_Lib[i].book == book)
                {
                    current = i;
                    break;
                }
            }
            return current;
        }

        //выдача книг
        public void Give_book(Subscriber sub, Book book)
        {
            if (Books_in_Lib.Count != 0)
            {
                if (sub.Missing_book().Count != 0) Console.WriteLine("Выдача книги не возможна");
                else
                {
                    if (sub.Having_book().Count == 5) Console.WriteLine("Выдача книги не возможна");
                    else
                    {
                        int rare_num_book = 0;
                        foreach (Book b in sub.Having_book())
                        {
                            if (b.Rare == true) rare_num_book++;
                        }
                        if ((rare_num_book < 2 && book.Rare != true) || (rare_num_book < 1 && book.Rare == true))
                        {
                            int num = Book_Index(book);
                            if (num != -1)
                            {
                                Books_in_Lib.RemoveAt(Book_Index(book));
                                Book_Subscr newbook = new Book_Subscr(book, sub, DateTime.Today.Date);
                                Books_in_Lib.Add(newbook);
                            }
                            else Console.WriteLine("Такой книги нет");
                        }
                        else Console.WriteLine("Выдача книги невозможна");
                    }
                }
            }
            else Console.WriteLine("В библиотеке нет книг");
        }
        //возврат книг
        public void Return_book(Book book)
        {
            if (Books_in_Lib.Count() != 0)
            {
                int num = Book_Index(book);
                if (num == -1) Console.WriteLine("Книгу брали не из данной библиотеки");
                else
                {
                    Books_in_Lib.RemoveAt(num);
                    Book_Subscr newbook = new Book_Subscr(book, null, DateTime.Today.Date);
                    Books_in_Lib.Add(newbook);
                }
            }
            else Console.WriteLine("Возврат не возможен, в библиотеке нет книг");
        }
    }

    public class Book
    {
        public string Author { get; private set; }
        public string Name { get; private set; }
        public bool Rare { get; private set; }
        public Library library { get; private set; }
        public Book(Library lib, string auth, string name, bool rare)
        {
            library = lib;
            Author = auth;
            Name = name;
            Rare = rare;

        }

        //Основные свойства книги(автор, название, редкость книги)
        public void Describtion()
        {
            Console.WriteLine("Автор: " + Author);
            Console.WriteLine("Название: " + Name);
            if (Rare == true) Console.WriteLine("редкая книга");
            else Console.WriteLine("распространенная книга");
        }

        //Находится книга в библиотеке или у абонента(у какого именно абонента)
        public string Where_book()
        {
            string where = "";
            for (int i = 0; i < library.Books_in_Lib.Count(); i++)
            {
                if ((library.Books_in_Lib[i].book == this))
                {
                    if (library.Books_in_Lib[i].subscr == null) where = "Книга в библиотеке";
                    else where = "Книга " + Name + " предоставлена: " + library.Books_in_Lib[i].subscr.Name;
                    break;
                }
            }
            return where;
        }

        //Когда книга была выдана, если она находится у абонента
        public string When_book()
        {
            string when = "";
            for (int i = 0; i < library.Books_in_Lib.Count(); i++)
            {
                if ((library.Books_in_Lib[i].book == this))
                {
                    if (library.Books_in_Lib[i].subscr == null) when = "Книга в библиотеке";
                    else when = "Книга " + Name + " выдана: " + library.Books_in_Lib[i].date.ToShortDateString();
                    break;
                }
            }
            return when;
        }
    }

    public class Subscriber
    {
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public Library library { get; private set; }
        public Subscriber(Library lib, string name, string phone)
        {
            library = lib;
            Name = name;
            Phone = phone;
        }

        public Book this[int bookPos]
        {
            get { return Having_book()[bookPos]; }
        }

        //Основные свойства абонента(имя, телефон)
        public void Describtion()
        {
            Console.WriteLine("Имя: " + Name);
            Console.WriteLine("Телефон: " + Phone);
        }

        //Список книг, которые находятся у него на руках
        public List<Book> Having_book()
        {
            List<Book> books = new List<Book>();
            for (int i = 0; i < library.Books_in_Lib.Count(); i++)
            {
                if((library.Books_in_Lib[i].subscr != null) && (library.Books_in_Lib[i].subscr == this))
                {
                    books.Add(library.Books_in_Lib[i].book);
                }
            }
            return books;
        }

        //Список просроченных книг у него на руках
        public List<Book> Missing_book()
        {
            List<Book> books = new List<Book>(); books = new List<Book>();
            for (int i = 0; i < library.Books_in_Lib.Count(); i++)
            {
                if ((library.Books_in_Lib[i].subscr != null) && (library.Books_in_Lib[i].subscr == this))
                {
                    if(DateTime.Today.Subtract(library.Books_in_Lib[i].date).TotalDays > 14)
                    {
                        books.Add(library.Books_in_Lib[i].book);
                    }
                }
            }
            return books;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Описание книг: ");
            Library library = new Library();
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
            library.Give_book(sub1, book4);
            Console.WriteLine(book4.Where_book());
            Console.WriteLine(book4.When_book());
            library.Give_book(sub1, book6);
            Console.WriteLine(book6.Where_book());
            Console.WriteLine(book6.When_book());
            library.Give_book(sub1, book7);
            Console.WriteLine("Книги на руках абонента {0}", sub1.Name);
            foreach (Book book in sub1.Having_book())
            {
                Console.WriteLine("Название: {0}; Автор: {1}", book.Name, book.Author);
            }

            Console.WriteLine();
            Console.WriteLine("Книги в библиотеке");
            foreach(Book book in library.Books_lib())
            {
                Console.WriteLine("Название: {0}; Автор: {1}", book.Name, book.Author);
            }
            Console.WriteLine();
            Console.WriteLine("Книги у абонентов");
            foreach (Book book in library.Books_abon())
            {
                Console.WriteLine("Название: {0}; Автор: {1}", book.Name, book.Author);
            }

            //Возврат книг
            library.Return_book(book1);
            library.Return_book(book2);
            library.Return_book(book3);
            library.Return_book(book4);
            library.Return_book(book6);
            Console.WriteLine();
            Console.WriteLine("Книги в библиотеке");
            foreach (Book book in library.Books_lib())
            {
                Console.WriteLine("Название: {0}; Автор: {1}", book.Name, book.Author);
            }

            //Выдача более 2х редких книг
            Console.WriteLine();
            library.Give_book(sub2, book2);
            library.Give_book(sub2, book5);

            //подменим дату у выданной книги на более раннюю
            Book_Subscr newbook = new Book_Subscr(book1, sub2, Convert.ToDateTime("11.05.2017"));
            library.Books_in_Lib.RemoveAt(library.Book_Index(book1));
            library.Books_in_Lib.Add(newbook);
            book1.When_book();

            //выдача данному пользователю
            library.Give_book(sub2, book1);
            Console.WriteLine("Просроченные книги на руках абонента {0}", sub2.Name);
            foreach (Book book in sub2.Missing_book())
            {
                Console.WriteLine("Название: {0}; Автор: {1}", book.Name, book.Author);
            }

            Console.WriteLine();
            Book_Subscr search = new Book_Subscr();
            try {
                //Поиск по названию
                search = library.Name_search("Шинель");
                Console.WriteLine("Название: {0}; Автор: {1}", search.book.Name, search.book.Author);
                if (search.subscr == null) Console.WriteLine("В библиотеке");
                else Console.WriteLine("Взял в библиотеке: " + search.subscr.Name);
            }
            catch
            {
                Console.WriteLine("такой книги в библиотеке нет");
            }
            try { 
            //Поиск по автору
            search = library.Author_search("Достоевский Ф. М.");
                Console.WriteLine("Название: {0}; Автор: {1}", search.book.Name, search.book.Author);
                if (search.subscr == null) Console.WriteLine("В библиотеке");
                else Console.WriteLine("Взял в библиотеке: " + search.subscr.Name);
            }
            catch
            {
                Console.WriteLine("такой книги в библиотеке нет");
            }
            Console.ReadKey();
        }
    }
}
