using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    private GameObject target;//跟随的目标
    private Vector3 offset;
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        offset = this.transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.transform.position + offset;
	}
}
