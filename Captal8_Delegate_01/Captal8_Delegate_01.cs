using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Captal8_Delegate_01
{
    class Captal8_Delegate_01
    {
        // 1.声明委托
        delegate void MyDelegate(int para1, int para2);

        static void Main(string[] args)
        {
            // 3. 声明委托对象
            MyDelegate d;

            // 4. 实例化委托类型，传递或绑定方法（可以是实例方法 或者 静态方法）
            d = new MyDelegate(new Captal8_Delegate_01().Add);

            // 5. 委托类型作为参数传递给另一个方法，在另一个方法内调用（从另一个方法获得参数）
            Captal8_Delegate_01.MyMethod(d);

            Console.ReadKey();
        }

        /// <summary>
        /// 2. 创建跟委托相同签名和返回类型的 实例方法
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        void Add(int p1,int p2)
        {
            int sum = p1 + p2;
            Console.WriteLine($"{p1}+{p2}之和为：{sum}");
        }

        /// <summary>
        /// 5. 方法的参数是委托类型
        /// </summary>
        /// <param name="myDelegate"></param>
        private static void MyMethod(MyDelegate myDelegate)
        {
            // 在方法中调用委托
            myDelegate(1, 2);
        }
    }
}
