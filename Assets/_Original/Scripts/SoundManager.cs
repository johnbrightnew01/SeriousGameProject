using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource bgAudio;
    public AudioSource thunderSound;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void DoPlayBG()
    {
    //   bgAudio.Play();
        PlaySound(bgAudio, 88.19f);
    }

    public void PlaySound(AudioSource audio, float startTime = 0f, float endTime = 0f, float volume = 1f, float pitch = 1f)
    {
        Debug.Log("thunder");
        if (true)
        {
            audio.pitch = pitch;
            audio.volume = volume;
            if (startTime != 0)
            {
                audio.time = startTime;
                audio.Play();
                audio.SetScheduledEndTime(AudioSettings.dspTime + (endTime - startTime));
            }
            else
            {

                audio.Play();
            }

        }
    }


}
