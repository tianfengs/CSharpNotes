using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital9_Event_02EventArgs
{
    class Capital9_Event_02EventArgs
    {
        // 1. 声明事件结构的委托(主类)
        public delegate void MarryHandle(object sender, MarryEventArgs e);

        // 3. 创建委托对象，定义事件（消息发出者，事件发布者）
        public event MarryHandle MarryEvent;

        // 5. 调用委托，激活事件（消息发出者，事件发布者）发出通知
        public void OnBirthdayCom(string msg)
        {
            MarryEvent?.Invoke(this, new MarryEventArgs(msg));
        }

        static void Main(string[] args)
        {
            // 初始化朋友对象
            Friend friend1 = new Friend("zhangsan");
            Friend friend2 = new Friend("lisi");
            Friend friend3 = new Friend("wangwu");
            // 初始化新郎官对象
            Capital9_Event_02EventArgs bridegroom = new Capital9_Event_02EventArgs();

            // 4. 把实际方法绑定到委托对象，订阅事件（主类）
            bridegroom.MarryEvent += friend1.SendMessage;
            bridegroom.MarryEvent += friend2.SendMessage;

            // 5. 调用委托，激活事件（消息发出者，事件发布者）发出通知
            bridegroom.OnBirthdayCom("我明天过生日！");

            Console.ReadKey();
        }
    }

    /// <summary>
    /// 朋友类（从属类）
    /// </summary>
    public class Friend
    {
        public string Name { get; set; }

        public Friend(string n)
        {
            Name = n;
        }

        // 2. 根据委托签名和返回值创建 实际方法，事件处理函数（信息接收者，事件订阅者）
        public void SendMessage(object sender, MarryEventArgs e)
        {
            Console.WriteLine(e.Message);

            Console.WriteLine($"{this.Name}收到了，到时候准时参加。");
        }
    }

    /// <summary>
    /// 自定义事件数据类，并使其带有事件数据
    /// </summary>
    public class MarryEventArgs : EventArgs
    {
        public string Message { get; set; }
        public MarryEventArgs(string msg)
        {
            Message = msg;
        }
    }
}
