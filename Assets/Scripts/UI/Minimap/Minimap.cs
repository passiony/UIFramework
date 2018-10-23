using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {
    private Camera cameraMinimap;//获取到摄像机（小地图）
	void Awake()
	{
		
	}

	void Start () 
	{
        cameraMinimap = GameObject.FindGameObjectWithTag("CameraMinimap").GetComponent<Camera>();
	}
	
	void Update () 
	{
	
	}
    public void onBtnAdd()
    {
        if (cameraMinimap.orthographicSize < 15.0f)//限制在最大值是15
        {
            cameraMinimap.orthographicSize += 1.0f;
        }
        
    }
    public void onBtnMinus()
    {
        if (cameraMinimap.orthographicSize >3.0f)//限制在最小值3
        {
            cameraMinimap.orthographicSize -= 1.0f;
        }
        
    }
}
