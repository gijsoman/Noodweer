using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable), typeof(Rigidbody))]
public class Wieldable : MonoBehaviour
{
    [EnumFlags]
    [Tooltip("The flags used to attach this object to the hand.")]
    public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;

    [Tooltip("The local point which acts as a positional and rotational offset to use while held")]
    public Transform attachmentOffset;

    public delegate void WieldEvent();
    public WieldEvent OnAttachObject;
    public WieldEvent OnDetachObject;

    [HideInInspector]
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType == GrabTypes.Grip)
        {
            OnAttachObject?.Invoke();
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags, attachmentOffset);
            rb.isKinematic = true;       
        }
    }

    private void HandAttachedUpdate(Hand hand)
    {
        GrabTypes endingGrabType = hand.GetGrabEnding();

        if (endingGrabType == GrabTypes.Grip)
        {
            hand.DetachObject(gameObject, false);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = false;
            OnDetachObject?.Invoke();
        }
    }

    private void OnDestroy()
    {
        OnDetachObject = null;
        OnAttachObject = null;
    }
}
