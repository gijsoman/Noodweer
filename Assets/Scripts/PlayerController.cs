using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;    
    public float speed;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * new Vector3(input.axis.x, 0, input.axis.y);
    }

    
}
