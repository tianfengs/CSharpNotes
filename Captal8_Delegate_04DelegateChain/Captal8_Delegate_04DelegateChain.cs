using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Captal8_Delegate_04DelegateChain
{
    class Captal8_Delegate_04DelegateChain
    {
        // 1. 声明委托
        public delegate void DelegateTest();

        static void Main(string[] args)
        {
            // 创建委托对象1，绑定一个静态方法
            DelegateTest dtstatic = new DelegateTest(methodStatic);
            // 创建委托对象2，绑定一个实例方法
            DelegateTest dtinstance = new DelegateTest(new Captal8_Delegate_04DelegateChain().methodInstance);

            // 创建一个委托对象，并绑定委托对象1，使用"+"添加委托对象2，成为委托链
            DelegateTest delegateChain = dtstatic;
            delegateChain += dtinstance;

            // 调用委托对象
            delegateChain.Invoke();

            Console.WriteLine();
            Console.WriteLine("从委托链上移除静态方法后：");

            // 从委托链上移除静态方法
            delegateChain -= dtstatic;

            delegateChain.Invoke();

            Console.ReadKey();
        }

        // 静态方法
        private static void methodStatic()
        {
            Console.WriteLine("这是静态方法");
        }

        // 实例方法
        private void methodInstance()
        {
            Console.WriteLine("这是实例方法");
        }
    }
}
