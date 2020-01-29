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
        transform.position = new Vector3(VRCamera.position.x + startOffset.x, VRCamera.position.y + startOffset.y, VRCamera.position.z - startOffset.z);

        HandleBodyRotation();
    }

    private void HandleBodyRotation()
    {
        float VRcamY = VRCamera.localEulerAngles.y;
        float bodyY = transform.localEulerAngles.y;

        Debug.Log(VRcamY + " | " + bodyY);

        switch (bodyY)
        {
            case 0:
                if (VRcamY < 270 && VRcamY > 180)
                    transform.localEulerAngles = new Vector3(0, 270, 0);
                if (VRcamY > 90 && VRcamY < 180)
                    transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
            case 90:
                if (VRcamY > 270)
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                if (VRcamY > 180 && VRcamY < 270)
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 180:
                if (VRcamY < 90)
                    transform.localEulerAngles = new Vector3(0, 90, 0);
                if (VRcamY > 270)
                    transform.localEulerAngles = new Vector3(0, 270, 0);
                break;
            case 270:
                if (VRcamY > 0 && VRcamY < 90)
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                if (VRcamY < 180 && VRcamY > 90)
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
        }

        
    }
}
