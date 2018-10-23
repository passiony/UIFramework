using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UILoadingPanel : UIBasePanel {
    public override void OnInit()
    {
        base.OnInit();
        this.showMode = EUIShowMode.hideAll;
        this.id = EUiId.ID_LoadingPanel;
    }
    void Update()
    {
    }
}
