typora-copy-images-to: mdPics

# ��Learnig Hard C# ѧϰ�ʼǡ�C#�Ĵ��졪��C# 3.0 �е����ܱ����� ��13��

C# 3.0 �߸������ǵĴ����д��񣬳�����Lambda���ʽ��Linq�������ԡ�

### һ���Զ�����

```
// ����ɶ�д����
public string Name { get; set; }

// ����ֻ������
public int Age { get; private set; }
```

���������Զ�Ϊ���Ǵ���˽���ֶΡ�

ע�⣺�ڽṹ����ʹ���Զ�����ʱ�����еĹ��캯������Ҫ��ʽ�ĵ����޲ι��캯��this���������ֱ������ԭ���ǣ�����������������֪�����е��ֶζ��Ѿ�����ֵ�ˡ�Ȼ������ȴ����Ҫ��ʽ�ĵ����޲ι��캯����

(**��ʾ��:"Capital13_01"**)

```
public struct PersonStruct
{
    public string Name { get; set; }
    public int Age { get; private set; }

    // �ṹ���У�����ʾ�ĵ����޲ι��캯��this()ʱ������ֱ������
    public PersonStruct(string name)
        :this() // ���û�а����е����Ը�ֵ����Ҫ��ʾ�����޲ι��캯��
    {
        this.Name = name;
        //Age = 5;
    }
}
```

### ������ʽ����(Implicit)

1. var�ؼ��֡��磺

   ```
   class Program
   {
       static void Main(string[] args)
       {
           var stringvariable = "Hello World!";
           stringvariable = 2;	// <---����ᱨ������ʱ������Ϊ�Ѿ��ƶ�varΪstring����
       }
   }
   ```

   - �������ı���������һ���ֶΣ�������̬�ֶκ�ʵ���ֶΣ�
   - ��������ʱ�����뱻��ʼ������Ϊ������Ҫ�ƶ����͡�
   - �������ܳ�ʼ��Ϊһ�������飬Ҳ���ܳ�ʼ��Ϊһ������������
   - �������ܳ�ʼ��Ϊnull
   - �ȵȡ�����
   - ��ʽ���͵���ȱ�㣺(**��ʾ��:"Capital13_02_ImplicitType"**)

   ```
   class Capital13_02_ImplicitType
   {
   	static void Main(string[] args)
   	{
   		// ��ʽ���� Implicit
   		// �ŵ�: ʹ����ʽ���ͣ��Ͳ����ڵ�ʽ���߶�дһ��Dictionary<string, string>
   		var dictionary = new Dictionary<string, string>();
     
   		// ȱ��: ʹ����������⡣
   		var a = 2147483649;
   		var b = 928888888888;
   		var c = 2147483644;
   		Console.WriteLine($"����a������Ϊ��{a.GetType()}");
   		Console.WriteLine($"����b������Ϊ��{b.GetType()}");
   		Console.WriteLine($"����c������Ϊ��{c.GetType()}");
     
   		Console.Read();
   	}
   }
   ```

2. ��ʽ��������

### �������󼯺ϳ�ʼ����

C# 3.0 �ṩ��**��ʼ����**������Ϊ��ͬ����µĳ�ʼ����ʹ��Ĭ�Ϲ��캯�����ɡ�

1. �����ʼ����

   (**��ʾ��:"Capital13_03_Initializer"**)

   ```
   class Capital13_03_Initializer
   {
       static void Main(string[] args)
       {
           // ��ʼ������ֻʹ��Ĭ�Ϲ��캯������,����ҪΪ��ͬ���д������캯��
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

   ע�⣺

   **����Զ����˹��캯��������������޲ι��캯���򲻴�����**��**�ṹ���޲ι��캯��һֱ���ڣ��ṹ��Ҳ�����Լ������޲ι��캯�����ᱨ��**����Ҫʹ�ó�ʼ������Ҳ**Ҫ��ʾ��**��**�����޲ι��캯��**�����£�

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
       
       // ע�����´��룬�޲ι��캯��������ֱ���ʱ����
       public Person(){}    
   }
   ```

2. ���ϳ�ʼ����

   ���磺

   ```
   class Program
   {
       static void Main(string[] args)
       {
       	// ʹ�ü��ϳ�ʼ����
           List<string> newnames= new List<string>
           {
               "Zhangsan"
               , "Lisi"
               , "Wangwu"
           };
       }
   }
   ```

### �ġ���������

�������ͣ�����û��ָ�����͵����͡�

ͨ��**��ʽ����**��**�����ʼ����**�������Դ�����һ������δ֪�Ķ��󣬿����ڲ��������͵������ʵ�ֶ���Ĵ�����

(**��ʾ��:"Capital13_04_AnonymousType"**)

```
/// <summary>
/// �������� = ��ʽ���� + �����ʼ����
/// </summary>
class Capital13_04_AnonymousType
{
    static void Main(string[] args)
    {
        // �����������Ͷ���
        var person = new { Name = "Zhangsan", Age = 25 };
        Console.WriteLine($"{person.Name}������Ϊ{person.Age}�ꡣ");

        // ����������������
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

        Console.WriteLine($"�����Ϊ{totalAge}��");
        Console.ReadKey();
    }
}
```













