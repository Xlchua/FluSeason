using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBullet : AbstractDamage
{
    //private float bulletSpeed = 25f;

    private Rigidbody rb;

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        Vector3 direction = Quaternion.AngleAxis(Random.Range(-30, 30), Vector3.forward) * transform.right;
        rb.velocity = direction * speed;
    }

    protected override void Update()
    {
        Destroy(this.gameObject, 2);
    }
}
