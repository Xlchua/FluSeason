using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Infection : MonoBehaviour
{
    TextMeshProUGUI infectionText;
    GameObject playerManagerComponent;

    // Start is called before the first frame update
    void Start()
    {
        infectionText = this.GetComponent<TextMeshProUGUI>();

        playerManagerComponent = GameObject.Find("PlayerManager");

        playerManagerComponent.GetComponent<PlayerManager>().playerInfected.AddListener(currInfectionListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void currInfectionListener(int infection)
    {
        infectionText.text = string.Format("Infection: {000}%", infection);
    }
}
