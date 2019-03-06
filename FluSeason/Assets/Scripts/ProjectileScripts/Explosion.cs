using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : AbstractDamage
{
    Rigidbody rb;
    Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speed;   
    }

    public void setDirection(Vector3 v)
    {
        direction = v;
    }

    public int getDamage()
    {
        return (int)damage;
    }
}
