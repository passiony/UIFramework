using UnityEngine;
using System.Collections;
/// <summary>
/// 存放物品
/// </summary>
public class UIItemsPanel : MonoBehaviour {
    public ItemGridUI[] grids;//面板上所有的背包格子
    //显示和隐藏效果
    private CanvasGroup cg;
    private float lerpSpeed = 2.0f;
    private float targetAlpha = 1;
    protected virtual void Start()
    {
        grids = this.GetComponentsInChildren<ItemGridUI>();
        cg = this.GetComponent<CanvasGroup>();
    }
	void Update () {
        if (targetAlpha != this.cg.alpha)
        {
            this.cg.alpha = Mathf.Lerp(this.cg.alpha, targetAlpha, lerpSpeed * Time.deltaTime);
            if (Mathf.Abs(this.cg.alpha - targetAlpha) <= 0.01f)
            {
                this.cg.alpha = targetAlpha;
            }
        }
	}
    /// <summary>
    /// 根据物品数据来存储物品
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool storeItemWithData(ItemData item)
    {
        if (item == null) return false;
        if (item.itemProperty.Capacity == 1)//如果该物品在物品槽中只能存放一个
        {
            ItemGridUI grid = FindEmptyGridUI();//直接找到一个还没有使用的物品格子
            if (grid != null)//如果找到了空的物品槽，将该物品放进去格子里
            {
                grid.storeItem(item);//在格子上存放物品
            }
            else//如果没有找到，则将说明背包以满
            {
                Debug.Log("没有找到空的物品槽，可能是背包以满");
                return false;
            }
        }
        else//物品可以叠加
        {
            //查找已经存放了该物品的格子
            ItemGridUI grid = FindFreeSlot(item.itemProperty.ID);
            if (grid != null)
            {
                grid.storeItem(item);//把物品存放到格子中
            }
            else
            {
                ItemGridUI s = FindEmptyGridUI();//查找一个空格子
                if (s != null)
                {
                    s.storeItem(item);//存放物品
                }
                else
                {
                    Debug.Log("没有找到空的物品槽，可能是背包以满");
                    return false;
                }
            }
        }
        return true; ;
    }
    /// <summary>
    /// 根据物品ID来存储物品
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool storeItemWithID(int id)
    {
        ItemData good = BagManager.Instance.getItemDataWithId(id);//从物品数据中心获取到一个数据
        return storeItemWithData(good);
    }
    /// <summary>
    /// 根据ID寻找存储了该物品的物品槽
    /// </summary>
    /// <returns></returns>
    private ItemGridUI FindFreeSlot(int ID)
    {
        foreach (ItemGridUI s in grids)
        {
            if (s.isStoreItem && s.getGoodID() == ID)
            {
                return s;
            }
        }

        return null;
    }
    private ItemGridUI FindEmptyGridUI()
    {
        foreach (ItemGridUI s in grids)
        {
            if (!s.isStoreItem)//没有存放数据
            {
                return s;
            }
        }

        //没有找到空的物品槽，说明背包以满
        return null;
    }
    public void show()
    {
        targetAlpha = 1;
        this.cg.blocksRaycasts = true;
    }


    public void hide()
    {
        targetAlpha = 0;
        this.cg.blocksRaycasts = false;
    }
}
