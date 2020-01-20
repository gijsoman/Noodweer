using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSpotLight : MonoBehaviour
{
    [SerializeField] private float Speed = 10;

    private Vector3 rotation;

    private void Update()
    {
        rotation.y += Speed * Time.deltaTime;
        transform.eulerAngles = rotation;
    }
}
