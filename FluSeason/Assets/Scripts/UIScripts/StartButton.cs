using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public AudioClip sbSound;

    public void PlayGame()
    {
        AudioManager.instance.PlaySingle(sbSound);
        SceneManager.LoadScene("ShishirScene");
    }
}
