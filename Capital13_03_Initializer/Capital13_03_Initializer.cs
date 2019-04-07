using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital13_03_Initializer
{
    class Capital13_03_Initializer
    {
        static void Main(string[] args)
        {
            // 初始化器，只使用默认构造函数即可,不需要为不同情况写多个构造函数
            Person p1 = new Person() { Name = "Zhangsan", Age = 25, Height = 176, Weight = 65 };
            Person p2 = new Person { Name = "Lisi" };
        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public int Weight { get; set; }
            public int Height { get; set; }
        }
    }
}
