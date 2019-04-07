using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital12_03_AnonymousMethode
{
    // 声明一个委托
    delegate void VoteDelegate(string name);

    class Capital12_03_AnonymousMethode
    {
        static void Main(string[] args)
        {
            CallByDelegate();

            CallByAnonymousMethode();

            Console.ReadKey();
        }

        // 1. 通过委托调用
        static void CallByDelegate()
        {
            VoteDelegate vote = new VoteDelegate(new Friend().Vote);
            vote.Invoke("小咪");

        }

        public class Friend
        {
            public void Vote(string nickname)
            {
                Console.WriteLine($"昵称为：{nickname}: 这是通过委托调用的。");
                Console.WriteLine();
            }
        }

        // 2. 通过匿名函数调用
        static void CallByAnonymousMethode()
        {
            VoteDelegate vote = delegate (string nickname)
            {
                Console.WriteLine($"昵称为：{nickname}：这是通过匿名函数调用的");
                Console.WriteLine();
            };

            vote.Invoke("小夏");
        }
    }
}
