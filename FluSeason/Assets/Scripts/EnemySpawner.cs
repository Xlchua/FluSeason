using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner instance;

    public GameObject enemyPrefab;
    public float spawnInterval;
    public int enemyMax = 50;
    public int enemiesIncreasePerWave = 10;

    [SerializeField] private int enemyCount = 0;
    [SerializeField] private int enemyCurrent = 0;
    [SerializeField] private bool isSpawning = false;

    private IEnumerator spawnEnemiesCoroutine;

    void Awake()
    {
        if (instance == null)
            instance = this;

        spawnEnemiesCoroutine = SpawnEnemiesCoroutine();
        enemyMax = 50;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManagement.instance.UpdateWave();
        StartSpawning();
        //simulationTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {/*
        if(enemyCount >= enemyMax)
        {
            StopSpawning();
        }

        else if(enemyCount < enemyMax)
        {
            if (!isSpawning)
                StartSpawning();
        }*/

        if(!isSpawning)
        {
            //Wave has ended.

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            Debug.Log(enemies.Length);
            if(enemies.Length <= 0)
            {
                StopSpawning();
                GameManagement.instance.UpdateWave();
                enemyMax += enemiesIncreasePerWave;
                StartSpawning();
            }
            //StartSpawning();
        }
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        for(int i = 0; i < enemyMax; i++)
        { 
            int rand = Random.Range(1, 5);
            var whereToSpawn = this.transform.GetChild(rand);

            enemyCount++;
            //enemyCurrent++;
            Instantiate(enemyPrefab, whereToSpawn.position, Quaternion.identity, whereToSpawn);
      
            yield return new WaitForSeconds(spawnInterval);

        }
        isSpawning = false;

    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
        isSpawning = true;
    }

    private void StopSpawning()
    {
        StopCoroutine(spawnEnemiesCoroutine);
        enemyCount = 0;
    }

}
