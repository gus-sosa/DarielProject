using System;
using System.Collections;
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
            var queue = new Queue<BitArray>();
            var dict = new HashSet<BitArray>();
            var seed = new BitArray(matrix.GetLength(0), true);
            var marks = new bool[seed.Count];
            queue.Enqueue(seed);
            var result = new HashSet<BitArray>();
            while (queue.Count > 0)
            {
                var currentSet = queue.Dequeue();
                var flag = true;
                for (int i = 0; i < currentSet.Count - 1; i++)
                    for (int j = i + 1; currentSet[i] && j < currentSet.Count; j++)
                        if (currentSet[j] && !matrix[i, j])
                        {
                            flag = false;
                            var num1 = new BitArray(currentSet);
                            num1[i] = false;
                            if (!dict.Contains(num1))
                                queue.Enqueue(num1);
                            var num2 = new BitArray(currentSet);
                            num2[j] = false;
                            if (!dict.Contains(num2))
                                queue.Enqueue(num2);
                        }

                if (flag && !result.Contains(currentSet) && AllAreMarked(currentSet, marks))
                {
                    result.Add(currentSet);

                    for (int i = 0; i < currentSet.Length; i++)
                        marks[i] = !marks[i] && currentSet[i];
                }
            }

            var list2 = new List<List<int>>();
            foreach (var item in result)
            {
                var list = new List<int>();
                for (int i = 0; i < item.Length; i++)
                    if (item[i])
                        list.Add(i);
                list2.Add(list);
            }
            return list2;
        }

        private static bool AllAreMarked(BitArray currentSet, bool[] marks)
        {
            for (int i = 0; i < currentSet.Length; i++)
                if (currentSet[i] && !marks[i])
                    return false;
            return true;
        }
    }
}
