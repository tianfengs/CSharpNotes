using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital17_02_COM
{
    using Microsoft.Office.Interop.Word;
    using System.Runtime.InteropServices;

    /// <summary>
    /// COM互操作中的错误处理
    /// 打开一个现有的Word文档并插入相应的文本，当指定的Word文档不存在时，此时就会出现调用COM对象的Open方法失败的情况
    /// </summary>
    class Capital17_02_COM
    {
        static void Main(string[] args)
        {
            // 打开存在的文档，插入文本
            object filename = Environment.CurrentDirectory + @"NewCOMCallMethod.doc";
            OpenWordDocument(filename);

            Console.ReadKey();
        }

        private static void OpenWordDocument(object filename)
        {
            // 启动Word应用程序
            Application wordApp = new Application() { Visible = true };
            Document wordDoc = null;
            try
            {
                wordDoc = wordApp.Documents.Open(filename);

                Range wordRange = wordDoc.Range(0, 0);

                wordRange.Text = "这是插入的文本";

                wordDoc.Save();
            }
            catch(Exception ex)
            {
                // COM中根据方法返回的HRESULT来判断调用是否成功
                int HResult = Marshal.GetHRForException(ex);

                // 设置控制台前景色，即输出文本的颜色
                Console.ForegroundColor = ConsoleColor.Red;

                // 把HRESULT的值以16进制输出
                Console.WriteLine("调动异常，异常类型为：{0}，HRESULT= 0x{1：x}", ex.GetType().Name, HResult);
                Console.WriteLine("异常信息为：" + ex.Message.Replace('\r', ' '));
            }
            finally
            {
                // 关闭文档
                if (wordDoc != null)
                {
                    wordDoc.Close();
                }

                // 退出Word程序
                wordApp.Quit();
            }
        }
    }
}
