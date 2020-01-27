using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class HolsterSlot : MonoBehaviour
{
    public ItemType HolsterItemType;
    public GameObject HolsteredItem;
    public GameObject HolsterHighlighter;

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

            //Set the function for unholstering
            Wieldable wieldable = HolsteredItem.GetComponent<Wieldable>();
            if (wieldable != null)
                wieldable.OnAttachObject += UnHolsterItem;

            //detach object from the hand
            wieldable.UnWieldItem(wieldable.handWieldedTo);

            HolsteredItem.transform.SetParent(transform);

            //check how we can fix the positioning.
            HolsteredItem.transform.localPosition = currentHolsterableItem.HolsteredOffset.localPosition;
            HolsteredItem.transform.localRotation = currentHolsterableItem.HolsteredOffset.localRotation;

            currentHolsterableItem = null;
            HolsterHighlighter.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (vrPositioned)
        {
            if (HolsteredItem == null)
            {
                currentHolsterableItem = other.GetComponent<Holsterable>();
            }
            if (currentHolsterableItem != null && currentHolsterableItem.Type == HolsterItemType)
            {
                HolsterHighlighter.SetActive(true);
                currentHolsterableItem.wieldable.OnDetachObject += HolsterItem;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (vrPositioned)
        {
            if (HolsteredItem == null && currentHolsterableItem == null)
            {
                currentHolsterableItem = other.GetComponent<Holsterable>();
            }
            if (currentHolsterableItem != null && currentHolsterableItem.Type == HolsterItemType && !HolsterHighlighter.activeInHierarchy)
            {
                HolsterHighlighter.SetActive(true);
                currentHolsterableItem.wieldable.OnDetachObject += HolsterItem;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (vrPositioned)
        {
            if (currentHolsterableItem != null)
            {
                HolsterHighlighter.SetActive(false);
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
