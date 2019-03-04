using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Infection : MonoBehaviour
{
    TextMeshProUGUI infectionText;
    GameObject playerManagerComponent;
    Image imageComponet;
    public Sprite zero;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite five;
    public Sprite six;
    public Sprite seven;
    public Sprite eight;
    public Sprite nine;
    public Sprite ten;

    // Start is called before the first frame update
    void Start()
    {
        //infectionText = this.GetComponent<TextMeshProUGUI>();

        imageComponet = GetComponent<Image>();

        playerManagerComponent = GameObject.Find("PlayerManager");

        playerManagerComponent.GetComponent<PlayerManager>().playerInfected.AddListener(currInfectionListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void currInfectionListener(int infection)
    {
        //infectionText.text = string.Format("Infection: {000}%", infection);
        if (infection == 10)
        {
            imageComponet.sprite = one;
        }
        else if (infection == 20)
        {
            imageComponet.sprite = two;
        }
        else if (infection == 30)
        {
            imageComponet.sprite = three;
        }
        else if (infection == 40)
        {
            imageComponet.sprite = four;
        }
        else if (infection == 50)
        {
            imageComponet.sprite = five;
        }
        else if (infection == 60)
        {
            imageComponet.sprite = six;
        }
        else if (infection == 70)
        {
            imageComponet.sprite = seven;
        }
        else if (infection == 80)
        {
            imageComponet.sprite = eight;
        }
        else if (infection == 90)
        {
            imageComponet.sprite = nine;
        }
        else if (infection == 100)
        {
            imageComponet.sprite = ten;
        }
        
    }
}
