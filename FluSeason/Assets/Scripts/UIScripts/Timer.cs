using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeElapsed = 0f;
    TextMeshProUGUI m_meshProUGUI;

    // Start is called before the first frame update
    void Start()
    {
        m_meshProUGUI = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        m_meshProUGUI.text = string.Format("Timer: {0:0.00}", timeElapsed);
    }
}
