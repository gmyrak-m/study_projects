using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Лаб_4
{
    class Program
    {
        static int SIZE= 0;
        static float[,] M = new float[1,1]; 
        

        static void readMatrix(string file)
        {
            
            bool init = false;
            int i = 0;

            foreach (string line in File.ReadLines(file))
            {
                
                float[] Ms = line.Split(' ').Select(x => float.Parse(x)).ToArray();

                if (!init)
                {
                    SIZE = Ms.Length;
                    M = new float[SIZE, SIZE];
                    init = true;
                }

                for (int j=0; j<SIZE; j++)
                {
                    M[i, j] = Ms[j];
                }

                i++;
            }
        }


        static void Main(string[] args)
        {
            readMatrix("input.txt");

            for(int i=0; i<SIZE; i++)
            {
                for(int j=0; j<SIZE; j++)
                {
                    Console.Write("{0} ", M[i, j]);
                }
                Console.WriteLine();
            }

        }
    }
}
