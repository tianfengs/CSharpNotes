---
typora-copy-images-to: mdPics
---

# ��Learnig Hard C# ѧϰ�ʼǡ����� ��11��

C# 1.0�е�**ί��**����**ʹ��������Ϊ**����������**����������**����C# 2.0�������**����**��������**���Ϳ��Ա�������**���Ӷ�������Ϊ��ͬ�������ṩ����汾�ķ���ʵ�֡�

�������ṩ�Ĵ����������㷨�����ã���ĳ������ʵ�ֲ���Ҫ���������������ݵ����͡�

### һ�����͵����

���ͣ�genericͨ�õģ����ʹ���ľ��ǡ�ͨ�����͡����ɴ���������������ͣ�ʹ**���Ͳ�����**��

�Ӷ�����ֻʵ��һ�������Ϳ��Բ��������������͵�Ŀ�ġ�

���ͽ� **������ʵ��** �� **������������������** ���룬ʵ��**��������**��

### ����C# 2.0���뷺�͵Ķ���

1. ʵ���������͡������ַ����ıȽ�

   ���淽����**��ʾ��Capital11_Generic_01**) 

   ```
   // ��ͨ�ıȽ���ͷ���
   public class Compare
   {
       public static int CompareInt(int int1,int int2)
       {
           return int1.CompareTo(int2) > 0 ? int1 : int2;
       }
   
       public static string CompareStr(string str1,string str2)
       {
           return str1.CompareTo(str2) > 0 ? str1 : str2;
       }
   }
   ```

   ���ͷ���

   ```
   public class Compare<T> where T : IComparable
   {
       public static T CompareGeneric(T t1, T t2)
       {
           return t1.CompareTo(t2) > 0 ? t1 : t2;
       }
   }
   ```

   ʹ�÷��ͷ���

   ```
   static void Main(string[] args)
   {
       var res1 = Compare<int>.CompareGeneric(1, 2);
       var res2 = Compare<string>.CompareGeneric("aaa", "ddd");
   
       Console.WriteLine(res1);
       Console.WriteLine(res2);
   
       Console.ReadKey();
   }
   ```

2. �ȽϷ��ͷ�������ͨ������װ��������**�����ϵ�����**��**��ʾ��Capital11_Generic_02**��

   ��������-ֵ���ͼ����ת����ת����װ��������������������ʧ�������Ǳ�����ʧ����Ч������

   ```
   using System;
   using System.Collections.Generic;
   using System.Collections;
   using System.Diagnostics;
   
   class Capital11_Generic_02
   {
       static void Main(string[] args)
       {
           // ���Է������͵�����ʱ��
           testGeneric();
           // ���ԷǷ������͵�����ʱ��
           testNonGeneric();
   
           Console.ReadKey();
       }
   
       /// <summary>
       /// ���Է������Ͳ���������ʱ��
       /// </summary>
       private static void testGeneric()
       {
           Stopwatch stopwatch = new Stopwatch();
   
           List<int> genericList = new List<int>();
   
           stopwatch.Start();
   
           for(int i = 0; i < 10000000; i++)
           {
               genericList.Add(i);
           }
   
           stopwatch.Stop();
   
           //���ʱ��
           TimeSpan timeSpan = stopwatch.Elapsed;
           // 00:00:00�������
           string elapsedTime = String.Format($"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}:{timeSpan.Milliseconds / 10:00}");
           Console.WriteLine($"���͵�����ʱ�䣺{elapsedTime}");
       }
   
       /// <summary>
       /// ���ԷǷ������Ͳ���������ʱ��
       /// </summary>
       private static void testNonGeneric()
       {
           Stopwatch stopwatch = new Stopwatch();
   
           ArrayList arrayList = new ArrayList();
   
           stopwatch.Start();
   
           for(int i = 0; i < 10000000; i++)
           {
               arrayList.Add(i);
           }
   
           stopwatch.Stop();
   
           //���ʱ��
           TimeSpan timeSpan = stopwatch.Elapsed;
           // 00:00:00�������
           string elapsedTime = String.Format($"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}:{timeSpan.Milliseconds / 10:00}");
           Console.WriteLine($"�Ƿ��͵�����ʱ�䣺{elapsedTime}");
   
       }
   }
   ```

   ���Ϊ��

   ![1554042478987](mdPics\1554042478987.png) 

### ����ȫ���������

1. ���Ͳ��� T

   TΪ����ռλ������Ϊ���ࣺ��**��ʾ��Capital11_Generic_03**��

   - δ�󶨵ķ��ͣ�û��Ϊ���Ͳ����ṩʵ�����͡��������͡�
   - �ѹ���ķ��ͣ���ָ����ʵ��������Ϊ����
     - �������ͣ��������Ͳ����ķ���
     - �ܷ����ͣ��Ѿ�Ϊÿһ�����Ͳ�����������ʵ���������͵ķ��͡�

   ```
   public class DictionaryStringKey<T> : Dictionary<string, T>
   {
   }
   
   class Capital11_Generic_03
   {
       static void Main(string[] args)
       {
           // Dictionary<,>��һ�����������͡��������������Ͳ���
           Type t = typeof(Dictionary<,>);
           Console.WriteLine("Dictionary <,>�Ƿ�Ϊ�������ͣ�" + t.ContainsGenericParameters);
   
           //
           t = typeof(DictionaryStringKey<>);
           Console.WriteLine("Dictionary <,>�Ƿ�Ϊ�������ͣ�" + t.ContainsGenericParameters);
   
           //
           t = typeof(DictionaryStringKey<int>);
           Console.WriteLine("Dictionary <,>�Ƿ�Ϊ�������ͣ�" + t.ContainsGenericParameters);
   
           //
           t = typeof(Dictionary<int, int>);
           Console.WriteLine("Dictionary <,>�Ƿ�Ϊ�������ͣ�" + t.ContainsGenericParameters);
   
           Console.ReadKey();
       }
   }
   ```

   ����ǣ�

   ![1554125777745](mdPics\1554125777745.png) 

   - Type.ContainsGenericParameters �ж����Ͷ����Ƿ����Υ���ǻ�������������Ͳ���
   - typeof()�ο���http://msdn.microsoft.com/zh-cn/library/58918ffs(v=vs.110).aspx

2. �����еľ�̬�ֶκ;�̬��������

   - ��̬�������������͵ġ�һ����ֻ��һ���þ�̬�ֶε�ֵ��
   - ÿ����յķ������ͣ�����һ���������ľ�̬���ݣ�**��ʾ��Capital11_Generic_04**��

   ```
   // �������ͣ�����һ�����Ͳ���
   public static class TypeWithStaticField<T>
   {
       // static field
       public static string field;
       // static ctor
       public static void OutField()
       {
           Console.WriteLine(field + ":" + typeof(T).Name);
       }
   }
   
   // �Ƿ�����
   public static class NoGenericTypeWithStaticField
   {
       public static string field;
       public static void OutField()
       {
           Console.WriteLine(field);
       }
   }
   
   static void Main(string[] args)
   {
       // ʵ��������ʵ��
       TypeWithStaticField<int>.field = "һ";
       TypeWithStaticField<string>.field = "��";
       TypeWithStaticField<Guid>.field = "��";
   
       // ʵ�����Ƿ���ʵ��
       NoGenericTypeWithStaticField.field = "�Ƿ��;�̬�ֶ�һ";
       NoGenericTypeWithStaticField.field = "�Ƿ��;�̬�ֶζ�";
       NoGenericTypeWithStaticField.field = "�Ƿ��;�̬�ֶ���";
   
       //���
       NoGenericTypeWithStaticField.OutField();
   
       TypeWithStaticField<int>.OutField();
       TypeWithStaticField<string>.OutField();
       TypeWithStaticField<Guid>.OutField();
   
       Console.ReadKey();
   }
   ```

   �����

   ![1554126839777](mdPics\1554126839777.png) 

3. ���Ͳ������ƶ�

   ʹ�÷���ʱ����Ҫд"<"��">"����������ʱʡ�ԣ��ɱ������ƶϡ�

4. ���Ͳ�����Լ��

   ��Capital11_Generic_01�У�ʹ��

   ```
   public class Compare<T> where T : IComparable
   ```

   ����where�������ʹ���Ͳ����̳���IComparable�ӿڣ��Ӷ������Ͳ�������Լ����

   ����Լ��ʹ��**where**�ؼ���������ĳ������ʵ�ε����͡�

   ������Լ����

   - ��������Լ��

     ```
     where T : class
     ```

     ���磺

     ```
     using System.IO;
     public class SampleReference<T> where T : Stream
     {
         public void Test(T stream)
         {
         	stream.Close();
         }
     }
     ```

     ���û��ָ��Լ����Ĭ��ΪSystem.Object�������Լ��������ʽָ�����ᱨ��

   - ֵ����Լ��

     ```
     where T : struct
     ```

     ȷ�����ݵ�����ʵ����**ֵ����**������**ö��**����**�����ǿɿ�**���͡����磺

     ```
     public class SampleValuetype<T> where T : struct
     {
         public static T Test()
         {
             return new T();
         }
     }
     ```

     ����ֵ���Ͷ���һ�������޲ι��캯�����������ͻᱨ��

   - ���캯������Լ��

     ```
     where T : new()
     ```

     ������Ͳ���Լ���ж�������**���������һ��**��

     ��Լ����֤��**���͵�ʵ����һ�������޲ι��캯���ķǳ�������**��

     ������ֵ���͡��Ǿ�̬���ǳ���û����ʾ�������캯�����ࡣ

     ע�⣺����ͬʱָ����������Լ�� �� structԼ����

   - ת�����͵�Լ��

     ```
     where T : ������	(ȷ��ָ��������ʵ�Σ������ǻ���������Ի��������)
     where T : �ӿ���	(ȷ��ָ��������ʵ�Σ������ǽӿڻ�ʵ�ֽӿڵ���)
     where T : U		  (ȷ��ָ��������ʵ�Σ�������U�ṩ������ʵ�λ�������U�ṩ������ʵ��)
     ```

     ���磺

     | ����                                  | �ѹ������͵�����                                             |
     | ------------------------------------- | ------------------------------------------------------------ |
     | Class Sample<T> where T : Stream      | Sample<Stream>����Ч��<br />Sample<String>����Ч��           |
     | Class Sample<T> where T : IDisposable | Sample<Stream>����Ч��<br />Sample<StringBuilder>����Ч��    |
     | Class Sample<T,U> where T : U         | Sample<Stream��IDisposable>����Ч��<br />Sample<String��IDisposable>����Ч�� |

   - ���Լ��

     �ǽ������ͬ�����Լ���ϲ���һ��������

     - ��ͻ���������ͺ�ֵ���� 
     - ��Լ����ǰ�棬�ӿ�Լ���ź���

     - ����

       ```
       ��Ч��
       class Sample<T> where T: class, IDisposable, new()
       class Sample<T,U> where T: class Where U: struct
       ��Ч:
       class Sample<T> where T: class, struct	()
       class Sample<T> where T: Stream, class	()
       class Sample<T> where T: new(), Stream	()
       class Sample<T> where T: IDisposable, Stream
       class Sample<T,U> where T: struct where U:class
       class Sample<T,U> where T: Stream, U:IDisposable
       ```

       