using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital11_Generic_03
{
    public class DictionaryStringKey<T> : Dictionary<string, T>
    {
    }

    class Capital11_Generic_03
    {
        static void Main(string[] args)
        {
            // Dictionary<,>是一个“开放类型”，它有两个类型参数
            Type t = typeof(Dictionary<,>);
            Console.WriteLine("Dictionary <,>是否为开放类型：" + t.ContainsGenericParameters);

            //
            t = typeof(DictionaryStringKey<>);
            Console.WriteLine("Dictionary <,>是否为开放类型：" + t.ContainsGenericParameters);

            //
            t = typeof(DictionaryStringKey<int>);
            Console.WriteLine("Dictionary <,>是否为开放类型：" + t.ContainsGenericParameters);

            //
            t = typeof(Dictionary<int, int>);
            Console.WriteLine("Dictionary <,>是否为开放类型：" + t.ContainsGenericParameters);

            Console.ReadKey();
        }
    }
}
