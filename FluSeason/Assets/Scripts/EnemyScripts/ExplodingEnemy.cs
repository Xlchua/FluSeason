using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : EnemyBehaviour
{
    private List<Vector3> directions = new List<Vector3>();

    public GameObject explosionPrefabs;
    public float explosionSpawnInterval = 0.2f;

    private void Start()
    {
        directions.Add(new Vector3(0f, 1f, 0f)); // Up
        directions.Add(new Vector3(1f, 1f, 0f)); // Up Right
        directions.Add(new Vector3(1f, 0f, 0f)); // Right
        directions.Add(new Vector3(1f, -1f, 0f)); // Down Right

        directions.Add(new Vector3(0f, -1f, 0f)); // Down
        directions.Add(new Vector3(-1f, -1f, 0f)); // Down Left
        directions.Add(new Vector3(-1f, 0f, 0f)); // Left
        directions.Add(new Vector3(-1f, 1f, 0f)); // Up Left
    }

    IEnumerator ExplodeCoroutine()
    {
        Instantiate(explosionPrefabs, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(explosionSpawnInterval);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("EnemyBullet"))
        {
            //print("DIE");
            curHP -= other.gameObject.GetComponent<AbstractDamage>().damage;
            healthBar.fillAmount = curHP / baseHP;

            Destroy(other.gameObject);

            if (curHP <= 0)
            {
                //EnemySpawner.instance.DecrementEnemyCount();
                Explode();
                if (!isDead)
                    EnemySpawner.instance.enemiesRemaining--;
                isDead = true;
                GameObject deathParticle = Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(deathParticle, 1f);
                Destroy(this.gameObject);
            }
        }
    }


    void Explode()
    {
        for(int i = 0; i < directions.Count; i++)
        {
            //Debug.Log(directions[i]);
            Vector3 newDirection = this.transform.position + directions[i];

            float angle = Mathf.Atan2(newDirection.y - this.transform.position.y, newDirection.x - this.transform.position.x) * Mathf.Rad2Deg;

            GameObject explode =Instantiate(explosionPrefabs, this.transform.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));
            explode.GetComponent<Explosion>().setDirection(directions[i]);
        }
    }
    // TODO: explosion needs to affect the player as well

}
