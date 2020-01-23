using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabbingScript : MonoBehaviour
{
    public delegate void StabEvent();
    public StabEvent IStabbed;

    public Animator EnemyAnimator;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.name == "VRPlayer")
        {
            EnemyAnimator.SetTrigger("Stab");
            IStabbed?.Invoke();
        }
    }
}
