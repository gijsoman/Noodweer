using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;    
    public float speed;
    private Vector3 moveDirection;

    private Transform cameraRig = null;
    private Transform head = null;

    // Start is called before the first frame update
    void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        head = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * new Vector3(input.axis.x, 0, input.axis.y);
    }

    
}
