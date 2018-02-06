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

            List<String> menus = Barnave.GetMenus();
            foreach(var item in menus)
            {
                Console.WriteLine(item);
            }

        }
    }
}