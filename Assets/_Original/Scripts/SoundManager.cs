using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource bgAudio;
    public AudioSource thunderSound;
    public AudioSource barSound;
    public AudioSource streetSong;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void ToggleBarSound(bool isPlay)
    {
        if (isPlay)
        {
            barSound.Play();
        }
        else
        {
            barSound.DOFade(0, 1f).OnComplete(() => { 
            
            
                barSound.Stop();
            });
        }
    }

    public void DoPlayBGSound(AudioSource src, bool isPlay = true, bool isFadein = false)
    {
        if(src != null)
        {
            if (isFadein)
            {
                if (isPlay)
                {
                    src.volume = 0f;
                    src.Play();                  
                    src.DOFade(1f, 1f);
                }
                else
                {
                    src.DOFade(0f, 1f).OnComplete(() => {
                        src.Stop();
                    });
                }
              
            }
            else
            {
                if (isPlay)
                {
                    src.Play();
                }
                else
                {
                    src.Stop();
                }
            }

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