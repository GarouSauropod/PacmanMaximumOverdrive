using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    private static SoundManager soundManager;

    public static SoundManager instance
    {
        get
        {
            if (!soundManager)
            {
                soundManager = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (!soundManager)
                {
                    Debug.LogError("There needs to be one active SoundManager script on a GameObject in your scene.");
                }
            }

            return soundManager;
        }
    }

    [SerializeField]
    private AudioClip music;
    [SerializeField]
    private AudioSource source;

    void Start()
    {
        source.clip = music;
        source.Play();
    }

}
