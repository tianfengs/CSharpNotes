using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital12_02___
{
    class Program
    {
        static void Main(string[] args)
        {
            NullcoalescingOperator();

            Console.ReadKey();

        }

        private static void NullcoalescingOperator()
        {
            // ??运算符用于可空类型
            int? nullable = null;
            int? nullhasvalue = 1;

            int x = nullable ?? 12;
            int y = nullhasvalue ?? 123;

            Console.WriteLine($"可空类型没有值：{x}");
            Console.WriteLine($"可空类型有值：{y}");

            Console.WriteLine();

            // ??运算符用于引用类型
            string stringisnull = null;
            string stringnotnull = "123";

            string rst = stringisnull ?? "345";
            string rst1 = stringnotnull ?? "12";

            Console.WriteLine($"引用类型没有值：{rst}");
            Console.WriteLine($"引用类型有值：{rst1}");
        }
    }
}
