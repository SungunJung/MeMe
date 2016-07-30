using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FTouch
{
    public bool Enabled;
    public int Index;
    public Vector3 Position;
    public FTouch() { }
    public FTouch(bool enable, int index)
    {
        Index = index;
        Enabled = enable;
    }
}

public class InputSystem : MonoBehaviour {

    public Transform UITargeting1;
    public Transform UITargeting2;
    public Transform Target;
    public Camera MainCam;
    public Camera UICam;

    private Vector3 Pos;
    readonly int Max = 2;

    FTouch[] FTouchList = new FTouch[2];

    // Use this for initialization
    void Start () {

        //Cam = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();

        for( int i=0; i< Max; ++i )
        {
            FTouchList[i] = new FTouch(false,-1);
        }

        UITargeting1.gameObject.SetActive(false);
        UITargeting2.gameObject.SetActive(false);
    }
    private Vector3 WorldPos;
    private Vector3 ToWorldPos;
    void DoTouchDrag( int count, ref FTouch[] touch, ref int index)
    {
        if (count == 1)
        {
            
            Pos = touch[index].Position;
            Pos.y += 150;
            Pos.z = MainCam.nearClipPlane + 0.01f;
            UITargeting1.transform.position = UICam.ScreenToWorldPoint(Pos);
           
            /*
            WorldPos = touch[index].Position;
            ToWorldPos = MainCam.WorldToViewportPoint(Target.position);

            Debug.Log(WorldPos);

            ToWorldPos.x = Mathf.Clamp(ToWorldPos.x, 0.02f, 0.98f);
            ToWorldPos.y = Mathf.Clamp(ToWorldPos.y, 0.02f, 0.98f);
            ToWorldPos.z = MainCam.nearClipPlane + 0.01f;

            //transform.localScale = Vector3.one;
            // Renderer.enabled = true;

            Pos = UICam.ViewportToWorldPoint(ToWorldPos);
            Pos.z = 1;
            UITargeting1.transform.position = Pos;
            */

            if (UITargeting1.gameObject.activeSelf == false)
            {
                UITargeting1.gameObject.SetActive(true);
                UITargeting2.gameObject.SetActive(false);
            }
        }
        else if (count == 2)
        {
            Pos.x = Mathf.Abs(touch[1].Position.x + touch[0].Position.x) * 0.5f;
            Pos.y = Mathf.Abs(touch[1].Position.y + touch[0].Position.y) * 0.5f;
            
            UITargeting2.position = MainCam.ScreenToWorldPoint(Pos);

            if (UITargeting2.gameObject.activeSelf == false)
            {
                UITargeting1.gameObject.SetActive(false);
                UITargeting2.gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update () {

#if UNITY_EDITOR
      
        FTouchList[0].Position = Input.mousePosition;

        int index = 0;

        DoTouchDrag(1, ref FTouchList, ref index);

#elif UNITY_ANDROID

       if (Input.touchCount-1 < Max)
        {            
            int count = Input.touchCount;

            for (int i = 0; i < Input.touchCount; ++i)
            {
                Touch touch = Input.GetTouch(i);

                if (TouchPhase.Began == touch.phase)
                {                    
                    if (FTouchList[i].Index == -1)
                    {
                        FTouchList[i].Index = touch.fingerId;
                        FTouchList[i].Enabled = true;
                        FTouchList[i].Position = touch.position;
                    }
                }
                else if (TouchPhase.Moved == touch.phase || TouchPhase.Stationary == touch.phase)
                {
                    FTouchList[i].Position = touch.position;

                    DoTouchDrag(count, ref FTouchList, ref i);
                }
                else if( TouchPhase.Ended == touch.phase || TouchPhase.Canceled == touch.phase )
                {
                    for( int k=0;k< Max; ++k)
                    {
                        if(FTouchList[i].Index == touch.fingerId)
                        {
                            FTouchList[i].Index = -1;
                            FTouchList[i].Enabled = false;
                            FTouchList[i].Position = Vector3.zero;                           
                        }
                    }
                }
            }
        }
#endif

    }
}
