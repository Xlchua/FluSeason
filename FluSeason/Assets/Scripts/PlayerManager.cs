using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class IntUnityEvent : UnityEvent<int> {};

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    public int currInfection = 0;

    public int maxInfection = 100;

    public IntUnityEvent playerInfected = new IntUnityEvent();


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addInfection(int infection)
    {
        currInfection += infection;
        playerInfected.Invoke(currInfection);
        if (currInfection >= maxInfection)
        {
            Debug.Log("Player has been infected");
            SceneManager.LoadScene("GameOver");
        }  
    }


}
