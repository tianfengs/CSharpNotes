typora-copy-images-to: mdPics

# ��Learnig Hard C# ѧϰ�ʼǡ�Lambda���ʽ ��14��

Lambda���ʽ��C# 3.0 ����Ҫ������֮һ��

### һ��Lambda���ʽ���

Lambda���ʽ��һ�����������������԰������ʽ����䣬���ڴ���ί�л�ת��Ϊ���ʽ����

ʹ��**"=>"�����**������ goes to������������������������**�������**���ұ���**���ʽ������** 

1. Lambda���ʽ���ݱ����

   Lambda���ʽ���Ǵ���������һ·�ݱ�����ġ�(**��ʾ����Capital14_Lambda_01��**)

   - C# 1.0 �д���ί��ʵ���Ĵ���

     ```
     Func<string, int> delegatetest1 = new Func<string, int>(CallBackMethod);
     ```

   - C# 2.0 ������������������ί��ʵ��������Ҫ�����ص�������

     ```
     Func<string, int> delegatetest2 = delegate (string text)
     {
     	return text.Length;
     };
     ```

   - C# 3.0 ʹ��Lambda���ʽ

     ```
     // �з���ֵ�ķ���
     Func<string, int> delegatetest4 = new Func<string, int>(text => text.Length);
     
     Func<string, int> delegatetest3 = text => text.Length;
     
     // û�з���ֵ�ķ���
     Action<string, int> delegatetest5 = new Action<string, int>((str, strInt)=>
     {
     	Console.WriteLine($"{str}����{strInt}�ꡣ");
     });
     
     Action<string, int> delegatetest6 = (str, strInt) => Console.WriteLine($"{str}����{strInt}�ꡣ");
     ```

     ����Lambda���ʽ��ί��ʵ����

     ```
     Console.WriteLine("ʹ��Lambda���ʽ�����ַ������ȣ�" + delegatetest3("ssdfsfsd"));
     
     delegatetest6("zhangsan", 25);
     ```

   Lambda���ʽ�����µı�̷�����ڴ�������������

2. Lambda���ʽ��ʹ��

   ί�е�һ����;�����ڡ������¼�����������ʾ**��Lambda���ʽʵ�ֶ����¼�**��

   (**��ʾ����Capital14_Lambda_02_Event��**)

   C# 2.0 �ķ�������**��������**�����¼��ͳ�ʼ��

   ```
   static void EventsSubscribeOld()
   {
       Button button = new Button();
       button.Text = "�����";
   
       EventHandler clickEvent = delegate (object sender, EventArgs e)
       {
           ReportEvent("Click�¼�", sender, e);
       };
       button.Click += clickEvent;
   
       button.KeyPress += delegate (object sender, KeyPressEventArgs e)    // �����ť��ͬʱ�����¿ո���Ŵ���
       {
           ReportEvent("KeyPress�¼�", sender, e);
       };
   
       // ��C# 3.0 ֮ǰ�����·��� ��ʼ������
       Form form = new Form();
       form.Name = "�ڿ���̨�д����Ĵ���";
       form.AutoSize = true;
       form.Controls.Add(button);
   
       // ���д���
       Application.Run(form);
   }
   ```

   C# 3.0 �ķ�������**Lambda���ʽ**����������������**��ʼ������ʼ��**��ʹ����ܼ�ࡣ

   ```
   static void EventSuscribeCSharp3()
   {
       Button button = new Button() { Text = "����ң���ʼ������" };
   
       button.Click += (sender, e) => ReportEvent("Click�¼�", sender, e);
       button.KeyPress += (sender, e) => ReportEvent("KeyPress�¼�", sender, e);
   
       Form form = new Form() { 
           Name = "�ڿ���̨�д������壨��ʼ������"
           , AutoSize = true
           , Controls = { button } 
       };
   
       Application.Run(form);
   }
   ```

### �������ʽ��

Lambda���ʽ��������ת���ɱ��ʽ������ơ����ʽĿ¼��������**��������ʾLambda���ʽ�߼���һ�����ݽṹ**�����������ʾ��һ��**����������**�����ǿ�ִ�д��롣

> ���ʽ�����������ݽṹ��ʾ���룬����ÿһ���ڵ㶼��һ�ֱ��ʽ��
>
> ����ԶԱ��ʽ���еĴ�����б༭�����㡣 �����ܹ���̬�޸Ŀ�ִ�д��롢�ڲ�ͬ���ݿ���ִ�� LINQ ��ѯ�Լ�������̬��ѯ��
>
> ���ʽ���������ڶ�̬��������ʱ (DLR) ���ṩ��̬���Ժ� .NET Framework ֮��Ļ������ԣ�ͬʱ��֤��������дԱ�ܹ�������ʽ������ Microsoft �м����� (MSIL)��
>
> 1. ���� Lambda ���ʽ�������ʽ��
>
>    C# ������ֻ�ܴӱ��ʽ Lambda������ Lambda�����ɱ��ʽ���� ���޷�������� lambda ������� lambda���� 
>
>    ���д���ʾ��չʾ���ͨ�� C# ������������ʾ Lambda ���ʽ `num => num < 5` �ı��ʽ����
>
>    ```
>    Expression<Func<int, bool>> lambda = num => num < 5;
>    ```
>
> 2. ͨ�� API �������ʽ��
>
>    ͨ�� API �������ʽ����Ҫʹ�� [Expression](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.expressions.expression) �ࡣ
>
>    ����������ض����ͱ��ʽ���ڵ�ľ�̬����������
>
>    - ParameterExpression����ʾ���������������˼��ֱ��ʽ���ͣ�
>      - MethodCallExpression
>      - System.Linq.Expressions
>      - ��һ�־�����ʽ����
>    - MethodCallExpression����ʾ�������á�
>
>    ���д���ʾ��չʾ���ʹ�� API ������ʾ Lambda ���ʽ `num => num < 5` �ı��ʽ��
>
>    ```
>    // Add the following using directive to your code file:  
>    // using System.Linq.Expressions;  
>      
>    // Manually build the expression tree for   
>    // the lambda expression num => num < 5.  
>    ParameterExpression numParam = Expression.Parameter(typeof(int), "num");  
>    ConstantExpression five = Expression.Constant(5, typeof(int));  
>    BinaryExpression numLessThanFive = Expression.LessThan(numParam, five);  
>    Expression<Func<int, bool>> lambda1 =  
>        Expression.Lambda<Func<int, bool>>(  
>            numLessThanFive,  
>            new ParameterExpression[] { numParam });  
>    ```
>
>    

1. ��̬�Ĺ���һ�����ʽ��(��msdn��https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/expression-trees/index)

   (**��ʾ����Capital14_Lambda_03_ExpressionTrees��**)

   - ���조a+b���ı��ʽ�ṹ�����������ʽ��

   ```
   /// <summary>
   /// ���조a+b���ı��ʽ�ṹ�����������ʽ��
   /// </summary>
   static void CreateExpressionTree()
   {
       // ���ʽ������
       ParameterExpression a = Expression.Parameter(typeof(int), "a");
       ParameterExpression b = Expression.Parameter(typeof(int), "b");
   
       // ���ʽ����
       BinaryExpression be = Expression.Add(a, b);
   
       // ������ʽ��
       Expression<Func<int, int, int>> expressionTree = Expression.Lambda<Func<int, int, int>>(be, a, b);
   
       // �������ṹ
       BinaryExpression body = (BinaryExpression)expressionTree.Body;
       ParameterExpression left = (ParameterExpression)body.Left;
       ParameterExpression right = (ParameterExpression)body.Right;
   
       // ������ʽ��
       Console.WriteLine("���ʽ�ṹΪ��"); Console.WriteLine(expressionTree);
       Console.WriteLine("���ʽ����Ϊ��"); Console.WriteLine(expressionTree.Body);
       Console.WriteLine("���ʽ��ڵ�Ϊ��{0}{4} �ڵ�����Ϊ��{1}{4}{4} ���ʽ�ҽڵ�Ϊ��{2}{4}" +
          " �ڵ�����Ϊ��{3}{4}", left.Name, left.Type, right.Name, right.Type, 
          Environment.NewLine);
   }
   ```

   - msdn���ֶ����� ���ʽ����������������

   ```
   /// <summary>
   /// msdn���ֶ����� ���ʽ����������������
   /// </summary>
   static void CreatEprsTreeByAPI()
   {
       // Add the following using directive to your code file:  
       // using System.Linq.Expressions;  
   
       // Manually build the expression tree for   
       // the lambda expression num => num < 5.  
       ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
       ConstantExpression five = Expression.Constant(5, typeof(int));
       BinaryExpression numLessThanFive = Expression.LessThan(numParam, five);
       Expression<Func<int, bool>> lambda1 =
           Expression.Lambda<Func<int, bool>>(
               numLessThanFive,
               new ParameterExpression[] { numParam });
   
       // ���Lambda���ʽ��
       Console.WriteLine("���Lambda���ʽ��(lambda1)��\t\t\t"+lambda1);
   
       // ��������һ�����ʽ��
       var isLessThanFive = lambda1.Compile()(6);
       // ������
       Console.WriteLine("������ʽ����lambda1.Compile()(6)��������ǣ�\t" + 
       IsLessThanFive);
   
       Console.WriteLine();
   }
   ```

2. ͨ��Lambda���ʽ��������ʽ��

   ���д���ʾ��չʾ���ͨ�� C# ������������ʾ Lambda ���ʽ `num => num < 5` �ı��ʽ����

   ```
   Expression<Func<int, bool>> lambda = num => num < 5;
   ```

3. �ѱ��ʽ��ת���ɿ�ִ�д���

   ͨ��Expression<TDelegate>���Compile()�����������ʽ�������ί��ʵ������ͨ��ί�е��õķ�ʽ�õ��ṹ��

   ```
   var isLessThanFive = lambda1.Compile()(6);
   ```

### �����ܽ�

Lambda���ʽ�������������ݱ������ʵ���Ǹ�����������

��ʾ��ʹ��Lambda���ʽ���ӳ�ʼ���������¼����Ĵ���ķ�����

������Lambda���ʽ���߼����ṹ�����ʽ���������ǿ�ִ�д��룬��Ҫͨ������Compile()������ת��Ϊί�ж��󣬲���ʹ�á�

