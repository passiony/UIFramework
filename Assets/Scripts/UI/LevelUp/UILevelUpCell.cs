using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HearInfo
{
    public int level;
    public int num;
    public string name;
    public int hp;
    public int mp;
    public int damage;
}

public class UILevelUpCell : MonoBehaviour {

    public Text level;
    public Image head;
    public Image progress;

    //根据json读取的数据，初始化cell
    public void Init(HearInfo info)
    {

    }

    public void SetLevel()
    {

    }
    public void SetHead()
    {

    }

    public void SetProgress()
    {

    }
}
