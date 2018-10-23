using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;//引入动画控制的命名空间
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;
/// <summary>
/// UI的基类
/// 1：UI的ID
/// 2：显示UI
/// 3：隐藏UI
/// 4：数据和UI控件的初始化
/// 5：UI的层级关系
/// 6：UI的显示模式（当前UI显示的时候其它UI要显示还是隐藏）
/// 7：UI显示的动画效果（进来，出去）
/// </summary>
public class UIBasePanel : MonoBehaviour {

    protected EUiId id = EUiId.Null;//当前的UI的id
    protected EUiId idBefore = EUiId.Null;//当前界面的上一个界面
    protected Transform thisTrans;//保存当前Transform对象
    protected Tweener animShow;//UI显示时的动画
    protected Tweener animHide;//UI隐藏时的动画
    private bool isKeepAbove = false;//主要是用在主界面和菜单显示，一直不消失，除非特殊情况

    public void setHide()
    {
        this.gameObject.SetActive(false);
        UIPanelManager.Instance.delUI(this.id);
    }
    public bool IsKeepAbove
    {
        get { return isKeepAbove; }
        set { isKeepAbove = value; }
    }
    public EUIShowMode showMode=EUIShowMode.doNothing;//ui的显示模式（打开什么也不干，隐藏所有其它的UI，除了主界面UI隐藏所有其它UI）
    public virtual void OnInit()//1：用来初始化一些数据和UI组件
    {
    }
    public virtual void OnShow(System.Object data = null)//2：显示UI时候调用的方法
    { 
        
    }
    public virtual void OnHide()//3：隐藏UI时候调用的方法
    { 
    
    }
    protected virtual void Awake()
	{
        thisTrans = this.transform;//提高效率，之后不用每次都查找transform
        OnInit();
        
	}
    public void show()
    {
        this.gameObject.SetActive(true);
        if (animShow != null)
        {
            //this.gameObject.SetActive(true);
            animShow.Restart();//重新播放动画
        }
        OnShow();
    }
    /// <summary>
    /// 隐藏UI的方法
    /// </summary>
    /// <param name="a">隐藏UI之后需要做的事情</param>
    public void hide(Action a=null)
    {
        OnHide();
        if (animHide != null)//如果有UI出去的动画
        {
            animHide.Restart();
        }
        else//如果没有UI出去的动画
        {
            this.gameObject.SetActive(false);//直接隐藏
        }
        if (a!=null)//隐藏UI之后做的事情，默认不需要做任何事情
        {
            a();
        }

        
    }
}
