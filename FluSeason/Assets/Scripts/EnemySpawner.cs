using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner instance;

    public GameObject enemyPrefab;
    public float spawnInterval;
    public int enemyMax;

    [SerializeField] private int enemyCount = 0;
    private bool isSpawning = false;

    private IEnumerator spawnEnemiesCoroutine;

    void Awake()
    {
        if (instance == null)
            instance = this;

        spawnEnemiesCoroutine = SpawnEnemiesCoroutine();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartSpawning();
        //simulationTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCount >= enemyMax)
        {
            StopSpawning();
        }

        else if(enemyCount < enemyMax)
        {
            if (!isSpawning)
                StartSpawning();
        }
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        while(enemyCount < enemyMax)
        {
            Instantiate(enemyPrefab, this.transform.position, Quaternion.identity, this.transform);
            enemyCount++;

            yield return new WaitForSeconds(spawnInterval);

        }
    }

    private void StartSpawning()
    {
        StartCoroutine(spawnEnemiesCoroutine);
        isSpawning = true;
    }

    private void StopSpawning()
    {
        StopCoroutine(spawnEnemiesCoroutine);
        isSpawning = false;
    }

    public void DecrementEnemyCount()
    {
        enemyCount--;
    }

}
