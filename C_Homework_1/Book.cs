using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Homework_1
{
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
}
