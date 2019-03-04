using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBulletMovement : MonoBehaviour
{
    private float bulletSpeed = 25f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = Quaternion.AngleAxis(Random.Range(-30, 30), Vector3.forward) * transform.right;
        rb.velocity = direction * bulletSpeed;
    }

    void Update()
    {
        Destroy(this.gameObject, 2);
    }
}
