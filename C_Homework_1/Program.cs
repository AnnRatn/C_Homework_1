using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Homework_1
{
    public struct Book_Subscr
    {
        public Book book;
        public Subscriber subscr;
        public string date;
    }

    public class Library
    {
        public List<Book_Subscr> Books_in_Lib = new List<Book_Subscr>();
        
        //добавление книг
        public void Add_book(Book book)
        {
            Book_Subscr newbook = new Book_Subscr();
            newbook.book = book;
            newbook.subscr = null;
            newbook.date = null;
            Books_in_Lib.Add(newbook);
        }

        //книги на руках у абонентов
        public void Books_abon()
        {
            for(int i = 0; i < Books_in_Lib.Count(); i++)
            {
                if(Books_in_Lib[i].subscr != null)
                {
                    Console.WriteLine(Books_in_Lib[i].book.Name);
                }
            }
        }

        //книги в библиотеке
        public void Books_lib()
        {
            for (int i = 0; i < Books_in_Lib.Count(); i++)
            {
                if (Books_in_Lib[i].subscr == null)
                {
                    Console.WriteLine(Books_in_Lib[i].book.Name);
                }
            }
        }

        //поиск по автору
        public void Author_search(string aurth_name)
        {
            bool exist = false;
            for(int i = 0; i < Books_in_Lib.Count(); i++)
            {
                if(Books_in_Lib[i].book.Author == aurth_name)
                {
                    exist = true;
                    Console.WriteLine("Название: {0}, автор: {1}", Books_in_Lib[i].book.Name, Books_in_Lib[i].book.Author);
                    if (Books_in_Lib[i].subscr == null) Console.WriteLine("выдана");
                    else Console.WriteLine("в библиотеке");
                }
            }
            if (exist == false) Console.WriteLine("Такой книги нет");
        }

        //поиск по названию
        public void Name_search(string book_name)
        {
            bool exist = false;
            for (int i = 0; i < Books_in_Lib.Count(); i++)
            {
                if (Books_in_Lib[i].book.Name == book_name)
                {
                    exist = true;
                    Console.WriteLine("Название: {0}, автор: {1}", Books_in_Lib[i].book.Name, Books_in_Lib[i].book.Author);
                    if (Books_in_Lib[i].subscr == null) Console.WriteLine("выдана");
                    else Console.WriteLine("в библиотеке");
                }
            }
            if (exist == false) Console.WriteLine("Такой книги нет");
        }

        //выдача книг
        public void Give_book(Subscriber sub, Book book)
        {
            if (Books_in_Lib.Count() != 0)
            {
                bool active = true;
                int num_book = 0;
                int rare_num_book = 0;
                int currbook = -1;
                for (int i = 0; i < Books_in_Lib.Count(); i++)
                {
                    if (Books_in_Lib[i].book == book)
                    {
                        currbook = i;
                        if (Books_in_Lib[i].subscr != null) active = false;
                    }
                    else
                    {
                        if (Books_in_Lib[i].subscr != null)
                        {
                            if (Books_in_Lib[i].subscr == sub)
                            {
                                num_book++;
                                if (Books_in_Lib[i].book.Rare == true) rare_num_book++;
                            }
                            if (DateTime.Today.Subtract(Convert.ToDateTime(Books_in_Lib[i].date)).TotalDays > 14) active = false;
                        }
                    }
                }
                if (currbook == -1) Console.WriteLine("Данной книги в библиотеке нет");
                else
                {
                    if (active && (num_book < 5) && ((rare_num_book < 2 && book.Rare != true) || (rare_num_book < 1 && book.Rare == true)))
                    {
                        Books_in_Lib.RemoveAt(currbook);
                        Book_Subscr newbook = new Book_Subscr();
                        newbook.book = book;
                        newbook.subscr = sub;
                        newbook.date = DateTime.Today.Date.ToShortDateString();
                        Books_in_Lib.Add(newbook);
                    }
                    else Console.WriteLine("Выдача книги невозможна");
                }
            }
            else Console.WriteLine("В библиотеке нет книг");
        }
        //возврат книг
        public void Return_book(Book book)
        {
            if (Books_in_Lib.Count() != 0)
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
                if (current == -1) Console.WriteLine("Книгу брали не из данной библиотеки");
                else
                {
                    Books_in_Lib.RemoveAt(current);
                    Book_Subscr newbook = new Book_Subscr();
                    newbook.book = book;
                    newbook.subscr = null;
                    newbook.date = null;
                    Books_in_Lib.Add(newbook);
                }
            }
            else Console.WriteLine("Возврат не возможен, в библиотеке нет книг");
        }
    }

    public class Book
    {
        public string Author;
        public string Name;
        public bool Rare;
        Library library { get; }
        public Book(Library lib)
        {
            library = lib;
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
        public void Where_book()
        {
            for (int i = 0; i < library.Books_in_Lib.Count(); i++)
            {
                if ((library.Books_in_Lib[i].book.Name == Name)&&(library.Books_in_Lib[i].book.Author == Author))
                {
                    if (library.Books_in_Lib[i].subscr == null) Console.WriteLine("Книга в библиотеке");
                    else Console.WriteLine("Книга " + Name + " предоставлена: " + library.Books_in_Lib[i].subscr.Name);
                    break;
                }
            }
        }

        //Когда книга была выдана, если она находится у абонента
        public void When_book()
        {
            for (int i = 0; i < library.Books_in_Lib.Count(); i++)
            {
                if ((library.Books_in_Lib[i].book.Name == Name) && (library.Books_in_Lib[i].book.Author == Author))
                {
                    if (library.Books_in_Lib[i].subscr == null) Console.WriteLine("Книга в библиотеке");
                    else Console.WriteLine("Книга " + Name + " выдана: " + library.Books_in_Lib[i].date);
                    break;
                }
            }
        }
    }

    public class Subscriber
    {
        public string Name;
        public string Phone;
        Library library { get; }
        public Subscriber(Library lib)
        {
            library = lib;
        }

        //Основные свойства абонента(имя, телефон)
        public void Describtion()
        {
            Console.WriteLine("Имя: " + Name);
            Console.WriteLine("Телефон: " + Phone);
        }

        //Список книг, которые находятся у него на руках
        public void Having_book()
        {
            Console.WriteLine("Книги на руках абонента " + Name + ":");
            for (int i = 0; i < library.Books_in_Lib.Count(); i++)
            {
                if((library.Books_in_Lib[i].subscr != null) && (library.Books_in_Lib[i].subscr.Name == Name) && (library.Books_in_Lib[i].subscr.Phone == Phone))
                {
                    Console.WriteLine(library.Books_in_Lib[i].book.Name);
                }
            }
        }

        //Список просроченных книг у него на руках
        public void Missing_book()
        {
            Console.WriteLine("Просроченные книги абонента " + Name + ":");
            for (int i = 0; i < library.Books_in_Lib.Count(); i++)
            {
                if ((library.Books_in_Lib[i].subscr != null) && (library.Books_in_Lib[i].subscr.Name == Name) && (library.Books_in_Lib[i].subscr.Phone == Phone))
                {
                    if(DateTime.Today.Subtract(Convert.ToDateTime(library.Books_in_Lib[i].date)).TotalDays > 14)
                    {
                        Console.WriteLine(library.Books_in_Lib[i].book.Name);
                    }
                }
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Описание книг: ");
            Library library = new Library();
            Book book1 = new Book(library);
            book1.Author = "Достоевский Ф. М.";
            book1.Name = "Преступление и наказание";
            book1.Rare = false;
            book1.Describtion();
            library.Add_book(book1);

            Book book2 = new Book(library);
            book2.Author = "Данте А.";
            book2.Name = "Божественная комедия";
            book2.Rare = true;
            book2.Describtion();
            library.Add_book(book2);

            Book book3 = new Book(library);
            book3.Author = "Митчел М.";
            book3.Name = "Унесенные ветром";
            book3.Rare = false;
            book3.Describtion();
            library.Add_book(book3);

            Book book4 = new Book(library);
            book4.Author = "Купер Ф.";
            book4.Name = "Зверобой";
            book4.Rare = false;
            book4.Describtion();
            library.Add_book(book4);

            Book book5 = new Book(library);
            book5.Author = "Шекспир";
            book5.Name = "Гамлет";
            book5.Rare = true;
            book5.Describtion();
            library.Add_book(book5);

            Book book6 = new Book(library);
            book6.Author = "Гоголь Н. В.";
            book6.Name = "Шинель";
            book6.Rare = false;
            book6.Describtion();
            library.Add_book(book6);

            Book book7 = new Book(library);
            book7.Author = "Пушкин А. С.";
            book7.Name = "Капитанская дочка";
            book7.Rare = false;
            book7.Describtion();
            library.Add_book(book7);

            Console.WriteLine();
            Console.WriteLine("Описание абонентов:");
            Subscriber sub1 = new Subscriber(library);
            sub1.Name = "Иванов Федор";
            sub1.Phone = "8(911)5555555";
            sub1.Describtion();

            Subscriber sub2 = new Subscriber(library);
            sub2.Name = "Капитанова Ирина";
            sub2.Phone = "9(931)4444444";
            sub2.Describtion();
            Console.WriteLine();


            //проверка работы
            //Выдача более 5ти книг
            library.Give_book(sub1, book1);
            book1.Where_book();
            book1.When_book();
            library.Give_book(sub1, book2);
            book2.Where_book();
            book2.When_book();
            library.Give_book(sub1, book3);
            book3.Where_book();
            book3.When_book();
            library.Give_book(sub1, book4);
            book4.Where_book();
            book4.When_book();
            library.Give_book(sub1, book6);
            book6.Where_book();
            book6.When_book();
            library.Give_book(sub1, book7);
            sub1.Having_book();

            Console.WriteLine();
            Console.WriteLine("Книги в библиотеке");
            library.Books_lib();
            Console.WriteLine();
            Console.WriteLine("Книги у абонентов");
            library.Books_abon();

            //Возврат книг
            library.Return_book(book1);
            library.Return_book(book2);
            library.Return_book(book3);
            library.Return_book(book4);
            library.Return_book(book6);
            Console.WriteLine();
            Console.WriteLine("Книги в библиотеке");
            library.Books_lib();

            //Выдача более 2х редких книг
            Console.WriteLine();
            library.Give_book(sub2, book2);
            library.Give_book(sub2, book5);

            //подменим дату у выданной книги на более раннюю
            int current = -1;
            for (int i = 0; i < library.Books_in_Lib.Count(); i++)
            {
                if (library.Books_in_Lib[i].book == book1)
                {
                    current = i;
                    break;
                }
            }
            Book_Subscr newbook = new Book_Subscr();
            newbook.book = book1;
            newbook.subscr = sub2;
            newbook.date = "11.05.2017";
            library.Books_in_Lib.RemoveAt(current);
            library.Books_in_Lib.Add(newbook);
            book1.When_book();

            //выдача данному пользователю
            library.Give_book(sub2, book1);
            sub2.Missing_book();

            Console.WriteLine();
            //Поиск по названию
            library.Name_search("Шинель");
            //Поиск по автору
            library.Author_search("Достоевский Ф. М.");

            Console.ReadKey();
        }
    }
}
