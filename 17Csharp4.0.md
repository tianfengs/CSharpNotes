typora-copy-images-to: mdPics

# 《Learnig Hard C# 学习笔记》C# 4.0 中的微小改动 第17章

C# 4.0 最核心的特征——动态类型。

### 一、可选参数和命名实参

1. 可选参数

   如下代码中定义的方法包含3个参数，一个必备参数和两个可选参数：

   ```
   static void TestMethod(int x, int y=10, string name="LearningHard")
   {
       Console.WriteLine("x={0} y={1} name={2}", x, y, name);
   }
   ```

   注意事项：

   - 所有可选参数必须位于必选参数之后。

   - 可选参数的默认值必须为常量，如数组、常量字符串、null、const成员和枚举成员等。下面语句非法：

     ```
     static void TestMethod(DateTime dt=DAtaTime.Now);
     ```

   - 参数数组（有params修饰符声明）不能为可选参数。下面语句非法：

     ```
     static void TestMethod(params int[] input=null);
     ```

   - 用ref或out关键字标识的参数不能被设置为可选参数。

2. 命名实参

   当调用带有可选参数的方法时，如果我们省略了一个参数，编译器默认我们省略的是最后一个参数。

   如果我们只想省略第二个参数，就可以使用命名实参来解决这个问题。例如：

   ```
   static void Main(string[] args)
   {
       // 省略name
       TestMethod(2,14);
       // 省略后两个参数
       TestMethod(2);
       // 只省略第二个参数
       TestMethod(1, name : "Jerry");
       
       Console.ReadKey();
   }
   ```

3. COM互操作的福音

   可选参数和命名参数是C# 4.0 中最简单的两个特性，最大的好处是简化了**C#与COM组件的互操作**。

   COM（Component Object Model，组件对象模型）。C#语言可以去调用COM组件的功能。可以参考MSND中：<http://msdn.microsoft.com/zh-cn/library/bd9cdfyx(v=vs.110).aspx> 。

   关于互操作性，可参考： http://www.cnblogs.com/zhili/archive/2013/01/14/NetInterop.html 

   C# 4.0 之前调用COM组件的过程：(**见示例：Capital17_01_COM**)

   ```
   using Microsoft.Office.Interop.Word;
   
   class Capital17_01_COM
   {
       static void Main(string[] args)
       {
           Object missing = Type.Missing;
   
           // 启动Word应用程序并使Word可见
           Application wordApp = new Application { Visible = true };
   
           // 新建一个Word文档
           wordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);
           Document wordDoc = wordApp.ActiveDocument;
   
           // 添加一个段落
           Paragraph para = wordDoc.Paragraphs.Add(ref missing);
           para.Range.Text = "欢迎你，使用旧的调用COM对象的方法，建立Word文档。";
   
           // 保存文档
           object filename = @"OldCOMCallMethod.doc";
           object format = WdSaveFormat.wdFormatDocument97;
           wordDoc.SaveAs2(ref filename, ref format,
            ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing);
   
           // 关闭Word文档和Word应用程序
           wordDoc.Close(ref missing, ref missing, ref missing);
           wordApp.Application.Quit(ref missing, ref missing, ref missing);
       }
   }
   ```

   C# 4.0 调用COM组件的过程，使用了**可选参数**和**命名参数**之后，不必再传递一些不需要的参数了：

   ```
   static void NewCOMCallMethod4()
   {
       //Object missing = Type.Missing;
   
       // 启动Word应用程序并使Word可见
       Application wordApp = new Application() { Visible = true };
   
       // 新建一个Word文档
       wordApp.Documents.Add();
       Document wordDoc = wordApp.ActiveDocument;
   
       // 添加一个段落
       Paragraph para = wordDoc.Paragraphs.Add();
       para.Range.Text = "欢迎你，使用新的调用COM对象的方法，建立Word文档。";
   
       // 保存文档
       object filename = Environment.CurrentDirectory + @"NewCOMCallMethod.doc";
       object format = WdSaveFormat.wdFormatDocument97;
       wordDoc.SaveAs2(ref filename, ref format);
   
       // 关闭Word文档和Word应用程序
       wordDoc.Close();
       wordApp.Application.Quit();
   }
   ```

   > C# 互操作性：<http://www.cnblogs.com/zhili/archive/2013/01/14/NetInterop.html>
   >
   > .NET 平台下提供了3种互操作性的技术：
   >
   > - Platform Invoke(P/Invoke)，即**平台调用**,主要用于调用C库函数和Windows API
   >
   >   使用平台调用的技术可以在托管代码中调用动态链接库（Dll）中实现的非托管函数，如Win32 Dll和C/C++ 创建的dll。
   >
   >   - 在托管代码中通过平台调用来调用非托管代码的步骤
   >
   >     (1).  **获得**非托管函数的信息，即**dll的名称**，需要调用的非托管函数名等信息
   >
   >     (2). 在托管代码中对非**托管函数**进行**声明**，并且**附加**平台调用所需要**属性**
   >
   >     (3). 在托管代码中**直接调用**第二步中声明的**托管函数**
   >
   >   - 平台调用的调用过程
   >
   >     ![1554617165489](mdPics\1554617165489.png) 
   >
   > - C++ Introp, 主要用于Managed C++(托管C++)中调用C++类库
   >
   >   C++ Interop 方式有一个与平台调用不一样的地方，就是C++ Interop 允许托管代码和非托管代码存在于一个程序集中，甚至同一个文件中。C++ Interop 是在源代码上直接链接和编译非托管代码来实现与非托管代码进行互操作的，而平台调用是加载编译后生成的非托管DLL并查找函数的入口地址来实现与非托管函数进行互操作的。**C++ Interop使用托管C++来包装非托管C++代码，然后编译生成程序集，然后再托管代码中引用该程序集，从而来实现与非托管代码的互操作**。
   >
   > - COM Interop, 主要用于在.NET中调用COM组件和在COM中使用.NET程序集。
   >
   >   COM Interop不仅支持在托管代码中使用COM组件，而且还支持向COM组件功能托管对象。
   >
   >   - 在.NET中使用COM组件
   >
   >     在.NET中使用COM对象，主要有3种方法：
   >
   >     1. 使用TlbImp工具为COM组件创建一个互操作程序集来绑定早期的COM对象，这样就可以在程序中添加互操作程序集来调用COM对象
   >     2. 通过反射来后期绑定COM对象
   >     3. 通过P/Invoke创建COM对象或使用C++ Interop为COM对象编写包装类
   >
   >     我们经常使用的都是方法一，下面介绍下使用方法一在.NET 中使用COM对象的步骤：
   >
   >     1. 找到要使用的COM 组件并注册它。使用 regsvr32.exe 注册或注销 COM DLL。                               
   >
   >     2. 在项目中添加对 COM 组件或类型库的引用。                             
   >
   >        **添加引用时，Visual Studio 会用到Tlbimp.exe（类型库导入程序），Tlbimp.exe程序将生成一个 .NET Framework 互操作程序集。该程序集又称为运行时可调用包装 (RCW)，其中包含了包装COM组件中的类和接口。Visual Studio 将生成组件的引用添加至项目。**                           
   >
   >     3. 创建RCW中类的实例，这样就可以使用托管对象一样来使用COM对象。
   >
   >     ![1554617497040](mdPics\1554617497040.png) 
   >
   >   - 在COM中使用.NET程序集
   >
   >     .NET 公共语言运行时通过**COM可调用包装**（COM Callable Wrapper,即**CCW**）来完成与COM类型库的交互。CCW可以使COM客户端认为是在与普通的COM类型交互，同时使.NET组件认为它正在与托管应用程序交互。在这里**CCW是非托管COM客户端与托管对象之间的一个代理。 CCW既可以维护托管对象的生命周期，也负责数据类型在COM和.NET之间的相互转换**。实现在COM使用.NET 类型的基本步骤如：
   >
   >     - 在C#项目中添加互操作特性
   >
   >       ![1554617724056](mdPics\1554617724056.png)
   >
   >     - 生成COM类型库并对它进行注册以供COM客户端使用
   >
   >       ![1554617773194](mdPics\1554617773194.png)
   >
   >       勾选“**为COM互操作注册**”选项后，Visual Studio会调用类型库导出工具(Tlbexp.exe)为.NET程序集生成COM类型库再使用程序集注册工具(Regasm.exe)来完成对.NET程序集和生成的COM类型库进行注册，这样COM客户端可以使用CCW服务来对.NET对象进行调用了。

   > **在C# 中调用COM组件** 
   >
   > 实现原理剖析
   >
   > 程序里添加”**Microsoft.Office.Interop.Word 14.0.0.0** “ 这个引用，14.0.0.0版本是对应于Office 2010。
   >
   > **Microsoft.Office.Interop.Word.dll** 确实是一个.NET程序集，并且它也叫做COM组件的互操作程序集，
   >
   > `这个程序集中包含了COM组件中定义的类型的元数据， 托管代码通过调用互操作程序集中公开的接口或对象来间接地调用COM对象和接口的。由于托管代码中不能直接使用COM对象和接口，所以托管代码对COM对象的调用时是通过CLR的 "COM Interop" 层作为代理完成的，这个代理就是RCW（即Runtime Callable Wrapper,运行时可调用包装），所以对COM对象的调用，都是通过RCW来完成的，RCW做的工作主要有激活COM对象和在托管代码和非托管代码之间进行数据封送处理` 
   >
   > （从这里可以看出，RCW就是 .NET平台和COM组件之间的一个代理，微软的很多技术都使用了代理的，例如WCF技术——我们在代码中创建的对象其实只是服务的一个代理，通过代理对象来访问真真的对象的服务，即方法。讲到代理的技术，C#中的委托也是代理的一种实现，此时又想到了23种设计模式中的——代理模式）。下面通过一个图来演示下 **在.NET中调用COM组件的原理：** 
   >
   > ![1554618418832](mdPics\1554618418832.png)  
   >
   > 通过Tlblmp.exe工具来生成互操作程序集步骤，可以参考MSDN中这个工具详细使用说明 ：<http://msdn.microsoft.com/zh-cn/library/tt0cf3sx(v=VS.80).aspx> 。
   >
   > 我们也可以使用Visual Studio中内置的支持来完成为COM类型库创建互操作程序集的工作，我们只需要在**VS中为.NET 项目添加对应的COM组件的引用，此时VS就会自动将COM类型库中的COM类型库转化为程序集中的元数据**，并在项目的Bin目录下生成对于的互操作程序集，所以在VS中添加COM引用，其实最后程序中引用的是互操作程序集，然后通过RCW来对COM组件进行调用。
   >
   >  然而对于Office中的**Microsoft.Office.Interop.Wordd.dll，**这个程序集也是互操作程序集，但是它又是主互操作程序集，即PIA(Primary Interop Assemblies)。主互操作程序集是一个由供应商提供的**唯一**的程序集，为了生成主互操作程序集，可以在使用TlbImp命令是打开 /primary 选项。看到这里，朋友们肯定有这样的疑问：PIA与普通程序集到底有什么区别呢？——区别就是PIA除了包含了COM组件定义的数据类型外，还包含了一些特殊的信息，如公钥，COM类型库的提供者等信息。
   >
   > 为什么需要主互操作程序集的呢 ？答案是——主互操作程序集可以帮助我们解决部署程序时，引用互操作程序集版本不一致的问题。(如果开发人员会为一个COM组件类型库生成多个互操作程序集，项目中引用的互操作程序集版本与部署时的互操作程序集版本不一致的问题，有了互操作程序集时，我们可以直接引用官方提供主互操作程序集。)
   >
   > 
   >
   > **错误处理**：(**见示例：Capital17_02_COM**)
   >
   > 错误的处理的方法和我们平常托管代码中的处理方式是一样的，下面就具体看看是如何获取错误信息的，下面这段代码的功能是——打开一个现有的Word文档并插入相应的文本，当指定的Word文档不存在时，此时就会出现调用COM对象的Open方法失败的情况。
   >
   > 
   >
   > **Microsoft.Office.Interop.Word使用**：
   >
   > (**见示例：Capital17_03_COMWord**)
   >
   > <https://www.cnblogs.com/xh6300/p/5915717.html>
   >
   > http://blog.csdn.net/ruby97/article/details/7406806
   >
   > http://blog.csdn.net/yhrun/article/details/7674540
   >
   > http://www.cnblogs.com/eye-like/p/4121219.html
   >
   > http://www.cnblogs.com/knowledgesea/archive/2013/05/24/3095376.html
   >
   > http://www.cnblogs.com/shi2172843/p/5848116.html
   >
   > <https://msdn.microsoft.com/en-us/library/bb257531(v=office.12).aspx> 英文
   >
   > <https://docs.microsoft.com/zh-cn/office/vba/api/overview/word> 中文
   >
   > http://wenku.baidu.com/view/80ec0a6c1eb91a37f1115cab.html?from=search

?	















