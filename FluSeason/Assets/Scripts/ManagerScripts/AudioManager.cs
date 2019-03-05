using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //public static AudioManager instance { get; private set; }
    public static AudioManager instance = null;

    //Audio source variables - background && sound effects.
    public AudioSource efxSource, musicSource;

    //Pitch
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

   
    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx (params AudioClip[] clips)
    {
        //generate random number to play random clip in array/list.
        int randomIndex = Random.Range(0, clips.Length);

        //Pitch..?
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        efxSource.pitch = randomPitch;

        //Set clip to randomly chosen index.
        efxSource.clip = clips[randomIndex];

        //Play
        efxSource.Play();
    }
}
