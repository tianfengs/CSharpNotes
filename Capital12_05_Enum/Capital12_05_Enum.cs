using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital12_05_Enum
{
    public class Capital12_05_Enum
    {
        static void Main(string[] args)
        {
            // 使用迭代类
            Friends friends = new Friends();

            foreach(Friend f in friends)
            {
                Console.WriteLine(f.Name);
            }

            Console.ReadKey();
        }

        // 创建朋友类
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
            private Friend[] friends;

            public Friends()
            {
                friends = new Friend[] {
                    new Friend("zhangsan")
                    , new Friend("lisi")
                    , new Friend("wangwu")
                };
            }

            // 索引器
            public Friend this[int index]
            {
                get { return friends[index]; }
            }

            public int Count
            {
                get { return friends.Length; }
            }

            public IEnumerator GetEnumerator()
            {
                //// 1. 有索引器返回
                //return new FriendIterator(this);
                // 2. 返回数组的IEnumerator
                return friends.GetEnumerator();
            }
        }
    }

    internal class FriendIterator : IEnumerator
    {
        private Capital12_05_Enum.Friends friends;
        private int index;
        private Capital12_05_Enum.Friend current;

        public FriendIterator(Capital12_05_Enum.Friends friends)
        {
            this.friends = friends;
            this.index = 0;
        }

        public object Current { get { return current; } }

        public bool MoveNext()
        {
            if (index + 1 > friends.Count)
            {
                return false;
            }
            else
            {
                current = friends[index];
                index++;
                return true;
            }
        }

        public void Reset()
        {
            index = 0;
        }
    }
}
