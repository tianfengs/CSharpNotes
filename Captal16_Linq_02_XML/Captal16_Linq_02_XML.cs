using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Captal16_Linq_02_XML
{
    using System.Xml;
    using System.Xml.Linq;

    class Captal16_Linq_02_XML
    {
        static void Main(string[] args)
        {
            XPathQuery();

            LinqQuery();

            Console.ReadKey();
        }

        private static void XPathQuery()
        {
            // 导入XML文件
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("nameInfo.xml");

            // 创建查询XML文件的XPath
            string xPath = "/Persons/Person";

            // 查询Person节点元素和属性
            XmlNodeList querynodes = xmlDoc.SelectNodes(xPath);
            foreach(XmlNode node in querynodes)
            {
                // 查询名字为“李四”的元素
                foreach(XmlNode childnode in node.ChildNodes)
                {
                    if (childnode.InnerXml == "李四")
                    {
                        // 输出查询结果
                        Console.WriteLine($"姓名为{childnode.InnerXml}" +
                            $", Id为{node.Attributes["Id"].Value}" +
                            $", 年龄为{node.SelectSingleNode("Age").InnerText}");
                    }
                }
            }
        }

        private static void LinqQuery()
        {
            // 导入XML文件
            XElement xDoc = XElement.Load("nameInfo.xml");

            // 创建查询，获取姓名为“李四”的元素
            var queryResult = from element in xDoc.Elements("Person")
                                where element.Element("Name").Value == "李四"
                                select element;

            // 输出查询结果
            foreach (var xele in queryResult)
            {
                Console.WriteLine($"姓名为{xele.Element("Name").Value}" +
                    $", Id为{xele.Attribute("Id").Value}" +
                    $", 年龄为{xele.Element("Age").Value}");
            }
        }
    }
}

