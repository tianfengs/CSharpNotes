---
typora-copy-images-to: mdPics

---

# ��Learnig Hard C# ѧϰ�ʼǡ�����֪ʶ 1-7��

  

### ʲô��.NET Framework��

C#�Ǳ�����ԣ�����ʵ���������Ի�
.NET Framework��Ӧ�ó�������ʱ��ִ�л������ṩ����ķ���
   - ȫ������
- �ڴ����
- ͨ������ϵͳ��Common Type System, CTS) CTS�����˿������м�������ʹ�õ�Ԥ������������
- �����ṹ�ͼ���   .NET Framework�ṩ�˿����ض�Ӧ������Ŀ⣬��WebӦ�ó����ASP.NET����
- ���Ի�������    ���������ṩ�������м����Դ���Ļ��ƣ����ֻ���ʹ�ò�ͬ����֮����л�������Ϊ���ܡ�

### .NET Framework���

1. ������������ʱ��Common Language Runtime, **CLR**)
   �ṩ�ڴ�����̹߳�����쳣����ȷ��񣬸���Դ���������Ͱ�ȫ��顣
   ����CLR����Ĵ����Ϊ �йܴ��루managed Code��������CLR����Ĵ����Ϊ ���йܴ��루unmanaged code��
   CLR���������֣�

   - ͨ������ϵͳ��Common Type System��**CTS**��

     - CTS���ڽ����ͬ����֮���������Ͳ�ͬ�����⡣
       -  ���磺C#��������int��VB.NET��Integer��CTS���԰����Ǳ��ͨ������Int32��
     -  CTS
       -  ���ͣ��������͡�ֵ���͡�
       -  ����֮��ת����װ�䣨box�������䣨unbox��

   - �������Թ淶��Common Language Specification��**CLS**��

     CLS���ڽ����ͬ����֮�����Թ淶��ͬ������

     - CLS��һ����͵����Ա�׼������������.NETƽ̨ΪĿ�������������֧�ֵ���С�������Լ�����֮��ʵ�ֻ��������걸����
     - ���磺C#�������ִ�Сд��VB.NET���������֣�CLS�涨��������IL������˴�Сд����������������ͬ

2. .NET Framework��⣨Framework Class Library��**FCL**��
   .NET Framework������һ��DLL���򼯵ļ��ϡ������˴��������ͣ�������ص�һ�����ͷŵ�һ�������������ռ������֡�

### C#��.NET Framework�Ĺ�ϵ

![Csharp��dotNETFramework�Ĺ�ϵ](mdPics\Csharp��dotNETFramework�Ĺ�ϵ.png)

  

  

### C#�����ִ�й���
�����׶Σ�

   - C#�������Ϊ�м����Դ��루Common Intermediate Language��CIL�������������C#����������ɡ�
     - ������IL���뽫�洢��һ�������У���IL�����⣬������Ԫ���ݺͿ�ѡ����Դ�ļ���
         - Ԫ���������������ͣ����Ͷ�������������ͳ�Ա���������������ó�Ա��������

         - ��ѡ����Դ�ļ�ָ�������ݣ���ͼƬ�ȡ�

           ![](mdPics\C#�����ִ�й���.png)

         - ���򼯷�Ϊ���֣���չ��Ϊ.exe�Ŀ�ִ���ļ�����չ��Ϊ.dll�Ŀɱ����õĿ��ļ���

     - �м����Դ������Ϊ�������루native code������������� ��ʱ��������Just-In-Time�� JIT������ɡ�

![](mdPics\C#�����ִ�й���2.png)

### ����C#����

   - ��������
- ���������
     - F5���к͵��ԡ�Ctrl+F5ֻ����

     - ͨ�������б��������

       ```
       csc [options] sourceFiles 
       ```

        

### C#�е���


4.3.6 ������

**������**�������������������һ���������е������Ա��

���壺

```
 [���η�] �������� this[�������� index]
    {
        get { ������������ĳ��Ԫ�� }
        set { ������ĳ����Ԫ�ظ�ֵ }
    }
```

���磺
����һ�������������ࣺ

```
class Person
{
    private int[] intarray = new int[10];
    // ����������
    public int this[int index]
    {
        get
        {
            return intarray[index];
        }
        set
        {
            intarray[index] = value;
        }
    }
}
```

������ʾ�����ʹ����������            

    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            // ͨ�������������е�������и�ֵ
            person[0] = 1;
            person[1] = 2;
            person[2] = 3;
            
            Console.WriteLine(person[0]);
            Console.WriteLine(person[1]);
            Console.WriteLine(person[2]);
    
            Console.ReadKey();
        }
    }
4.5 ����ṹ�������

- �﷨�ϵ����𣺶���Ĺؼ��� **class** �� **struct**
- �ṹ�岻�ɶ������ֶν��г�ʼ����������Զ������ֶν��г�ʼ����
-  ����Զ�����һ��**�޲�ʵ�����캯��**����һ����ʽ�����˹��캯���������ʽ���캯����û���ˡ���**�ṹ��**�������Ƿ���ʽ�Ķ����˹��캯������ʽ���캯������**һֱ����**�ġ�
- �ṹ���ز�����ʽ�Ķ����޲ι��캯����˵���ṹ�����޲ι��캯��һֱ���ڡ�
- �ڽṹ��Ĺ��캯���У�����ҪΪ�ṹ�������е��ֶθ�ֵ��
-  �����ṹ�������Բ�ʹ��new������ʱ�ṹ������е��ֶ���û�г�ʼֵ�ģ��������ʹ��new�ؼ�������������
- **�ṹ��**���ܼ̳нṹ�����࣬��**����ʵ�ֽӿ�**��**��****���Լ̳����ʵ�ֽӿ�**�������ܼ̳нṹ
- �����������ͣ����ṹ����ֵ����
- �ṹ��������������������������
- ������abstract��sealed���νṹ�壬�������

   

### ��7�� IL����
IL��Intermediate Language������Ҳ��ΪCIL����MSIL��**�м�����**��

IL��ECMA��֯��ECMA-335��׼���ṩ�����Ķ���͹淶��

���ǿ���ֱ�Ӱ�C#Դ�������Ϊ.exe��.dll�ļ�����ʱ��������ĳ�����벢���Ƕ����ƴ��룬����IL���롣

1. ##### ����ȥ�鿴IL���룺
   ΢���ṩ�˷������� ���� **ILDasm.exe**     [C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin](file://C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin)
   Ҳ������Visual Studio 2010�ҵ��ù��ߣ� C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX4.0Tools

- ������룺 

![](mdPics\HelloWorld����.png)

- ��ILDasm.exe��ConsoleApp1.exe�ļ�

![](mdPics\ILDasm.exe����.png)

- MANIFEST��һ���嵥�ļ����������򼯵����ԣ����磺���������汾�š���ϣ�㷨������ģ�飬�Լ����ⲿ���ó����������Ŀ

![](mdPics\MANIFEST�ļ��嵥����.png)

![](mdPics\ILDasm.exe����ָ��˵��.png)

- Program���IL�������

  - ˫��.class private auto ansi beforefieldinit

    ![img](mdPics\896449-20180211113121451-881183676.png)  

    - .class ����Program��һ���࣬extends����Program�̳��ڳ���mscorlib�е�System.Object��
    - privateΪ����Ȩ�ޣ�����������˽�еġ�
    - auto�����������ʱ�ڴ�Ĳ�������CLR�����ģ��������ɳ�����Ƶġ�
    - ansi������ı���Ϊansi����
    - beforefieldinit����CLR�����ڵ�һ�η��ʾ�̬�ֶ�֮ǰ���κ�ʱ��ִ�����͹��캯�������͹��캯�����Ǿ�̬���캯����

  - ˫�� .ctor:void()

    ![img](mdPics\896449-20180211114351982-445894811.png)  

    - .ctor��ʾ�÷�������Ĺ��캯�������IL������ǹ��캯����IL����
    - cil managed�����������еĴ�����IL���룬�����йܴ��룬��������CLR���п��ϴ���
    - .maxstack����ִ�й��캯���ǣ�������ջ���������������������������ջ�Ǳ��淽�������������ֵ��һ���ڴ����򣬸������ڷ���ִ�н���ʱ�ᱻ��գ����ߴ洢һ������ֵ
    - IL_0000�Ǵ��뿪ͷ������֮ǰ�Ĳ���Ϊ�����������ͳ�ʼ������
    - ldarg.0�������ص�һ����Ա����������ldarg��load argument����д
    - call ָ��һ�����ڵ��þ�̬��������δ�����call�����ǵ��þ�̬���������ǵ���System.Object���캯��������һ��ָ��callvirһ�����ʵ��������
      - ���ȼ�鱻���ú����Ƿ�Ϊ�麯��
      - �ǵĻ�����������Ƿ���д�������д�͵��������ʵ�֣����û����д�����������ԭ������
    - retָ���ʾִ����ϣ�return�ļ�д

  - ˫�� Main��void��string[])

    ![img](mdPics\896449-20180211120451654-1402338139.png) 

    - Main�����ǳ�������

    - hidebysigָ���ʾ�����ǰ����Ϊ���࣬�ø�ָ���ǵķ��������ᱻ����̳�

    - .entrypointָ�����ú����������ں�����ÿ���й�Ӧ�ó�������ֻ��һ����ں�����CLR���س���ʱ�����ȴ�.entrypoint������ʼִ��

    - .locals init ([0] string helloString)��ʾ����string���͵ı�������������ΪhelloString

    - nop �� No operation����˼����û���κβ���

    - ldstr ��Hello World�� ָ�load string����ʾ���ַ���ѹ�� ����ջ����ʱ��Hello World��λ������ջ��ջ��

    - stloc.0 ָ�stack local 0��(��store local 0)��ʾ��ֵ������ջ�е���������ֵ������ջ��call stack���еĵ�0��������Ҳ����helloString����������ջΪһ����žֲ��������ڴ�����

      ����ջ�͵���ջ������CLR���й���ġ���.localָ�IL_0006֮���IL������൱��C#����

      ```
      string helloString = "Hello World";
      ```

    - ldloc.0ָ���ʾ�ѵ�0���ֲ�����ѹ������ջ�С�

      > ldǰ׺��ʾ��ջ������stΪǰ׺��ָ������ջ����

    - callָ���ʾ���þ�̬������������õ���Console���е�WriteLine(string)�������ѵ�0���ֲ��������������̨�С�

2. ##### (7.3)����IL����

   IL��һ�Ż��ڶ�ջ�����������������ԡ�

   1. IL��������

      ![1553699176593](mdPics\1553699176593.png)

   2. ����������

      C#���ԣ�

      ```
      ��������  �������ƣ�
      ```

      IL���룺�������������ѱ�������"����ջ" [0]

      ```
      .local init ([0] �������� ��������)
      ```

   3. ��������

      - �������㣺�ӷ�add���˷�sub������div������rem��

      - λ���㣺һԪָ��not����and����or�ȡ������1��0�ֱ��ʾ��ͼ١�������ѹ������ջջ����

      - �Ƚ����㣺����ָ��cgt��С��clt�͵���ceq

        ������

        C#Դ���룺

        ```
        class Captal7_IL_01
        {
            static void Main(string[] args)
            {
                int i = 2;
                int j = 3;
                int result = i + j;
            }
        }
        ```

        IL��Main������

        ```
        .method private hidebysig static void  Main(string[] args) cil managed
        {
          .entrypoint
          // �����С       10 (0xa)
          .maxstack  2
          .locals init ([0] int32 i,	// ����3������32λ����i��j��result
                   [1] int32 j,
                   [2] int32 result)
          IL_0000:  nop
          IL_0001:  ldc.i4.2	// ����2��4�ֽڳ�������ѹ������ջ
          IL_0002:  stloc.0		// ������ջջ����ֵ����������ֵ����0���������� i=2
          IL_0003:  ldc.i4.3	// ����3��4�ֽڳ�������ѹ������ջ
          IL_0004:  stloc.1		// ������ջջ����ֵ����������ֵ����1���������� j=3
          IL_0005:  ldloc.0		// �ѵ�0������ѹ������ջ����i
          IL_0006:  ldloc.1		// �ѵ�1������ѹ������ջ����j
          IL_0007:  add			// ִ��add������֮�󽫱���i��j��գ����ѽ����������ջջ��
          IL_0008:  stloc.2		// ������ջջ����ֵ����������ֵ����2���������� result=i+j
          IL_0009:  ret			// ����
        } // end of method Captal7_IL_01::Main
        ```

   4. IL�е����̿���

      ��ӦC#�����е����̿������if-else��䡢while����for���ȡ�

      ������

      C#Դ�룺

      ```
      class Captal7_IL_02
      {
          static void Main(string[] args)
          {
              int i = 2;
              if (i > 0)
              {
              	Console.WriteLine("iΪ����");
              }
              else
              {
              	Console.WriteLine("iΪ����");
              }
          }
      }
      ```

      ���ɵ�IL���룺

      ```
      .method private hidebysig static void  Main(string[] args) cil managed
      {
        .entrypoint
        // �����С       40 (0x28)
        .maxstack  2
        .locals init ([0] int32 i,	// ������������������32λ i�������� V_1
                 [1] bool V_1)
        IL_0000:  nop
        IL_0001:  ldc.i4.2	// ����2��4�ֽڳ�������ѹ������ջ
        IL_0002:  stloc.0		// ������ջջ����ֵ����������ֵ����0���������� i=2
        IL_0003:  ldloc.0		// �ѵ�0������ѹ������ջ����i
        IL_0004:  ldc.i4.0	// ����0��4�ֽڳ�������ѹ������ջ
        IL_0005:  cgt			// ִ�д���ָ��Ƚ�i��0�����н������ջ��
        IL_0007:  stloc.1		// �Ѵ���ָ��Ľ��������ջջ����������������1������ V_1
        IL_0008:  ldloc.1		// �ѵ�1����������V_1ѹ������ջջ��
        IL_0009:  brfalse.s  IL_001a	// ���ջ������V_1Ϊfalse������ת�� IL_001a
        IL_000b:  nop
        IL_000c:  ldstr      bytearray (69 00 3A 4E 63 6B 70 65 )	// i.:Nckpe�ַ���ѹ��ջ��
        IL_0011:  call       void [mscorlib]System.Console::WriteLine(string)	//�����cmd
        IL_0016:  nop
        IL_0017:  nop
        IL_0018:  br.s       IL_0027	// ��������ת�� IL_0027
        IL_001a:  nop
        IL_001b:  ldstr      bytearray (69 00 3A 4E 1F 8D 70 65 )	// i.:N..pe�ַ���ѹ��ջ��
        IL_0020:  call       void [mscorlib]System.Console::WriteLine(string)	//�����cmd
        IL_0025:  nop
        IL_0026:  nop
        IL_0027:  ret			// ����
      } // end of method Captal7_IL_02::Main
      ```

   ILָ��������ݣ����Բο�EMAC�ĵ����ر���"Partition II:Metadata Definition and Semantics (��2���֣�Ԫ���ݶ��������)"��"Partition III:CLI Instrction Set(��3���֣�CILָ�)"��ƪ����ȡ������

   - https://github.com/dotnet/coreclr/blob/master/Documentation/project-docs/dotnet-standards.md
   - MSDN�ϵ�ECMA C# and Common Language Infrastructure Standards(https://docs.microsoft.com/en-us/previous-versions/dotnet/articles/ms973879(v=msdn.10))��
   - Ecma International��վ�ϵ�Standard ECMA-355-Common Language Infrastructure(CLI)(http://www.ecma-international.org/publications/standards/Ecma-335.htm)��

