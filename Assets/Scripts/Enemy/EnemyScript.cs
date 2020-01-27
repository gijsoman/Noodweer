using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(Animator))]
public class EnemyScript : MonoBehaviour
{
    public delegate void EnemyEvent();
    public EnemyEvent IDied;
    public EnemyEvent IStabbed;
    public StudioEventEmitter PlayerStabbed;

    private bool alive = true;
    private Animator anim;
    public bool PlayerInRange = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Stab");        
        }
    }

    public void InvokePlayerDeath()
    {
        if (PlayerInRange && !GameManager.Instance.playerDied && !GameManager.Instance.enemyDied)
        {
            PlayerStabbed.Play();
            IStabbed?.Invoke();
        }
    }

    public void DoDie()
    {
        if (!GameManager.Instance.playerDied)
        {
            if (alive)
                anim.SetTrigger("Death");
            alive = false;
            IDied?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "VRPlayer")
            PlayerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "VRPlayer")
            PlayerInRange = false;
    }

    private void OnDestroy()
    {        
        IDied = null;
    }
}
