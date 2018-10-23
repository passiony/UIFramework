using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 物品数据的基本属性
/// </summary>
public class ItemConfig
{
    public int ID { get; set; }//物品ID
    public string Name { get; set; }//物品的名字
    public string Description { get; set; }//物品的描述
    public int Capacity { get; set; }//物品在格子中的容量
    public int PriceBuy { get; set; }//购买的价格
    public int PriceSell { get; set; }//卖出去的价格
    public string Sprite { get; set; }//物品的图片名字
    public ItemType Type { get; set; }//物品类型
    public ItemQuality Quality { get; set; }//物品的品质

    public ItemConfig()
    {}

    public ItemConfig(int ID, string name, string description, int capacity, int buyPrice, int sellPrice, string sprite, ItemType goodType,ItemQuality goodQuality)
    {
        this.ID = ID;
        this.Name = name;
        this.Description = description;
        this.Capacity=capacity;
        this.PriceBuy = buyPrice;
        this.PriceSell = sellPrice;
        this.Sprite = sprite;
        this.Type = goodType;
        this.Quality = goodQuality;
    }
}