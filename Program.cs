using System;

namespace ConsoleApp1
{
    class Hello 
    {
        static void Main()
        {
            dynamic[] cours = CoursAde.GetNext();
            foreach(var item in cours)
            {
                Console.WriteLine(item.ToString());
            }
            
        }
    }
}