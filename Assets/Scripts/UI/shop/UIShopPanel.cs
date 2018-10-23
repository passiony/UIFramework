using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class UIShopPanel : UIBasePanel {
    private GameObject goBtnClose;
    public override void OnInit()
    {
        base.OnInit();
        //1：指定ID
        this.id = EUiId.ID_ShopPanel;
        goBtnClose = UtilUI.findChild(this.gameObject, "BtnClose").gameObject;
        UGUIEventListener.Get(goBtnClose).onClick = OnBtnClose;

        //3：如果有动画添加动画
        //显示动画
        animShow = this.transform.DOLocalMoveY(805, 1.0f).From();
        animShow.Pause();
        animShow.SetAutoKill(false);

        //隐藏动画
        animHide = this.transform.DOLocalMoveY(805, 1.0f);
        animHide.Pause();
        animHide.SetAutoKill(false);
        animHide.OnComplete(()=> { this.gameObject.SetActive(false); });

    }

    private void OnBtnClose(GameObject obj)
    {
        UIPanelManager.Instance.hideUI(this.id);
    }

}
