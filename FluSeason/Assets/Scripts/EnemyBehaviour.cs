using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {Idle, Patrolling, Following, Attacking};

public class EnemyBehaviour : MonoBehaviour
{

    private Vector3 origin = new Vector3(0,0,0);
    private Vector3 wanderingTo = new Vector3(0,0,0);
    private float wanderTime;
    private bool isWandering = false;
    private EnemyState m_state = EnemyState.Patrolling;

    public float patrolDistance = 5f;
    public float patrolRate = 10f;
    public float attackRange;
    public float aggroRange;

    //public float travelSpeed = 0.5f;

    // Start is called before the first frame update
    private void Awake()
    {
        origin = this.transform.position;
        
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

        if (m_state != EnemyState.Patrolling)
            m_state = EnemyState.Patrolling;

        switch (m_state)
        {
            case EnemyState.Patrolling:
                Wander();
                break;
            case EnemyState.Following:
                break;
            case EnemyState.Attacking:
                break;
            case EnemyState.Idle:
                break;

        }
    }

    void Wander()
    {
        if (Time.time > wanderTime + patrolRate)
        {
            Debug.Log("wanderTime updated");
            wanderTime = Time.time;
            if (!isWandering)
            {
                SetWander();
            }

        }
        //Debug.Log(wanderingTo);
        transform.position = Vector3.Lerp(this.transform.position, wanderingTo, Time.deltaTime);
        if(Vector3.Distance(this.transform.position, wanderingTo) <= 1.0f)
        {
            isWandering = false;
        }
    }

    void SetWander()
    {
        //wanderingTo = new Vector3(origin.x + (Random.Range(-1.0f, 1.0f) * patrolDistance),
        //      origin.y + (Random.Range(-1.0f, 1.0f) * patrolDistance), 0);

        float r = patrolDistance * Mathf.Sqrt(Random.value);
        float theta = Mathf.PI * Random.value * 2;

        float randX = r * Mathf.Cos(theta);
        float randY = r * Mathf.Sin(theta);

        wanderingTo = origin + new Vector3(randX, randY);
        //wanderingTo = this.transform.position + (Vector3)(Random.insideUnitCircle * patrolDistance);
        isWandering = true;
    }

}
