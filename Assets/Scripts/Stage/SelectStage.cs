using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectStage : MonoBehaviour, IBeginDragHandler,IEndDragHandler{
    private float[] pageValues;
    private float targetValue;
    private ScrollRect rect;
    private float speed = 3.0f;
    private Transform tContent;//存放关卡列表的内容面板
    private GameObject prefabStage;//存放预制体的对象
    private bool isDrag;//是否正在拖拽
    public Toggle[] pages;
    public GameObject[] goStages;
    public int stageNum = 50;
	void Awake()
	{
		pageValues=new float[5];
        for (int i = 0; i < pageValues.Length; i++)
        {
            pageValues[i] = 0.25f*i;
        }
	}

	void Start () 
	{
        prefabStage = Resources.Load("Prefabs/UIItem/BtnStage") as GameObject;
	    rect=this.GetComponent<ScrollRect>();
        tContent = transform.Find("Viewport/Content");
        goStages = new GameObject[stageNum];
        for (int i = 0; i < stageNum; i++)
        {
            GameObject go= GameObject.Instantiate(prefabStage);
            go.transform.parent = tContent;
            Text txtStageNum = go.transform.Find("StageNum").GetComponent<Text>();
            txtStageNum.text = (i + 1).ToString();
            UGUIEventListener.Get(go).onClick = OnStage;//自动的添加点击事件
        }
	}

    private void OnStage(GameObject obj)
    {
        print("Onstage");
        UtilUI.OpenLoadSceneHelper();
        UIPanelManager.Instance.showUI(EUiId.ID_LoadingPanel);
        LoadSceneHelper.Instance.loadScene("battle", delegate
        {
            UIPanelManager.Instance.hideUI(EUiId.ID_LoadingPanel);//隐藏loading界面
            UIPanelManager.Instance.showUI(EUiId.ID_MainPanel);//显示主UI
        });
    }
    public 

    void Update()
    {
        if (!isDrag)
        {
            rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, targetValue, Time.deltaTime * speed);
        }
    }
    public void OnPage1()
    {
        targetValue=pageValues[0];
        //rect.horizontalNormalizedPosition = targetValue;

    }
    public void OnPage2()
    {
        targetValue = pageValues[1];
        //rect.horizontalNormalizedPosition = targetValue;
    }
    public void OnPage3()
    {
        targetValue = pageValues[2];
        //rect.horizontalNormalizedPosition = targetValue;
    }
    public void OnPage4()
    {
        targetValue = pageValues[3];
        //rect.horizontalNormalizedPosition = targetValue;
    }
    public void OnPage5()
    {
        targetValue = pageValues[4];
        //rect.horizontalNormalizedPosition = targetValue;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        float rectPoint = rect.horizontalNormalizedPosition;
        float offset= Mathf.Abs(rectPoint - pageValues[0]);//和第一页的相对位置
        int index = 0;//记录距离拖动位置最小的下标
        for (int i = 1; i < pageValues.Length; i++)
        {
            float tmpOffset = Mathf.Abs(rectPoint - pageValues[i]);
            if (tmpOffset < offset)
            {
                index = i;
                offset = tmpOffset;
            }
        }
        targetValue=pageValues[index];
        pages[index].isOn = true;
        isDrag = false;
    }
}
