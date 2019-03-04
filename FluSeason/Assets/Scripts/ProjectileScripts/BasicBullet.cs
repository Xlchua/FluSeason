using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : AbstractDamage
{
    private Rigidbody rb;

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        rb.velocity = transform.right * speed;
    }

    protected override void Update()
    {
        Destroy(this.gameObject, 2);
    }
}
