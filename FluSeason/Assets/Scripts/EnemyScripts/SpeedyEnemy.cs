using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyEnemy : EnemyBehaviour {

    void OnTriggerEnter(Collider other)
    {
        curHP -= other.gameObject.GetComponent<AbstractDamage>().damage;
        healthBar.fillAmount = curHP / baseHP;

        Destroy(other.gameObject);

        this.gameObject.GetComponent<EnemyBehaviour>().moveSpeed = 10;
        Destroy(this.gameObject, 2);

    }
}
