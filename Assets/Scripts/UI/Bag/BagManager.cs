
using LitJson;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 单例模式
/// 1：解析物品数据（json）
/// 2：管理面板的显示和隐藏（物品描述面板）
/// 3：鼠标上的物品UI显示
/// 
/// </summary>
class BagManager:MonoBehaviour
{
    private Animator animator;//动画状态控制机

    private static BagManager instance;//单列模式
    public static BagManager Instance//属性的封装
    {
        get { return BagManager.instance; }
    }
    public GameObject goMouseItem;//鼠标上的UI
    private Vector3 oldPickPosition;//鼠标上的UI所在的初始位置（一般都放在屏幕外面）
    public bool isPickItem;//鼠标上是否已经有物品
    private GameObject canvas;//UI画布

    private List<ItemData> listItemData;//所有的数据列表
    public ItemUI MouseItemUI//获取鼠标上的物品UI
    {
        get { return goMouseItem.GetComponent<ItemUI>(); }
    }

    public GameObject goUIItemTip;//物品提示面板
    public bool isShowItemTip;//是否显示物品提示框
    void Awake()
    {
        instance = this;
        parseItemsJson();//加载数据
    }
    void Start()
    {
        canvas = GameObject.Find("UI");
        goMouseItem = GameObject.Find("PickItemUI");
        oldPickPosition = goMouseItem.transform.localPosition;//局部坐标
        goUIItemTip = GameObject.Find("UIItemTip");
        animator = this.GetComponent<Animator>();//获取动画状态机
    }
    public void resetMouseItemUI()//重置鼠标上UI的位置
    {
        goMouseItem.transform.localPosition = oldPickPosition;
        isPickItem = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            int rand = Random.Range(1, listItemData.Count+1);
            UIBagPanel.Instance.storeItemWithID(rand);//添加物品
        }
        if (isPickItem)//鼠标已经拾取物品了
        {
            Vector2 point = Vector2.zero;
            //该函数是将屏幕坐标转化以第一个参数对象的子节点坐标
            //参数一：需要转换的坐标以该对象作为父节点
            //参数二：鼠标点
            //参数三：参数一对象以哪个摄像机渲染（由于该参数一画布没有相机渲染，故为null）
            //参数四：返回一个需要转换的目标点
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<Canvas>().transform as RectTransform, 
                Input.mousePosition, null, out point);
            goMouseItem.transform.localPosition = point;
        }
        //EventSystem.current.IsPointerOverGameObject()：判断是否点击到了游戏中的3D物体
        if (isPickItem && Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            isPickItem = false;
            resetMouseItemUI();

            hideItemTip();
        }
        if (isShowItemTip)//显示物品提示UI
        {
            Vector2 point = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<Canvas>().transform as RectTransform,
                Input.mousePosition, null, out point);
            goUIItemTip.GetComponent<UIItemTip>().SetLocalPosition(point);
        }
    }

    /// <summary>
    /// 解析JSON数据文件
    /// 物品数据都放入了容器中listItemData
    /// </summary>
    void parseItemsJson()
    {
        listItemData = new List<ItemData>();
        //第一种数据读取方式
        //Application.streamingAssetsPath
        string path = Application.streamingAssetsPath + "/Item.txt";
        string json = "";//保存文本内容

        //StreamReader sr = new StreamReader(path);
        //json = sr.ReadToEnd();
        //sr.Close();
        //第二种数据读取方式
        TextAsset s = Resources.Load("ItemData/Item") as TextAsset;
        json = s.text;
        //Debug.Log(json);

        JsonData data = JsonMapper.ToObject(json);//把字符串文本解析成对象
        
        int i = 0;
        for (i = 0; i <= data.Count - 1; i++)
        {
            ItemConfig gp = JsonMapper.ToObject<ItemConfig>(data[i]["ItemConfig"].ToJson());

            ItemData kg = null;
            switch (gp.Type)
            {
                case ItemType.Consumable://消耗品
                    int HP = int.Parse(data[i]["HP"].ToString());
                    int MP = int.Parse(data[i]["MP"].ToString());
                    kg = new ConsumableData(HP, MP, gp);
                    break;
                case ItemType.Equipment://装备
                    int strength = int.Parse(data[i]["strength"].ToString());
                    int intellect = int.Parse(data[i]["intellect"].ToString());
                    int agility = int.Parse(data[i]["agility"].ToString());
                    int stamina = int.Parse(data[i]["stamina"].ToString());
                    EquipType equitmentType = (EquipType)System.Enum.Parse(typeof(EquipType), data[i]["equipType"].ToString());
                    kg = new EquipData(strength, intellect, agility, stamina, equitmentType, gp);

                    break;
                case ItemType.Meterial://材料

                    break;
                case ItemType.Weapon://武器
                    int damage = int.Parse(data[i]["damage"].ToString());
                    WeaponType weaponType = (WeaponType)System.Enum.Parse(typeof(WeaponType), data[i]["weaponType"].ToString());
                    kg = new WeaponData(damage, weaponType, gp);

                    break;
                default:
                    break;
            }

            listItemData.Add(kg);//放入容器中
        }
    }

    public ItemData getItemDataWithId(int id)
    {
        foreach (ItemData data in listItemData)
        {
            if (id == data.itemProperty.ID)
            {
                return data;
            }
        }

        return null;
    }
    public void showItemTip(string str = " ")
    {
        isShowItemTip = true;
        goUIItemTip.GetComponent<UIItemTip>().showPanel(str);
    }

    public void hideItemTip()
    {
        isShowItemTip = false;
        goUIItemTip.GetComponent<UIItemTip>().hidePanel();
    }
    public void OnBtnClose()
    {
        animator.SetBool("IsOut",true);//设置出去的变量为true,就会自动切换动画状态
    }
    public void OnShowPanel()
    {
        animator.SetBool("IsFirst",true);
        animator.SetBool("IsOut", false);//不出去，那么就进来
    }
    public void printMsg()
    {
        print("UI进入动画播放到了一半");
    }
    public void printMsgEnd()
    {
        print("UI进入动画播放完成");
    }
}
