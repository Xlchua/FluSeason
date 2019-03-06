using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public AudioClip sbSound;

    public void PlayGame()
    {
        //AudioManager.instance.PlaySingle(sbSound);
        GameObject [] destroyList = GameObject.FindGameObjectsWithTag("GameController");
        if (destroyList != null)
            foreach (GameObject g in destroyList)
                Destroy(g);

        SceneManager.LoadScene("ShishirScene");
    }
}
