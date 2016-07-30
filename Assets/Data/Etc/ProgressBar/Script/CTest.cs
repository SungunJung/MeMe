using UnityEngine;
using System.Collections;

public class CTest : MonoBehaviour {

    public Transform Target = null;
    public Transform MyUI = null;
    Vector3 ToWorldPos;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("1");
        ToWorldPos = Camera.main.WorldToScreenPoint(Target.position);
        ToWorldPos.z = Camera.main.nearClipPlane + 0.01f;

        MyUI.position = ToWorldPos;
    }
}
