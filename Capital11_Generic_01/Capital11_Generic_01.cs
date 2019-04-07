using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital11_Generic_01
{
    class Capital11_Generic_01
    {
        static void Main(string[] args)
        {
            var res1 = Compare<int>.CompareGeneric(1, 2);
            var res2 = Compare<string>.CompareGeneric("aaa", "ddd");

            Console.WriteLine(res1);
            Console.WriteLine(res2);

            Console.ReadKey();
        }
    }

    // 普通的比较类和方法
    public class Compare
    {
        public static int CompareInt(int int1,int int2)
        {
            return int1.CompareTo(int2) > 0 ? int1 : int2;
        }

        public static string CompareStr(string str1,string str2)
        {
            return str1.CompareTo(str2) > 0 ? str1 : str2;
        }
    }

    // 泛型的比较类和方法
    public class Compare<T> where T : IComparable
    {
        public static T CompareGeneric(T t1, T t2)
        {
            return t1.CompareTo(t2) > 0 ? t1 : t2;
        }
    }
}
