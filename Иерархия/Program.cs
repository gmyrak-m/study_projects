using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Program
{
    static int SIZE = 0;

    static bool[,] readMatrix(string file)
    {

        bool[,] data = new bool[1, 1];
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

            for (int m = 0; m < SIZE; m++)
            {
                data[n, m] = (line[m] == '1') || m == n;
            }
            n++;
        }

        return data;
    }

    static bool[,] mult(bool[,] A, bool[,] B)
    {
        bool[,] M = new bool[SIZE, SIZE];

        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                bool s = false;
                for (int k = 0; k < SIZE; k++)
                {
                    s |= A[i, k] & B[k, j];
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

    static void printSet(HashSet<int> s)
    {
        Console.Write("{");
        foreach (int k in s)
        {
            Console.Write("{0},", k);
        }
        Console.Write("} ");
    }


    static void Main(string[] args)
    {
        bool[,] SM = readMatrix("input.txt");

        bool[,] D = SM;
        bool[,] Mn = SM;

        for (int i = 0; i < SIZE - 1; i++)
        {
            Mn = mult(Mn, SM);
            D = plus(D, Mn);
        }
        //-------- получили матрицу достижимости ---------------

        HashSet<int>[,] COL = new HashSet<int>[SIZE, 3];
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                COL[i, j] = new HashSet<int>();
            }
        }
        //--------- заполнили таблицу пустыми множествами -------------------


        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                if (D[i, j]) COL[i, 0].Add(j + 1);
            }
        }
        // ------- заполнили 2-й столбец. Вершины, которых можно достичь из i-ой вершины. (Достижимые вершины)



        for (int j = 0; j < SIZE; j++)
        {
            for (int i = 0; i < SIZE; i++)
            {
                if (D[i, j]) COL[j, 1].Add(i + 1);
            }
        }
        // ------- заполнили 3-й столбец. Вершины, из которые можно достигнуть i-ю вершину. (Вершины - предшественницы)


        for (int i = 0; i < SIZE; i++)
        {
            COL[i, 2] = new HashSet<int>(COL[i, 0]);
            COL[i, 2].IntersectWith(COL[i, 1]);
        }
        //------ заполнили 4-й столбец. Достижимые вершины и вершины - предшественницы. (Общие вершины)

   
        HashSet<int> exept = new HashSet<int>();
        exept.Add(1);
       
        while (exept.Count > 0)
        {

            exept = new HashSet<int>();

            for (int i = 0; i < SIZE; i++)
            {
                if (COL[i, 2].Count > 0)
                {
                     if (COL[i, 1].SetEquals(COL[i, 2]))
                    {
                        exept.Add(i + 1);
                    }
                }
            }

            if (exept.Count > 0)
            {
                printSet(exept);
                Console.WriteLine();

                //------ Сравнили столбцы 3 и 4. Если совпадают, номера строк записали в exept (для последующего исключения).


                for (int i = 0; i < SIZE; i++)
                {
                    if (COL[i, 2].Count > 0)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            COL[i, j].ExceptWith(exept);
                        }
                    }

                }
                //--- Исключили числа найденные на предыдущем этапе
            }

        }

    }
}
    