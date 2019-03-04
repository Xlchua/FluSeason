using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyEnemy : EnemyBehaviour {

    public float enemySlidingInterval = 2f;
    private bool isDead = false;

    private void FixedUpdate()
    {
        if (isDead)
            skid();
    }

    void skid()
    {
        print("skid");
        rb.velocity = rb.transform.forward;
    }

    IEnumerator DieCoroutine()
    {
        print("about to die");

        yield return new WaitForSeconds(enemySlidingInterval);
        Destroy(this.gameObject);
        print("ded");
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        //curHP -= other.gameObject.GetComponent<AbstractDamage>().damage;
        healthBar.fillAmount = curHP / baseHP;

        Destroy(other.gameObject);

        isDead = true;

        this.gameObject.GetComponent<EnemyBehaviour>().moveSpeed = 10;
        this.gameObject.GetComponent<EnemyBehaviour>().damage = 1;
        

        StartCoroutine(DieCoroutine());
    }
}
