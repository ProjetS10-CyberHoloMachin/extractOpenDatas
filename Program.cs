using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class MainClass 
    {
        static void Main()
        {
            dynamic[] cours = CoursAde.GetNext();

            foreach (var item in cours)
            {
                Console.WriteLine(item);
            }


//            List<String> cours = CoursAde.GetSallesLibres();
//            foreach(var item in cours)
//            {
//                Console.WriteLine(item);
//            }
        }
    }
}