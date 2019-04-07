typora-copy-images-to: mdPics

# 《Learnig Hard C# 学习笔记》C#的春天——C# 3.0 中的智能编译器 第13章

C# 3.0 颠覆了我们的代码编写风格，出现了Lambda表达式和Linq两个特性。

### 一、自动属性

```
// 定义可读写属性
public string Name { get; set; }

// 定义只读属性
public int Age { get; private set; }
```

编译器会自动为我们创建私有字段。

注意：在结构体重使用自动属性时，所有的构造函数都需要显式的调用无参构造函数this，否则会出现编译错误。原因是：这样，编译器才能知道所有的字段都已经被赋值了。然而，类却不需要显式的调用无参构造函数。

(**见示例:"Capital13_01"**)

```
public struct PersonStruct
{
    public string Name { get; set; }
    public int Age { get; private set; }

    // 结构体中，不显示的调用无参构造函数this()时，会出现编译错误
    public PersonStruct(string name)
        :this() // 如果没有把所有的属性赋值，则要显示调用无参构造函数
    {
        this.Name = name;
        //Age = 5;
    }
}
```

### 二、隐式类型(Implicit)

1. var关键字。如：

   ```
   class Program
   {
       static void Main(string[] args)
       {
           var stringvariable = "Hello World!";
           stringvariable = 2;	// <---这里会报错，编译时错误，因为已经推断var为string类型
       }
   }
   ```

   - 被声明的变量不能是一个字段（包括静态字段和实例字段）
   - 变量声明时，必须被初始化，因为编译器要推断类型。
   - 变量不能初始化为一个方法组，也不能初始化为一个匿名方法。
   - 变量不能初始化为null
   - 等等。。。
   - 隐式类型的优缺点：(**见示例:"Capital13_02_ImplicitType"**)

   ```
   class Capital13_02_ImplicitType
   {
   	static void Main(string[] args)
   	{
   		// 隐式类型 Implicit
   		// 优点: 使用隐式类型，就不用在等式两边都写一遍Dictionary<string, string>
   		var dictionary = new Dictionary<string, string>();
     
   		// 缺点: 使代码难于理解。
   		var a = 2147483649;
   		var b = 928888888888;
   		var c = 2147483644;
   		Console.WriteLine($"变量a的类型为：{a.GetType()}");
   		Console.WriteLine($"变量b的类型为：{b.GetType()}");
   		Console.WriteLine($"变量c的类型为：{c.GetType()}");
     
   		Console.Read();
   	}
   }
   ```

2. 隐式类型数组

### 三、对象集合初始化器

C# 3.0 提供了**初始化器**，可以为不同情况下的初始化，使用默认构造函数即可。

1. 对象初始化器

   (**见示例:"Capital13_03_Initializer"**)

   ```
   class Capital13_03_Initializer
   {
       static void Main(string[] args)
       {
           // 初始化器，只使用默认构造函数即可,不需要为不同情况写多个构造函数
           Person p1 = new Person(){ Name="Zhangsan", Age=25, Height=176, Weight=65 };
           Person p2 = new Person { Name = "Lisi" };
       }
   
       public class Person
       {
           public string Name { get; set; }
           public int Age { get; set; }
           public int Weight { get; set; }
           public int Height { get; set; }
       }
   }
   ```

   注意：

   **如果自定义了构造函数，则类里面的无参构造函数则不存在了**（**结构体无参构造函数一直存在，结构体也不能自己定义无参构造函数，会报错**），要使用初始化器，也**要显示的**再**定义无参构造函数**，如下：

   ```
   public class Person
   {
       public string Name { get; set; }
       public int Age { get; set; }
       public int Weight { get; set; }
       public int Height { get; set; }
       
       public Person(string name)
       {
       	this.Name = name;        
       }
       
       // 注释以下代码，无参构造函数，会出现编译时错误
       public Person(){}    
   }
   ```

2. 集合初始化器

   例如：

   ```
   class Program
   {
       static void Main(string[] args)
       {
       	// 使用集合初始化器
           List<string> newnames= new List<string>
           {
               "Zhangsan"
               , "Lisi"
               , "Wangwu"
           };
       }
   }
   ```

### 四、匿名类型

匿名类型，就是没有指明类型的类型。

通过**隐式类型**和**对象初始化器**两种特性创建了一个类型未知的对象，可以在不定义类型的情况下实现对象的创建。

(**见示例:"Capital13_04_AnonymousType"**)

```
/// <summary>
/// 匿名类型 = 隐式类型 + 对象初始化器
/// </summary>
class Capital13_04_AnonymousType
{
    static void Main(string[] args)
    {
        // 定义匿名类型对象
        var person = new { Name = "Zhangsan", Age = 25 };
        Console.WriteLine($"{person.Name}的年龄为{person.Age}岁。");

        // 定义匿名类型数组
        var personCollection = new[]
        {
            new{Name="zhangsan",Age=30},
            new{Name="lisi",Age=22},
            new{Name="wangwu",Age=32},

            //new{Name="Jerry"}
        };

        int totalAge = 0;
        foreach(var p in personCollection)
        {
            totalAge += p.Age;
        }

        Console.WriteLine($"年龄和为{totalAge}。");
        Console.ReadKey();
    }
}
```













