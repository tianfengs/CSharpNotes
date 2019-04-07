typora-copy-images-to: mdPics

# 《Learnig Hard C# 学习笔记》Lambda表达式 第14章

Lambda表达式是C# 3.0 最重要的特性之一。

### 一、Lambda表达式简介

Lambda表达式是一个匿名方法。它可以包含表达式和语句，用于创建委托或转换为表达式树。

使用**"=>"运算符**（读作 goes to），运算符左边是匿名方法的**输入参数**，右边是**表达式或语句块** 

1. Lambda表达式的演变过程

   Lambda表达式，是从匿名方法一路演变过来的。(**见示例“Capital14_Lambda_01”**)

   - C# 1.0 中创建委托实例的代码

     ```
     Func<string, int> delegatetest1 = new Func<string, int>(CallBackMethod);
     ```

   - C# 2.0 中用匿名方法来创建委托实例，则不需要创建回调函数了

     ```
     Func<string, int> delegatetest2 = delegate (string text)
     {
     	return text.Length;
     };
     ```

   - C# 3.0 使用Lambda表达式

     ```
     // 有返回值的方法
     Func<string, int> delegatetest4 = new Func<string, int>(text => text.Length);
     
     Func<string, int> delegatetest3 = text => text.Length;
     
     // 没有返回值的方法
     Action<string, int> delegatetest5 = new Action<string, int>((str, strInt)=>
     {
     	Console.WriteLine($"{str}今年{strInt}岁。");
     });
     
     Action<string, int> delegatetest6 = (str, strInt) => Console.WriteLine($"{str}今年{strInt}岁。");
     ```

     调用Lambda表达式的委托实例：

     ```
     Console.WriteLine("使用Lambda表达式返回字符串长度：" + delegatetest3("ssdfsfsd"));
     
     delegatetest6("zhangsan", 25);
     ```

   Lambda表达式，是新的编程风格，用于代替匿名方法。

2. Lambda表达式的使用

   委托的一个用途是用于“订阅事件”，这里演示**用Lambda表达式实现订阅事件**。

   (**见示例“Capital14_Lambda_02_Event”**)

   C# 2.0 的方法：用**匿名方法**订阅事件和初始化

   ```
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
   ```

   C# 3.0 的方法：用**Lambda表达式**代替匿名方法，用**初始化器初始化**，使代码很简洁。

   ```
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
   ```

### 二、表达式树

Lambda表达式，还可以转换成表达式树（或称“表达式目录树”），**是用来表示Lambda表达式逻辑的一种数据结构**。它将代码表示成一个**“对象树”**，而非可执行代码。

> 表达式树以树形数据结构表示代码，其中每一个节点都是一种表达式。
>
> 你可以对表达式树中的代码进行编辑和运算。 这样能够动态修改可执行代码、在不同数据库中执行 LINQ 查询以及创建动态查询。
>
> 表达式树还能用于动态语言运行时 (DLR) 以提供动态语言和 .NET Framework 之间的互操作性，同时保证编译器编写员能够发射表达式树而非 Microsoft 中间语言 (MSIL)。
>
> 1. 根据 Lambda 表达式创建表达式树
>
>    C# 编译器只能从表达式 Lambda（或单行 Lambda）生成表达式树。 它无法解析语句 lambda （或多行 lambda）。 
>
>    下列代码示例展示如何通过 C# 编译器创建表示 Lambda 表达式 `num => num < 5` 的表达式树。
>
>    ```
>    Expression<Func<int, bool>> lambda = num => num < 5;
>    ```
>
> 2. 通过 API 创建表达式树
>
>    通过 API 创建表达式树需要使用 [Expression](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.expressions.expression) 类。
>
>    类包含创建特定类型表达式树节点的静态工厂方法：
>
>    - ParameterExpression：表示参数变量。解释了几种表达式类型：
>      - MethodCallExpression
>      - System.Linq.Expressions
>      - 另一种具体表达式类型
>    - MethodCallExpression：表示方法调用。
>
>    下列代码示例展示如何使用 API 创建表示 Lambda 表达式 `num => num < 5` 的表达式树
>
>    ```
>    // Add the following using directive to your code file:  
>    // using System.Linq.Expressions;  
>      
>    // Manually build the expression tree for   
>    // the lambda expression num => num < 5.  
>    ParameterExpression numParam = Expression.Parameter(typeof(int), "num");  
>    ConstantExpression five = Expression.Constant(5, typeof(int));  
>    BinaryExpression numLessThanFive = Expression.LessThan(numParam, five);  
>    Expression<Func<int, bool>> lambda1 =  
>        Expression.Lambda<Func<int, bool>>(  
>            numLessThanFive,  
>            new ParameterExpression[] { numParam });  
>    ```
>
>    

1. 动态的构造一个表达式树(见msdn：https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/expression-trees/index)

   (**见示例“Capital14_Lambda_03_ExpressionTrees”**)

   - 构造“a+b”的表达式结构，并解析表达式树

   ```
   /// <summary>
   /// 构造“a+b”的表达式结构，并解析表达式树
   /// </summary>
   static void CreateExpressionTree()
   {
       // 表达式树参数
       ParameterExpression a = Expression.Parameter(typeof(int), "a");
       ParameterExpression b = Expression.Parameter(typeof(int), "b");
   
       // 表达式主体
       BinaryExpression be = Expression.Add(a, b);
   
       // 构造表达式树
       Expression<Func<int, int, int>> expressionTree = Expression.Lambda<Func<int, int, int>>(be, a, b);
   
       // 分析树结构
       BinaryExpression body = (BinaryExpression)expressionTree.Body;
       ParameterExpression left = (ParameterExpression)body.Left;
       ParameterExpression right = (ParameterExpression)body.Right;
   
       // 输出表达式树
       Console.WriteLine("表达式结构为："); Console.WriteLine(expressionTree);
       Console.WriteLine("表达式主体为："); Console.WriteLine(expressionTree.Body);
       Console.WriteLine("表达式左节点为：{0}{4} 节点类型为：{1}{4}{4} 表达式右节点为：{2}{4}" +
          " 节点类型为：{3}{4}", left.Name, left.Type, right.Name, right.Type, 
          Environment.NewLine);
   }
   ```

   - msdn上手动创建 表达式树，并编译解析结果

   ```
   /// <summary>
   /// msdn上手动创建 表达式树，并编译解析结果
   /// </summary>
   static void CreatEprsTreeByAPI()
   {
       // Add the following using directive to your code file:  
       // using System.Linq.Expressions;  
   
       // Manually build the expression tree for   
       // the lambda expression num => num < 5.  
       ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
       ConstantExpression five = Expression.Constant(5, typeof(int));
       BinaryExpression numLessThanFive = Expression.LessThan(numParam, five);
       Expression<Func<int, bool>> lambda1 =
           Expression.Lambda<Func<int, bool>>(
               numLessThanFive,
               new ParameterExpression[] { numParam });
   
       // 输出Lambda表达式树
       Console.WriteLine("输出Lambda表达式树(lambda1)：\t\t\t"+lambda1);
   
       // 编译运行一个表达式树
       var isLessThanFive = lambda1.Compile()(6);
       // 输出结果
       Console.WriteLine("编译表达式树（lambda1.Compile()(6)），结果是：\t" + 
       IsLessThanFive);
   
       Console.WriteLine();
   }
   ```

2. 通过Lambda表达式来构造表达式树

   下列代码示例展示如何通过 C# 编译器创建表示 Lambda 表达式 `num => num < 5` 的表达式树。

   ```
   Expression<Func<int, bool>> lambda = num => num < 5;
   ```

3. 把表达式树转换成可执行代码

   通过Expression<TDelegate>类的Compile()方法，将表达式树编译成委托实例，再通过委托调用的方式得到结构。

   ```
   var isLessThanFive = lambda1.Compile()(6);
   ```

### 三、总结

Lambda表达式是由匿名方法演变过来，实质是个匿名方法。

演示了使用Lambda表达式，加初始化器，简化事件订阅代码的方法。

介绍了Lambda表达式的逻辑树结构：表达式树。它不是可执行代码，需要通过调用Compile()方法来转化为委托对象，才能使用。

