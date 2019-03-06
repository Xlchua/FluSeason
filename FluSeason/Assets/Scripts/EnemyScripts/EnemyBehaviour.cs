using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState {Idle, Wandering, Following, Attacking};

public class EnemyBehaviour : MonoBehaviour
{

    private Vector3 origin = new Vector3(0,0,0);
    private Vector3 wanderingTo = new Vector3(0,0,0);
    private float wanderTime;
    private bool isWandering = false;
    private bool isAttacking = false;
    protected Rigidbody rb;


    public EnemyState m_state = EnemyState.Idle;

    public Transform player;

    public GameObject deathEffect;


    public float baseHP = 10f;
    public float curHP;
    public int damage;
    public int points;


    public float wanderDistance = 5f;
    public float wanderRate = 10f;
    public float idleTime = 1f;
    public float attackRange;
    public float aggroRange;
    public float moveSpeed = 5f;

    public float repelForce = 10f;
    public float rangeFromPlayer;

    public Image healthBar;

    // Start is called before the first frame update
    private void Awake()
    {
        origin = this.transform.position;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        curHP = baseHP;
        
    }

    void Start()
    {
        wanderTime = Time.time;
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

        this.transform.position = Vector3.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    IEnumerator AttackCoroutine(float range)
    {
        isAttacking = true;

        if(range < attackRange)
        {
            PlayerManager.instance.addInfection(damage);
            yield return new WaitForSeconds(3f);
        }

        isAttacking = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        /*if (collision.collider.CompareTag("Enemy"))
        {

            Vector3 pos = rb.position - collision.rigidbody.position;
            pos.Normalize();
            rb.AddForce(pos * repelForce);

        }*/

        //if (collision.collider.CompareTag("Bullet"))
        //{
        //    Debug.Log("DIE");
        //}
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            print("DIE");
            curHP -= other.gameObject.GetComponent<AbstractDamage>().damage;
            healthBar.fillAmount = curHP / baseHP;

            Destroy(other.gameObject);

            if (curHP <= 0)
            {
                //EnemySpawner.instance.DecrementEnemyCount();
                EnemySpawner.instance.enemiesRemaining -= 1;
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        
    }
}
