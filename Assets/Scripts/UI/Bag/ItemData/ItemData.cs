using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 物品基类
/// </summary>
public class ItemData {
    public ItemConfig itemProperty { get; set; }

    public ItemData()
    { 
    }

    public ItemData(ItemConfig property)
    {
        this.itemProperty = property;
    }
    public virtual string GetDescribe()
    {
        string color = Enum.GetName(typeof(ItemQuality),itemProperty.Quality);
        //switch (goodProperty.goodQuality)
        //{
        //    case GoodQuality.white:
        //        color = "白色";
        //        break;
        //    case GoodQuality.green:
        //        color = "绿色";
        //        break;
        //    case GoodQuality.blue:
        //        color = "蓝色";
        //        break;
        //    case GoodQuality.purple:
        //        color = "紫色";
        //        break;
        //    case GoodQuality.orange:
        //        color = "橙色";
        //        break;
        //    default:
        //        break;
        //}

        string describe = string.Format("<color={0}>{1}</color>\n<size=10><color=green>购买价格：{2} 出售价格：{3}</color></size>\n<color=yellow><size=10>{4}</size></color>", color, itemProperty.Name, itemProperty.PriceBuy, itemProperty.PriceSell, itemProperty.Description);

        return describe;
    }
}
