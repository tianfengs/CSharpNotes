---
typora-copy-images-to: mdPics
---

# ��Learnig Hard C# ѧϰ�ʼǡ��¼� ��9��

�¼��漰�����ɫ�����¼������ߡ��¼�������

### һ��ʹ���¼�

1. �����¼�

   ```
   �������η� event ί������ �¼���;
   ���磺
   public event EventHandler birthday;
   ```

   - **�������η�**һ�㶨��Ϊpublic��
   - **ί������** �ȿ������Զ����ί�����ͣ�Ҳ������EventHandler��

2. ���ĺ�ȡ���¼�

   ��"+="��"-="������ඩ�ĺ�ȡ����**(������"Capital9_Event_01")** 

   ����ί�еķ�ʽ������Ϣ������һ���ķ�����

   - ���Զ����ί�������������¼�

   - ��.NET�����Ԥ�����ί������EventHandler�������¼� ����� **// II.** 5��������գ�

     ```
     public delegate void EventHandler(Object sender, EventArgs e);
     ```

     - ��ί�еķ�������Ϊvoid
     - ��һ������**sender**����**�Գ����¼����������**������Ϊobject
     - �ڶ�������**e**���𱣴�**�¼�����**��EventArgs��Ҳ��.NET����е���

   ```
   /// <summary>
   /// �¼��ķ����ߣ�����������
   /// </summary>
   class Capital9_Event_01
   {
       // 1. ����ί�У����ࣩ
       public delegate void MarryHandler(string msg);
       // 3. ����ί�б������������¼����¼������ߣ�
       public event MarryHandler MarryEvent;
       
       // II. 1,3.��.NET�����Ԥ�����ί������EventHandler�������¼�
       //public event EventHandler MarryEvent;    // II.
   
       // 5. ����ί�б����������¼�����Ϣ�����ߣ�
       public void OnMarriageComing(string msg)
       {
           MarryEvent?.Invoke(msg);    
   
           //MarryEvent?.Invoke(this, new EventArgs()); // II. ��.NET�����Ԥ�����ί������EventHandler�������¼�
           //MarryEvent?.Invoke(msg, new EventArgs()); // II. Ҳ���԰�sender����msg�������������ݣ�����һ�㶼����new EventArgs()�������ݣ�����һ������
       }
   
       static void Main(string[] args)
       {
           // ��ʼ�����ɹٶ���
           Capital9_Event_01 bridegroom = new Capital9_Event_01();
   
           // ʵ�������Ѷ���
           Friend friend1 = new Friend("����");
           Friend friend2 = new Friend("����");
           Friend friend3 = new Friend("����");
   
           // 4.��ʵ�������󶨵�ί�ж��󣬶����¼������ࣩ
           bridegroom.MarryEvent += new MarryHandler(friend1.SendMessage);
           bridegroom.MarryEvent += new MarryHandler(friend2.SendMessage);
           //bridegroom.MarryEvent += new EventHandler(friend1.SendMessage); // II. 
           //bridegroom.MarryEvent += new EventHandler(friend2.SendMessage); // II. 
   
           // 5. ����ί�б����������¼�����Ϣ�����ߣ�
           bridegroom.OnMarriageComing("����˵��\t�����ǣ��ҽ���ˣ���ʱ��׼���μӻ���");
           Console.WriteLine("-----------------------------------");
   
           // ���������ߣ�ȥ�����ģ��������壬�ٷ�һ��
           bridegroom.MarryEvent -= friend2.SendMessage;
           bridegroom.MarryEvent += friend3.SendMessage;
           bridegroom.OnMarriageComing("����˵��\t�����ǣ��ҽ���ˣ���ʱ��׼���μӻ���");
   
           Console.ReadKey();
       }
   }
   
   /// <summary>
   /// �¼������ߣ������Ǵ���
   /// </summary>
   public class Friend
   {
       public string Name { get; set; }
   
       public Friend(string name)
       {
           Name = name;
       }
   
       // 2. �¼����������¼������ߣ�
       public void SendMessage(string message)
       //public void SendMessage(object message, EventArgs eventArgs)  // II.
       {
           Console.WriteLine(message);
   
           Console.WriteLine($"{this.Name}��Ӧ��\t��֪���ˣ���ʱ��һ��׼ʱ�μ�!\n");
       }
   }
   ```

   ���н����

   ![1553999738437](mdPics\1553999738437.png) 

   EventHandler���ڴ��������¼����ݵ��¼�����������¼��а����¼����ݣ���ͨ��EventArgsʵ�֡�

3. ��չEventArgs��

   ����ͨ������EventArgs����ʹ�¼�����e�����¼����ݡ�**(������"Capital9_Event_02")** 

   ```
   class Capital9_Event_02EventArgs
   {
       // 1. �����¼��ṹ��ί��(����)
       public delegate void MarryHandle(object sender, MarryEventArgs e);
   
       // 3. ����ί�ж��󣬶����¼�����Ϣ�����ߣ��¼������ߣ�
       public event MarryHandle MarryEvent;
   
       // 5. ����ί�У������¼�����Ϣ�����ߣ��¼������ߣ�����֪ͨ
       public void OnBirthdayCom(string msg)
       {
           MarryEvent?.Invoke(this, new MarryEventArgs(msg));
       }
   
       static void Main(string[] args)
       {
           // ��ʼ�����Ѷ���
           Friend friend1 = new Friend("zhangsan");
           Friend friend2 = new Friend("lisi");
           Friend friend3 = new Friend("wangwu");
           // ��ʼ�����ɹٶ���
           Capital9_Event_02EventArgs bridegroom = new Capital9_Event_02EventArgs();
   
           // 4. ��ʵ�ʷ����󶨵�ί�ж��󣬶����¼������ࣩ
           bridegroom.MarryEvent += friend1.SendMessage;
           bridegroom.MarryEvent += friend2.SendMessage;
   
           // 5. ����ί�У������¼�����Ϣ�����ߣ��¼������ߣ�����֪ͨ
           bridegroom.OnBirthdayCom("����������գ�");
   
           Console.ReadKey();
       }
   }
   
   /// <summary>
   /// �����ࣨ�����ࣩ
   /// </summary>
   public class Friend
   {
       public string Name { get; set; }
   
       public Friend(string n)
       {
           Name = n;
       }
   
       // 2. ����ί��ǩ���ͷ���ֵ���� ʵ�ʷ������¼�����������Ϣ�����ߣ��¼������ߣ�
       public void SendMessage(object sender, MarryEventArgs e)
       {
           Console.WriteLine(e.Message);
   
           Console.WriteLine($"{this.Name}�յ��ˣ���ʱ��׼ʱ�μӡ�");
       }
   }
   
   /// <summary>
   /// �Զ����¼������࣬��ʹ������¼�����
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

   - ͨ��ί�д�������	                                     |	�����Զ����¼�	��5�����裩

     1. ����ί������                  �����ࣩ            |	�����¼��ṹ��ί�У���**����**�н��У�
     2. ����ʵ�ʵķ���              ����Ϣ�����ߣ� |        �¼�������           ���¼������ߣ�
     3. ����ί�ж���                  ����Ϣ�����ߣ� |        �����¼�                   ���¼������ߣ�
     4. ��ʵ�ʷ����󶨵�ί��   �����ࣩ            |        �����¼�                   �����ࣩ
     5. ����ί�б�����������Ϣ����Ϣ�����ߣ�|        �����¼�                   ���¼������ߣ�

     **�Ӵ���**��**������**������Ϣ

     ![1554021123982](mdPics\1554021123982.png) 

     **������**��**�Ӵ���**������Ϣ

      ![1554021617637](mdPics\1554021617637.png) 

   - ���ϴ���ͨ����չEventArgs�¼���

     - ʹMarryEventArgs����һ����ΪMessage���¼�����
     - �ڶ��Ķ����SendMessage�����У�ͨ��e.Message���**�¼�����**��������¼�����

   ### �����¼��ı���

   �¼���ί�еĹ�ϵ���¼�������Ķ�·�㲥ί�С��¼��ṩ�˶�˽��ί���ֶν��з��ʵķ�����

   C#Դ����**(������"Capital9_Event_03")** 

   ```
   class Capital9_Event_03
   {
       // �Զ���ί��
       public delegate void MarryHandler(string msg);
   
       // ʹ���Զ���ί�����Ͷ����¼����¼���ΪMarry Event
       public event MarryHandler MarryEvent;
               
       static void Main(string[] args)
       {
       }
   }
   ```

   IL�Ĵ���Ϊ��

   ![1554023178012](mdPics\1554023178012.png)  

   - MarryHandlerί�б������һ����

   - MarryEvent�¼������������

     ```
     .event Capital9_Event_03.Capital9_Event_03/MarryHandler MarryEvent
     {
       .addon instance void Capital9_Event_03.Capital9_Event_03::add_MarryEvent(class Capital9_Event_03.Capital9_Event_03/MarryHandler)
       .removeon instance void Capital9_Event_03.Capital9_Event_03::remove_MarryEvent(class Capital9_Event_03.Capital9_Event_03/MarryHandler)
     } // end of event Capital9_Event_03::MarryEvent
     ```

     ��������������add\_�� remove_ǰ׺���������C#�¼����ơ�**add_MarryEvent()**���£�

     ```
     .method public hidebysig specialname instance void 
             add_MarryEvent(class Capital9_Event_03.Capital9_Event_03/MarryHandler 'value') cil managed
     {
       ... ...
       // ���� Delegate.Combine()����
       IL_000b:  call       class [mscorlib]System.Delegate [mscorlib]System.Delegate::Combine(class [mscorlib]System.Delegate,                                                  class [mscorlib]System.Delegate)
       ... ...
     } // end of method Capital9_Event_03::add_MarryEvent
     ```

     **add_MarryEvent()**������ͨ������Delegate.Combine()����ʵ�ֵģ��˷��������ί�����Ϊһ����·�㲥ί�С�**remove_MarryEvent()** 

     ```
     .method public hidebysig specialname instance void 
             remove_MarryEvent(class Capital9_Event_03.Capital9_Event_03/MarryHandler 'value') cil managed
     {
       ... ...
       IL_000b:  call       class [mscorlib]System.Delegate [mscorlib]System.Delegate::Remove(class [mscorlib]System.Delegate,                                                   class [mscorlib]System.Delegate)
       ... ...
     } // end of method Capital9_Event_03::remove_MarryEvent
     ```

     ˫��MarryEvent:private class Capital9_Event_03.Capital9_Event_03/MarryHandler

     ```
     .field private class Capital9_Event_03.Capital9_Event_03/MarryHandler MarryEvent
     ```

     - .fieldָ���������һ���ֶΣ��ֶ�����ΪMarryHandler���ֶ�����ΪMarryEvent��
     - ���ֶ���˽�У����ڱ�����¼������������ã��Ҹ�ί�����͵ı���Ϊ˽�У�ֻ�ܴӶ�����¼������н��з��ʡ�

     

     