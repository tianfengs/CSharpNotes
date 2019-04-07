using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital13_04_AnonymousType
{
    /// <summary>
    /// 匿名类型 = 隐式类型 + 对象初始化器
    /// </summary>
    class Capital13_04_AnonymousType
    {
        static void Main(string[] args)
        {
            // 定义匿名类型对象
            var person = new { Name = "Zhangsan", Age = 25 };
            Console.WriteLine($"{person.Name}的年龄为{person.Age}岁。");

            // 定义匿名类型数组
            var personCollection = new[]
            {
                new{Name="zhangsan",Age=30},
                new{Name="lisi",Age=22},
                new{Name="wangwu",Age=32},

                //new{Name="Jerry"}
            };

            int totalAge = 0;
            foreach(var p in personCollection)
            {
                totalAge += p.Age;
            }

            Console.WriteLine($"年龄和为{totalAge}。");
            Console.ReadKey();
        }
    }
}
