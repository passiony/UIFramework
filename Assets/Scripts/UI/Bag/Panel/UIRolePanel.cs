using UnityEngine;
using System.Collections;

public class UIRolePanel : UIItemsPanel {

    private static UIRolePanel instance;
    public static UIRolePanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("UIRole").GetComponent<UIRolePanel>();
            }
            return instance;
        }
    }
}
