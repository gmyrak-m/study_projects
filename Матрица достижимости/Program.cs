using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Матрица_достижимости
{
    class Program
    {
        static void Main(string[] args)
        {
            //Hello!!!
            // fix1

            bool[,] S = new bool[1,1];
            bool init = false;
            int RANGE = 0;
            int n = 0;

            foreach (string line in File.ReadLines("Смежность.txt"))
            {
                if (!init)
                {
                    RANGE = line.Length;
                    init = true;
                    S = new bool[RANGE, RANGE];
                }

                for(int m=0; m< RANGE; m++)
                {
                    S[n, m] = line[m] == '1';
                }
                n++;
            }

            string[] output = new string[RANGE];

            for(int i=0; i<RANGE; i++)
            {
                string line = "";
                for(int j=0; j<RANGE; j++)
                {
                    line += " " + S[i, j];
                }
                output[i] = line;

            }
            File.WriteAllLines("Достижимость.txt", output);
        }
    }
}
