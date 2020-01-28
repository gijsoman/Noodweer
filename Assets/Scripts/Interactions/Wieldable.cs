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

    public Hand handWieldedTo;

    public bool allowedToWield = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType == GrabTypes.Grip && allowedToWield && hand.currentAttachedObject == null)
        {
            OnAttachObject?.Invoke();
            handWieldedTo = hand;
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags, attachmentOffset);
            rb.isKinematic = true;
            OnAttachObject = null;
        }
    }

    private void HandAttachedUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType == GrabTypes.Grip)
        {
            OnDetachObject?.Invoke();
        }
    }

    public void UnWieldItem(Hand hand)
    {
        hand.DetachObject(gameObject, false);
        handWieldedTo = null;
        OnDetachObject = null;
    }

    private void OnDestroy()
    {
        OnDetachObject = null;
        OnAttachObject = null;
    }
}
