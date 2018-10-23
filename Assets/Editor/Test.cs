using UnityEngine;
using System.Collections.Generic;
using LitJson;
using System.IO;
using UnityEditor;
using System.Text;

public class Test : Editor {

	[MenuItem("Tools/Make Hero Json")]
	public static void Start () {

        List<HearInfo> list = new List<HearInfo>();

        HearInfo info1 = new HearInfo();
        info1.name = "雷德罗";
        info1.level = 33;
        info1.hp = 44;
        list.Add(info1);

        HearInfo info2 = new HearInfo();
        info2.name = "雷德罗";
        info2.level = 33;
        info2.hp = 44;
        list.Add(info2);

        HearInfo info3 = new HearInfo();
        info3.name = "雷德罗";
        info3.level = 33;
        info3.hp = 44;
        list.Add(info3);

        HearInfo info4 = new HearInfo();
        info4.name = "雷德罗";
        info4.level = 33;
        info4.hp = 44;
        list.Add(info4);

        string json = JsonMapper.ToJson(list);

        FileStream stream = new FileStream(@"‪hero.txt", FileMode.Create);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        stream.Write(bytes, 0, bytes.Length);
        stream.Close();
    }
}
