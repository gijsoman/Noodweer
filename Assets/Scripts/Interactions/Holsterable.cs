using UnityEngine;

public enum ItemType { Gun, Pepper };

[RequireComponent(typeof(Wieldable), typeof(Rigidbody))]
public class Holsterable : MonoBehaviour
{
    public ItemType Type;
    public Transform HolsteredOffset;

    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public Wieldable wieldable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        wieldable = GetComponent<Wieldable>();
    }
}
