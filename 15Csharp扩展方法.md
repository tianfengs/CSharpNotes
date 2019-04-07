typora-copy-images-to: mdPics

# 《Learnig Hard C# 学习笔记》扩展方法 第15章

扩展方法使您能够**向现有类型“添加”方法**，**而无需创建新的派生类型**、**重新编译或以其他方式修改原始类型**。扩展方法是一种特殊的静态方法，但可以像扩展类型上的实例方法一样进行调用。

可以参考msdn上：<https://docs.microsoft.com/zh-cn/previous-versions/visualstudio/visual-studio-2008/bb383977(v=vs.90)>

C# 3.0 的特性之一。

### 一、扩展方法的使用

1. 定义扩展方法

   （**见示例：Captal5_ExtMethod_01**）

   ```
   /// <summary>
   /// 扩展方法必须在“非泛型”“静态类”中定义
   /// </summary>
   public static class ListExten
   {
       // 定义扩展方法
       /// <summary>
       /// 计算数组中下标为奇数的数组成员之和。
       /// </summary>
       /// <param name="source"></param>
       /// <returns></returns>
       public static int JSum(this IEnumerable<int> source)
       {
           if (source == null)
           {
               throw new ArgumentException("输入数组为空");
           }
   
           int jsum = 0;
   
           bool flag = false;
           foreach(int current in source)
           {
               if (!flag)
               {
                   jsum += current;
                   flag = true;
               }
               else
               {
                   flag = false;
               }
           }
   
           return jsum;
       }
   }
   ```

   扩展方法的定义规则：

   - 扩展方法必须在一个非嵌套、非泛型的静态类中定义
   - 它至少有一个参数
   - 第一个参数必须加上this关键字作为前缀（第一个类型也称为扩展类型，即指方法对这个类型进行扩展）
   - 第一个参数不能使用任何其它的修饰符（如不能使用ref、out等修饰符）
   - 第一个参数的类型不能是指针类型

2. 调用扩展方法

   还是（**见示例：Captal5_ExtMethod_01**）

   ```
   class Captal5_ExtMethod_01
   {
       static void Main(string[] args)
       {
           List<int> source = new List<int>() { 1, 2, 3, 4, 5, 6, 3 };
   
           int jSum = ListExten.JSum(source);
           Console.WriteLine($"数组的奇数和为:{jSum}");
   
           Console.ReadKey();
       }
   }
   ```

   另外一种调用方式：*****

   ```
   class Captal5_ExtMethod_01
   {
       static void Main(string[] args)
       {
           List<int> source = new List<int>() { 1, 2, 3, 4, 5, 6, 3 };
   
           int jSum = source.JSum();   // 扩展方法 调用
           Console.WriteLine($"数组的奇数和为:{jSum}");
   
           Console.ReadKey();
       }
   }
   ```

### 二、编译器如何发现扩展方法

编译器会检查导入的命名空间和当前命名空间的扩展方法，并将变量类型匹配到扩展类型。

例如，前面代码重`List<T>`到我们的扩展类型IEnumerable<int>就存在一个隐式转换，因为List<T>实现了IEnumerable<T>接口。因为C#中，从子类到父类的转换可以通过隐式转换来完成（协变）。

扩展方法前面都有一个**向下的箭头**标识![1554531470722](mdPics\1554531470722.png):

![1554531316009](mdPics\1554531316009.png) 

方法调用的优先级为：类的实例方法——>当前命名空间的扩展方法——>导入命名空间的扩展方法。

注意：如果同一个命名空间下的两个类含有相同的扩展方法，则会出现编译错误。

（**见示例：Captal5_ExtMethod_02**）

```
class Captal5_ExtMethod_02
{
    static void Main(string[] args)
    {
        Person person = new Person() { Name = "zhangsan" };

        person.Print();

        Console.Read();
    }
}

// 自定义类型
public class Person
{
    public string Name { get; set; }
}

public static class ExtensionClass1
{
    public static void Print(this Person per)
    {
        Console.WriteLine($"调用当前命名空间下ExtensionClass1扩展方法输出姓名为：{per.Name}");
    }
}

public static class ExtensionClass2
{
    public static void Print(this Person per)
    {
        Console.WriteLine($"调用当前命名空间下ExtensionClass2扩展方法输出姓名为：{per.Name}");
    }
}
```

![1554532131672](mdPics\1554532131672.png) 

### 三、空引用（null）调用扩展方法

在C#中，null（空引用）调用实例方法，会引发NullReferenceException异常，但空引用却可以调用扩展方法。

因此：

当我们为一个类型定义扩展方法是，赢尽量扩展具体类型，而不要扩展器基类，避免对其它子类产生“**污染**”。

















