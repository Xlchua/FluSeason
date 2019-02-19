using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private Vector3 origin = new Vector3(0,0,0);
    private Vector3 wanderingTo = new Vector3(0,0,0);
    private float wanderTime;
    private bool isWandering = false;

    public float wanderDistance = 5f;
    public float wanderRate = 10f;
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
        if(Time.time > wanderTime + wanderRate)
        {
            Debug.Log("wanderTime updated");
            wanderTime = Time.time;
            if (!isWandering)
            {
                SetWander();
            }

        }
        Wander();
    }

    void Wander()
    { 
        //Debug.Log(wanderingTo);
        transform.position = Vector3.Lerp(this.transform.position, wanderingTo, Time.deltaTime/2);
        if(Vector3.Distance(this.transform.position, wanderingTo) <= 1.0f)
        {
            isWandering = false;
        }
    }

    void SetWander()
    {
        //wanderingTo = new Vector3(origin.x + (Random.Range(-1.0f, 1.0f) * wanderDistance),
        //      origin.y + (Random.Range(-1.0f, 1.0f) * wanderDistance), 0);

        wanderingTo = origin + (Vector3)(Random.insideUnitCircle * wanderDistance);
        isWandering = true;
    }

}
