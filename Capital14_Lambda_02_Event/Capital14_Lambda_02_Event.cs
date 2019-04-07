using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capital14_Lambda_02_Event
{
    class Capital14_Lambda_02_Event
    {
        static void Main(string[] args)
        {
            // C# 2.0 的方法，匿名方法订阅事件和初始化
            EventsSubscribeOld();

            // C# 3.0 的方法，用Lambda表达式代替匿名方法，用初始化器初始化，使代码很简洁。
            EventSuscribeCSharp3();
        }

        static void EventSuscribeCSharp3()
        {
            Button button = new Button() { Text = "点击我（初始化器）" };

            button.Click += (sender, e) => ReportEvent("Click事件", sender, e);
            button.KeyPress += (sender, e) => ReportEvent("KeyPress事件", sender, e);

            Form form = new Form() {
                Name = "在控制台中创建窗体（初始化器）"
                , AutoSize = true
                , Controls = { button }
            };

            Application.Run(form);
        }

        static void EventsSubscribeOld()
        {
            Button button = new Button();
            button.Text = "点击我";

            EventHandler clickEvent = delegate (object sender, EventArgs e)
            {
                ReportEvent("Click事件", sender, e);
            };
            button.Click += clickEvent;

            button.KeyPress += delegate (object sender, KeyPressEventArgs e)    // 点击按钮的同时，按下空格键才触发
            {
                ReportEvent("KeyPress事件", sender, e);
            };

            // 在C# 3.0 之前用以下方法 初始化对象
            Form form = new Form();
            form.Name = "在控制台中创建的窗体";
            form.AutoSize = true;
            form.Controls.Add(button);

            // 运行窗体
            Application.Run(form);
        }

        private static void ReportEvent(string v, object sender, EventArgs e)
        {
            Console.WriteLine($"发生的事件为{v}");
            Console.WriteLine($"发生的事件的对象为{sender}");
            Console.WriteLine($"发生的事件的参数为{e.GetType()}");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
