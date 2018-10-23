using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;//事件系统的名字空间
/// <summary>
/// 控制背包格子UI
/// </summary>
public class ItemGridUI : MonoBehaviour ,IPointerEnterHandler,IPointerDownHandler, IPointerExitHandler{
    public GameObject prefabItemUI;//物品UI预制体
    public Transform transItemUI;//物品UITransform
    public ItemUI itemUI;//物品UI组件
    public bool isStoreItem//格子上是否已经存储了物品
    {
        get {
            transItemUI = this.transform.Find("ItemUI");
            return transItemUI.gameObject.activeInHierarchy;
        }
    }
	void Start () 
	{
        prefabItemUI = Resources.Load("Prefabs/ItemGoods") as GameObject;
        transItemUI = this.transform.Find("ItemUI");
        itemUI = transItemUI.GetComponent<ItemUI>();
	}
    public int getGoodID()
    {
        if (!isStoreItem) return -1;
        ItemUI gu = transItemUI.GetComponent<ItemUI>();
        return gu.Data.itemProperty.ID;
    }
    /// <summary>
    /// 存储物品，默认存储一个物品
    /// </summary>
    /// <param name="data"></param>
    /// <param name="num"></param>
    public void storeItem(ItemData data, int num = 1)
    {
        if (!isStoreItem)//如果格子还没有存物品
        {
            //加载预制体
            //GameObject obj = GameObject.Instantiate(prefabGoods);
            //obj.transform.SetParent(this.transform);
            //obj.transform.localPosition = Vector3.zero;
            //obj.transform.localScale = Vector3.one;

            //可以把预制体放在格子下面，此方法不需要加载
            transItemUI = this.transform.Find("ItemUI");
            itemUI = transItemUI.GetComponent<ItemUI>();
            if (transItemUI&&!transItemUI.gameObject.activeInHierarchy)
            {
                transItemUI.gameObject.SetActive(true);
                if (data != null)
                {
                    itemUI.setItemData(data, num);
                }
            }
        }
        else//如果格子已经存物品
        {
            if (transItemUI.gameObject.activeInHierarchy == false)
            {
                itemUI.Show();
                itemUI.setItemData(data, num);
            }
            else
            {
                itemUI.addItem();
            }

        }
    }
    /// <summary>
    /// 鼠标按下逻辑处理
    /// 1:物品槽里没有物品	
    ///     if 鼠标指针上有物品
    ///         将指针上的物品放置到该物品槽内
    /// 2:物品槽里有物品	
    ///     if 鼠标指针上有物品
    ///         将物品槽的物品替换指针上的物品
    ///     else
    ///         将该物品槽的物品放置在指针上
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        //print("OnPointerDown");
        if (!isStoreItem)//格子里还没有物品
        {
            if (BagManager.Instance.isPickItem)
            {
                storeItem(BagManager.Instance.MouseItemUI.Data, BagManager.Instance.MouseItemUI.Num);
                BagManager.Instance.resetMouseItemUI();
                //BagManager.Instance.isPickItem = false;
            }
        }
        else {//格子里已经有物品了
            if (BagManager.Instance.isPickItem)//鼠标上有拾取的物品
            {
                itemUI.setItemData(BagManager.Instance.MouseItemUI.Data, BagManager.Instance.MouseItemUI.Num);
                BagManager.Instance.resetMouseItemUI();
                //BagManager.Instance.isPickItem = false;
            }
            else //鼠标上没有物品
            {

                BagManager.Instance.MouseItemUI.replaceItemUI(itemUI);//把物品格子中的UI放到鼠标指针上面
                //Destroy(transItemUI.gameObject);
                transItemUI.gameObject.SetActive(false);
                BagManager.Instance.isPickItem = true;
            }
        }
    }

    /// <summary>
    /// 用于隐藏物品提示
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isStoreItem)
        {
            BagManager.Instance.hideItemTip();
        }
    }
    /// <summary>
    /// 用于显示物品提示
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isStoreItem)
        {
            string content = itemUI.Data.GetDescribe();
            BagManager.Instance.showItemTip(content);
        }
    }
}
