using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ромб
{
    class Program
    {
        static void Main(string[] args)

        {
            const int center = 40;
            const int hight = 14;

            for (int i = 0; i < hight; i++) starLine(center - i, center + i);
            for (int i = hight; i >= 0; i--) starLine(center - i, center + i);
            //starLine(2, 4);


        }


        static void starLine(int a, int b)
        {
            for (int i = 0; i < a; i++) Console.Write(" ");
            for (int i = 0; i < b-a+1; i++) Console.Write("*");
            Console.WriteLine();
           
        }
    }
} 

        
