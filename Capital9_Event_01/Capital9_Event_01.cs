using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital9_Event_01
{
    /// <summary>
    /// 事件的发布者，本例是主类
    /// </summary>
    class Capital9_Event_01
    {
        //// 1. 声明委托（主类）
        //public delegate void MarryHandler(string msg);
        //// 3. 创建委托变量，即定义事件（事件发布者）
        //public event MarryHandler MarryEvent;

        public event EventHandler MarryEvent;   // II. 用.NET类库中预定义的委托类型EventHandler来定义事件

        // 5. 调用委托变量，发出事件（消息发出者）
        public void OnMarriageComing(string msg)
        {
            //MarryEvent?.Invoke(msg);

            //MarryEvent?.Invoke(this, new EventArgs()); // II. 用.NET类库中预定义的委托类型EventHandler来定义事件
            MarryEvent?.Invoke(msg, new EventArgs()); // II. 也可以把sender换成msg，用来传递数据，不过一般都是用new EventArgs()传递数据，见下一个例子
        }

        static void Main(string[] args)
        {
            // 初始化新郎官对象
            Capital9_Event_01 bridegroom = new Capital9_Event_01();

            // 实例化朋友对象
            Friend friend1 = new Friend("张三");
            Friend friend2 = new Friend("李四");
            Friend friend3 = new Friend("王五");

            // 4.把实例方法绑定到委托对象，订阅事件（主类）
            //bridegroom.MarryEvent += new MarryHandler(friend1.SendMessage);
            //bridegroom.MarryEvent += new MarryHandler(friend2.SendMessage);
            bridegroom.MarryEvent += new EventHandler(friend1.SendMessage); // II. 用.NET类库中预定义的委托类型EventHandler来定义事件
            bridegroom.MarryEvent += new EventHandler(friend2.SendMessage); // II. 用.NET类库中预定义的委托类型EventHandler来定义事件

            // 5. 调用委托变量，激活事件（消息发出者）
            bridegroom.OnMarriageComing("新郎说：\t朋友们，我结婚了，到时候准备参加婚礼！");
            Console.WriteLine("-----------------------------------");

            // 换个订阅者，去掉李四，换成王五，再发一遍
            bridegroom.MarryEvent -= friend2.SendMessage;
            bridegroom.MarryEvent += friend3.SendMessage;
            bridegroom.OnMarriageComing("新郎说：\t朋友们，我结婚了，到时候准备参加婚礼！");

            Console.ReadKey();
        }
    }

    /// <summary>
    /// 事件接收者，本例是从类
    /// </summary>
    public class Friend
    {
        public string Name { get; set; }

        public Friend(string name)
        {
            Name = name;
        }

        // 2. 事件处理函数（事件订阅者）
        //public void SendMessage(string message)
        public void SendMessage(object message, EventArgs eventArgs)  // II. 用.NET类库中预定义的委托类型EventHandler来定义事件
        {
            Console.WriteLine(message);

            Console.WriteLine($"{this.Name}回应：\t我知道了，到时候一定准时参加!\n");
        }
    }
}
