using UnityEngine;
using System.Collections;

public class UIHp : MonoBehaviour {

	void Awake()
	{
		
	}

	void Start () 
	{
	
	}
	
	void Update () 
	{
        transform.rotation = Camera.main.transform.rotation;
	}
    public void testBtn()
    {
        print("你点击了按钮");
    }
}
