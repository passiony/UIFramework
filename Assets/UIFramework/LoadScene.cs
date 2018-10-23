using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class LoadSceneHelper : SingletonMono<LoadSceneHelper>
{
    private AsyncOperation asyn;//异步对象
    public Action actionAfterLoadScene;//加载完场景之后需要做的事情
    private int curLoadedValue;//当前加载的进度
    public Image imgLoading;//进度图片显示
    public Text txtValue;//进度文字显示
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }
    public void loadScene(string name, Action a)
    {
        imgLoading = GameObject.Find("LoadingImg").GetComponent<Image>();
        txtValue = GameObject.Find("LoadingValue").GetComponent<Text>();
        
        imgLoading.fillAmount = 0.0f;
        txtValue.text = "";

        actionAfterLoadScene = a;
        curLoadedValue = 0;
        StartCoroutine(taskLoadScene(name));//开启一个协程
    }
    IEnumerator taskLoadScene(string name)
    {
        asyn=SceneManager.LoadSceneAsync(name);//异步加载场景的方法
        asyn.allowSceneActivation = false;
        yield return asyn;
    }
    void Update()
    {
        if (asyn == null)
        {
            return;
        }
        int trueProgress = 0;
        if (asyn.progress < 0.9f)//异步加载场景的进度值
        {
            trueProgress = (int)asyn.progress * 100;
        }
        else {
            trueProgress = 100;
        }

        if (curLoadedValue < trueProgress)
        {
            curLoadedValue++;
        }
        imgLoading.fillAmount = curLoadedValue / 100f;//把百分比赋值给图片
        txtValue.text = curLoadedValue + "%";//赋值给文字
        if (curLoadedValue >= 100)
        {
            asyn.allowSceneActivation = true;
        }
        if (asyn.isDone)
        {
            if (actionAfterLoadScene != null)
            {
                actionAfterLoadScene();
            }
            imgLoading.fillAmount = 0.0f;
            asyn = null;
            UtilUI.clearMemory();
            this.gameObject.SetActive(false);
        }
    }
}
