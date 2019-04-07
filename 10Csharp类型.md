---
typora-copy-images-to: mdPics
---

# 《Learnig Hard C# 学习笔记》类型 第10章

本章详细分析C#中的值类型和引用类型，以及他们之间的转换方法。

### 一、值类型和引用类型

1. 定义

   - 值类型主要包括**简单类型**、**枚举类型**和**结构体类型**。

     值类型的实例，被分配在线程的**堆栈**上，变量的内容就是实例数据本身

   - 引用类型包括**类类型**、**接口类型**、**委托类型**和**字符串类型**

     引用类型的实例被分配在**托管堆**上，变量保存的是实例数据的内存地址

   > 堆栈和托管堆
   >
   > 首先堆栈和堆（托管堆）都在进程的虚拟内存中。
   >
   > **堆栈stack**：
   >
   >   堆栈中存储值类型。
   >
   >   堆栈实际上是向下填充，即由高内存地址指向低内存地址填充。堆栈中的变量是从下向上释放，这样就 保证了堆栈中先进后出的规则不与变量的生命周期起冲突！
   >
   >   堆栈的工作方式是先分配内存的变量后释放（先进后出原则）。
   >
   > **堆（托管堆）heap**
   >
   > 堆（托管堆）存储引用类型。
   >
   > 此堆非彼堆，.NET中的堆由垃圾收集器自动管理。
   >
   > 与堆栈不同，堆是从下往上分配，所以自由的空间都在已用空间的上面。
   >
   > 比如：创建一个对象：
   >
   > Customer cus;
   >
   > cus = new Customer();
   >
   > 申明一个Customer的引用cus，在堆栈上给这个引用分配存储空间。这仅仅只是一个引用，不是实际的Customer对象！cus占4个字节的空间，包含了存储Customer的引用地址。
   >
   > 接着分配堆上的内存以存储Customer对象的实例，假定Customer对象的实例是32字节，为了在堆上找到一个存储Customer对象的存储位置。.NET运行库在堆中搜索第一个从未使用的，32字节的连续块存储Customer对象的实例！然后把分配给Customer对象实例的地址赋给cus变量！
   >
   > 从这个例子中可以看出，建立对象引用的过程比建立值变量的过程复杂，且不能避免性能的降低！
   >
   > 实际上就是.NET运行库保存对的状态信息，在堆中添加新数据时，堆栈中的引用变量也要更新。性能上损失很多！
   >
   > 有种机制在分配变量内存的时候，不会受到堆栈的限制：把一个引用变量的值赋给一个相同类型的变量，那么这两个变量就引用同一个堆中的对象。
   >
   > 当一个应用变量出作用域时，它会从堆栈中删除。但引用对象的数据仍然保留在堆中，一直到程序结束 或者 该数据不被任何变量应用时，垃圾收集器会删除它。

2. 区别

   它们有不同的内存分布。

   值类型被分配到线程的堆栈上，管理由操作系统负责。

   引用类型被分配到托管堆上，管理由垃圾回收器（Garbage Collection，GC）负责。

   管理涉及对内存空间进行分配和释放。

   （见示例："Capital10_Type_01"） 

   ```
   class Capital10_Type_01
   {
       static void Main(string[] args)
       {
           // valuetype是值类型
           int valuetype = 3;
           // reftype是引用类型
           string reftype = "abd";
       }
   }
   ```

   ![1554026618637](mdPics\1554026618637.png) 

   - 不管是值类型变量还是引用类型变量，**变量**都存储在线程的堆栈上

   - 引用类型只有“**变量的实例数据**”存储在托管堆上。

   - **变量**只是**实例数据**的一个**引用**，或一个**地址** 

   - 值类型实例“通常”会存储在堆栈中。但在在引用类型中嵌套值类型时，或在值类型装箱的的情况下，值类型的实例就会被分配到托管堆上。
     - 引用类型中嵌套定义值类型

       类的字段类型时值类型，将作为引用类型实例的一部分，被分配到托管堆中。

       但局部变量的值类型，仍然会被分配到线程堆栈中。

       ```
       public class NestedValueTypeInRef
       {
       	// valuetype作为引用类型的一部分被分配到托管堆上
           private int valuetype=3;
           
           public void method()
           {
           	// c被分配到线程堆栈上
           	char c='c';    
           }
       }
       class Program
       {
           static void Main(string[] args)
           {
               NestedRefTypeInValue reftype = new NestedRefTypeInValue();
           }
       }
       ```

       ![1554033822369](mdPics\1554033822369.png) 

     - 值类型中嵌套定义引用类型

       线程的堆栈上将保留该引用类型的引用，实际的数据则依然保存在托管堆中。

       ```
       public class TestClass
       {
           public int x;
           public int y;
       }
       
       // 值类型嵌套定义引用类型的情况
       public struct NestedRefTypeInValue
       {
       	// 结构体字段，注意，结构体重字段不能被初始化
           private TestClass classinValuetype;
           
           // 结构体中的构造函数，注意，结构体中不能定义无参的构造函数
           public NestedRefTypeInValue(TestClass t)
           {
               classinValuetype.x=3;
               classinValuetype.y=5;
               classinValuetype=t;
           }
       }
       class Program
       {
           static void Main(string[] args)
           {
           	// 值类型变量
               NestedRefTypeInValue valuetype = new NestedRefTypeInValue(new TestClass());
           }
       }
       ```

       ![1554034271095](mdPics\1554034271095.png) 

   - 值类型与引用类型的其它区别

     - 值类型继承在ValueType，ValueType又继承自System.Object；引用类型继承自System.Object
     - 值类型作用域结束时，值类型会被操作系统自行释放；引用类型的内存管理由GC完成。
     - 若值类型时密封的sealed，将不能作为其它类型的基类；引用类型一般具有继承性，类或接口
     - 值类型默认为0，引用类型默认为null
     - 值类型参数传递不会影响变量本身。引用类型变量保存的是引用地址，被传递时，参数改变时，会影响引用类型变量的值。

3. 值类型与引用类型之间的类型转换——**装箱**与**拆箱** 

   类型转换方式：

   - 隐式类型转换。由低级别向高级别转换的过程。如：派生类可以隐式的转换为它的父类，装箱。

   - 显式类型转换。强制转换类型。格式为：

     ```
     (type)(变量或函数)
     ```

   - 通过is和as运算符进行安全的类型转换。参考 http://msdn.microsoft.com/zh-cn/library/cscsdfbt(v=vs.110).aspx 和 http://msdn.microsoft.com/zh-cn/library/scekt9xw(v=vs.110).aspx （https://docs.microsoft.com/zh-cn/previous-versions/scekt9xw%28v%3dvs.110%29）。

   - 通过.NET类库中的Convert类来完成转换。参考http://msdn.microsoft.com/zh-cn/library/system.convert(v=vs.110).aspx

   举例说明

   ```
   class Program
   {
       static void Main(string[] args)
       {
       	int i=3;
           //装箱
           object o=i;
           //拆箱
           int y=(int)o;
       }
   }
   ```

   - 装箱步骤

     - 内存分配：在托管堆中分配好内存空间以存放复制的实际数据
     - 完成实际数据的复制：将值类型实例的实际数据复制到新分配的内存中
     - 地址返回：将托管堆中的对象地址返回给引用类型的变量

     ![1554038535247](mdPics\1554038535247.png) 

   - 拆箱步骤

     - 检查实例：检查要进行拆箱操作的引用类型变量是否为null，为null则抛出NullReferenceException异常，如不为null，则继续检查是否和拆箱后的类型相同，如不同，则InvalidCastException异常
     - 地址返回：返回已装箱变量的实际数据部分的地址
     - 数据复制：将托管堆中的实际数据复制到栈中

     ![1554038779150](mdPics\1554038779150.png) 

     **编写代码时，尽量避免装箱和拆箱操作，最好使用泛型编程，见第11章**。

	### 二、参数传递问题剖析

参数传递四种方式：

- 值类型参数——按值传递
- 引用类型参数——按值传递
- 值类型参数——按引用传递
- 引用类型参数——按引用传递

1. 值类型参数按值传递

   传递的是一个副本，所以方法中对参数的改变不影响实参的值。

2. 引用类型参数的按值传递

   当方法对地址进行操作时，实际上操作了地址指向的值，**所有调用方法后，原来实参的值也被修改**了。

   ![1554039347540](mdPics\1554039347540.png) 

   **string引用类型参数按值传递的特殊情况**：

   string比较特殊，虽然是按值传递，但实参却不会因方法中形参的改变而改变。因为string类型具有不变性，系统会重新为其分配内存。

3. 值类型和引用类型的按引用传递

   两个类型都可以使用**ref**和**out**关键字来实现参数按引用传递。

   方法的定义和调用都必须显示的用ref或out关键字，不可省略。

























