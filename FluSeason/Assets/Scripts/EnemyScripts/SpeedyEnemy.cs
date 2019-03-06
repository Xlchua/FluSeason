using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyEnemy : EnemyBehaviour {

    public float enemySlidingInterval = 2f;

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
        if (!isDead)
            EnemySpawner.instance.enemiesRemaining--;
        isDead = true;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        print("ded");
        
    }

    //protected override void OnTriggerEnter(Collider other)
    //{
    //    //curHP -= other.gameObject.GetComponent<AbstractDamage>().damage;
    //    healthBar.fillAmount = curHP / baseHP;

    //    Destroy(other.gameObject);

    //    isDead = true;

    //    this.gameObject.GetComponent<EnemyBehaviour>().moveSpeed = 10;
    //    this.gameObject.GetComponent<EnemyBehaviour>().damage = 1;
        

    //    StartCoroutine(DieCoroutine());
    //}
}
