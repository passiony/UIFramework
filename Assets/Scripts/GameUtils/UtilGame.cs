using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;

public class UtilGame {
    private UtilGame() { }
    public static void loadXml(string path)
    {
        FileInfo file = new FileInfo(path);
        StreamReader sr = new StreamReader(file.OpenRead(), Encoding.UTF8);//读取到流中
        XmlDocument doc = new XmlDocument();//读取XML文档
        doc.Load(sr);
        string name= doc.SelectSingleNode("/Level/Name").InnerText;//关卡名字
        string cardImage = doc.SelectSingleNode("/Level/CardImage").InnerText;//卡片名字
        string background = doc.SelectSingleNode("/Level/Background").InnerText;//背景图片
        string road = doc.SelectSingleNode("/Level/Road").InnerText;//路的图片名字
        int initScore = int.Parse(doc.SelectSingleNode("/Level/InitScore").InnerText);//初始分数
        XmlNodeList nodes;//获取相同子节点
        nodes = doc.SelectNodes("/Level/Holder/Point");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];//取子节点
            int.Parse(node.Attributes["X"].Value);//取Point节点下的X属性
            int.Parse(node.Attributes["Y"].Value);//取Point节点下的Y属性
        }

        nodes = doc.SelectNodes("/Level/Path/Point");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];
            int.Parse(node.Attributes["X"].Value);
            int.Parse(node.Attributes["Y"].Value);
        }

        nodes = doc.SelectNodes("/Level/Rounds/Round");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];

            int.Parse(node.Attributes["Monster"].Value);
            int.Parse(node.Attributes["Count"].Value);
        }

        sr.Close();
        sr.Dispose();
    }
}
