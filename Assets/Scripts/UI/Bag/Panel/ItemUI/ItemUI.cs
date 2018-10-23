using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 背包格子里的内容（物品的图标，物品的数量，物品的品质）
/// </summary>
public class ItemUI : MonoBehaviour
{
    public ItemData Data { get; private set; }//物品槽中存放的物品
    public int Num { get; private set; }//物品槽存放物品的数量
    
    private Image icon;//物品ICON
    private Text txtNum;//物品的数量
	void Awake()
	{
		
	}

	void Start () 
	{
        
	}
	
	void Update () 
	{
	
	}
    public void addItem(int num = 1)
    {
        this.Num += num;
        if (txtNum)
        {
            if (Data.itemProperty.Capacity > 1)
            {
                txtNum.text = this.Num.ToString();
            }
            else
            {
                txtNum.text = "";
            }
        }
        
    }
    /// <summary>
    /// 设置数据
    /// </summary>
    /// <param name="data"></param>
    /// <param name="num"></param>
    public void setItemData(ItemData data, int num = 1)
    {
        this.Data = data;
        this.Num = num;
        //查找孩子中的组件
        icon = transform.GetComponentInChildren<Image>();
        txtNum = transform.GetComponentInChildren<Text>();
        if (icon)
        {
            //通过配置数据去资源目录下读取图片
            icon.sprite = Resources.Load<Sprite>(Data.itemProperty.Sprite);
        }
        if (txtNum)
        {
            if (Data.itemProperty.Capacity > 1)
            {
                txtNum.text = this.Num.ToString();
            }
            else
            {
                txtNum.text = "";
            }
        }
        
    }
    /// <summary>
    /// 替换物品显示
    /// </summary>
    /// <param name="itemUI"></param>
    public void replaceItemUI(ItemUI itemUI)
    {
        this.Data = itemUI.Data;
        this.Num = itemUI.Num;
        setItemData(this.Data, this.Num);
    }
    /// <summary>
    /// 两个格子上的物品进行交换
    /// </summary>
    /// <param name="itemUI"></param>
    public void exchangeItemUI(ItemUI itemUI)
    {
        ItemData tempGood = itemUI.Data;
        int tempAmount = itemUI.Num;

        itemUI.setItemData(Data, Num);
        this.setItemData(tempGood, tempAmount);
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
