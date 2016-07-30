using UnityEngine;
using System.Collections;

public class CWeapon1 : MonoBehaviour {

    public Camera Cam ;
    public Transform Weapon1;
    public Transform Targating;
    public GameObject Bullet;

    float Trigger;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        
        Vector3 pos = Input.mousePosition;
        pos.y += 166.5f;
        pos.z = Cam.nearClipPlane + 15f;
        pos = Cam.ScreenToWorldPoint(pos);
        Weapon1.LookAt(pos);
        
        /*
        Vector3 pos = Input.mousePosition;       
        pos.z = 15;
        pos = Cam.ScreenToWorldPoint(pos);
        Weapon1.LookAt(pos);
        */
        if ( Time.time > Trigger )
        {
            Trigger = Time.time + 0.2f;
            Instantiate(Bullet, Weapon1.position, Weapon1.rotation );
        }
    }
}
