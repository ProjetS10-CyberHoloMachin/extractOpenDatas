using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class MainClass 
    {
        static void Main()
        {
            List<String> cours = CoursAde.GetSallesLibres();
            foreach(var item in cours)
            {
                Console.WriteLine(item);
            }
        }
    }
}