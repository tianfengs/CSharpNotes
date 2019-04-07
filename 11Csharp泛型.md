---
typora-copy-images-to: mdPics
---

# 《Learnig Hard C# 学习笔记》泛型 第11章

C# 1.0中的**委托**特性**使方法可作为**其他方法的**参数来传递**，而C# 2.0中提出的**泛型**特性则是**类型可以被参数化**，从而不必再为不同的类型提供特殊版本的方法实现。

泛型所提供的代码重用是算法的重用，即某个方法实现不需要考虑所操作的数据的类型。

### 一、泛型的理解

泛型，generic通用的，泛型代表的就是“通用类型”，可代替任意的数据类型，使**类型参数化**。

从而可以只实现一个方法就可以操作多种数据类型的目的。

泛型将 **方法的实现** 与 **方法操作的数据类型** 分离，实现**代码重用**。

### 二、C# 2.0引入泛型的动机

1. 实现两个整型、两个字符串的比较

   常规方法（**见示例Capital11_Generic_01**) 

   ```
   // 普通的比较类和方法
   public class Compare
   {
       public static int CompareInt(int int1,int int2)
       {
           return int1.CompareTo(int2) > 0 ? int1 : int2;
       }
   
       public static string CompareStr(string str1,string str2)
       {
           return str1.CompareTo(str2) > 0 ? str1 : str2;
       }
   }
   ```

   泛型方法

   ```
   public class Compare<T> where T : IComparable
   {
       public static T CompareGeneric(T t1, T t2)
       {
           return t1.CompareTo(t2) > 0 ? t1 : t2;
       }
   }
   ```

   使用泛型方法

   ```
   static void Main(string[] args)
   {
       var res1 = Compare<int>.CompareGeneric(1, 2);
       var res2 = Compare<string>.CompareGeneric("aaa", "ddd");
   
       Console.WriteLine(res1);
       Console.WriteLine(res2);
   
       Console.ReadKey();
   }
   ```

2. 比较泛型方法与普通方法加装箱拆箱操作**性能上的优势**（**见示例Capital11_Generic_02**）

   引用类型-值类型间存在转换，转换的装箱拆箱操作会引起性能损失，泛型是避免损失的有效方法。

   ```
   using System;
   using System.Collections.Generic;
   using System.Collections;
   using System.Diagnostics;
   
   class Capital11_Generic_02
   {
       static void Main(string[] args)
       {
           // 测试泛型类型的运行时间
           testGeneric();
           // 测试非泛型类型的运行时间
           testNonGeneric();
   
           Console.ReadKey();
       }
   
       /// <summary>
       /// 测试泛型类型操作的运行时间
       /// </summary>
       private static void testGeneric()
       {
           Stopwatch stopwatch = new Stopwatch();
   
           List<int> genericList = new List<int>();
   
           stopwatch.Start();
   
           for(int i = 0; i < 10000000; i++)
           {
               genericList.Add(i);
           }
   
           stopwatch.Stop();
   
           //输出时间
           TimeSpan timeSpan = stopwatch.Elapsed;
           // 00:00:00这样输出
           string elapsedTime = String.Format($"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}:{timeSpan.Milliseconds / 10:00}");
           Console.WriteLine($"泛型的运行时间：{elapsedTime}");
       }
   
       /// <summary>
       /// 测试非泛型类型操作的运行时间
       /// </summary>
       private static void testNonGeneric()
       {
           Stopwatch stopwatch = new Stopwatch();
   
           ArrayList arrayList = new ArrayList();
   
           stopwatch.Start();
   
           for(int i = 0; i < 10000000; i++)
           {
               arrayList.Add(i);
           }
   
           stopwatch.Stop();
   
           //输出时间
           TimeSpan timeSpan = stopwatch.Elapsed;
           // 00:00:00这样输出
           string elapsedTime = String.Format($"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}:{timeSpan.Milliseconds / 10:00}");
           Console.WriteLine($"非泛型的运行时间：{elapsedTime}");
   
       }
   }
   ```

   结果为：

   ![1554042478987](mdPics\1554042478987.png) 

### 三、全面解析泛型

1. 类型参数 T

   T为类型占位符。分为两类：（**见示例Capital11_Generic_03**）

   - 未绑定的泛型：没有为类型参数提供实际类型。开放类型。
   - 已构造的泛型：已指定了实际类型作为参数
     - 开放类型：包含类型参数的泛型
     - 密封类型：已经为每一个类型参数都传递了实际数据类型的泛型。

   ```
   public class DictionaryStringKey<T> : Dictionary<string, T>
   {
   }
   
   class Capital11_Generic_03
   {
       static void Main(string[] args)
       {
           // Dictionary<,>是一个“开放类型”，它有两个类型参数
           Type t = typeof(Dictionary<,>);
           Console.WriteLine("Dictionary <,>是否为开放类型：" + t.ContainsGenericParameters);
   
           //
           t = typeof(DictionaryStringKey<>);
           Console.WriteLine("Dictionary <,>是否为开放类型：" + t.ContainsGenericParameters);
   
           //
           t = typeof(DictionaryStringKey<int>);
           Console.WriteLine("Dictionary <,>是否为开放类型：" + t.ContainsGenericParameters);
   
           //
           t = typeof(Dictionary<int, int>);
           Console.WriteLine("Dictionary <,>是否为开放类型：" + t.ContainsGenericParameters);
   
           Console.ReadKey();
       }
   }
   ```

   结果是：

   ![1554125777745](mdPics\1554125777745.png) 

   - Type.ContainsGenericParameters 判断类型对象是否包含违背是积累下替代的类型参数
   - typeof()参考：http://msdn.microsoft.com/zh-cn/library/58918ffs(v=vs.110).aspx

2. 泛型中的静态字段和静态函数问题

   - 静态数据是属于类型的。一个类只有一个该静态字段的值。
   - 每个封闭的泛型类型，都有一个属于它的静态数据（**见示例Capital11_Generic_04**）

   ```
   // 泛型类型，具有一个类型参数
   public static class TypeWithStaticField<T>
   {
       // static field
       public static string field;
       // static ctor
       public static void OutField()
       {
           Console.WriteLine(field + ":" + typeof(T).Name);
       }
   }
   
   // 非泛型类
   public static class NoGenericTypeWithStaticField
   {
       public static string field;
       public static void OutField()
       {
           Console.WriteLine(field);
       }
   }
   
   static void Main(string[] args)
   {
       // 实例化泛型实例
       TypeWithStaticField<int>.field = "一";
       TypeWithStaticField<string>.field = "二";
       TypeWithStaticField<Guid>.field = "三";
   
       // 实例化非泛型实例
       NoGenericTypeWithStaticField.field = "非泛型静态字段一";
       NoGenericTypeWithStaticField.field = "非泛型静态字段二";
       NoGenericTypeWithStaticField.field = "非泛型静态字段三";
   
       //输出
       NoGenericTypeWithStaticField.OutField();
   
       TypeWithStaticField<int>.OutField();
       TypeWithStaticField<string>.OutField();
       TypeWithStaticField<Guid>.OutField();
   
       Console.ReadKey();
   }
   ```

   结果：

   ![1554126839777](mdPics\1554126839777.png) 

3. 类型参数的推断

   使用泛型时都需要写"<"和">"，可以在有时省略，由编译器推断。

4. 类型参数的约束

   在Capital11_Generic_01中，使用

   ```
   public class Compare<T> where T : IComparable
   ```

   其中where语句用来使类型参数继承于IComparable接口，从而对类型参数进行约束。

   类型约束使用**where**关键字来限制某个类型实参的类型。

   有四种约束：

   - 引用类型约束

     ```
     where T : class
     ```

     例如：

     ```
     using System.IO;
     public class SampleReference<T> where T : Stream
     {
         public void Test(T stream)
         {
         	stream.Close();
         }
     }
     ```

     如果没有指定约束，默认为System.Object，但这个约束不能显式指定，会报错。

   - 值类型约束

     ```
     where T : struct
     ```

     确保传递的类型实参是**值类型**，包括**枚举**，但**不能是可空**类型。例如：

     ```
     public class SampleValuetype<T> where T : struct
     {
         public static T Test()
         {
             return new T();
         }
     }
     ```

     所有值类型都有一个公共无参构造函数。引用类型会报错。

   - 构造函数类型约束

     ```
     where T : new()
     ```

     如果类型参数约定有多个，这个**必须是最后一个**。

     此约束保证：**类型的实参有一个公共无参构造函数的非抽象类型**。

     适用于值类型、非静态、非抽象、没有显示声明构造函数的类。

     注意：不能同时指定：构造器约束 和 struct约束。

   - 转换类型的约束

     ```
     where T : 基类名	(确保指定的类型实参：必须是基类或派生自基类的子类)
     where T : 接口名	(确保指定的类型实参：必须是接口或实现接口的类)
     where T : U		  (确保指定的类型实参：必须是U提供的类型实参或派生自U提供的类型实参)
     ```

     例如：

     | 声明                                  | 已构造类型的例子                                             |
     | ------------------------------------- | ------------------------------------------------------------ |
     | Class Sample<T> where T : Stream      | Sample<Stream>是有效的<br />Sample<String>是有效的           |
     | Class Sample<T> where T : IDisposable | Sample<Stream>是有效的<br />Sample<StringBuilder>是无效的    |
     | Class Sample<T,U> where T : U         | Sample<Stream，IDisposable>是有效的<br />Sample<String，IDisposable>是无效的 |

   - 组合约束

     是将多个不同种类的约束合并在一起的情况。

     - 冲突：引用类型和值类型 
     - 类约束放前面，接口约束放后面

     - 例子

       ```
       有效：
       class Sample<T> where T: class, IDisposable, new()
       class Sample<T,U> where T: class Where U: struct
       无效:
       class Sample<T> where T: class, struct	()
       class Sample<T> where T: Stream, class	()
       class Sample<T> where T: new(), Stream	()
       class Sample<T> where T: IDisposable, Stream
       class Sample<T,U> where T: struct where U:class
       class Sample<T,U> where T: Stream, U:IDisposable
       ```

       