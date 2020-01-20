using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyScript : MonoBehaviour
{
    public delegate void EnemyEvent();
    public EnemyEvent IDied;

    private bool alive = true;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bullet_45mm_Bullet(Clone)")
        {
            if(alive)
                anim.SetTrigger("Death");
            alive = false;
            IDied?.Invoke();
        }
    }

    private void OnDestroy()
    {        
        IDied = null;
    }
}
