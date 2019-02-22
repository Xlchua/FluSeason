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
    private EnemyState m_state = EnemyState.Idle;


    public int baseHP;
    public int curHP;
    public int damage;
    public int points;

    [SerializeField] private bool isDead;

    public float wanderDistance = 5f;
    public float wanderRate = 10f;
    public float attackRange;
    public float aggroRange;

    //public float travelSpeed = 0.5f;

    // Start is called before the first frame update
    private void Awake()
    {
        origin = this.transform.position;
        isDead = false;
        
    }

    void Start()
    {
        wanderTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        /* Player Not Implemented Yet.

        float rangeFromPlayer = Vector3.Distance(player.transform.position, this.transform.position);

        if(rangeFromPlayer =< attackRange)
        {
            m_state = EnemyState.Attacking;
        }


        if(rangeFromPlayer > attackRange && rangeFromPlayer <= aggroRange)
        {
            m_state = EnemyState.Following;
        }
        */
        switch (m_state)
        {
            case EnemyState.Wandering:
                if(!isWandering)
                    StartCoroutine(WanderCoroutine());
                break;
            case EnemyState.Following:
                break;
            case EnemyState.Attacking:
                break;
            case EnemyState.Idle:
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
            yield return null;
        }
        //Debug.Log(wanderingTo);
        isWandering = false;
        m_state = EnemyState.Idle;
    }

    IEnumerator IdleCoroutine()
    { 
        yield return new WaitForSeconds(3f);
        m_state = EnemyState.Wandering;
    }

    void SetWander()
    {
        //wanderingTo = new Vector3(origin.x + (Random.Range(-1.0f, 1.0f) * patrolDistance),
        //      origin.y + (Random.Range(-1.0f, 1.0f) * patrolDistance), 0);




        //wanderingTo = this.transform.position + (Vector3)(Random.insideUnitCircle * patrolDistance);
        isWandering = true;
    }

}
