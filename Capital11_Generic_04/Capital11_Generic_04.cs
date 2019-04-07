using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital11_Generic_04
{
    class Capital11_Generic_04
    {
        // 泛型类型，具有一个类型参数
        public static class TypeWithStaticField<T>
        {
            // static field
            public static string field;
            // static ctor
            public static void OutField()
            {
                Console.WriteLine(field + ":" + typeof(T).Name);
            }
        }

        // 非泛型类
        public static class NoGenericTypeWithStaticField
        {
            public static string field;
            public static void OutField()
            {
                Console.WriteLine(field);
            }
        }

        static void Main(string[] args)
        {
            // 实例化泛型实例
            TypeWithStaticField<int>.field = "一";
            TypeWithStaticField<string>.field = "二";
            TypeWithStaticField<Guid>.field = "三";

            // 实例化非泛型实例
            NoGenericTypeWithStaticField.field = "非泛型静态字段一";
            NoGenericTypeWithStaticField.field = "非泛型静态字段二";
            NoGenericTypeWithStaticField.field = "非泛型静态字段三";

            //输出
            NoGenericTypeWithStaticField.OutField();

            TypeWithStaticField<int>.OutField();
            TypeWithStaticField<string>.OutField();
            TypeWithStaticField<Guid>.OutField();

            Console.ReadKey();
        }
    }
}
