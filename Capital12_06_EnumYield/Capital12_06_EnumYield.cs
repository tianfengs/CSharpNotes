using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital12_06_EnumYield
{
    class Capital12_06_EnumYield
    {
        static void Main(string[] args)
        {
            // 使用迭代类
            Friends friends = new Friends();

            foreach (Friend f in friends)
            {
                Console.WriteLine(f.Name);
            }

            Console.ReadKey();
        }

        // 朋友类
        public class Friend
        {
            public string Name { get; set; }

            public Friend(string name)
            {
                Name = name;               
            }
        }

        // 朋友类集合
        public class Friends : IEnumerable
        {
            Friend[] friends;

            public Friends()
            {
                friends = new Friend[]
                {
                    new Friend("zhangsan"),
                    new Friend("lisi"),
                    new Friend("wangwu")
                };
            }

            public IEnumerator GetEnumerator()
            {
                for(int i = 0; i < friends.Length; i++)
                {
                    yield return friends[i];
                }
            }
        }
    }
}
