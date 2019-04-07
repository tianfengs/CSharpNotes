using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital14_Lambda_01
{
    class Capital14_Lambda_01
    {
        static void Main(string[] args)
        {
            // Lambda表达式的演变过程
            // 下面是C# 1.0 中创建委托实例的代码
            Func<string, int> delegatetest1 = new Func<string, int>(CallBackMethod);

            // C# 2.0 中用匿名方法来创建委托实例，则不需要创建回调函数了
            Func<string, int> delegatetest2 = delegate (string text)
              {
                  return text.Length;
              };

            // C# 3.0 使用Lambda表达式
            Func<string, int> delegatetest3 = text => text.Length;

            Func<string, int> delegatetest4 = new Func<string, int>(text => text.Length);

            Action<string, int> delegatetest5 = new Action<string, int>((str, strInt)=>{
                Console.WriteLine($"{str}今年{strInt}岁。");
            });

            Action<string, int> delegatetest6 = (str, strInt) => Console.WriteLine($"{str}今年{strInt}岁。");

            Console.WriteLine("使用Lambda表达式返回字符串长度：" + delegatetest3("ssdfsfsd"));

            delegatetest6("zhangsan", 25);

            Console.Read();


        }

        private static int CallBackMethod(string arg)
        {
            return arg.Length;
        }
    }
}
