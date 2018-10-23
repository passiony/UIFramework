using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public class UIRoleBagPanel : UIBasePanel {

    private GameObject goBtnClose;
    public override void OnInit()
    {
        //1:必须给定ID
        this.id = EUiId.ID_RoleBagPanel;
        //2：获取组件,如果有事件添加事件
        goBtnClose = UtilUI.findChild(this.gameObject,"BtnClose").gameObject;
        UGUIEventListener.Get(goBtnClose).onClick = OnBtnClose;
        //3：如果有动画添加动画
        //显示动画
        animShow=this.transform.DOLocalMoveY(805, 1.0f).From();
        animShow.Pause();
        animShow.SetAutoKill(false);

        //隐藏动画
        animHide = this.transform.DOLocalMoveY(805, 1.0f);
        animHide.Pause();
        animHide.SetAutoKill(false);
        animHide.OnComplete(delegate() { this.gameObject.SetActive(false); });

        
    }
    public override void OnShow(object data = null)//显示UI的时候需要做的事情
    {
        base.OnShow(data);

    }

    public override void OnHide()//隐藏UI需要做的事情
    {
        base.OnHide();
        
    }

    #region 按钮调用的事件函数
    private void OnBtnClose(GameObject obj)
    {
        UIPanelManager.Instance.hideUI(this.id);
    }
    #endregion
}
