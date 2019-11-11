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
        struct matrix 
        {
            public int range;
            public bool[,] data;
        }

        static matrix readMatrix(string file) 
        {            
            
            int size = 0;
            bool[,] data = new bool[1,1];
            bool init = false;            
            int n = 0;

            foreach (string line in File.ReadLines(file))
            {
                if (!init)
                {
                    size = line.Length;                    
                    data = new bool[size, size];                    
                    init = true;
                }

                for(int m=0; m< size; m++)
                {
                    data[n, m] = line[m] == '1';
                }
                n++;
            }

            matrix result;
            result.data = data;
            result.range = size;
            return result;
        }

        static void writeMatrix(matrix m, string file)
        {     
            string[] output = new string[m.range];

            for(int i=0; i<m.range; i++)
            {
                string line = "";
                for(int j=0; j<m.range; j++)
                {
                    line += m.data[i, j] ? '1': '0';
                }
                output[i] = line;
            }
            File.WriteAllLines(file, output);

        }

        static matrix mult(matrix A, matrix B) 
        {           
            int N = A.range;
            bool[,] M = new bool[N, N];

            for(int i=0; i<N; i++) 
            {
                for(int j=0; j<N; j++) 
                {
                    bool s = false;
                    for(int k=0; k<N; k++) {
                        s |= A.data[i,k] & B.data[k,j];
                    }
                    M[i, j] = s;
                }
            }

            matrix result;
            result.data = M;
            result.range = N;
            return result;
        }

        static matrix plus(matrix A, matrix B)
        {
            int N = A.range;
            bool[,] M = new bool[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    M[i, j] = A.data[i, j] | B.data[i, j];
                }
            }
            matrix result;
            result.data = M;
            result.range = N;
            return result;
        }




        static void Main(string[] args)
        {

            matrix M = readMatrix("input.txt");
            int size = M.range;
            matrix S = M;
            matrix Mn = M;

            for (int i = 0; i < size-1; i++) 
            {
                Mn = mult(Mn, M);
                S = plus(S, Mn);
            }

            writeMatrix(S, "output.txt");

        }

        

    }
}
