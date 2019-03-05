using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner instance;

    public GameObject enemyPrefab;
    public float spawnInterval;
    public int enemyMax;
    public int enemiesRemaining;
    public int enemiesIncreasePerWave = 10;

    [Header("Enemy Tiers")]
    public List<GameObject> TierOnePrefabs;
    public List<GameObject> TierTwoPrefabs;
    public List<GameObject> TierThreePrefabs;
    public List<GameObject> TierFourPrefabs;
    public List<GameObject> TierFivePrefabs;
    public List<GameObject> upgrades;

    public List<GameObject> currentTier;

    [SerializeField] private int enemyCount = 0;
    [SerializeField] private int enemyCurrent = 0;
    [SerializeField] private bool isSpawning = false;
    private Transform centerSpawn;

    

    private IEnumerator spawnEnemiesCoroutine;

    //public IntUnityEvent enemyLeft = new IntUnityEvent();

    void Awake()
    {
        if (instance == null)
            instance = this;

        spawnEnemiesCoroutine = SpawnEnemiesCoroutine();
        enemiesRemaining = enemyMax;
        centerSpawn = this.transform.GetChild(0);
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
    {
        GameManagement.instance.UpdateCount(enemiesRemaining);
        /*
        if(enemyCount >= enemyMax)
        {
            StopSpawning();
        }

        else if(enemyCount < enemyMax)
        {
            if (!isSpawning)
                StartSpawning();
        }*/

        if (!isSpawning)
        {
            //Wave has ended.

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            if(enemies.Length <= 0)
            {
                StopSpawning();
                GameManagement.instance.UpdateWave();
                enemyMax += enemiesIncreasePerWave;
                enemiesRemaining = enemyMax;

                Instantiate(upgrades[Random.Range(0, upgrades.Count)], centerSpawn.position, Quaternion.identity);
                StartSpawning();
            }
            //StartSpawning();
        }
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        int wave = GameManagement.instance.GetWave();
        Debug.Log(wave);

        switch(wave)
        {
            case 1:
            case 2:
                {
                    currentTier = TierOnePrefabs;
                    break;
                }
            case 3:
            case 4:
            case 5:
                {
                    currentTier = TierTwoPrefabs;
                    break;
                }
            case 6:
            case 7:
                {
                    currentTier = TierThreePrefabs;
                    break;
                }
            case 8:
            case 9:
                {
                    currentTier = TierFourPrefabs;
                    break;
                }
            default:
                currentTier = TierFivePrefabs;
                break;
        }

        for (int i = 0; i < enemyMax; i++)
        { 
            int rand = Random.Range(1, 5);
            var whereToSpawn = this.transform.GetChild(rand);

            enemyCount++;
            //enemyCurrent++;
            Instantiate(currentTier[Random.Range(0, currentTier.Count)], whereToSpawn.position, Quaternion.identity, whereToSpawn);
      
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
