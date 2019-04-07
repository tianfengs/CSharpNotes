---
typora-copy-images-to: mdPics
---

# 《Learnig Hard C# 学习笔记》可空类型、匿名方法和迭代器 第12章

C# 2.0 提出了可空类型、匿名方法和迭代器。

### 一、可空类型

1. 简介

   可空类型也是值类型，但它是**包含null值的值类型**。例如：

   ```
   int? nullable = null;
   ```

   int? 会被编译成 Nullable<int>类型。

   ```
   public struct Nullable<T> where T : struct
   ```

   举例(**见示例:"Capital12_01_Nullable"**)

   ```
   static void Main(string[] args)
   {
       Nullable<int> value = 1;
   
       Console.WriteLine("可空类型有值输出：");
       Display(value);
       Console.WriteLine();
       Console.WriteLine();
   
       value = new Nullable<int>();
       Console.WriteLine("可空类型无值输出：");
       Display(value);
   
       Console.ReadKey();
   }
   
   private static void Display(int? value)
   {
       Console.WriteLine($"可空类型是否有值：{value.HasValue}");
   
       if (value.HasValue)
       {
           Console.WriteLine($"值为：{value.Value}");
       }
   
       Console.WriteLine($"GetValueOrDefault():{value.GetValueOrDefault()}");
   
       // GetValueOrDefault()表示如果HasValue为true，则为Value属性值；
       // 如果HasValue为false，则返回defaultValue的值，此处为2； 
       Console.WriteLine($"GetValueOrDefault重载方法{value.GetValueOrDefault(2)}");
   
       // GetHashCode()表示如果HasValue为true，则为Value属性返回对象的哈希代码；
       // 如果HasValue为false，则返回0；
       Console.WriteLine($"GetHashCode()方法的使用：{value.GetHashCode()}");
   }
   ```

2. 空合并操作符

   ```
   表达式1  ？？ 表达式2
   ```

   - 左边的数不为null，返回左边的数
   - 左边的数为null，就返回右边的数
   - 可用于可空类型，也可用于引用类型。不可用于值类型

   举例(**见示例:"Capital12_02_？？"**)

3. 可空类型的装箱和拆箱操作

   当可空类型赋值给引用类型时，CLR会对可空类型进行装箱操作。如果为null，则不进行装箱，如果不为null，则获取值，并对其进行装箱操作

### 二、匿名方法

1. 理解：

   匿名方法，就是没有名字的方法。因为没有名字，匿名方法只能在函数定义的时时候被调用（把方法的定义和实现嵌套在一起）。编译器会在编译匿名方法时会为其生成一个方法名，在IL里是有一个自动生成的名字。

   (**见示例:"Capital12_03_AnonymousMethode"**)

   - 利用委托调用方法

     ```
     delegate void VoteDelegate(string name);
     
     class Capital12_03_AnonymousMethode
     {
         static void Main(string[] args)
         {
             CallByDelegate();
     
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
     }
     ```

   - 匿名方法

     ```
     delegate void VoteDelegate(string name);
     
     class Capital12_03_AnonymousMethode
     {
         static void Main(string[] args)
         {
             CallByAnonymousMethode();
     
             Console.ReadKey();
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
     ```

     匿名方法会自动形成"闭包"。

2. 对变量捕捉过程的剖析

   闭包，指的是在匿名方法中捕捉（引用）了外部变量。理解"外部变量"和"被捕捉的外部变量"。

   (**见示例:"Capital12_04_ClosePack"**)

### 三、迭代器

1. 迭代器简介

   迭代器记录了集合中的某个位置，它使程序只能向前移动。C# 1.0使用foreach语句来实现访问迭代器，遍历集合元素。

   foreach被编译器编译后，会调用GetEnumerator来返回一个迭代器，也就是一个集合中的初始位置。

2. C# 1.0 中实现的迭代器

   - **IEnumerable**或**IEnumerable<T>**接口：一个类如果想用foreach遍历，必须实现**IEnumerable**或**IEnumerable<T>**接口，并实现IEnumerable接口中的 **GetEnumerator()** 方法。
   - **IEnumerator**接口：而要实现迭代器，则必须实现**IEnumerator**接口中的**MoveNext()**和**Reset()**方法。

   （C# 2.0 中，用yield关键字来简化实现IEnumerator迭代器）

   C# 1.0 中实现迭代器：(**见示例:"Capital12_05_Enum"**)

   **实现IEnumerable接口** :

   ```
   // 创建一个朋友类
   public class Friend
   {
       public string Name { get; set; }
   
       public Friend(string name)
       {
       	Name = name;
       }
   }
   // 朋友类集合，实现IEnumerable接口
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
   ```

   **创建迭代器**：

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

3. C# 2.0 简化了迭代器的实现

   (**见示例:"Capital12_06_EnumYield"**)

   实现IEnmerable

   ```
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
   ```

   **yield return语句**会在中间代码中生成一个IEnumerator接口对象，可以通过**Reflector反射工具**进行查看。

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

   yield return语句其实是C#中提供的另一个语法糖（语法糖：是C#为方便编程提供的一种简化编程的语法）。

4. 迭代器的执行过程

   ![1554456325859](mdPics\1554456325859.png) 



### 四、总结

本章介绍了C# 2.0 的三个重要特性：

- 可空类型：使用方法，装箱、拆箱操作
- 匿名方法：使用方法，匿名方法捕获（引用）外部变量，延长变量声明周期的原理
- 迭代器：C# 2.0 的迭代器用语法糖yield return简洁实现 C# 1.0 中Enumerator类中的语法。



下一章进入C# 3.0 的世界。





















