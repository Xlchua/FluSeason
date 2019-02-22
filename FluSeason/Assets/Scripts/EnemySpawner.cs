using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float spawnInterval;
    public int enemyMax;

    [SerializeField] private int enemyCount = 0;
    private float simulationTime;

    // Start is called before the first frame update
    void Start()
    {
        simulationTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > simulationTime + spawnInterval && enemyCount < enemyMax)
        {
            simulationTime = Time.time;
            //Currently spawns in center;
            Instantiate(enemyPrefab, this.transform.position, Quaternion.identity, this.transform);
            enemyCount++;
        }
    }
}
