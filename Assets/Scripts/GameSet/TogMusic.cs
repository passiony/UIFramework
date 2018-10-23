using UnityEngine;
using System.Collections;

public class TogMusic : MonoBehaviour {
    private Transform tOn, tOff;
	void Awake()
	{
		
	}

	void Start () 
	{
        tOn = transform.Find("On");
        tOff = transform.Find("Off");
	}
    public void OnMusic(bool isOn)
    {
        tOn.gameObject.SetActive(isOn);
        tOff.gameObject.SetActive(!isOn);
    }
}
