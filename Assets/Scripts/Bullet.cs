using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IPooledObject
{
    public float BulletForce;

    private Rigidbody rb;

    public void OnObjectSpawned()
    {
        rb = GetComponent<Rigidbody>();
        ResetRigidBody();
        rb.AddForce(transform.forward * BulletForce);
    }

    private void ResetRigidBody()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.angularVelocity = new Vector3(0f, 0f, 0f);
    }

    private void OnCollisionStay(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
