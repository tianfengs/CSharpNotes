using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital17_01_COM
{
    using Microsoft.Office.Interop.Word;

    /// <summary>
    /// 通过调用使用和COM的互操作性方法，
    /// 实现建立Word文档，并在其中加入一句话："欢迎你，使用新的调用COM对象的方法，建立Word文档。"
    /// </summary>
    class Capital17_01_COM
    {
        static void Main(string[] args)
        {
            NewCOMCallMethod4();

        }

        static void NewCOMCallMethod4()
        {
            //Object missing = Type.Missing;

            // 启动Word应用程序并使Word可见
            Application wordApp = new Application() { Visible = false };

            // 新建一个Word文档
            wordApp.Documents.Add();
            Document wordDoc = wordApp.ActiveDocument;

            // 添加一个段落
            Paragraph para = wordDoc.Paragraphs.Add();
            para.Range.Text = "欢迎你，使用新的调用COM对象的方法，建立Word文档。";

            // 保存文档
            object filename = Environment.CurrentDirectory + @"NewCOMCallMethod.doc";
            object format = WdSaveFormat.wdFormatDocument97;
            wordDoc.SaveAs2(ref filename, ref format);

            // 关闭Word文档和Word应用程序
            wordDoc.Close();
            wordApp.Application.Quit();
        }

        static void OldCOMCallMethod()
        {
            Object missing = Type.Missing;

            // 启动Word应用程序并使Word可见
            Application wordApp = new Application { Visible = true };

            // 新建一个Word文档
            wordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);
            Document wordDoc = wordApp.ActiveDocument;

            // 添加一个段落
            Paragraph para = wordDoc.Paragraphs.Add(ref missing);
            para.Range.Text = "欢迎你，使用旧的调用COM对象的方法，建立Word文档。";

            // 保存文档
            object filename = @"OldCOMCallMethod.doc";
            object format = WdSaveFormat.wdFormatDocument97;
            wordDoc.SaveAs2(ref filename, ref format,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing);

            // 关闭Word文档和Word应用程序
            wordDoc.Close(ref missing, ref missing, ref missing);
            wordApp.Application.Quit(ref missing, ref missing, ref missing);
        }        
    }
}
