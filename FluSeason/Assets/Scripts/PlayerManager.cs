using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    public float currInfection = 0f;

    public float maxInfection = 100f;

    public UnityEvent playerInfected;


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

    public void addInfection(float infection)
    {
        currInfection += infection;
        if (currInfection >= maxInfection)
        {
            playerInfected.Invoke();
            Debug.Log("Player has been infected");
        }  
    }


}
