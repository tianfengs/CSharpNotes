using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 返回集合中是偶数的序列：使用foreach和Linq两种方法来实现
/// </summary>
namespace Captal16_Linq_01
{
    class Captal16_Linq_01
    {
        static void Main(string[] args)
        {
            // 初始化一个要查询的数据
            List<int> inputArray = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            ForeachQuery(inputArray);

            LinqQuery(inputArray);            

            Console.ReadKey();

        }

        public static void ForeachQuery(List<int> vs)
        {
            List<int> queryResult = new List<int>();
            foreach(int item in vs)
            {
                if (item % 2 == 0)
                {
                    queryResult.Add(item);
                }
            }

            foreach(int item in queryResult)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
        }

        public static void LinqQuery(List<int> vs)
        {
            var queryResult = from item in vs
                                where item % 2 == 0
                                select item;

            queryResult = vs.Where(item => item % 2 == 0);

            foreach(int item in queryResult)
            {
                Console.Write(item + " ");
            }

        }
    }
}
