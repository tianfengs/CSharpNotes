---
typora-copy-images-to: mdPics
---

# 《Learnig Hard C# 学习笔记》事件 第9章

事件涉及两类角色——事件发布者、事件订阅者

### 一、使用事件

1. 定义事件

   ```
   访问修饰符 event 委托类型 事件名;
   例如：
   public event EventHandler birthday;
   ```

   - **访问修饰符**一般定义为public。
   - **委托类型** 既可以是自定义的委托类型，也可以是EventHandler。

2. 订阅和取消事件

   用"+="和"-="运算符类订阅和取消。**(见代码"Capital9_Event_01")** 

   跟用委托的方式进行消息传递是一样的方法。

   - 用自定义的委托类型来定义事件

   - 用.NET类库中预定义的委托类型EventHandler来定义事件 （标记 **// II.** 5处代码比照）

     ```
     public delegate void EventHandler(Object sender, EventArgs e);
     ```

     - 该委托的返回类型为void
     - 第一个参数**sender**保存**对出发事件对象的引用**，类型为object
     - 第二个参数**e**负责保存**事件数据**，EventArgs类也是.NET类库中的类

   ```
   /// <summary>
   /// 事件的发布者，本例是主类
   /// </summary>
   class Capital9_Event_01
   {
       // 1. 声明委托（主类）
       public delegate void MarryHandler(string msg);
       // 3. 创建委托变量，即定义事件（事件发布者）
       public event MarryHandler MarryEvent;
       
       // II. 1,3.用.NET类库中预定义的委托类型EventHandler来定义事件
       //public event EventHandler MarryEvent;    // II.
   
       // 5. 调用委托变量，发出事件（消息发出者）
       public void OnMarriageComing(string msg)
       {
           MarryEvent?.Invoke(msg);    
   
           //MarryEvent?.Invoke(this, new EventArgs()); // II. 用.NET类库中预定义的委托类型EventHandler来定义事件
           //MarryEvent?.Invoke(msg, new EventArgs()); // II. 也可以把sender换成msg，用来传递数据，不过一般都是用new EventArgs()传递数据，见下一个例子
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
           bridegroom.MarryEvent += new MarryHandler(friend1.SendMessage);
           bridegroom.MarryEvent += new MarryHandler(friend2.SendMessage);
           //bridegroom.MarryEvent += new EventHandler(friend1.SendMessage); // II. 
           //bridegroom.MarryEvent += new EventHandler(friend2.SendMessage); // II. 
   
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
       public void SendMessage(string message)
       //public void SendMessage(object message, EventArgs eventArgs)  // II.
       {
           Console.WriteLine(message);
   
           Console.WriteLine($"{this.Name}回应：\t我知道了，到时候一定准时参加!\n");
       }
   }
   ```

   运行结果：

   ![1553999738437](mdPics\1553999738437.png) 

   EventHandler用于处理不包含事件数据的事件。如果想在事件中包含事件数据，可通过EventArgs实现。

3. 扩展EventArgs类

   可以通过派生EventArgs类来使事件参数e带有事件数据。**(见代码"Capital9_Event_02")** 

   ```
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
   ```

   - 通过委托传递数据	                                     |	创建自定义事件	（5个步骤）

     1. 声明委托类型                  （主类）            |	声明事件结构的委托（在**主类**中进行）
     2. 创建实际的方法              （信息接收者） |        事件处理函数           （事件订阅者）
     3. 创建委托对象                  （信息发出者） |        定义事件                   （事件发布者）
     4. 把实际方法绑定到委托   （主类）            |        订阅事件                   （主类）
     5. 调用委托变量，传递信息（消息发出者）|        激活事件                   （事件发布者）

     **从窗体**向**主窗体**发送信息

     ![1554021123982](mdPics\1554021123982.png) 

     **主窗体**向**从窗体**发送信息

      ![1554021617637](mdPics\1554021617637.png) 

   - 以上代码通过扩展EventArgs事件类

     - 使MarryEventArgs带有一个名为Message的事件参数
     - 在订阅对象的SendMessage方法中，通过e.Message获得**事件数据**，并输出事件数据

   ### 三、事件的本质

   事件和委托的关系。事件是特殊的多路广播委托。事件提供了对私有委托字段进行访问的方法。

   C#源代码**(见代码"Capital9_Event_03")** 

   ```
   class Capital9_Event_03
   {
       // 自定义委托
       public delegate void MarryHandler(string msg);
   
       // 使用自定义委托类型定义事件，事件名为Marry Event
       public event MarryHandler MarryEvent;
               
       static void Main(string[] args)
       {
       }
   }
   ```

   IL的代码为：

   ![1554023178012](mdPics\1554023178012.png)  

   - MarryHandler委托被编译成一个类

   - MarryEvent事件被编译成如下

     ```
     .event Capital9_Event_03.Capital9_Event_03/MarryHandler MarryEvent
     {
       .addon instance void Capital9_Event_03.Capital9_Event_03::add_MarryEvent(class Capital9_Event_03.Capital9_Event_03/MarryHandler)
       .removeon instance void Capital9_Event_03.Capital9_Event_03::remove_MarryEvent(class Capital9_Event_03.Capital9_Event_03/MarryHandler)
     } // end of event Capital9_Event_03::MarryEvent
     ```

     两个公共方法带add\_和 remove_前缀，后面跟着C#事件名称。**add_MarryEvent()**如下：

     ```
     .method public hidebysig specialname instance void 
             add_MarryEvent(class Capital9_Event_03.Capital9_Event_03/MarryHandler 'value') cil managed
     {
       ... ...
       // 调用 Delegate.Combine()方法
       IL_000b:  call       class [mscorlib]System.Delegate [mscorlib]System.Delegate::Combine(class [mscorlib]System.Delegate,                                                  class [mscorlib]System.Delegate)
       ... ...
     } // end of method Capital9_Event_03::add_MarryEvent
     ```

     **add_MarryEvent()**方法是通过调用Delegate.Combine()方法实现的，此方法将多个委托组合为一个多路广播委托。**remove_MarryEvent()** 

     ```
     .method public hidebysig specialname instance void 
             remove_MarryEvent(class Capital9_Event_03.Capital9_Event_03/MarryHandler 'value') cil managed
     {
       ... ...
       IL_000b:  call       class [mscorlib]System.Delegate [mscorlib]System.Delegate::Remove(class [mscorlib]System.Delegate,                                                   class [mscorlib]System.Delegate)
       ... ...
     } // end of method Capital9_Event_03::remove_MarryEvent
     ```

     双击MarryEvent:private class Capital9_Event_03.Capital9_Event_03/MarryHandler

     ```
     .field private class Capital9_Event_03.Capital9_Event_03/MarryHandler MarryEvent
     ```

     - .field指令表明这是一个字段，字段类型为MarryHandler，字段名称为MarryEvent。
     - 该字段是私有，用于保存对事件处理方法的引用，且该委托类型的变量为私有，只能从定义该事件的类中进行访问。

     

     