using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Captal8_Delegate_02Greeting
{
    // 声明委托
    public delegate void GreetingDelegate(string name);

    class Captal8_Delegate_02Greeting
    {
        static void Main(string[] args)
        {
            Captal8_Delegate_02Greeting greet = new Captal8_Delegate_02Greeting();

            // 不使用委托，实现打招呼的方法
            Console.WriteLine("不用委托方法，用switch方法实现：");
            greet.Greeting("Tom", "en-us");
            greet.Greeting("小张", "zh-cn");
            Console.WriteLine();

            // 使用委托的方法
            Console.WriteLine("用委托方法实现：");
            greet.GreetingDeleMeth("Tom", greet.EnglishGreeting);
            greet.GreetingDeleMeth("小王", greet.ChineseGreeting);

            Console.ReadKey();
        }

        ////////////////
        /// 不使用委托，实现打招呼的方法
        /// 
        public void Greeting(string name,string language)
        {
            switch (language)
            {
                case "zh-cn":
                    ChineseGreeting(name);
                    break;
                case "en-us":
                    EnglishGreeting(name);
                    break;
                default:
                    EnglishGreeting(name);
                    break;
            }
        }

        //////////////////
        /// 使用委托的方法
        /// 
        public void GreetingDeleMeth(string name, GreetingDelegate callback)
        {
            callback(name);
        }

        // 英国人打招呼
        private void EnglishGreeting(string name)
        {
            Console.WriteLine($"Hello, {name}");
        }

        // 中国人打招呼
        private void ChineseGreeting(string name)
        {
            Console.WriteLine($"你好, {name}");
        }
    }
}
