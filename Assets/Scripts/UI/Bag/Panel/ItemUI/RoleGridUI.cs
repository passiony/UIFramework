using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
/// <summary>
/// 人物属性的格子
/// </summary>
public class RoleGridUI : ItemGridUI
{

    public EquipType equipType;
    public WeaponType weaponType;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// if 鼠标上有装备
    ///     if 人物格子上有装备
    ///         if 符合该格子的装备类型
    ///             交换
    ///     if  人物格子上没有装备
    ///         if  符合该格子的装备类型
    ///             放入
    /// else 鼠标上没有装备
    ///     if  人物格子上有装备
    ///         将该物品放入鼠标指针
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        if (BagManager.Instance.isPickItem)//鼠标上有装备
        {
            if (isStoreItem)//人物格子上有装备
            {
                if (isMatchThisGrid(BagManager.Instance.MouseItemUI.Data))//符合该格子的装备类型
                {
                    this.itemUI.exchangeItemUI(BagManager.Instance.MouseItemUI);//交换
                    BagManager.Instance.resetMouseItemUI();
                }
            }
            else//人物格子上没有装备
            {
                if (isMatchThisGrid(BagManager.Instance.MouseItemUI.Data))////符合该格子的装备类型
                {
                    storeItem(BagManager.Instance.MouseItemUI.Data, BagManager.Instance.MouseItemUI.Num);
                    BagManager.Instance.resetMouseItemUI();
                }

            }
        }
        else//鼠标上没有装备 
        {
            if (isStoreItem)//人物格子上有装备
            {
                //将该物品放入鼠标指针
                BagManager.Instance.MouseItemUI.setItemData(this.itemUI.Data,this.itemUI.Num);
                BagManager.Instance.isPickItem = true;
                transItemUI.gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// 判断当前选择的物品是否和格子能够匹配得上
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private bool isMatchThisGrid(ItemData data)
    {
        if (data is EquipData && ((EquipData)data).equipType == this.equipType
          || data is WeaponData && ((WeaponData)data).weaponType == this.weaponType)
        {
            return true;
        }

        return false;
    }
}
