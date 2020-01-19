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
    }

    private void Update()
    {
        transform.position = new Vector3(VRCamera.position.x + startOffset.x, VRCamera.position.y + startOffset.y, VRCamera.position.z-startOffset.z);
    }
}
