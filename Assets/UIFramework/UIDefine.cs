using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum EUiId//所有UI的ID定义
{ 
    Null,
    ID_MainPanel,//主UI的ID
    ID_RoleBagPanel,//人物背包的ID
    ID_LoadingPanel,//加载资源ID
    ID_SettingPanel,//设置面板ID
    ID_ShopPanel,//商店面板
    ID_StagesPanel,//关卡选择ID
    ID_LevelUpPanel
}

public enum EUIShowMode
{ 
    doNothing,//当前UI显示出来的时候其它UI不做任何操作
    hideAllOutAbove,//除了最前面的都隐藏
    hideAll//隐藏所有的UI，包括最前面的
}
public class UIPath
{
    private static Dictionary<EUiId, string> dicPath = new Dictionary<EUiId, string>
    {
        {EUiId.ID_MainPanel,"PrefabUI/UIMainPanel"},
        {EUiId.ID_RoleBagPanel,"PrefabUI/UIRoleBagPanel"},
        {EUiId.ID_LoadingPanel,"PrefabUI/UILoadingPanel"},
        {EUiId.ID_StagesPanel,"PrefabUI/UIStagesPanel"},
        {EUiId.ID_ShopPanel,"PrefabUI/UIShopPanel"},
        {EUiId.ID_LevelUpPanel,"PrefabUI/UILevelUpPanel"},
    };
    public static string getUiIdPath(EUiId id)
    {
        if (dicPath.ContainsKey(id))
        {
            return dicPath[id];
        }
        return null;
    }
}