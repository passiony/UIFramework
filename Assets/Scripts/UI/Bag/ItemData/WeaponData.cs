using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class WeaponData : ItemData
{
    public int damage { get; set; }
    public WeaponType weaponType { get; set; }
    public WeaponData(int damage, WeaponType weaponType, ItemConfig baseProperty)
        : base(baseProperty)
    {
        this.damage = damage;
        this.weaponType = weaponType;
    }
    public override string GetDescribe()
    {
        string baseDecribe = base.GetDescribe();

        string weaponDescribe = string.Format("{0}\n\n<color=blue>武器类型：{1}\n攻击力：{2}</color>", "武器", baseDecribe, weaponType.ToString(), damage);

        return weaponDescribe;
    }
}