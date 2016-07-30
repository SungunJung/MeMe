using UnityEngine;
using System.Collections;

public class CBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {

        DestroyObject(this.gameObject,2 );
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.position += Time.deltaTime * this.transform.forward * 50;
	}
}
