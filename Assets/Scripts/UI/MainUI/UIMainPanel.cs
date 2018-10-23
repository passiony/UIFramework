using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// 应用层的ui
/// 1.初始化主面板的按钮
/// 2.初始化按钮的点击事件
/// </summary>
public class UIMainPanel : UIBasePanel
{
    private GameObject buttonRoleBag;//人物背包的按钮对象
    private GameObject butotnSet;//设置界面的按钮对象
    private GameObject buttonStage;//关卡界面的按钮对象
    private GameObject buttonShop;//关卡界面的按钮对象
    public override void OnInit()//用于初始化UI控件
    {
        //给定ID
        this.id = EUiId.ID_MainPanel;//对UI的ID赋值（重点，一定要赋值）
        this.IsKeepAbove = true;//保持在最前面

        //用于查找游戏对象
        buttonRoleBag = UtilUI.findChild(this.gameObject, "BtnRoleBag").gameObject;
        butotnSet = UtilUI.findChild(this.gameObject, "BtnSet").gameObject;
        buttonStage = UtilUI.findChild(this.gameObject, "BtnStages").gameObject;
        buttonShop = UtilUI.findChild(this.gameObject, "BtnShop").gameObject;

        UGUIEventListener.Get(buttonRoleBag).onClick += OnBtnRoleBag;//事件和方法的关联
        UGUIEventListener.Get(butotnSet).onClick = OnBtnSet;
        UGUIEventListener.Get(buttonStage).onClick = OnBtnStage;
        UGUIEventListener.Get(buttonShop).onClick = OnBtnShop;


        //3：如果有动画添加动画
        //显示动画
        animShow = this.transform.DOLocalMoveY(805, 1.0f).From();
        animShow.Pause();
        animShow.SetAutoKill(false);

        //隐藏动画
        animHide = this.transform.DOLocalMoveY(805, 1.0f);
        animHide.Pause();
        animHide.SetAutoKill(false);
        animHide.OnComplete(delegate() { this.gameObject.SetActive(false); });
    }

    #region 通过事件触发的函数
    private void OnBtnShop(GameObject obj)
    {
        UIPanelManager.Instance.showUI(EUiId.ID_ShopPanel);
    }
    private void OnBtnRoleBag(GameObject obj)
    {
        print("OnBtnRoleBag");
        UIPanelManager.Instance.showUI(EUiId.ID_RoleBagPanel);//使用框架里的的显示方式来显示UI
    }
    private void OnBtnSet(GameObject obj)
    {
        print("OnBtnSet");
    }
    private void OnBtnStage(GameObject obj)
    {
        UIPanelManager.Instance.showUI(EUiId.ID_LevelUpPanel);
    }
    #endregion
}