using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Skill : MonoBehaviour {
    public float skillCD = 1.0f;//技能冷却时间
    private bool isCD;//记录是否在CD中
    private Image imgCD;
    private float time;
    public KeyCode key;
	void Awake()
	{
        Transform t=transform.Find("IconCD");
        if(t!=null)
        {
            imgCD = t.GetComponent<Image>();
        }
        time = skillCD;
	}

	void Start () 
	{
	
	}
	
	void Update () 
	{
        if (Input.GetKeyDown(key))
        {
            if (!isCD)
            {
                isCD = true;
            }
        }
        if (isCD)
        {
            time -= Time.deltaTime;
            float rate = time / skillCD;
            imgCD.fillAmount = rate;
            if (time <= 0)//经过了Cd之后，可以继续点击技能按钮
            {
                time = skillCD;
                isCD = false;
            }
        }
	}
    public void OnBtnSkill()
    {
        if (!isCD)
        {
            isCD = true;
        }
    }
}
