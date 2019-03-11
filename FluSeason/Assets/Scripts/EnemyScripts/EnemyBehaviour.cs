using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState {Idle, Wandering, Following, Attacking};

public class EnemyBehaviour : MonoBehaviour
{
    private bool isAttacking = false;
    protected Rigidbody rb;

    protected bool isDead = false;

    public Transform player;

    public GameObject deathEffect;


    public float baseHP = 10f;
    public float curHP;
    public int damage;
    public int points;

    public float attackTimer;

    public float attackCooldown;
    public float attackRange;
    public float aggroRange;
    public float moveSpeed = 5f;

    public float repelForce = 10f;
    public float rangeFromPlayer;

    public Image healthBar;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        curHP = baseHP;
        attackTimer = Time.time;
        
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(!player)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            
        rangeFromPlayer = Vector3.Distance(player.position, this.transform.position);

        if (curHP <= 0)
            Destroy(this.gameObject);

        //Debug.Log(rangeFromPlayer);
        // COMMENT HERE TO THE NEXT INDICATED COMMENT TO DISABLE ENEMY MOVEMENT FOR TESTING

        if(rangeFromPlayer >= attackRange)
        {
            //Enemy not in attack range. Enemy follows
            isAttacking = false;
            Follow();
        } 
        else
        {
            //Enemy in attack range. Enemy attacks
            if(!isAttacking)
            {
                StartCoroutine(AttackCoroutine(rangeFromPlayer));
            }
        }

        // COMMENT HERE TO THE NEXT INDICATED COMMENT TO DISABLE ENEMY MOVEMENT FOR TESTING
    }

    public void Follow()
    {
        float angle = Mathf.Atan2(player.position.y - this.transform.position.y, player.position.x - this.transform.position.x) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    IEnumerator AttackCoroutine(float range)
    {
        isAttacking = true;

        if (Time.time > attackTimer + attackCooldown)
        {
            attackTimer = Time.time;
            PlayerManager.instance.addInfection(damage);
            yield return new WaitForSeconds(1f);
        }

        isAttacking = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet") || other.CompareTag("EnemyBullet"))
        {
            //print("DIE");
            curHP -= other.gameObject.GetComponent<AbstractDamage>().damage;
            healthBar.fillAmount = curHP / baseHP;

            Destroy(other.gameObject);

            if (curHP <= 0)
            {
                //EnemySpawner.instance.DecrementEnemyCount();
                if(!isDead)
                    EnemySpawner.instance.enemiesRemaining--;
                isDead = true;
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        
    }


    private void OnCollisionExit(Collision collision)
    {
        print("stop");
        if (collision.gameObject.CompareTag("Walkthrough")) {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -.5f);
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
        }
    }
}
