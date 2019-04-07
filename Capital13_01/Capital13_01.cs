using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital13_01
{
    class Capital13_01
    {
        static void Main(string[] args)
        {
            var stringvariable = "Hello World!";
            //stringvariable = 2;

            PersonStruct ps = new PersonStruct("zhangsan");

            Console.WriteLine($"名字是{ps.Name}，年龄是{ps.Age}岁。");

            Console.ReadKey();
        }
    }

    public struct PersonStruct
    {
        public string Name { get; set; }
        public int Age { get; private set; }

        // 结构体中，不显示的调用无参构造函数this()时，会出现编译错误
        public PersonStruct(string name)
            :this() // 如果没有把所有的属性赋值，则要显示调用无参构造函数
        {
            this.Name = name;
            //Age = 5;
        }
    }
}
