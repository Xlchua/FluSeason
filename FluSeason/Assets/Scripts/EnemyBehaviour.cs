using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {Idle, Wandering, Following, Attacking};

public class EnemyBehaviour : MonoBehaviour
{

    private Vector3 origin = new Vector3(0,0,0);
    private Vector3 wanderingTo = new Vector3(0,0,0);
    private float wanderTime;
    private bool isWandering = false;
    private bool isAttacking = false;
    private Rigidbody2D rb;


    public EnemyState m_state = EnemyState.Idle;

     public Transform player;


    public int baseHP;
    public int curHP;
    public int damage;
    public int points;

    [SerializeField] private bool isDead;

    public float wanderDistance = 5f;
    public float wanderRate = 10f;
    public float idleTime = 1f;
    public float attackRange;
    public float aggroRange;
    public float moveSpeed = 5f;

    public float repelForce = 10f;
    public float rangeFromPlayer;

   

    // Start is called before the first frame update
    private void Awake()
    {
        origin = this.transform.position;
        isDead = false;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        
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

        //Player Not Implemented Yet.

        rangeFromPlayer = Vector3.Distance(player.position, this.transform.position);

        //Debug.Log(rangeFromPlayer);

        if(rangeFromPlayer <= attackRange)
        {
            m_state = EnemyState.Attacking;
        } 
        else if(rangeFromPlayer > attackRange && rangeFromPlayer <= aggroRange)
        {
            m_state = EnemyState.Following;
        }

        Debug.Log(m_state);

        switch (m_state)
        {
            case EnemyState.Wandering:
                isAttacking = false;
                if (!isWandering)
                    StartCoroutine(WanderCoroutine());
                break;
            case EnemyState.Following:
                isWandering = false;
                isAttacking = false;
                StopAllCoroutines();
                Follow();
                break;
            case EnemyState.Attacking:
                StopAllCoroutines();
                if(!isAttacking)
                    StartCoroutine(AttackCoroutine(rangeFromPlayer));
                break;
            case EnemyState.Idle:
                isAttacking = false;
                StopCoroutine(WanderCoroutine());
                StartCoroutine(IdleCoroutine());
                break;

        }
    }

    IEnumerator WanderCoroutine()
    {
        isWandering = true;

        //Math for uniformly random point within a circle of radius r
        float r = wanderDistance * Mathf.Sqrt(Random.value);
        float theta = Mathf.PI * Random.value * 2;

        float randX = r * Mathf.Cos(theta);
        float randY = r * Mathf.Sin(theta);

        wanderingTo = origin + new Vector3(randX, randY);

        while(Vector3.Distance(this.transform.position, wanderingTo) > 0.5f) {
            transform.position = Vector3.Lerp(this.transform.position, wanderingTo, Time.deltaTime/2);

            if(rangeFromPlayer <= aggroRange)
            {
                m_state = EnemyState.Following;
            }

            yield return null;
        }
        //Debug.Log(wanderingTo);
        isWandering = false;
        m_state = EnemyState.Idle;
    }

    IEnumerator IdleCoroutine()
    { 
        yield return new WaitForSeconds(idleTime);
        //Debug.Log("Should change from idle to wandering");
        m_state = EnemyState.Wandering;
    }

    public void Follow()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);
        //Debug.Log("Player in range");
        if(rangeFromPlayer > aggroRange)
        {
            m_state = EnemyState.Idle;
        }
    }

    IEnumerator AttackCoroutine(float range)
    {
        isAttacking = true;

        if(range < attackRange)
        {
            PlayerManager.instance.addInfection(damage);
            yield return new WaitForSeconds(5f);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Vector3 pos = rb.position - collision.rigidbody.position;
            pos.Normalize();
            rb.AddForce(pos * repelForce);

        }
    }


}
