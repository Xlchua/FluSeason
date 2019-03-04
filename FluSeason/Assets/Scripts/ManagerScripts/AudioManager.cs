using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    //Audio source variables - background && sound effects.
    public AudioSource m_BGMPlayer, m_SFXPlayer;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySFX(string clip)
    {
        PlaySFX(clip, Random.Range(0.5f, 1f));
    }

    public void PlaySFX(string clip, float volume)
    {
        AudioClip newClip = Resources.Load($"Audio/{clip}") as AudioClip;
        m_SFXPlayer.PlayOneShot(newClip, volume);
    }
}
