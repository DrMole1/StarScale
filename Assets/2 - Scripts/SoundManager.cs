using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // ============================ VARIABLES ============================

    static GameObject instance;

    [Header("Sound Clips")]
    public AudioClip[] clips;

    private AudioSource audioSource;

    // ===================================================================


    private void Awake()
    {
        if (instance)
        { // if bgAudio already references some object...
            Destroy(gameObject); // suicide
        }
        else
        { // if bgAudio is null, this is the first TronBG object:
            instance = gameObject; // assign this one
        }

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void playAudioClip(int index)
    {
        if (clips[index] != null)
        {
            audioSource.volume = 1;
            audioSource.pitch = 1;
            audioSource.PlayOneShot(clips[index]);
        }
        else
            Debug.Log("Attention, clip vide.");
    }

    public void playAudioClipWithPitch(int index, float pitch)
    {
        if (clips[index] != null)
        {
            audioSource.volume = 1;
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(clips[index]);
        }
        else
            Debug.Log("Attention, clip vide.");
    }

    public void playAudioClipWithVolume(int index, float volume)
    {
        if (clips[index] != null)
        {
            audioSource.volume = volume;
            audioSource.pitch = 1;
            audioSource.PlayOneShot(clips[index]);
        }
        else
            Debug.Log("Attention, clip vide.");
    }

    public void resetVolumeSound()
    {
        audioSource.volume = 1;
    }
}
