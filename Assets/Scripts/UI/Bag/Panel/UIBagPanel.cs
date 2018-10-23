using UnityEngine;
using System.Collections;

public class UIBagPanel : UIItemsPanel {

    /// <summary>
    /// 单例模式
    /// </summary>
    private static UIBagPanel instance;
    public static UIBagPanel Instance
    {
        get {
            if (instance == null)
            {
                instance = GameObject.Find("UIBagPanel").GetComponent<UIBagPanel>();
            }
            return instance;
        }
    }
}
