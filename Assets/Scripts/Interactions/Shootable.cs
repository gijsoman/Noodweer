using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
using FMODUnity;

[RequireComponent(typeof(Interactable))]
public class Shootable : MonoBehaviour
{
    public SteamVR_Action_Boolean Shoot;
    public Transform EndOfBarrel;
    public GameObject muzzleFlashPrefab;

    public StudioEventEmitter shootEmitter;
    public StudioEventEmitter shootDeathEmitter;

    public bool allowedToShoot = true;

    private void HandAttachedUpdate(Hand hand)
    {        
        if (Shoot != null && Shoot.GetStateDown(hand.handType) && allowedToShoot)
        {
            Debug.Log("Shooting");
            Vector3 forward = EndOfBarrel.transform.TransformDirection(Vector3.right) * 10;
            ObjectPooler.Instance.SpawnFromPool("Bullet", EndOfBarrel.position, Quaternion.LookRotation(forward));
            if (muzzleFlashPrefab != null)
            {
                GameObject tempFlash;
                tempFlash = Instantiate(muzzleFlashPrefab, EndOfBarrel.position, Quaternion.LookRotation(forward));
                Destroy(tempFlash, 0.5f);
            }

            //Set the shoot sound event and play it
            //if we are not going to hit the enemy do the normal shoot sound. Ohterwise we play the death one.
            RaycastHit hit;
            Ray ray = new Ray(EndOfBarrel.position, forward);    
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == 9 && !GameManager.Instance.enemyDied)
                {
                    shootDeathEmitter.Play();
                    hit.collider.gameObject.GetComponent<EnemyScript>().DoDie();
                }
                else
                {
                    shootEmitter.Play();
                }
            }
            else
            {
                shootEmitter.Play();
            }
        }
    }
}
