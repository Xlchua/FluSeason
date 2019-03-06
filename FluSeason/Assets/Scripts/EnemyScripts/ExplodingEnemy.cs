using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : EnemyBehaviour
{
    public GameObject explosionPrefabs;
    public float explosionSpawnInterval = 0.2f;

    IEnumerator ExplodeCoroutine()
    {
        Instantiate(explosionPrefabs, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(explosionSpawnInterval);
    }

    void OnTriggerEnter(Collider other)
    {
        print("DIE");
        curHP -= other.gameObject.GetComponent<AbstractDamage>().damage;
        healthBar.fillAmount = curHP / baseHP;

        Destroy(other.gameObject);

        if (curHP <= 0)
        {
            //EnemySpawner.instance.DecrementEnemyCount();
            StartCoroutine(ExplodeCoroutine());
            EnemySpawner.instance.enemiesRemaining -= 1;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    // TODO: explosion needs to affect the player as well
    
}
