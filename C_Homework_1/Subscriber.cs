using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace C_Homework_1
{
    [DataContract]
    public class Subscriber
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Phone { get; private set; }

        [DataMember]
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
                if ((library.Books_in_Lib[i].subscr != null) && (library.Books_in_Lib[i].subscr == this))
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
                    if (DateTime.Today.Subtract(library.Books_in_Lib[i].date).TotalDays > 14)
                    {
                        books.Add(library.Books_in_Lib[i].book);
                    }
                }
            }
            return books;
        }
    }
}
