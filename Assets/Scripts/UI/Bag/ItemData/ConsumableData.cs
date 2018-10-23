using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 消耗品
/// </summary>
class ConsumableData:ItemData
{
    public int HP { get; set; }
    public int MP { get; set; }

    public ConsumableData()
    { 
    }

    public ConsumableData(int hp, int mp, ItemConfig property)
        : base(property)
    {
        this.HP = hp;
        this.MP = mp;
    }

	public override string GetDescribe()
	{
		string baseDescribe = base.GetDescribe();

		string consumabelDescribe = string.Format("{0}\n\n<color=blue>加血：{1}\n加蓝：{2}</color>", 
            baseDescribe, HP, MP);

		return consumabelDescribe;
	}
}