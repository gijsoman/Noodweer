using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowVRCam : MonoBehaviour
{
    public Transform VRCamera;
    private Vector3 startOffset;

    private void Start()
    {
        startOffset = transform.position;
        transform.eulerAngles = Vector3.zero;
    }

    private void Update()
    {
        transform.position = new Vector3(VRCamera.position.x + startOffset.x, VRCamera.position.y + startOffset.y, VRCamera.position.z-startOffset.z);

        float VRcamY = VRCamera.eulerAngles.y;


        float bodyY = transform.eulerAngles.y;     
        Debug.Log(bodyY + " | " + VRcamY);
        //we want seperate if statements for different states

        //if the body is at 0 we want to change when over 90 or under 270
        if (bodyY == 0)
        {
            if (VRcamY < 270 && VRcamY > 180)
                transform.eulerAngles = new Vector3(0, 270, 0);
            if (VRcamY > 90 && VRcamY < 180)
                transform.eulerAngles = new Vector3(0, 90, 0);
        }

        if (bodyY == 90)
        {
            if (VRcamY > 270)
                transform.eulerAngles = new Vector3(0, 0, 0);
            if (VRcamY > 180 && VRcamY < 270)
                transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (bodyY == 180)
        {
            if (VRcamY < 90)
                transform.eulerAngles = new Vector3(0, 90, 0);
            if (VRcamY > 270)
                transform.eulerAngles = new Vector3(0, 270, 0);
        }

        if (bodyY == 270)
        {
            if (VRcamY > 0 && VRcamY < 90)
                transform.eulerAngles = new Vector3(0, 0, 0);
            if (VRcamY < 180 && VRcamY > 90)
                transform.eulerAngles = new Vector3(0, 180, 0);
        }


        //if the body is at 90 we want to change when over 180 and over 270

        //if the body is at 180 we want to change when over 270 and under 90

        //if the body is at 270 we want to change when over 0 and under 180
    }
}
