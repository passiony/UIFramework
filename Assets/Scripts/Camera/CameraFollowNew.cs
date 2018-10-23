using UnityEngine;
using System.Collections;

public class CameraFollowNew : MonoBehaviour {
    public GameObject target;//跟随的目标
    public Vector3 offset;//摄像机和跟随目标的相对位置
	void Awake()
	{
		
	}

	void Start () 
	{
        target = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - target.transform.position;
	}
	
	void Update () 
	{
        transform.position = target.transform.position + offset;//更新摄像机的位置
	}
}
