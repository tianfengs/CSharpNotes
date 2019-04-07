using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital12_04_Closure
{
    // 定义闭包委托
    delegate void ClosureDelegate();

    class Capital12_04_Closure
    {
        static void Main(string[] args)
        {
            closureMethod();

            Console.ReadKey();
        }

        // 闭包方法。匿名方法延长了变量的生命周期。
        private static void closureMethod()
        {
            string outVariable = "外部变量";
            string capturedVarible = "被捕获的外部变量";

            ClosureDelegate closureDelegate = delegate
            {
                string localVariable = "匿名方法局部变量";

                Console.WriteLine(capturedVarible + " " + localVariable);
            };

            closureDelegate();
        }
    }
}
