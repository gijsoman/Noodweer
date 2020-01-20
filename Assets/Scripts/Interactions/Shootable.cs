using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

[RequireComponent(typeof(Interactable))]
public class Shootable : MonoBehaviour
{
    public SteamVR_Action_Boolean Shoot;
    public Transform EndOfBarrel;
    public GameObject muzzleFlashPrefab;

    private void HandAttachedUpdate(Hand hand)
    {
        
        if (Shoot != null && Shoot.GetStateDown(hand.handType))
        {
            Vector3 forward = EndOfBarrel.transform.TransformDirection(Vector3.right) * 10;
            ObjectPooler.Instance.SpawnFromPool("Bullet", EndOfBarrel.position, Quaternion.LookRotation(forward));
            if (muzzleFlashPrefab != null)
            {
                GameObject tempFlash;
                tempFlash = Instantiate(muzzleFlashPrefab, EndOfBarrel.position, Quaternion.LookRotation(forward));
                Destroy(tempFlash, 0.5f);
            }
        }
    }


}
