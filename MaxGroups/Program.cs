using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxGroups
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = new bool[,]
            {
                { false,true,true ,false,false,false,true},
                { true,false,false,false,false,false,false},
                { true,false,false,true,false,false,true},
                { false,false,true,false,false,false,false},
                {false,false,false,false,false,false,false},
                {false,false,false,false,false,false,false},
                {true,false,true,false,false,false,false},
            };

            List<List<int>> result = GetMaxGroups(matrix);
            foreach (var item in result)
            {
                string print = "";
                foreach (var index in item)
                    print += $" {index}";
                Console.WriteLine(print);
            }
        }

        private static List<List<int>> GetMaxGroups(bool[,] matrix)
        {
            var result = new List<List<int>>();
            var queue = new Queue<List<int>>();
            int? lengthMax = null;
            bool[] marks = new bool[matrix.GetLength(0)];
            int numNoMarks = marks.Length;
            //seeding
            queue.Enqueue(Enumerable.Range(0, matrix.GetLength(0)).ToList());
            while (queue.Count > 0 && numNoMarks > 0)
            {
                var currentList = queue.Dequeue();
                bool flag = true;
                for (int i = 0; i < currentList.Count; i++)
                    for (int j = i + 1; j < currentList.Count; j++)
                    {
                        int firstIndex = currentList[i];
                        int secondIndex = currentList[j];
                        if (!matrix[firstIndex, secondIndex])
                        {
                            flag = false;
                            queue.Enqueue(currentList.Where(ind => ind != firstIndex).ToList());
                            queue.Enqueue(currentList.Where(ind => ind != secondIndex).ToList());
                        }
                    }

                if (lengthMax.HasValue && currentList.Count != lengthMax)
                    break;


                if (flag)
                {
                    lengthMax = currentList.Count;
                    if (IncludeInResult(currentList, marks))
                    {
                        result.Add(currentList);
                        foreach (var item in currentList)
                        {
                            if (!marks[item])
                            {
                                marks[item] = true;
                                numNoMarks--;
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static bool IncludeInResult(List<int> currentList, bool[] marks)
        {
            foreach (var item in currentList)
                if (!marks[item])
                    return true;
            return false;
        }
    }
}
