using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIStagesPanel : UIBasePanel
{
    private GameObject buttonClose;
    public override void OnInit()
    {
        base.OnInit();
        this.id = EUiId.ID_StagesPanel;
        buttonClose = UtilUI.findChild(this.gameObject, "BtnClose").gameObject;
        UGUIEventListener.Get(buttonClose).onClick = OnClose;

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

    private void OnClose(GameObject obj)
    {
        print("OnClose");
        UIPanelManager.Instance.hideUI(this.id);
    }

}
