using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital13_02_ImplicitType
{
    class Capital13_02_ImplicitType
    {
        static void Main(string[] args)
        {
            // 隐式类型 Implicit
            // 优点: 使用隐式类型，就不用在等式两边都写一遍Dictionary<string, string>
            var dictionary = new Dictionary<string, string>();

            // 缺点
            var a = 2147483649;
            var b = 928888888888;
            var c = 2147483644;
            Console.WriteLine($"变量a的类型为：{a.GetType()}");
            Console.WriteLine($"变量b的类型为：{b.GetType()}");
            Console.WriteLine($"变量c的类型为：{c.GetType()}");

            Console.Read();
        }
    }
}
