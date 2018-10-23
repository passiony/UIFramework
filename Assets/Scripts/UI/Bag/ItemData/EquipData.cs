using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EquipData:ItemData
{
    public int strength;    //力量
    public int intellect;   //智力
    public int agility;     //敏捷
    public int stamina;     //体力
    public EquipType equipType;
    public EquipData(int strength, int intellect, int agility, int stamina, EquipType equitmentType, ItemConfig baseProperty)
        : base(baseProperty)
    {
        this.strength = strength;
        this.intellect = intellect;
        this.agility = agility;
        this.stamina = stamina;
        this.equipType = equitmentType;
    }
    public override string GetDescribe()
    {
        string baseDescribe = base.GetDescribe();//获取父类中的描述信息

        string equitmentDescribe = string.Format("{0}\n\n<color=blue>装备类型：{1}\n力量：{2}\n智力：{3}\n敏捷：{4}\n体力：{5}</color>", baseDescribe, "兵器", strength, intellect, agility, stamina);

        return equitmentDescribe;
    }
}