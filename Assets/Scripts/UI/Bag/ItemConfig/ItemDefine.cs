using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum ItemType
{
    Consumable, //消耗品
    Equipment,  //装备
    Weapon,     //武器
    Meterial    //材料
}

/// <summary>
/// 物品品质:白色、绿色、蓝色、紫色、橙色
/// </summary>
public enum ItemQuality
{
    //Common,     //普通
    //Uncommon,   //不普通
    //Rare,       //稀有
    //Epic,       //史诗
    //Legendary   //传奇
    white,
    green,
    blue,
    purple,
    orange
}
public enum EquipType
{
    None,
    Head,
    Neck,
    chest,
    Ring,
    Leg,
    Bracer,
    Boots,
    Shoulder,
    Belt,
    OffHand
}
public enum WeaponType
{
    None,
    OffHand,
    MainHand
}