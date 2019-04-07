typora-copy-images-to: mdPics

# ��Learnig Hard C# ѧϰ�ʼǡ�LINQ���� ��16��

�ɲο�msdn�����Լ��ɲ�ѯ (LINQ)����

<https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/linq/index> 

<https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/linq/> 

### һ�����LINQ

���Լ��ɲ�ѯ (LINQ) �� **.NET Framework 3.5** ���������һ��¹��ܣ����ڶ����������������֮�������һ�����������Լ��ɲ�ѯ (LINQ)�ṩһ�ֿ�Խ��������Դ��ͳһ�Ĳ�ѯ��ʽ����Ҫ�����ĸ������

- SQL Server ���ݿ⣺[LINQ to SQL](https://docs.microsoft.com/zh-cn/dotnet/framework/data/adonet/sql/linq/index) ��

  ���Բ�ѯ��ϵ���ݿ�����ݣ�΢��ֻʵ���˶�SQL Server��֧�֡�������ʵ�ֵĿ�Դ�⣬���� Linq to Oracle����ѿ�Դ����DbLinq2007���ɲο���http://www.cnblogs.com/hehuachina/articles/1194040.html����

- XML �ĵ���[LINQ to XML (C#)](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/linq/linq-to-xml) 

  �����滻ʹ��XPath����XML���в�ѯ��

- ADO.NET ���ݼ���[LINQ to DataSet](https://docs.microsoft.com/zh-cn/dotnet/framework/data/adonet/linq-to-dataset) 

  �ɲ�ѯDataSet�����е����ݣ���������ɾ�Ĳ顣

- .NET ���ϡ��ļ����ַ����ȣ�[LINQ to Objects (C#)](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/linq/linq-to-objects) 

  ���Բ�ѯ�������ݣ��������List�ȡ�

### ����LINQ���ŵ�

1. ��ѯ���ʽ

   ��`from�־�`��ͼ��������`select��group�־�`��β���м����Ƕ�׶��where�־䡢orderby��join�־䡣����

   ```
   var queryExp = from s in collection
   			   select s;
   ```

   ����һ�ַ�ʽ�������Ƿ�ʽ��������

   ```
   var queryExp = collection.Select(s=>s);
   ```

2. ʹ��Linq to Object��ѯ����

   ����for��foreach���Ĳ�ѯ��ʽ��

   (**��ʾ��"Captal16_Linq_01"**) 

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

3. ʹ��linq to XML��ѯXML�ļ�

   .NET 3.5��ǰ������ʹ��XPath����ѯXML�ļ�������ʹ��Linq���ŵ㣺

   - ��ʹ��XPath��������֪��XML����ṹ��ʹ��Linq����Ҫ֪��XML�Ľṹ
   - ʹ��Linq to XML�Ĵ�������

   (**��ʾ��"Captal16_Linq_02_XML"**) 

   nameInfo.xml�ļ�����Ϊ

   ```
   <?xml version="1.0" encoding="utf-8" ?>
   <Persons>
     <Person Id="1">
       <Name>����</Name>
       <Age>18</Age>
     </Person>
     <Person Id="2">
       <Name>����</Name>
       <Age>19</Age>
     </Person>
     <Person Id="3">
       <Name>����</Name>
       <Age>22</Age>
     </Person>
   </Persons>
   ```

   �þɵ�XPath��ʽ��ѯ

   ```
   private static void XPathQuery()
   {
       // ����XML�ļ�
       XmlDocument xmlDoc = new XmlDocument();
       xmlDoc.Load("nameInfo.xml");
   
       // ������ѯXML�ļ���XPath
       string xPath = "/Persons/Person";
   
       // ��ѯPerson�ڵ�Ԫ�غ�����
       XmlNodeList querynodes = xmlDoc.SelectNodes(xPath);
       foreach(XmlNode node in querynodes)
       {
           // ��ѯ����Ϊ�����ġ���Ԫ��
           foreach(XmlNode childnode in node.ChildNodes)
           {
               if (childnode.InnerXml == "����")
               {
                   // �����ѯ���
                   Console.WriteLine($"����Ϊ{childnode.InnerXml}" +
                       $", IdΪ{node.Attributes["Id"].Value}" +
                       $", ����Ϊ{node.SelectSingleNode("Age").InnerText}");
               }
           }
       }
   }
   ```

   ��Linq��ʽ��ѯ

   ```
   private static void LinqQuery()
   {
       // ����XML�ļ�
       XElement xDoc = XElement.Load("nameInfo.xml");
   
       // ������ѯ����ȡ����Ϊ�����ġ���Ԫ��
       var queryResult = from element in xDoc.Elements("Person")
                           where element.Element("Name").Value == "����"
                           select element;
   
       // �����ѯ���
       foreach (var xele in queryResult)
       {
           Console.WriteLine($"����Ϊ{xele.Element("Name").Value}" +
               $", IdΪ{xele.Attribute("Id").Value}" +
               $", ����Ϊ{xele.Element("Age").Value}");
       }
   }
   ```

4. Linq toSQL

   ���Բο�MSDN��http://msdn.microsoft.com/zh-cn/library/bb386976.aspx;

   Ҳ���Բο���http://www.cnblogs.com/lyj/archive/2008/03/25/1119671.html.

### ����LINQ�ı���

��IL�����֪��LINQ��ѯ���ʽ�ǽ�����Lambda���ʽ����չ�����Ļ����ϣ���Lambda���ʽ���ǽ�����ί�еĻ����ϣ���չ����Ҳ���ڷ�����

���ԣ�LINQ�ı��ʻ��ǶԷ����ĵ��á�

```
// ����XML�ļ�
XElement xDoc = XElement.Load("nameInfo.xml");

// ������ѯ����ȡ����Ϊ�����ġ���Ԫ��
var queryResult = from element in xDoc.Elements("Person")
                        where element.Element("Name").Value == "����"
                        select element;
```

ʹ��**Reflector**������з�����֮��

```
IEnumerable<XElement> enumerable = XElement.Load("nameInfo.xml").Elements("Person")
		.Where<XElement> 
(<>c.<>9__2_0 ??(<>c.<>9__2_0 = new Func<XElement, bool>(<>c.<>9.<LinqQuery>b__2_0)));

```

`??`Ϊ�պϲ�������������12�£���

<>9__2_0Ϊ���������ɵ�ί�����ͣ���������ΪFunc<XElement, bool>:

```
public static Func<XElement, bool> <>9__2_0;
```

��������һ����̬���� <LinqQuery>b__2_0(XElement)

```
internal bool <LinqQuery>b__2_0(XElement element)
{
    return (element.Element("Name").Value == "����");
}
```

**Lambda���ʽ�ı����Ƕ�IEnumerable<XELement>���͵�Where��չ�����ĵ���**�������Ϊһ������ί���ࡣ



