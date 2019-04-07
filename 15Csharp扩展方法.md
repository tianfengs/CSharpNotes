typora-copy-images-to: mdPics

# ��Learnig Hard C# ѧϰ�ʼǡ���չ���� ��15��

��չ����ʹ���ܹ�**���������͡���ӡ�����**��**�����贴���µ���������**��**���±������������ʽ�޸�ԭʼ����**����չ������һ������ľ�̬����������������չ�����ϵ�ʵ������һ�����е��á�

���Բο�msdn�ϣ�<https://docs.microsoft.com/zh-cn/previous-versions/visualstudio/visual-studio-2008/bb383977(v=vs.90)>

C# 3.0 ������֮һ��

### һ����չ������ʹ��

1. ������չ����

   ��**��ʾ����Captal5_ExtMethod_01**��

   ```
   /// <summary>
   /// ��չ���������ڡ��Ƿ��͡�����̬�ࡱ�ж���
   /// </summary>
   public static class ListExten
   {
       // ������չ����
       /// <summary>
       /// �����������±�Ϊ�����������Ա֮�͡�
       /// </summary>
       /// <param name="source"></param>
       /// <returns></returns>
       public static int JSum(this IEnumerable<int> source)
       {
           if (source == null)
           {
               throw new ArgumentException("��������Ϊ��");
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

   ��չ�����Ķ������

   - ��չ����������һ����Ƕ�ס��Ƿ��͵ľ�̬���ж���
   - ��������һ������
   - ��һ�������������this�ؼ�����Ϊǰ׺����һ������Ҳ��Ϊ��չ���ͣ���ָ������������ͽ�����չ��
   - ��һ����������ʹ���κ����������η����粻��ʹ��ref��out�����η���
   - ��һ�����������Ͳ�����ָ������

2. ������չ����

   ���ǣ�**��ʾ����Captal5_ExtMethod_01**��

   ```
   class Captal5_ExtMethod_01
   {
       static void Main(string[] args)
       {
           List<int> source = new List<int>() { 1, 2, 3, 4, 5, 6, 3 };
   
           int jSum = ListExten.JSum(source);
           Console.WriteLine($"�����������Ϊ:{jSum}");
   
           Console.ReadKey();
       }
   }
   ```

   ����һ�ֵ��÷�ʽ��*****

   ```
   class Captal5_ExtMethod_01
   {
       static void Main(string[] args)
       {
           List<int> source = new List<int>() { 1, 2, 3, 4, 5, 6, 3 };
   
           int jSum = source.JSum();   // ��չ���� ����
           Console.WriteLine($"�����������Ϊ:{jSum}");
   
           Console.ReadKey();
       }
   }
   ```

### ������������η�����չ����

���������鵼��������ռ�͵�ǰ�����ռ����չ������������������ƥ�䵽��չ���͡�

���磬ǰ�������`List<T>`�����ǵ���չ����IEnumerable<int>�ʹ���һ����ʽת������ΪList<T>ʵ����IEnumerable<T>�ӿڡ���ΪC#�У������ൽ�����ת������ͨ����ʽת������ɣ�Э�䣩��

��չ����ǰ�涼��һ��**���µļ�ͷ**��ʶ![1554531470722](mdPics\1554531470722.png):

![1554531316009](mdPics\1554531316009.png) 

�������õ����ȼ�Ϊ�����ʵ����������>��ǰ�����ռ����չ��������>���������ռ����չ������

ע�⣺���ͬһ�������ռ��µ������ຬ����ͬ����չ�����������ֱ������

��**��ʾ����Captal5_ExtMethod_02**��

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

// �Զ�������
public class Person
{
    public string Name { get; set; }
}

public static class ExtensionClass1
{
    public static void Print(this Person per)
    {
        Console.WriteLine($"���õ�ǰ�����ռ���ExtensionClass1��չ�����������Ϊ��{per.Name}");
    }
}

public static class ExtensionClass2
{
    public static void Print(this Person per)
    {
        Console.WriteLine($"���õ�ǰ�����ռ���ExtensionClass2��չ�����������Ϊ��{per.Name}");
    }
}
```

![1554532131672](mdPics\1554532131672.png) 

### ���������ã�null��������չ����

��C#�У�null�������ã�����ʵ��������������NullReferenceException�쳣����������ȴ���Ե�����չ������

��ˣ�

������Ϊһ�����Ͷ�����չ�����ǣ�Ӯ������չ�������ͣ�����Ҫ��չ�����࣬������������������**��Ⱦ**����

















