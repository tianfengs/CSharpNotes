typora-copy-images-to: mdPics

# ��Learnig Hard C# ѧϰ�ʼǡ�C# 4.0 �е�΢С�Ķ� ��17��

C# 4.0 ����ĵ�����������̬���͡�

### һ����ѡ����������ʵ��

1. ��ѡ����

   ���´����ж���ķ�������3��������һ���ر�������������ѡ������

   ```
   static void TestMethod(int x, int y=10, string name="LearningHard")
   {
       Console.WriteLine("x={0} y={1} name={2}", x, y, name);
   }
   ```

   ע�����

   - ���п�ѡ��������λ�ڱ�ѡ����֮��

   - ��ѡ������Ĭ��ֵ����Ϊ�����������顢�����ַ�����null��const��Ա��ö�ٳ�Ա�ȡ��������Ƿ���

     ```
     static void TestMethod(DateTime dt=DAtaTime.Now);
     ```

   - �������飨��params���η�����������Ϊ��ѡ�������������Ƿ���

     ```
     static void TestMethod(params int[] input=null);
     ```

   - ��ref��out�ؼ��ֱ�ʶ�Ĳ������ܱ�����Ϊ��ѡ������

2. ����ʵ��

   �����ô��п�ѡ�����ķ���ʱ���������ʡ����һ��������������Ĭ������ʡ�Ե������һ��������

   �������ֻ��ʡ�Եڶ����������Ϳ���ʹ������ʵ�������������⡣���磺

   ```
   static void Main(string[] args)
   {
       // ʡ��name
       TestMethod(2,14);
       // ʡ�Ժ���������
       TestMethod(2);
       // ֻʡ�Եڶ�������
       TestMethod(1, name : "Jerry");
       
       Console.ReadKey();
   }
   ```

3. COM�������ĸ���

   ��ѡ����������������C# 4.0 ����򵥵��������ԣ����ĺô��Ǽ���**C#��COM����Ļ�����**��

   COM��Component Object Model���������ģ�ͣ���C#���Կ���ȥ����COM����Ĺ��ܡ����Բο�MSND�У�<http://msdn.microsoft.com/zh-cn/library/bd9cdfyx(v=vs.110).aspx> ��

   ���ڻ������ԣ��ɲο��� http://www.cnblogs.com/zhili/archive/2013/01/14/NetInterop.html 

   C# 4.0 ֮ǰ����COM����Ĺ��̣�(**��ʾ����Capital17_01_COM**)

   ```
   using Microsoft.Office.Interop.Word;
   
   class Capital17_01_COM
   {
       static void Main(string[] args)
       {
           Object missing = Type.Missing;
   
           // ����WordӦ�ó���ʹWord�ɼ�
           Application wordApp = new Application { Visible = true };
   
           // �½�һ��Word�ĵ�
           wordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);
           Document wordDoc = wordApp.ActiveDocument;
   
           // ���һ������
           Paragraph para = wordDoc.Paragraphs.Add(ref missing);
           para.Range.Text = "��ӭ�㣬ʹ�þɵĵ���COM����ķ���������Word�ĵ���";
   
           // �����ĵ�
           object filename = @"OldCOMCallMethod.doc";
           object format = WdSaveFormat.wdFormatDocument97;
           wordDoc.SaveAs2(ref filename, ref format,
            ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing);
   
           // �ر�Word�ĵ���WordӦ�ó���
           wordDoc.Close(ref missing, ref missing, ref missing);
           wordApp.Application.Quit(ref missing, ref missing, ref missing);
       }
   }
   ```

   C# 4.0 ����COM����Ĺ��̣�ʹ����**��ѡ����**��**��������**֮�󣬲����ٴ���һЩ����Ҫ�Ĳ����ˣ�

   ```
   static void NewCOMCallMethod4()
   {
       //Object missing = Type.Missing;
   
       // ����WordӦ�ó���ʹWord�ɼ�
       Application wordApp = new Application() { Visible = true };
   
       // �½�һ��Word�ĵ�
       wordApp.Documents.Add();
       Document wordDoc = wordApp.ActiveDocument;
   
       // ���һ������
       Paragraph para = wordDoc.Paragraphs.Add();
       para.Range.Text = "��ӭ�㣬ʹ���µĵ���COM����ķ���������Word�ĵ���";
   
       // �����ĵ�
       object filename = Environment.CurrentDirectory + @"NewCOMCallMethod.doc";
       object format = WdSaveFormat.wdFormatDocument97;
       wordDoc.SaveAs2(ref filename, ref format);
   
       // �ر�Word�ĵ���WordӦ�ó���
       wordDoc.Close();
       wordApp.Application.Quit();
   }
   ```

   > C# �������ԣ�<http://www.cnblogs.com/zhili/archive/2013/01/14/NetInterop.html>
   >
   > .NET ƽ̨���ṩ��3�ֻ������Եļ�����
   >
   > - Platform Invoke(P/Invoke)����**ƽ̨����**,��Ҫ���ڵ���C�⺯����Windows API
   >
   >   ʹ��ƽ̨���õļ����������йܴ����е��ö�̬���ӿ⣨Dll����ʵ�ֵķ��йܺ�������Win32 Dll��C/C++ ������dll��
   >
   >   - ���йܴ�����ͨ��ƽ̨���������÷��йܴ���Ĳ���
   >
   >     (1).  **���**���йܺ�������Ϣ����**dll������**����Ҫ���õķ��йܺ���������Ϣ
   >
   >     (2). ���йܴ����жԷ�**�йܺ���**����**����**������**����**ƽ̨��������Ҫ**����**
   >
   >     (3). ���йܴ�����**ֱ�ӵ���**�ڶ�����������**�йܺ���**
   >
   >   - ƽ̨���õĵ��ù���
   >
   >     ![1554617165489](mdPics\1554617165489.png) 
   >
   > - C++ Introp, ��Ҫ����Managed C++(�й�C++)�е���C++���
   >
   >   C++ Interop ��ʽ��һ����ƽ̨���ò�һ���ĵط�������C++ Interop �����йܴ���ͷ��йܴ��������һ�������У�����ͬһ���ļ��С�C++ Interop ����Դ������ֱ�����Ӻͱ�����йܴ�����ʵ������йܴ�����л������ģ���ƽ̨�����Ǽ��ر�������ɵķ��й�DLL�����Һ�������ڵ�ַ��ʵ������йܺ������л������ġ�**C++ Interopʹ���й�C++����װ���й�C++���룬Ȼ��������ɳ��򼯣�Ȼ�����йܴ��������øó��򼯣��Ӷ���ʵ������йܴ���Ļ�����**��
   >
   > - COM Interop, ��Ҫ������.NET�е���COM�������COM��ʹ��.NET���򼯡�
   >
   >   COM Interop����֧�����йܴ�����ʹ��COM��������һ�֧����COM��������йܶ���
   >
   >   - ��.NET��ʹ��COM���
   >
   >     ��.NET��ʹ��COM������Ҫ��3�ַ�����
   >
   >     1. ʹ��TlbImp����ΪCOM�������һ�������������������ڵ�COM���������Ϳ����ڳ�������ӻ���������������COM����
   >     2. ͨ�����������ڰ�COM����
   >     3. ͨ��P/Invoke����COM�����ʹ��C++ InteropΪCOM�����д��װ��
   >
   >     ���Ǿ���ʹ�õĶ��Ƿ���һ�����������ʹ�÷���һ��.NET ��ʹ��COM����Ĳ��裺
   >
   >     1. �ҵ�Ҫʹ�õ�COM �����ע������ʹ�� regsvr32.exe ע���ע�� COM DLL��                               
   >
   >     2. ����Ŀ����Ӷ� COM ��������Ϳ�����á�                             
   >
   >        **�������ʱ��Visual Studio ���õ�Tlbimp.exe�����Ϳ⵼����򣩣�Tlbimp.exe��������һ�� .NET Framework ���������򼯡��ó����ֳ�Ϊ����ʱ�ɵ��ð�װ (RCW)�����а����˰�װCOM����е���ͽӿڡ�Visual Studio ����������������������Ŀ��**                           
   >
   >     3. ����RCW�����ʵ���������Ϳ���ʹ���йܶ���һ����ʹ��COM����
   >
   >     ![1554617497040](mdPics\1554617497040.png) 
   >
   >   - ��COM��ʹ��.NET����
   >
   >     .NET ������������ʱͨ��**COM�ɵ��ð�װ**��COM Callable Wrapper,��**CCW**���������COM���Ϳ�Ľ�����CCW����ʹCOM�ͻ�����Ϊ��������ͨ��COM���ͽ�����ͬʱʹ.NET�����Ϊ���������й�Ӧ�ó��򽻻���������**CCW�Ƿ��й�COM�ͻ������йܶ���֮���һ������ CCW�ȿ���ά���йܶ�����������ڣ�Ҳ��������������COM��.NET֮����໥ת��**��ʵ����COMʹ��.NET ���͵Ļ��������磺
   >
   >     - ��C#��Ŀ����ӻ���������
   >
   >       ![1554617724056](mdPics\1554617724056.png)
   >
   >     - ����COM���ͿⲢ��������ע���Թ�COM�ͻ���ʹ��
   >
   >       ![1554617773194](mdPics\1554617773194.png)
   >
   >       ��ѡ��**ΪCOM������ע��**��ѡ���Visual Studio��������Ϳ⵼������(Tlbexp.exe)Ϊ.NET��������COM���Ϳ���ʹ�ó���ע�Ṥ��(Regasm.exe)����ɶ�.NET���򼯺����ɵ�COM���Ϳ����ע�ᣬ����COM�ͻ��˿���ʹ��CCW��������.NET������е����ˡ�

   > **��C# �е���COM���** 
   >
   > ʵ��ԭ������
   >
   > ��������ӡ�**Microsoft.Office.Interop.Word 14.0.0.0** �� ������ã�14.0.0.0�汾�Ƕ�Ӧ��Office 2010��
   >
   > **Microsoft.Office.Interop.Word.dll** ȷʵ��һ��.NET���򼯣�������Ҳ����COM����Ļ��������򼯣�
   >
   > `��������а�����COM����ж�������͵�Ԫ���ݣ� �йܴ���ͨ�����û����������й����Ľӿڻ��������ӵص���COM����ͽӿڵġ������йܴ����в���ֱ��ʹ��COM����ͽӿڣ������йܴ����COM����ĵ���ʱ��ͨ��CLR�� "COM Interop" ����Ϊ������ɵģ�����������RCW����Runtime Callable Wrapper,����ʱ�ɵ��ð�װ�������Զ�COM����ĵ��ã�����ͨ��RCW����ɵģ�RCW���Ĺ�����Ҫ�м���COM��������йܴ���ͷ��йܴ���֮��������ݷ��ʹ���` 
   >
   > ����������Կ�����RCW���� .NETƽ̨��COM���֮���һ������΢��ĺܶ༼����ʹ���˴���ģ�����WCF�������������ڴ����д����Ķ�����ʵֻ�Ƿ����һ������ͨ�������������������Ķ���ķ��񣬼���������������ļ�����C#�е�ί��Ҳ�Ǵ����һ��ʵ�֣���ʱ���뵽��23�����ģʽ�еġ�������ģʽ��������ͨ��һ��ͼ����ʾ�� **��.NET�е���COM�����ԭ��** 
   >
   > ![1554618418832](mdPics\1554618418832.png)  
   >
   > ͨ��Tlblmp.exe���������ɻ��������򼯲��裬���Բο�MSDN�����������ϸʹ��˵�� ��<http://msdn.microsoft.com/zh-cn/library/tt0cf3sx(v=VS.80).aspx> ��
   >
   > ����Ҳ����ʹ��Visual Studio�����õ�֧�������ΪCOM���Ϳⴴ�����������򼯵Ĺ���������ֻ��Ҫ��**VS��Ϊ.NET ��Ŀ��Ӷ�Ӧ��COM��������ã���ʱVS�ͻ��Զ���COM���Ϳ��е�COM���Ϳ�ת��Ϊ�����е�Ԫ����**��������Ŀ��BinĿ¼�����ɶ��ڵĻ��������򼯣�������VS�����COM���ã���ʵ�����������õ��ǻ��������򼯣�Ȼ��ͨ��RCW����COM������е��á�
   >
   >  Ȼ������Office�е�**Microsoft.Office.Interop.Wordd.dll��**�������Ҳ�ǻ��������򼯣����������������������򼯣���PIA(Primary Interop Assemblies)����������������һ���ɹ�Ӧ���ṩ��**Ψһ**�ĳ��򼯣�Ϊ�����������������򼯣�������ʹ��TlbImp�����Ǵ� /primary ѡ�������������ǿ϶������������ʣ�PIA����ͨ���򼯵�����ʲô�����أ������������PIA���˰�����COM�����������������⣬��������һЩ�������Ϣ���繫Կ��COM���Ϳ���ṩ�ߵ���Ϣ��
   >
   > Ϊʲô��Ҫ�����������򼯵��� �����ǡ��������������򼯿��԰������ǽ���������ʱ�����û��������򼯰汾��һ�µ����⡣(���������Ա��Ϊһ��COM������Ϳ����ɶ�����������򼯣���Ŀ�����õĻ��������򼯰汾�벿��ʱ�Ļ��������򼯰汾��һ�µ����⣬���˻���������ʱ�����ǿ���ֱ�����ùٷ��ṩ�����������򼯡�)
   >
   > 
   >
   > **������**��(**��ʾ����Capital17_02_COM**)
   >
   > ����Ĵ���ķ���������ƽ���йܴ����еĴ���ʽ��һ���ģ�����;��忴������λ�ȡ������Ϣ�ģ�������δ���Ĺ����ǡ�����һ�����е�Word�ĵ���������Ӧ���ı�����ָ����Word�ĵ�������ʱ����ʱ�ͻ���ֵ���COM�����Open����ʧ�ܵ������
   >
   > 
   >
   > **Microsoft.Office.Interop.Wordʹ��**��
   >
   > (**��ʾ����Capital17_03_COMWord**)
   >
   > <https://www.cnblogs.com/xh6300/p/5915717.html>
   >
   > http://blog.csdn.net/ruby97/article/details/7406806
   >
   > http://blog.csdn.net/yhrun/article/details/7674540
   >
   > http://www.cnblogs.com/eye-like/p/4121219.html
   >
   > http://www.cnblogs.com/knowledgesea/archive/2013/05/24/3095376.html
   >
   > http://www.cnblogs.com/shi2172843/p/5848116.html
   >
   > <https://msdn.microsoft.com/en-us/library/bb257531(v=office.12).aspx> Ӣ��
   >
   > <https://docs.microsoft.com/zh-cn/office/vba/api/overview/word> ����
   >
   > http://wenku.baidu.com/view/80ec0a6c1eb91a37f1115cab.html?from=search

?	















