using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Captal15_ExtMethod_02
{
    class Captal15_ExtMethod_02
    {
        static void Main(string[] args)
        {
            Person person = new Person() { Name = "zhangsan" };

            person.Print();

            Console.Read();
        }
    }

    // 自定义类型
    public class Person
    {
        public string Name { get; set; }
    }

    public static class ExtensionClass1
    {
        public static void Print(this Person per)
        {
            Console.WriteLine($"调用当前命名空间下ExtensionClass1扩展方法输出姓名为：{per.Name}");
        }
    }

    public static class ExtensionClass2
    {
        public static void Print1(this Person per)
        {
            Console.WriteLine($"调用当前命名空间下ExtensionClass2扩展方法输出姓名为：{per.Name}");
        }
    }
}
