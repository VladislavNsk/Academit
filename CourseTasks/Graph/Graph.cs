using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Graph
    {
        static void Main(string[] args)
        {
            int[] array = { 5, 4, 3 };

            var dict = new Dictionary<string, int[]>();


            dict.Add("fff", array);

            dict["fff"] =new int[] { 0,0,0,};

            Console.WriteLine(string.Join(", ",dict["fff"]));
            int[,] graph =
            {
                { 0, 1, 0, 0, 0, 0, 0, 0 },
                { 1, 0, 1, 1, 0, 0, 0, 0 },
                { 0, 1, 0, 1, 0, 0, 0, 0 },
                { 0, 1, 1, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 1, 1, 0 },
                { 0, 0, 0, 0, 1, 0, 1, 0 },
                { 0, 0, 0, 0, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 }
            };

            bool[] visited = new bool[graph.GetLength(0)];

            //Чтобы из таблицы получить все вершины, смежные с i, мы
            //должны пройтись по строке с номером i, и найти все
            //индексы, по которым стоит 1
        }
    }
}
