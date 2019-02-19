using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private Vector3 origin;
    private Vector3 wanderingTo;
    private float wanderTime;

    public float wanderDistance = 5f;
    public float wanderRate = 15f;
    public float travelSpeed = 0.5f;

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
            Wander();
        }
    }

    void Wander()
    {
        wanderingTo = origin + (Vector3)(Random.insideUnitCircle * wanderDistance);
        transform.position = Vector3.Lerp(this.transform.position, wanderingTo, travelSpeed);
    }
}
