using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
public class EnemyMovement : MonoBehaviour
{
    public WOWCamera wowCam;
    public float RotationSpeed;

    private Animator animator;
    private Rigidbody rb;

    private float Vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vertical = Input.GetAxis("Vertical");

        Move();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            animator.SetBool("Run", true);
        if (Input.GetKeyUp(KeyCode.LeftShift))
            animator.SetBool("Run", false);

        animator.SetFloat("Forward", Vertical);
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(1) && wowCam != null)
        {
            transform.eulerAngles = new Vector3(0, wowCam.transform.eulerAngles.y, 0);
        }
        else if (animator.GetFloat("Forward") == 0)
        {
            // set animator bool to use rotate animations.
        }
        else if(Input.GetAxis("Horizontal") != 0)
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime);
        }
    }
}
