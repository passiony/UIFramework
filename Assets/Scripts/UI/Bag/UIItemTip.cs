using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIItemTip : MonoBehaviour {
    public Text txtContent;
    private CanvasGroup cg;
    private float alpha = 0.0f;
    private float speedAlpha = 4.0f;
	void Awake()
	{
		
	}

	void Start () 
	{
        txtContent = transform.Find("TxtContent").GetComponent<Text>();
        cg = this.transform.GetComponent<CanvasGroup>();
	}
	
	void Update () 
	{
        if (alpha != cg.alpha)
        {
            cg.alpha = Mathf.Lerp(cg.alpha, alpha, speedAlpha * Time.deltaTime);
            if (Mathf.Abs(alpha - cg.alpha) <= 0.01)
            {
                cg.alpha = alpha;
            }
        }
	}
    public void showPanel(string content)
    {
        txtContent.text = content;
        alpha = 1;
    }
    public void hidePanel()
    {
        alpha = 0;
    }
    public void SetLocalPosition(Vector3 point)
    {
        //设置ToolTilePanel相对于父节点的坐标
        this.transform.localPosition = point;
    }
}
