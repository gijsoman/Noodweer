using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(CharacterController))]
public class VRWalking : MonoBehaviour
{
    public float Gravity = 30.0f;
    public float Sensitivity = 0.1f;
    public float MaxSpeed =  1.0f;
    public float RotateIncrement = 90;

    //public SteamVR_Action_Boolean RotatePress = null;
    public SteamVR_Action_Boolean MovePress = null;
    public SteamVR_Action_Vector2 MoveValue = null;

    private float speed = 0.0f;

    private CharacterController characterController;
    private Transform cameraRig = null;
    private Transform head = null;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        head = SteamVR_Render.Top().head;
    }

    private void Update()
    {
        HandleHeight();
        CalculateMovement();
        //SnapRotation();
    }

    private void HandleHeight()
    {
        //get the head in local space
        float headHeight = Mathf.Clamp(head.localPosition.y, 1, 2);
        characterController.height = headHeight;

        //cut in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = characterController.height / 2;
        newCenter.y += characterController.skinWidth;

        //Move capsule in local space
        newCenter.x = head.localPosition.x;
        newCenter.z = head.localPosition.z;

        //apply
        characterController.center = newCenter;
    }

    private void CalculateMovement()
    {
        //figure out movement orientation  
        Quaternion orientation = CalculateOrientation();
        Vector3 movement = Vector3.zero;

        //if not moving
        if (MoveValue.axis.magnitude == 0)
        {
            speed = 0f;
        }

        //if button pressed
        //add clamp
        speed += MoveValue.axis.magnitude * Sensitivity;
        speed = Mathf.Clamp(speed, -MaxSpeed, MaxSpeed);

        //orientation and Gravity
        movement += orientation * (speed * Vector3.forward);
        movement.y -= Gravity * Time.deltaTime;

        //apply
        characterController.Move(movement * Time.deltaTime);
    }

    private Quaternion CalculateOrientation()
    {
        float rotation = Mathf.Atan2(MoveValue.axis.x, MoveValue.axis.y);
        rotation *= Mathf.Rad2Deg;

        Vector3 orientationEuler = new Vector3(0, head.eulerAngles.y + rotation, 0);
        return Quaternion.Euler(orientationEuler);
    }

    private void SnapRotation()
    {
        float snapValue = 0f;

        //move right
        if (MoveValue.axis.x > 0.8 && MovePress.GetStateDown(SteamVR_Input_Sources.Any))
        {
            snapValue = Mathf.Abs(RotateIncrement);
        }

        //move left
        if (MoveValue.axis.x < -0.8 && MovePress.GetStateDown(SteamVR_Input_Sources.Any))
        {
            snapValue = -Mathf.Abs(RotateIncrement);
        }

        transform.RotateAround(head.position, Vector3.up, snapValue);
    }
}
