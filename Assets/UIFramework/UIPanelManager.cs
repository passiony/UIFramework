using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/// <summary>
/// 1：当前UI引用
/// 2：缓存所有显示的UI
/// 3：缓存所有UI（包括显示和不显示）
/// 4：单例模式
/// 5：显示指定UI
/// 6：隐藏指定UI
/// 7：获得UI
/// </summary>
public class UIPanelManager : SingletonMono<UIPanelManager>
{
    [System.NonSerialized]
    public Transform transUIRoot;//UI根节点
    public Dictionary<EUiId, UIBasePanel> dicAllUI = new Dictionary<EUiId, UIBasePanel>();//所有打开过的UI,包括显示和不显示
    public Dictionary<EUiId, UIBasePanel> dicShowUI = new Dictionary<EUiId, UIBasePanel>();//所有正在显示的UI
    private Transform transUIRootKeepAbove;//保持在最上面UI的根节点
    private Transform transUIRootNormal;//普通UI的根节点

    public void delUI(EUiId id)
    {
        if (dicShowUI.ContainsKey(id))
        {
            dicShowUI.Remove(id);
        }
    }
    protected override void Awake()
    {
        base.Awake();
        init();
    }
    public void init()
    {
        transUIRoot = transform;//画布
        DontDestroyOnLoad(transUIRoot);//不销毁脚本所在的对象
        if (dicAllUI != null) dicAllUI.Clear();
        if (dicShowUI != null) dicShowUI.Clear();
        
        if (transUIRootKeepAbove == null)//主界面根节点
        {
            transUIRootKeepAbove = new GameObject("UIRootKeepAbove").transform;//创建根节点（主界面）对象
            UtilUI.addChildToParent(transUIRoot,transUIRootKeepAbove);//把此对象放在父对象下面
        }
        if (transUIRootNormal == null)//普通UI根节点
        {
            transUIRootNormal = new GameObject("UIRootNormal").transform;
            UtilUI.addChildToParent(transUIRoot, transUIRootNormal);
        }

        //第一个UI
        showUI(EUiId.ID_MainPanel);//显示主界面UI
    }

    public Transform getUIRoot(UIBasePanel ui)//获取此UI所在的根节点
    {
        if (ui.IsKeepAbove)
        {
            return transUIRootKeepAbove;
        }
        else
        {
            return transUIRootNormal;
        }
    }
    /// <summary>
    /// 从容器中获取需要显示的UI
    /// </summary>
    /// <param name="id">传入进来的ID</param>
    /// <returns></returns>
    public UIBasePanel getUI(EUiId id)
    {
        if (dicAllUI.ContainsKey(id))
        {
            return dicAllUI[id];
        }
        else
        {
            return null;
        }

    }

    public void showUI(EUiId id)
    {
        //1：加载当前UI
        if (dicShowUI.ContainsKey(id))//当前UI已经在显示列表中了，就直接返回
        {
            return;
        }
        UIBasePanel ui = getUI(id);//通过ID获取需要显示的UI，从dicAllUI容器中获取
        if (ui == null)//如果在容器中没有此UI，就从资源中读取ui预制体
        {
            string path = UIPath.getUiIdPath(id);//通过ID，获取对应的路径
            if (!string.IsNullOrEmpty(path))
            {
                GameObject prefab = Resources.Load<GameObject>(path);//加载资源
                if (prefab != null)//资源加载成功
                {
                    GameObject goWillShowUI = GameObject.Instantiate(prefab);//克隆游戏对象到层次面板上
                    //goWillShowUI.SetActive(true);
                    ui = goWillShowUI.GetComponent<UIBasePanel>();//获取此对象上的UI
                    if (ui != null)
                    {
                        Transform root = getUIRoot(ui);//获取UI所对应的根节点
                        //放入根节点下面
                        UtilUI.addChildToParent(root, goWillShowUI.transform);//放入根节点下面
                        prefab = null;//清空预制体对象
                    }
                }
                else
                {
                    Debug.LogError("资源" + path + "不存在");
                }
            }
        }
        //2:更新显示其它的UI
        UpdateOtherUIState(ui);

        //3:显示当前UI
        dicAllUI[id] = ui;
        dicShowUI[id] = ui;
        ui.show();
        
    }

    IEnumerator loadPrefab(string path)
    {

        yield return null;
    }
    public void hideUI(EUiId id,Action a=null)//隐藏UI，传入ID和需要做的事情
    {
        if (!dicShowUI.ContainsKey(id))//正在显示的容器中没有此ID
        {
            return;
        }
        if (a == null)//隐藏UI的时候不需要做别的事情
        {
            dicShowUI[id].hide();//直接隐藏
            dicShowUI.Remove(id);//从显示列表中删除
        }
        else//隐藏窗体之后需要做的事情
        {
            a += ()=> { dicShowUI.Remove(id); };
            dicShowUI[id].hide(a);
        }

        //查看当前面板是否隐藏了其他所有的面板，如果是，那么打开主面板 add zl 2017.08.24
        if (dicAllUI[id].showMode == EUIShowMode.hideAll)
        {
            foreach (KeyValuePair<EUiId, UIBasePanel> AboveUI in dicAllUI)
            {
                if (AboveUI.Value.IsKeepAbove)
                {
                    dicShowUI[AboveUI.Key] = AboveUI.Value;
                    AboveUI.Value.show();
                }
            }
        }
        
    }
    public void UpdateOtherUIState(UIBasePanel ui)//更新其它UI的状态（显示或者隐藏）
    {
        if (ui.showMode==EUIShowMode.hideAll)
        {
            hideAllUI(true);//隐藏所有的UI
        }
        else if (ui.showMode == EUIShowMode.hideAllOutAbove)//剔除最前面UI
        {
            hideAllUI(false);//隐藏所有的UI
        }
    }
    public void hideAllUI(bool isHideAbove)//隐藏所有的UI，参数表示是否隐藏最前面主UI
    {
        if (isHideAbove)//隐藏最上面的UI
        {
            foreach (KeyValuePair<EUiId, UIBasePanel> item in dicShowUI)//遍历所有正在显示的UI
            {
                item.Value.hide();
            }
            dicShowUI.Clear();
        }
        else {//不隐藏最上面的主UI
            List<EUiId> listRemove = new List<EUiId>();
            foreach (KeyValuePair<EUiId, UIBasePanel> item in dicShowUI)
            {
                if (item.Value.IsKeepAbove)
                {
                    continue;
                }
                item.Value.hide();
                listRemove.Add(item.Key);
            }
            for (int i = 0; i < listRemove.Count; i++)
            {
                dicShowUI.Remove(listRemove[i]);
            }
            listRemove.Clear();
        }
        
    }
    
}
