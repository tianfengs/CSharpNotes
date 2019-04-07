using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 比较泛型方法与普通方法加装箱拆箱操作性能上的优势
/// 
/// 引用类型-值类型间存在转换，转换的装箱拆箱操作会引起性能损失，泛型是避免损失的有效方法。
/// 
/// </summary>
namespace Capital11_Generic_02
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Diagnostics;

    class Capital11_Generic_02
    {
        static void Main(string[] args)
        {
            // 测试泛型类型的运行时间
            testGeneric();
            // 测试非泛型类型的运行时间
            testNonGeneric();

            Console.ReadKey();
        }

        /// <summary>
        /// 测试泛型类型操作的运行时间
        /// </summary>
        private static void testGeneric()
        {
            Stopwatch stopwatch = new Stopwatch();

            List<int> genericList = new List<int>();

            stopwatch.Start();

            for(int i = 0; i < 10000000; i++)
            {
                genericList.Add(i);
            }

            stopwatch.Stop();

            //输出时间
            TimeSpan timeSpan = stopwatch.Elapsed;
            // 00:00:00这样输出
            string elapsedTime = String.Format($"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}:{timeSpan.Milliseconds / 10:00}");
            Console.WriteLine($"泛型的运行时间：{elapsedTime}");
        }

        /// <summary>
        /// 测试非泛型类型操作的运行时间
        /// </summary>
        private static void testNonGeneric()
        {
            Stopwatch stopwatch = new Stopwatch();

            ArrayList arrayList = new ArrayList();

            stopwatch.Start();

            for(int i = 0; i < 10000000; i++)
            {
                arrayList.Add(i);
            }

            stopwatch.Stop();

            //输出时间
            TimeSpan timeSpan = stopwatch.Elapsed;
            // 00:00:00这样输出
            string elapsedTime = String.Format($"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}:{timeSpan.Milliseconds / 10:00}");
            Console.WriteLine($"非泛型的运行时间：{elapsedTime}");

        }
    }
}
