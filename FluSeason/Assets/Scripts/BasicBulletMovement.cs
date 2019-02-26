using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletMovement : MonoBehaviour
{
    private float bulletSpeed = 20f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * bulletSpeed;
    }

    void Update()
    {
        Destroy(this.gameObject, 2);
    }
}
