using ClassLibrary1.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace ClassLibrary1
{
    class Class1
    {
        static void Main(string[] args)
        {
            var nodes=FindOpenData();
            ShowOpenData(nodes);
            Console.ReadKey();
        }

        static List<OpenData> FindOpenData()
        {
            List<OpenData> result = new List<OpenData>();
            var xml = XElement.Load(@"https://apiservice.mol.gov.tw/OdService/download/A17000000J-020104-62d");
            var nodes = xml.Descendants("row").ToList();

            for(var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                OpenData item = new OpenData();
                item.縣市別 = getValue(node, "縣市別");
                item.應開戶數 = getValue(node, "應開戶數");
                item.已開戶數 = getValue(node, "已開戶數");
                result.Add(item);
            }



            return result;
        }


        private static string getValue(XElement node,string propertyName)
        {
            return node.Element(propertyName)?.Value?.Trim();
        }


        public static void ShowOpenData(List<OpenData> nodes)
        {
            Console.WriteLine(string.Format("共收到{0}筆的資料", nodes.Count));
            nodes.GroupBy(node => node.縣市別).ToList().ForEach(group =>
            {
                var key = group.Key;
                var groupDatas = group.ToList();
                var message = $"縣市別:{key},共有{groupDatas.Count()}筆資料";
                Console.WriteLine(message);

            });

            nodes.GroupBy(node => node.應開戶數).ToList().ForEach(group =>
            {
                var key = group.Key;
                var groupDatas = group.ToList();
                var message = $"應開戶數:{key},共有{groupDatas.Count()}筆資料";
                Console.WriteLine(message);

            });

            nodes.GroupBy(node => node.已開戶數).ToList().ForEach(group =>
            {
                var key = group.Key;
                var groupDatas = group.ToList();
                var message = $"已開戶數:{key},共有{groupDatas.Count()}筆資料";
                Console.WriteLine(message);

            });
        }
    }
}
