﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public float timeElapsed = 0f;

    public int wave = 10000;

    public static GameManagement instance;

    public TextMeshProUGUI wave_meshProUGUI;

    public TextMeshProUGUI timer_meshProUGUI;

    private int waveAddition = 0;

    private int waveIncrement = 10;

    GameObject enemyComponent;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //enemyComponent.GetComponent<EnemySpawner>().enemyLeft.AddListener(updateCount);
        //Every 10 seconds new wave. A little janky because timeElapsed is a float so regular modulus doesn't work
        //if (timeElapsed >= waveAddition)
        //UpdateWave();

        //timeElapsed += Time.deltaTime;
            //timer_meshProUGUI.text = string.Format("Enemies Left: {0:0}", EnemySpawner.enemies.Length);

    }

    public void UpdateCount(int length)
    {
        timer_meshProUGUI.text = string.Format("Enemies Left: {0:0}", length);
    }

    public void UpdateWave()
    {
        wave += 1;
        waveAddition += waveIncrement;

        wave_meshProUGUI.text = string.Format("Wave: {0:0}", wave);
        //Do things that happen after a new wave starts
    }

    public int GetWave()
    {
        return wave;
    }
}
