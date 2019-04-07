---
typora-copy-images-to: mdPics
---

# ��Learnig Hard C# ѧϰ�ʼǡ��ɿ����͡����������͵����� ��12��

C# 2.0 ����˿ɿ����͡����������͵�������

### һ���ɿ�����

1. ���

   �ɿ�����Ҳ��ֵ���ͣ�������**����nullֵ��ֵ����**�����磺

   ```
   int? nullable = null;
   ```

   int? �ᱻ����� Nullable<int>���͡�

   ```
   public struct Nullable<T> where T : struct
   ```

   ����(**��ʾ��:"Capital12_01_Nullable"**)

   ```
   static void Main(string[] args)
   {
       Nullable<int> value = 1;
   
       Console.WriteLine("�ɿ�������ֵ�����");
       Display(value);
       Console.WriteLine();
       Console.WriteLine();
   
       value = new Nullable<int>();
       Console.WriteLine("�ɿ�������ֵ�����");
       Display(value);
   
       Console.ReadKey();
   }
   
   private static void Display(int? value)
   {
       Console.WriteLine($"�ɿ������Ƿ���ֵ��{value.HasValue}");
   
       if (value.HasValue)
       {
           Console.WriteLine($"ֵΪ��{value.Value}");
       }
   
       Console.WriteLine($"GetValueOrDefault():{value.GetValueOrDefault()}");
   
       // GetValueOrDefault()��ʾ���HasValueΪtrue����ΪValue����ֵ��
       // ���HasValueΪfalse���򷵻�defaultValue��ֵ���˴�Ϊ2�� 
       Console.WriteLine($"GetValueOrDefault���ط���{value.GetValueOrDefault(2)}");
   
       // GetHashCode()��ʾ���HasValueΪtrue����ΪValue���Է��ض���Ĺ�ϣ���룻
       // ���HasValueΪfalse���򷵻�0��
       Console.WriteLine($"GetHashCode()������ʹ�ã�{value.GetHashCode()}");
   }
   ```

2. �պϲ�������

   ```
   ���ʽ1  ���� ���ʽ2
   ```

   - ��ߵ�����Ϊnull��������ߵ���
   - ��ߵ���Ϊnull���ͷ����ұߵ���
   - �����ڿɿ����ͣ�Ҳ�������������͡���������ֵ����

   ����(**��ʾ��:"Capital12_02_����"**)

3. �ɿ����͵�װ��Ͳ������

   ���ɿ����͸�ֵ����������ʱ��CLR��Կɿ����ͽ���װ����������Ϊnull���򲻽���װ�䣬�����Ϊnull�����ȡֵ�����������װ�����

### ������������

1. ��⣺

   ��������������û�����ֵķ�������Ϊû�����֣���������ֻ���ں��������ʱʱ�򱻵��ã��ѷ����Ķ����ʵ��Ƕ����һ�𣩡����������ڱ�����������ʱ��Ϊ������һ������������IL������һ���Զ����ɵ����֡�

   (**��ʾ��:"Capital12_03_AnonymousMethode"**)

   - ����ί�е��÷���

     ```
     delegate void VoteDelegate(string name);
     
     class Capital12_03_AnonymousMethode
     {
         static void Main(string[] args)
         {
             CallByDelegate();
     
             Console.ReadKey();
         }
     
         // 1. ͨ��ί�е���
         static void CallByDelegate()
         {
             VoteDelegate vote = new VoteDelegate(new Friend().Vote);
             vote.Invoke("С��");
     
         }
     
         public class Friend
         {
             public void Vote(string nickname)
             {
                 Console.WriteLine($"�ǳ�Ϊ��{nickname}: ����ͨ��ί�е��õġ�");
                 Console.WriteLine();
             }
         }
     }
     ```

   - ��������

     ```
     delegate void VoteDelegate(string name);
     
     class Capital12_03_AnonymousMethode
     {
         static void Main(string[] args)
         {
             CallByAnonymousMethode();
     
             Console.ReadKey();
         }
     
         // 2. ͨ��������������
         static void CallByAnonymousMethode()
         {
             VoteDelegate vote = delegate (string nickname)
             {
                 Console.WriteLine($"�ǳ�Ϊ��{nickname}������ͨ�������������õ�");
                 Console.WriteLine();
             };
     
             vote.Invoke("С��");
         }
     }
     ```

     �����������Զ��γ�"�հ�"��

2. �Ա�����׽���̵�����

   �հ���ָ���������������в�׽�����ã����ⲿ���������"�ⲿ����"��"����׽���ⲿ����"��

   (**��ʾ��:"Capital12_04_ClosePack"**)

### ����������

1. ���������

   ��������¼�˼����е�ĳ��λ�ã���ʹ����ֻ����ǰ�ƶ���C# 1.0ʹ��foreach�����ʵ�ַ��ʵ���������������Ԫ�ء�

   foreach������������󣬻����GetEnumerator������һ����������Ҳ����һ�������еĳ�ʼλ�á�

2. C# 1.0 ��ʵ�ֵĵ�����

   - **IEnumerable**��**IEnumerable<T>**�ӿڣ�һ�����������foreach����������ʵ��**IEnumerable**��**IEnumerable<T>**�ӿڣ���ʵ��IEnumerable�ӿ��е� **GetEnumerator()** ������
   - **IEnumerator**�ӿڣ���Ҫʵ�ֵ������������ʵ��**IEnumerator**�ӿ��е�**MoveNext()**��**Reset()**������

   ��C# 2.0 �У���yield�ؼ�������ʵ��IEnumerator��������

   C# 1.0 ��ʵ�ֵ�������(**��ʾ��:"Capital12_05_Enum"**)

   **ʵ��IEnumerable�ӿ�** :

   ```
   // ����һ��������
   public class Friend
   {
       public string Name { get; set; }
   
       public Friend(string name)
       {
       	Name = name;
       }
   }
   // �����༯�ϣ�ʵ��IEnumerable�ӿ�
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
   
       // ������
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
           //// 1. ������������
           //return new FriendIterator(this);
           // 2. ���������IEnumerator
           return friends.GetEnumerator();
       }
   }
   ```

   **����������**��

   ```
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
   ```

3. C# 2.0 ���˵�������ʵ��

   (**��ʾ��:"Capital12_06_EnumYield"**)

   ʵ��IEnmerable

   ```
   // ������
   public class Friend
   {
       public string Name { get; set; }
   
       public Friend(string name)
       {
           Name = name;               
       }
   }
   
   // �����༯��
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
   ```

   **yield return���**�����м����������һ��IEnumerator�ӿڶ��󣬿���ͨ��**Reflector���乤��**���в鿴��

   ![1554456165330](mdPics\1554456165330.png) 

   ```
   [CompilerGenerated]
   private sealed class <GetEnumerator>d__2 : IEnumerator<object>, IDisposable, IEnumerator
   {
       // Fields
       private int <>1__state;
       private object <>2__current;
       public Capital12_06_EnumYield.Capital12_06_EnumYield.Friends <>4__this;
       private int <i>5__1;
   
       // Methods
       [DebuggerHidden]
       public <GetEnumerator>d__2(int <>1__state);
       private bool MoveNext();
       [DebuggerHidden]
       void IEnumerator.Reset();
       [DebuggerHidden]
       void IDisposable.Dispose();
   
       // Properties
       object IEnumerator<object>.Current { [DebuggerHidden] get; }
       object IEnumerator.Current { [DebuggerHidden] get; }
   } 
   Expand Methods
   ```

   yield return�����ʵ��C#���ṩ����һ���﷨�ǣ��﷨�ǣ���C#Ϊ�������ṩ��һ�ּ򻯱�̵��﷨����

4. ��������ִ�й���

   ![1554456325859](mdPics\1554456325859.png) 



### �ġ��ܽ�

���½�����C# 2.0 ��������Ҫ���ԣ�

- �ɿ����ͣ�ʹ�÷�����װ�䡢�������
- ����������ʹ�÷��������������������ã��ⲿ�������ӳ������������ڵ�ԭ��
- ��������C# 2.0 �ĵ��������﷨��yield return���ʵ�� C# 1.0 ��Enumerator���е��﷨��



��һ�½���C# 3.0 �����硣





















