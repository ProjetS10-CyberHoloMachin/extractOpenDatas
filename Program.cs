using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class MainClass 
    {
        static void Main()
        {
            /*
             *  Le prochain cours
             */
//            dynamic[] coursNext = CoursAde.GetNext();
//            foreach (var item in coursNext)
//            {
//                Console.WriteLine(item);
//            }

            /*
             *  La liste des salles vides
             */
//            List<String> coursLibres = CoursAde.GetSallesLibres();
//            foreach(var item in coursLibres)
//            {
//                Console.WriteLine(item);
//            }

            List<List<List<string>>> menus = RestoU.GetMenus("ru-lepicea");
            foreach(var item in menus)
            {
                foreach(var item2 in item)
                {
                    Console.Write(" - ");
                    foreach(var item3 in item2)
                    {
                        Console.WriteLine(item3);
                    }
                }
                Console.WriteLine();
            }

        }
    }
}