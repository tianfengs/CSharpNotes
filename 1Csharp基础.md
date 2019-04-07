---
typora-copy-images-to: mdPics

---

# 《Learnig Hard C# 学习笔记》基础知识 1-7章

  

### 什么是.NET Framework？

C#是编程语言，用于实现与计算机对话
.NET Framework是应用程序运行时的执行环境，提供下面的服务：
   - 全面的类库
- 内存管理
- 通用类型系统（Common Type System, CTS) CTS定义了可以在中间语言中使用的预定义数据类型
- 开发结构和技术   .NET Framework提供了开发特定应用所需的库，如Web应用程序的ASP.NET技术
- 语言互操作性    编译器都提供了生成中间语言代码的机制，这种机制使得不同语言之间进行互操作成为可能。

### .NET Framework组成

1. 公共语言运行时（Common Language Runtime, **CLR**)
   提供内存管理、线程管理和异常处理等服务，负责对代码进行类型安全检查。
   将受CLR管理的代码称为 托管代码（managed Code），不受CLR管理的代码称为 非托管代码（unmanaged code）
   CLR包括两部分：

   - 通用类型系统（Common Type System，**CTS**）

     - CTS用于解决不同语言之间数据类型不同的问题。
       -  例如：C#的整型是int，VB.NET是Integer，CTS可以把它们变成通用类型Int32。
     -  CTS
       -  类型：引用类型、值类型。
       -  类型之间转换：装箱（box）、拆箱（unbox）

   - 公共语言规范（Common Language Specification，**CLS**）

     CLS用于解决不同语言之间语言规范不同的问题

     - CLS是一种最低的语言标准，它定制了以.NET平台为目标的语言所必须支持的最小特征，以及语言之间实现互操作的完备特征
     - 例如：C#变量区分大小写、VB.NET变量不区分，CLS规定，编译后的IL代码除了大小写，还必须有其它不同

2. .NET Framework类库（Framework Class Library，**FCL**）
   .NET Framework类库就是一组DLL程序集的集合。定义了大量的类型，并把相关的一组类型放到一个单独的命名空间来区分。

### C#与.NET Framework的关系

![Csharp与dotNETFramework的关系](mdPics\Csharp与dotNETFramework的关系.png)

  

  

### C#代码的执行过程
两个阶段：

   - C#代码编译为中间语言代码（Common Intermediate Language，CIL），这个过程由C#编译器来完成。
     - 创建的IL代码将存储在一个程序集中，除IL代码外，还包括元数据和可选的资源文件。
         - 元数据描述代码类型：类型定义的描述、类型成员的描述、类型引用成员的描述。

         - 可选的资源文件指其它数据，如图片等。

           ![](mdPics\C#代码的执行过程.png)

         - 程序集分为两种：扩展名为.exe的可执行文件，扩展名为.dll的可被调用的库文件。

     - 中间语言代码编译为本机代码（native code），这个过程由 及时编译器（Just-In-Time， JIT）来完成。

![](mdPics\C#代码的执行过程2.png)

### 创建C#程序

   - 创建程序
- 编译和运行
     - F5运行和调试、Ctrl+F5只运行

     - 通过命令行编译和运行

       ```
       csc [options] sourceFiles 
       ```

        

### C#中的类


4.3.6 索引器

**索引器**可以让我们像访问数组一样访问类中的数组成员。

定义：

```
 [修饰符] 数据类型 this[索引类型 index]
    {
        get { 返回类中数组某个元素 }
        set { 对类中某数组元素赋值 }
    }
```

例如：
定义一个带索引器的类：

```
class Person
{
    private int[] intarray = new int[10];
    // 索引器定义
    public int this[int index]
    {
        get
        {
            return intarray[index];
        }
        set
        {
            intarray[index] = value;
        }
    }
}
```

下面演示了如何使用索引器：            

    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            // 通过索引器对类中的数组进行赋值
            person[0] = 1;
            person[1] = 2;
            person[2] = 3;
            
            Console.WriteLine(person[0]);
            Console.WriteLine(person[1]);
            Console.WriteLine(person[2]);
    
            Console.ReadKey();
        }
    }
4.5 类与结构体的区别

- 语法上的区别：定义的关键字 **class** 和 **struct**
- 结构体不可对声明字段进行初始化，但类可以对声明字段进行初始化。
-  类会自动生成一个**无参实例构造函数**，但一旦显式定义了构造函数，这个隐式构造函数就没有了。而**结构体**，无论是否显式的定义了构造函数，隐式构造函数都是**一直存在**的。
- 结构体重不能显式的定义无参构造函数，说明结构体中无参构造函数一直存在。
- 在结构体的构造函数中，必须要为结构体中所有的字段赋值。
-  创建结构体对象可以不使用new，但此时结构体对象中的字段是没有初始值的；而类必须使用new关键字来创建对象。
- **结构体**不能继承结构或者类，但**可以实现接口**；**类****可以继承类和实现接口**，但不能继承结构
- 类是引用类型，而结构体是值类型
- 结构体无析构函数，类有析构函数
- 不能用abstract和sealed修饰结构体，而类可以

   

### 第7章 IL语言
IL（Intermediate Language），它也称为CIL或者MSIL，**中间语言**。

IL由ECMA组织（ECMA-335标准）提供完整的定义和规范。

我们可以直接把C#源程序编译为.exe或.dll文件，此时编译出来的程序代码并不是二进制代码，而是IL代码。

1. ##### 怎样去查看IL代码：
   微软提供了反编译器 —— **ILDasm.exe**     [C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin](file://C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin)
   也可以在Visual Studio 2010找到该工具： C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX4.0Tools

- 程序代码： 

![](mdPics\HelloWorld代码.png)

- 用ILDasm.exe打开ConsoleApp1.exe文件

![](mdPics\ILDasm.exe窗口.png)

- MANIFEST是一个清单文件，包含程序集的属性，例如：程序集名，版本号、哈希算法、程序集模块，以及对外部引用程序的引用项目

![](mdPics\MANIFEST文件清单窗口.png)

![](mdPics\ILDasm.exe窗口指令说明.png)

- Program类的IL代码分析

  - 双击.class private auto ansi beforefieldinit

    ![img](mdPics\896449-20180211113121451-881183676.png)  

    - .class 代表Program是一个类，extends代表Program继承于程序集mscorlib中的System.Object类
    - private为访问权限，表明该类是私有的。
    - auto表明程序加载时内存的布局室友CLR决定的，而不是由程序控制的。
    - ansi表明类的编码为ansi编码
    - beforefieldinit表明CLR可以在第一次访问静态字段之前的任何时刻执行类型构造函数。类型构造函数就是静态构造函数。

  - 双击 .ctor:void()

    ![img](mdPics\896449-20180211114351982-445894811.png)  

    - .ctor表示该方法是类的构造函数，这段IL代码就是构造函数的IL代码
    - cil managed表明方法体中的代码是IL代码，且是托管代码，即运行在CLR运行库上代码
    - .maxstack表明执行构造函数是，评估堆栈可容纳数据项的最大个数。评估堆栈是保存方法中所需变量的值的一个内存区域，该区域在方法执行结束时会被清空，或者存储一个返回值
    - IL_0000是代码开头。在它之前的部分为变量的声明和初始化操作
    - ldarg.0表明加载第一个成员参数，其中ldarg是load argument的缩写
    - call 指令一般用于调用静态方法，这段代码中call并不是调用静态函数，而是调用System.Object构造函数。另外一个指令callvir一般调用实例方法：
      - 首先检查被调用函数是否为虚函数
      - 是的化，则查子类是否重写，如果重写就调用子类的实现，如果没有重写，则继续调用原来函数
    - ret指令表示执行完毕，return的简写

  - 双击 Main：void（string[])

    ![img](mdPics\896449-20180211120451654-1402338139.png) 

    - Main函数是程序的入口

    - hidebysig指令表示如果当前类作为父类，用该指令标记的方法将不会被子类继承

    - .entrypoint指令代表该函数程序的入口函数。每个托管应用程序都有且只有一个入口函数。CLR加载程序时，首先从.entrypoint方法开始执行

    - .locals init ([0] string helloString)表示定义string类型的变量，变量名称为helloString

    - nop 即 No operation的意思，即没有任何操作

    - ldstr “Hello World” 指令（load string）表示将字符串压入 评估栈，此时“Hello World”位于评估栈的栈顶

    - stloc.0 指令（stack local 0）(或？store local 0)表示把值从评估栈中弹出，并赋值给调用栈（call stack）中的第0个变量，也就是helloString变量。调用栈为一个存放局部变量的内存区域。

      评估栈和调用栈都是由CLR进行管理的。从.local指令到IL_0006之间的IL代码就相当于C#代码

      ```
      string helloString = "Hello World";
      ```

    - ldloc.0指令表示把第0个局部变量压入评估栈中。

      > ld前缀表示入栈操作，st为前缀的指令代表出栈操作

    - call指令表示调用静态函数，这里调用的是Console类中的WriteLine(string)方法，把第0个局部变量输出到控制台中。

2. ##### (7.3)看的IL代码

   IL是一门基于堆栈操作的面向对象的语言。

   1. IL基本类型

      ![1553699176593](mdPics\1553699176593.png)

   2. 变量的声明

      C#语言：

      ```
      数据类型  变量名称；
      ```

      IL代码：声明变量，并把变量放入"调用栈" [0]

      ```
      .local init ([0] 数据类型 变量名称)
      ```

   3. 基本运算

      - 算术运算：加法add、乘法sub、除法div、求余rem等

      - 位运算：一元指令not、与and、或or等。结果以1和0分别表示真和假。运算结果压入评估栈栈顶。

      - 比较运算：大于指令cgt、小于clt和等于ceq

        举例：

        C#源代码：

        ```
        class Captal7_IL_01
        {
            static void Main(string[] args)
            {
                int i = 2;
                int j = 3;
                int result = i + j;
            }
        }
        ```

        IL的Main方法：

        ```
        .method private hidebysig static void  Main(string[] args) cil managed
        {
          .entrypoint
          // 代码大小       10 (0xa)
          .maxstack  2
          .locals init ([0] int32 i,	// 声明3个整型32位变量i、j、result
                   [1] int32 j,
                   [2] int32 result)
          IL_0000:  nop
          IL_0001:  ldc.i4.2	// 把数2以4字节长度整数压入评估栈
          IL_0002:  stloc.0		// 把评估栈栈顶的值弹出，并赋值给第0个变量，即 i=2
          IL_0003:  ldc.i4.3	// 把数3以4字节长度整数压入评估栈
          IL_0004:  stloc.1		// 把评估栈栈顶的值弹出，并赋值给第1个变量，即 j=3
          IL_0005:  ldloc.0		// 把第0个变量压入评估栈，即i
          IL_0006:  ldloc.1		// 把第1个变量压入评估栈，即j
          IL_0007:  add			// 执行add操作，之后将变量i和j清空，并把结果存于评估栈栈顶
          IL_0008:  stloc.2		// 把评估栈栈顶的值弹出，并赋值给第2个变量，即 result=i+j
          IL_0009:  ret			// 返回
        } // end of method Captal7_IL_01::Main
        ```

   4. IL中的流程控制

      对应C#语言中的流程控制语句if-else语句、while语句和for语句等。

      举例：

      C#源码：

      ```
      class Captal7_IL_02
      {
          static void Main(string[] args)
          {
              int i = 2;
              if (i > 0)
              {
              	Console.WriteLine("i为正数");
              }
              else
              {
              	Console.WriteLine("i为负数");
              }
          }
      }
      ```

      生成的IL代码：

      ```
      .method private hidebysig static void  Main(string[] args) cil managed
      {
        .entrypoint
        // 代码大小       40 (0x28)
        .maxstack  2
        .locals init ([0] int32 i,	// 声明两个变量：整型32位 i、布尔型 V_1
                 [1] bool V_1)
        IL_0000:  nop
        IL_0001:  ldc.i4.2	// 把数2以4字节长度整数压入评估栈
        IL_0002:  stloc.0		// 把评估栈栈顶的值弹出，并赋值给第0个变量，即 i=2
        IL_0003:  ldloc.0		// 把第0个变量压入评估栈，即i
        IL_0004:  ldc.i4.0	// 把数0以4字节长度整数压入评估栈
        IL_0005:  cgt			// 执行大于指令，比较i与0，运行结果存于栈顶
        IL_0007:  stloc.1		// 把大于指令的结果从评估栈栈顶弹出，并付给第1个变量 V_1
        IL_0008:  ldloc.1		// 把第1个变量，即V_1压入评估栈栈顶
        IL_0009:  brfalse.s  IL_001a	// 如果栈顶，即V_1为false，则跳转到 IL_001a
        IL_000b:  nop
        IL_000c:  ldstr      bytearray (69 00 3A 4E 63 6B 70 65 )	// i.:Nckpe字符串压入栈顶
        IL_0011:  call       void [mscorlib]System.Console::WriteLine(string)	//输出到cmd
        IL_0016:  nop
        IL_0017:  nop
        IL_0018:  br.s       IL_0027	// 无条件跳转到 IL_0027
        IL_001a:  nop
        IL_001b:  ldstr      bytearray (69 00 3A 4E 1F 8D 70 65 )	// i.:N..pe字符串压入栈顶
        IL_0020:  call       void [mscorlib]System.Console::WriteLine(string)	//输出到cmd
        IL_0025:  nop
        IL_0026:  nop
        IL_0027:  ret			// 返回
      } // end of method Captal7_IL_02::Main
      ```

   IL指令更多内容，可以参考EMAC文档，特别是"Partition II:Metadata Definition and Semantics (第2部分：元数据定义和语义)"和"Partition III:CLI Instrction Set(第3部分：CIL指令集)"两篇。获取方法：

   - https://github.com/dotnet/coreclr/blob/master/Documentation/project-docs/dotnet-standards.md
   - MSDN上的ECMA C# and Common Language Infrastructure Standards(https://docs.microsoft.com/en-us/previous-versions/dotnet/articles/ms973879(v=msdn.10))。
   - Ecma International网站上的Standard ECMA-355-Common Language Infrastructure(CLI)(http://www.ecma-international.org/publications/standards/Ecma-335.htm)。

