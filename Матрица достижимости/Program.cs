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

        static int SIZE = 0;



        static bool[,] readMatrix(string file) 
        {            
                        
            bool[,] data = new bool[1,1];
            bool init = false;            
            int n = 0;

            foreach (string line in File.ReadLines(file))
            {                
                if (!init)
                {
                    SIZE = line.Length;                    
                    data = new bool[SIZE, SIZE];                    
                    init = true;
                }

                for(int m=0; m< SIZE; m++)
                {
                    data[n, m] = line[m] == '1';
                }
                n++;
            }

            return data;
        }

        static void writeMatrix(bool[,] m, string file)
        {     
            string[] output = new string[SIZE];

            for(int i=0; i<SIZE; i++)
            {
                string line = "";
                for(int j=0; j<SIZE; j++)
                {
                    line += m[i, j] ? '1': '0';
                }
                output[i] = line;
            }
            File.WriteAllLines(file, output);

        }

        static bool[,] mult(bool[,] A, bool[,] B) 
        {                       
            bool[,] M = new bool[SIZE, SIZE];

            for(int i=0; i<SIZE; i++) 
            {
                for(int j=0; j<SIZE; j++) 
                {
                    bool s = false;
                    for(int k=0; k<SIZE; k++) {
                        s |= A[i,k] & B[k,j];
                    }
                    M[i, j] = s;
                }
            }

            return M;
        }

        static bool[,] plus(bool[,] A, bool[,] B)
        {
            bool[,] M = new bool[SIZE, SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    M[i, j] = A[i, j] | B[i, j];
                }
            }
            return M;
        }

        static void Main(string[] args)
        {

            bool[,] M = readMatrix("input.txt");            
            bool[,] S = M;
            bool[,] Mn = M;

            for (int i = 0; i < SIZE-1; i++) 
            {
                Mn = mult(Mn, M);
                S = plus(S, Mn);
            }

            writeMatrix(S, "output.txt");

        }
    }
}
