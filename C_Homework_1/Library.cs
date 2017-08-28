using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;

namespace C_Homework_1
{
    [DataContract]
    public struct Book_Subscr
    {
        [DataMember]
        public readonly Book book;

        [DataMember]
        public readonly Subscriber subscr;

        [DataMember]
        public readonly DateTime date;

        public Book_Subscr(Book b, Subscriber sub, DateTime dat)
        {
            book = b;
            subscr = sub;
            date = dat;
        }
    }

    public enum Book_State
    {
        inLib,
        notinLib,
    }

    [DataContract]
    public class Library
    {
        [DataMember]
        public List<Book_Subscr> Books_in_Lib = new List<Book_Subscr>();

        [DataMember]
        private Book_State state = Book_State.inLib;

        //событие
        public event EventHandler<Book> BookAdded;
        public event EventHandler<Book_State> StateChanged;

        //Поиск по автору или названию
        public Book_Subscr this[string author, string name]
        {
            get
            {
                Book_Subscr b = new Book_Subscr();
                foreach (Book_Subscr book in Books_in_Lib)
                {
                    if (book.book.Name == name || book.book.Author == author)
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
            BookAdded?.Invoke(this, book);
        }

        //книги на руках у абонентов
        public List<Book> Books_abon()
        {
            List<Book> books = new List<Book>();
            for (int i = 0; i < Books_in_Lib.Count(); i++)
            {
                if (Books_in_Lib[i].subscr != null)
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
            for (int i = 0; i < Books_in_Lib.Count(); i++)
            {
                if (Books_in_Lib[i].book.Author == aurth_name)
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
                                state = Book_State.notinLib;
                                StateChanged?.Invoke(this, state);
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
                    state = Book_State.inLib;
                    StateChanged?.Invoke(this, state);
                }
            }
            else Console.WriteLine("Возврат не возможен, в библиотеке нет книг");
        }

        //Сохранение состояния библиотеки в файл
        public void Serialize(String FileName)
        {
            var serializer = new NetDataContractSerializer();
            var xmlWriterSettings = new XmlWriterSettings { Indent = true };

            using (var xmlWriter =
                            XmlWriter.Create(FileName, xmlWriterSettings))
            {
                serializer.WriteObject(xmlWriter, this);
            }
        }
    }
}
