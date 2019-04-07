using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital12_01_Nullable
{
    class Capital12_01_Nullable
    {
        static void Main(string[] args)
        {
            Nullable<int> value = 1;

            Console.WriteLine("可空类型有值输出：");
            Display(value);
            Console.WriteLine();
            Console.WriteLine();

            value = new Nullable<int>();
            Console.WriteLine("可空类型无值输出：");
            Display(value);

            Console.ReadKey();
        }

        private static void Display(int? value)
        {
            Console.WriteLine($"可空类型是否有值：{value.HasValue}");

            if (value.HasValue)
            {
                Console.WriteLine($"值为：{value.Value}");
            }

            Console.WriteLine($"GetValueOrDefault():{value.GetValueOrDefault()}");

            // GetValueOrDefault()表示如果HasValue为true，则为Value属性值；
            // 如果HasValue为false，则返回defaultValue的值，此处为2； 
            Console.WriteLine($"GetValueOrDefault重载方法{value.GetValueOrDefault(2)}");

            // GetHashCode()表示如果HasValue为true，则为Value属性返回对象的哈希代码；
            // 如果HasValue为false，则返回0；
            Console.WriteLine($"GetHashCode()方法的使用：{value.GetHashCode()}");
        }
    }
}
