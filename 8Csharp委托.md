---
typora-copy-images-to: mdPics
---

# ��Learnig Hard C# ѧϰ�ʼǡ�ί�� ��8��



### һ��ί����ʲô

ί���������͡��������Ϊ������һ����װ����ʹ��C#�еĺ���������Ϊ�����������ݡ�

ί�а�װ�ķ�����һ��������������

- ����ǩ��������ί��һ�£�����ǩ�����������ĸ��������ͺ�˳��
- �����ķ���ֵ����Ҫ��ί��һ�£���Ȼ�������ڷ���ǩ����һ����

### ����ί�е�ʹ��

**��������Captal8_Delegate_01)** 

```
class Captal8_Delegate_01
{
    // 1.����ί��
    delegate void MyDelegate(int para1, int para2);

    static void Main(string[] args)
    {
        // 3. ����ί��ʵ��
        MyDelegate d;

        // 4. ʵ����ί�����ͣ����ݻ�󶨷�����������ʵ������ ���� ��̬������
        d = new MyDelegate(new Captal8_Delegate_01().Add);

        // 5. ί��������Ϊ�������ݸ���һ������������һ�������ڵ��ã�����һ��������ò�����
        Captal8_Delegate_01.MyMethod(d);

        Console.ReadKey();
    }

    /// <summary>
    /// 2. ������ί����ͬǩ���ͷ������͵� ʵ������
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    void Add(int p1,int p2)
    {
        int sum = p1 + p2;
        Console.WriteLine($"{p1}+{p2}֮��Ϊ��{sum}");
    }

    /// <summary>
    /// 5. �����Ĳ�����ί������
    /// </summary>
    /// <param name="myDelegate"></param>
    private static void MyMethod(MyDelegate myDelegate)
    {
        // �ڷ����е���ί��
        myDelegate(1, 2);
    }
}
```

ʹ��ί�еĲ��裺

����ί�����ͣ�����**ί��**�� --->  ����**ʵ������**  --->  ����**ί�ж���** --->  ʵ����ί�ж��󣬰�ʵ������  --->  ����ί�ж���

���ʣ�����������֮��Ĺ�ϵ��**ί��**(����)��**ʵ������**��**ί�ж���**

1����ί�У�2����ʵ��������3����ί�ж���4��ʵ�������󶨵�ί�ж���5����ί�ж�����������÷���

�����ĵ�5�����Ǵ���һ����̬����������ί�У�ί�ж�����Ϊ���������������̬������

### ��������ί�е�ԭ��

ί����C#��һ����õ����ԣ�**���Ǻ����ܶ�����ԵĻ���**��

���к�ʵ����ÿ�����Ҵ��к��ķ�ʽ��ͬ��**��������Captal8_Delegate_02Greeting)** 

�Ƚ�һ����switchʵ�ַ�����**����չ�Ժܲ�**���������������Ҵ��к��ķ�ʽ����Ҫ�޸�switch��䡣��ί����ʵ�־ͱȽϺã�

```
// ����ί��
public delegate void GreetingDelegate(string name);

class Captal8_Delegate_02Greeting
{
    static void Main(string[] args)
    {
        Captal8_Delegate_02Greeting greet = new Captal8_Delegate_02Greeting();

        // ��ʹ��ί�У�ʵ�ִ��к��ķ���
        Console.WriteLine("����ί�з�������switch����ʵ�֣�");
        greet.Greeting("Tom", "en-us");
        greet.Greeting("С��", "zh-cn");
        Console.WriteLine();

        // ʹ��ί�еķ���
        Console.WriteLine("��ί�з���ʵ�֣�");
        greet.GreetingDeleMeth("Tom", greet.EnglishGreeting);
        greet.GreetingDeleMeth("С��", greet.ChineseGreeting);

        Console.ReadKey();
    }

    ////////////////
    /// ��ʹ��ί�У�ʵ�ִ��к��ķ���
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
    /// ʹ��ί�еķ���
    /// 
    public void GreetingDeleMeth(string name, GreetingDelegate callback)
    {
        callback(name);
    }

    // Ӣ���˴��к�
    private void EnglishGreeting(string name)
    {
        Console.WriteLine($"Hello, {name}");
    }

    // �й��˴��к�
    private void ChineseGreeting(string name)
    {
        Console.WriteLine($"���, {name}");
    }
}
```

### �ġ�ί�еı���

ί����**"��"**���͵ġ�

C#Դ�룺**��������Captal8_Delegate_03)** 

```
class Captal8_Delegate_03
{
    // ����ί������
    public delegate void DelegateTest(int parm);

    static void Main(string[] args)
    {
    }
}
```

IL�е���ʾ��

![1553992077865](mdPics\1553992077865.png) 

���������ί�����ͱ�����������ͣ�

```
public class DelegateTest: System.MulticastDelegate
{
    public DelegateTest(Object object, IntPtr method);
    public virtual void Invoke(int32 parm);
    public virtual IAsyncResult BeginInvoke(Int32 parm, AsyncCallback, Object object);
    public virtual void EndInvoke(IAsyncResult result);
}
```

���ί��ֱ�Ӱ����첽���÷���**BeginInvoke()**��**EndInvoke()**���Ϳ�ʵ�ֿ��̵߳��÷���**Invoke()**��Invoke()����������ʾ�ĵ���ί�С�

ͨ��IL���룬���Կ�����ί����**�̳���System.MulticastDelegate����**������**���캯��**������**ʵ���첽��ͬ�����õķ���**��

### �塢ί����

C#��ί�п��Է�װ����������ѷ�װ���������ί�г�Ϊ**ί����**��**��·�㲥ί��**��

**��������Captal8_Delegate_04DelegateChain)**

1. ʹ��ί��������"+"
2. ��ί�������Ƴ�ί�У���"-"

```
class Captal8_Delegate_04DelegateChain
{
    // 1. ����ί��
    public delegate void DelegateTest();

    static void Main(string[] args)
    {
        // ����ί�ж���1����һ����̬����
        DelegateTest dtstatic = new DelegateTest(methodStatic);
        // ����ί�ж���2����һ��ʵ������
        DelegateTest dtinstance = new DelegateTest(new Captal8_Delegate_04DelegateChain().methodInstance);

        // ����һ��ί�ж��󣬲���ί�ж���1��ʹ��"+"���ί�ж���2����Ϊί����
        DelegateTest delegateChain = dtstatic;
        delegateChain += dtinstance;

        // ����ί�ж���
        delegateChain.Invoke();

        Console.WriteLine();
        Console.WriteLine("��ί�������Ƴ���̬������");

        // ��ί�������Ƴ���̬����
        delegateChain -= dtstatic;

        delegateChain.Invoke();

        Console.ReadKey();
    }

    // ��̬����
    private static void methodStatic()
    {
        Console.WriteLine("���Ǿ�̬����");
    }

    // ʵ������
    private void methodInstance()
    {
        Console.WriteLine("����ʵ������");
    }
}
```

�����ʾ��

![1553994942925](mdPics\1553994942925.png) 









