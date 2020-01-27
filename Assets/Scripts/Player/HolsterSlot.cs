﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class HolsterSlot : MonoBehaviour
{
    public ItemType HolsterItemType;
    public GameObject HolsteredItem;

    //is public to check stuff in inspec
    public Holsterable currentHolsterableItem;

    private bool vrPositioned;

    private void Start()
    {
        StartCoroutine(WaitForVRPositioning());
    }

    private IEnumerator WaitForVRPositioning()
    {
        yield return new WaitForSeconds(0.1f);
        vrPositioned = true;
    }

    public void HolsterItem()
    {
        if (HolsteredItem == null && currentHolsterableItem != null)
        {
            HolsteredItem = currentHolsterableItem.gameObject;
            currentHolsterableItem.rb.isKinematic = true;
            HolsteredItem.transform.SetParent(transform);

            //Set the function for unholstering
            Wieldable wieldable = HolsteredItem.GetComponent<Wieldable>();
            if (wieldable != null)
                wieldable.OnAttachObject += UnHolsterItem;

            //check how we can fix the positioning.
            HolsteredItem.transform.localPosition = currentHolsterableItem.HolsteredOffset.localPosition;
            HolsteredItem.transform.localRotation = currentHolsterableItem.HolsteredOffset.localRotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (vrPositioned)
        { 
            currentHolsterableItem = other.GetComponent<Holsterable>();
            if (currentHolsterableItem != null && currentHolsterableItem.Type == HolsterItemType)
                currentHolsterableItem.wieldable.OnDetachObject += HolsterItem;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (vrPositioned)
        {
            if (currentHolsterableItem != null)
            {
                currentHolsterableItem.wieldable.OnDetachObject -= HolsterItem;
                currentHolsterableItem = null;
            }
        }
    }

    private void UnHolsterItem()
    {
        HolsteredItem.GetComponent<Wieldable>().OnAttachObject -= UnHolsterItem;
        HolsteredItem = null;
    }
}
