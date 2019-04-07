---
typora-copy-images-to: mdPics
---

# 《Learnig Hard C# 学习笔记》委托 第8章



### 一、委托是什么

委托是类类型。可以理解为函数的一个包装，它使得C#中的函数可以作为参数来被传递。

委托包装的方法有一定的限制条件：

- 方法签名必须与委托一致，方法签名包括参数的个数、类型和顺序
- 方法的返回值类型要和委托一致，虽然它不属于方法签名的一部分

### 二、委托的使用

**（见代码Captal8_Delegate_01)** 

```
class Captal8_Delegate_01
{
    // 1.声明委托
    delegate void MyDelegate(int para1, int para2);

    static void Main(string[] args)
    {
        // 3. 声明委托实例
        MyDelegate d;

        // 4. 实例化委托类型，传递或绑定方法（可以是实例方法 或者 静态方法）
        d = new MyDelegate(new Captal8_Delegate_01().Add);

        // 5. 委托类型作为参数传递给另一个方法，在另一个方法内调用（从另一个方法获得参数）
        Captal8_Delegate_01.MyMethod(d);

        Console.ReadKey();
    }

    /// <summary>
    /// 2. 创建跟委托相同签名和返回类型的 实例方法
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    void Add(int p1,int p2)
    {
        int sum = p1 + p2;
        Console.WriteLine($"{p1}+{p2}之和为：{sum}");
    }

    /// <summary>
    /// 5. 方法的参数是委托类型
    /// </summary>
    /// <param name="myDelegate"></param>
    private static void MyMethod(MyDelegate myDelegate)
    {
        // 在方法中调用委托
        myDelegate(1, 2);
    }
}
```

使用委托的步骤：

定义委托类型（声明**委托**） --->  创建**实例方法**  --->  声明**委托对象** --->  实例化委托对象，绑定实例方法  --->  调用委托对象

本质：就三个概念之间的关系：**委托**(类型)、**实例方法**、**委托对象**

1声明委托，2创建实例方法，3创建委托对象，4把实例方法绑定到委托对象，5调用委托对象来代理调用方法

本例的第5步，是创建一个静态方法来调用委托，委托对象作为参数，传进这个静态方法。

### 三、引入委托的原因

委托是C#的一个最好的特性，**它是后来很多的特性的基础**。

打招呼实例。每个国家打招呼的方式不同。**（见代码Captal8_Delegate_02Greeting)** 

比较一下用switch实现方法的**可扩展性很差**，如果添加其它国家打招呼的方式，就要修改switch语句。用委托来实现就比较好：

```
// 声明委托
public delegate void GreetingDelegate(string name);

class Captal8_Delegate_02Greeting
{
    static void Main(string[] args)
    {
        Captal8_Delegate_02Greeting greet = new Captal8_Delegate_02Greeting();

        // 不使用委托，实现打招呼的方法
        Console.WriteLine("不用委托方法，用switch方法实现：");
        greet.Greeting("Tom", "en-us");
        greet.Greeting("小张", "zh-cn");
        Console.WriteLine();

        // 使用委托的方法
        Console.WriteLine("用委托方法实现：");
        greet.GreetingDeleMeth("Tom", greet.EnglishGreeting);
        greet.GreetingDeleMeth("小王", greet.ChineseGreeting);

        Console.ReadKey();
    }

    ////////////////
    /// 不使用委托，实现打招呼的方法
    /// 
    public void Greeting(string name,string language)
    {
        switch (language)
        {
            case "zh-cn":
                ChineseGreeting(name);
                break;
            case "en-us":
                EnglishGreeting(name);
                break;
            default:
                EnglishGreeting(name);
                break;
        }
    }

    //////////////////
    /// 使用委托的方法
    /// 
    public void GreetingDeleMeth(string name, GreetingDelegate callback)
    {
        callback(name);
    }

    // 英国人打招呼
    private void EnglishGreeting(string name)
    {
        Console.WriteLine($"Hello, {name}");
    }

    // 中国人打招呼
    private void ChineseGreeting(string name)
    {
        Console.WriteLine($"你好, {name}");
    }
}
```

### 四、委托的本质

委托是**"类"**类型的。

C#源码：**（见代码Captal8_Delegate_03)** 

```
class Captal8_Delegate_03
{
    // 定义委托类型
    public delegate void DelegateTest(int parm);

    static void Main(string[] args)
    {
    }
}
```

IL中的显示：

![1553992077865](mdPics\1553992077865.png) 

编译器会把委托类型编译成如下类型：

```
public class DelegateTest: System.MulticastDelegate
{
    public DelegateTest(Object object, IntPtr method);
    public virtual void Invoke(int32 parm);
    public virtual IAsyncResult BeginInvoke(Int32 parm, AsyncCallback, Object object);
    public virtual void EndInvoke(IAsyncResult result);
}
```

因此委托直接包括异步调用方法**BeginInvoke()**和**EndInvoke()**，和可实现跨线程调用方法**Invoke()**，Invoke()方法可以显示的调用委托。

通过IL代码，可以看出，委托是**继承自System.MulticastDelegate的类**，还有**构造函数**，还有**实现异步和同步调用的方法**。

### 五、委托链

C#中委托可以封装多个方法，把封装多个方法的委托称为**委托链**或**多路广播委托**。

**（见代码Captal8_Delegate_04DelegateChain)**

1. 使用委托链：用"+"
2. 从委托链中移除委托：用"-"

```
class Captal8_Delegate_04DelegateChain
{
    // 1. 声明委托
    public delegate void DelegateTest();

    static void Main(string[] args)
    {
        // 创建委托对象1，绑定一个静态方法
        DelegateTest dtstatic = new DelegateTest(methodStatic);
        // 创建委托对象2，绑定一个实例方法
        DelegateTest dtinstance = new DelegateTest(new Captal8_Delegate_04DelegateChain().methodInstance);

        // 创建一个委托对象，并绑定委托对象1，使用"+"添加委托对象2，成为委托链
        DelegateTest delegateChain = dtstatic;
        delegateChain += dtinstance;

        // 调用委托对象
        delegateChain.Invoke();

        Console.WriteLine();
        Console.WriteLine("从委托链上移除静态方法后：");

        // 从委托链上移除静态方法
        delegateChain -= dtstatic;

        delegateChain.Invoke();

        Console.ReadKey();
    }

    // 静态方法
    private static void methodStatic()
    {
        Console.WriteLine("这是静态方法");
    }

    // 实例方法
    private void methodInstance()
    {
        Console.WriteLine("这是实例方法");
    }
}
```

结果显示：

![1553994942925](mdPics\1553994942925.png) 









