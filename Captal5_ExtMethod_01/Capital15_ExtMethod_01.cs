using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital15_ExtMethod_01
{
    class Capital15_ExtMethod_01
    {
        static void Main(string[] args)
        {
            List<int> source = new List<int>() { 1, 2, 3, 4, 5, 6, 3 };

            //// 1. 调用方式一
            //int jSum = ListExten.JSum(source);

            // 2. 调用方式二
            int jSum = source.JSum();   // 扩展方法 调用

            Console.WriteLine($"数组的奇数和为:{jSum}");

            Console.ReadKey();
        }
    }

    /// <summary>
    /// 扩展方法必须在“非泛型”“静态类”中定义
    /// </summary>
    public static class ListExten
    {
        // 定义扩展方法
        /// <summary>
        /// 计算数组中下标为奇数的数组成员之和。
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int JSum(this IEnumerable<int> source)
        {
            if (source == null)
            {
                throw new ArgumentException("输入数组为空");
            }

            int jsum = 0;

            bool flag = false;
            foreach(int current in source)
            {
                if (!flag)
                {
                    jsum += current;
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }

            return jsum;
        }
    }
}
