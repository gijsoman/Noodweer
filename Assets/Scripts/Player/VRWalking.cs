﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public enum walkingState
{
    Idle,
    Walking,
}

[RequireComponent(typeof(CharacterController))]
public class VRWalking : MonoBehaviour
{
    public float Gravity = 30.0f;
    public float Sensitivity = 0.1f;
    public float MaxSpeed =  1.0f;
    public float RotateIncrement = 90;

    //public SteamVR_Action_Boolean RotatePress = null;
    public SteamVR_Action_Vector2 MoveValue = null;
    public SteamVR_Action_Boolean EastPress = null;
    public SteamVR_Action_Boolean WestPress = null;

    private float speed = 0.0f;

    private CharacterController characterController;
    public Transform head = null;

    public walkingState walkState;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        if (MoveValue.changed)
        {
            CalculateMovement();
            walkState = walkingState.Walking;
        }
        else
        {
            walkState = walkingState.Idle;
        }

        //SnapRotation();
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
        if (EastPress.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            Debug.Log("Rotating");
            snapValue = Mathf.Abs(RotateIncrement);
        }

        //move left
        if (WestPress.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            Debug.Log("Rotating");
            snapValue = -Mathf.Abs(RotateIncrement);
        }

        transform.RotateAround(head.position, Vector3.up, snapValue);
    }
}
