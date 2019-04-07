typora-copy-images-to: mdPics

# 《Learnig Hard C# 学习笔记》LINQ解析 第16章

可参考msdn《语言集成查询 (LINQ)》：

<https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/linq/index> 

<https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/linq/> 

### 一、理解LINQ

语言集成查询 (LINQ) 是 **.NET Framework 3.5** 版中引入的一项创新功能，它在对象领域和数据领域之间架起了一座桥梁。语言集成查询 (LINQ)提供一种跨越各种数据源的统一的查询方式，主要包含四个组件：

- SQL Server 数据库：[LINQ to SQL](https://docs.microsoft.com/zh-cn/dotnet/framework/data/adonet/sql/linq/index) 。

  可以查询关系数据库的数据，微软只实现了对SQL Server的支持。第三方实现的开源库，例如 Linq to Oracle的免费开源工具DbLinq2007（可参考：http://www.cnblogs.com/hehuachina/articles/1194040.html。）

- XML 文档：[LINQ to XML (C#)](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/linq/linq-to-xml) 

  用来替换使用XPath来对XML进行查询。

- ADO.NET 数据集：[LINQ to DataSet](https://docs.microsoft.com/zh-cn/dotnet/framework/data/adonet/linq-to-dataset) 

  可查询DataSet对象中的数据，并可以增删改查。

- .NET 集合、文件、字符串等：[LINQ to Objects (C#)](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/linq/linq-to-objects) 

  可以查询集合数据，如数组或List等。

### 二、LINQ的优点

1. 查询表达式

   以`from字句`开图，并且以`select或group字句`结尾。中间可以嵌套多个where字句、orderby、join字句。例：

   ```
   var queryExp = from s in collection
   			   select s;
   ```

   另外一种方式，“点标记方式”，例：

   ```
   var queryExp = collection.Select(s=>s);
   ```

2. 使用Linq to Object查询集合

   代替for或foreach语句的查询方式。

   (**见示例"Captal16_Linq_01"**) 

   ```
   public static void ForeachQuery(List<int> vs)
   {
       List<int> queryResult = new List<int>();
       foreach(int item in vs)
       {
           if (item % 2 == 0)
           {
               queryResult.Add(item);
           }
       }
   
       foreach(int item in queryResult)
       {
           Console.Write(item + " ");
       }
   
       Console.WriteLine();
   }
   
   public static void LinqQuery(List<int> vs)
   {
       var queryResult = from item in vs
                           where item % 2 == 0
                           select item;
   
       queryResult = vs.Where(item => item % 2 == 0);
   
       foreach(int item in queryResult)
       {
           Console.Write(item + " ");
       }
   
   }
   ```

3. 使用linq to XML查询XML文件

   .NET 3.5以前，可以使用XPath来查询XML文件，现在使用Linq，优点：

   - 但使用XPath必须首先知道XML具体结构，使用Linq则不需要知道XML的结构
   - 使用Linq to XML的代码更简洁

   (**见示例"Captal16_Linq_02_XML"**) 

   nameInfo.xml文件代码为

   ```
   <?xml version="1.0" encoding="utf-8" ?>
   <Persons>
     <Person Id="1">
       <Name>张三</Name>
       <Age>18</Age>
     </Person>
     <Person Id="2">
       <Name>李四</Name>
       <Age>19</Age>
     </Person>
     <Person Id="3">
       <Name>王五</Name>
       <Age>22</Age>
     </Person>
   </Persons>
   ```

   用旧的XPath方式查询

   ```
   private static void XPathQuery()
   {
       // 导入XML文件
       XmlDocument xmlDoc = new XmlDocument();
       xmlDoc.Load("nameInfo.xml");
   
       // 创建查询XML文件的XPath
       string xPath = "/Persons/Person";
   
       // 查询Person节点元素和属性
       XmlNodeList querynodes = xmlDoc.SelectNodes(xPath);
       foreach(XmlNode node in querynodes)
       {
           // 查询名字为“李四”的元素
           foreach(XmlNode childnode in node.ChildNodes)
           {
               if (childnode.InnerXml == "李四")
               {
                   // 输出查询结果
                   Console.WriteLine($"姓名为{childnode.InnerXml}" +
                       $", Id为{node.Attributes["Id"].Value}" +
                       $", 年龄为{node.SelectSingleNode("Age").InnerText}");
               }
           }
       }
   }
   ```

   用Linq方式查询

   ```
   private static void LinqQuery()
   {
       // 导入XML文件
       XElement xDoc = XElement.Load("nameInfo.xml");
   
       // 创建查询，获取姓名为“李四”的元素
       var queryResult = from element in xDoc.Elements("Person")
                           where element.Element("Name").Value == "李四"
                           select element;
   
       // 输出查询结果
       foreach (var xele in queryResult)
       {
           Console.WriteLine($"姓名为{xele.Element("Name").Value}" +
               $", Id为{xele.Attribute("Id").Value}" +
               $", 年龄为{xele.Element("Age").Value}");
       }
   }
   ```

4. Linq toSQL

   可以参考MSDN：http://msdn.microsoft.com/zh-cn/library/bb386976.aspx;

   也可以参考：http://www.cnblogs.com/lyj/archive/2008/03/25/1119671.html.

### 三、LINQ的本质

从IL代码可知，LINQ查询表达式是建立在Lambda表达式和扩展方法的基础上，而Lambda表达式又是建立在委托的基础上，扩展方法也属于方法。

所以，LINQ的本质还是对方法的调用。

```
// 导入XML文件
XElement xDoc = XElement.Load("nameInfo.xml");

// 创建查询，获取姓名为“李四”的元素
var queryResult = from element in xDoc.Elements("Person")
                        where element.Element("Name").Value == "李四"
                        select element;
```

使用**Reflector**软件进行反编译之后

```
IEnumerable<XElement> enumerable = XElement.Load("nameInfo.xml").Elements("Person")
		.Where<XElement> 
(<>c.<>9__2_0 ??(<>c.<>9__2_0 = new Func<XElement, bool>(<>c.<>9.<LinqQuery>b__2_0)));

```

`??`为空合并操作符（见第12章）。

<>9__2_0为编译器生成的委托类型，具体类型为Func<XElement, bool>:

```
public static Func<XElement, bool> <>9__2_0;
```

还生成了一个静态方法 <LinqQuery>b__2_0(XElement)

```
internal bool <LinqQuery>b__2_0(XElement element)
{
    return (element.Element("Name").Value == "李四");
}
```

**Lambda表达式的本质是对IEnumerable<XELement>类型的Where扩展方法的调用**，其参数为一个匿名委托类。



