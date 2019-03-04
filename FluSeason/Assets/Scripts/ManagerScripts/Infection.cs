using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Infection : MonoBehaviour
{
    GameObject playerManagerComponent;
    Image infectionBarComponet;
    float maxInfection = 100f;
    

    // Start is called before the first frame update
    void Start()
    {

        infectionBarComponet = GetComponent<Image>();

        playerManagerComponent = GameObject.Find("PlayerManager");

        playerManagerComponent.GetComponent<PlayerManager>().playerInfected.AddListener(currInfectionListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void currInfectionListener(int infection)
    {
        infectionBarComponet.fillAmount = infection / maxInfection;
    }
}
