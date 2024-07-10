using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[DefaultExecutionOrder(-99)]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource bgAudio;
    public AudioSource thunderSound;
    public AudioSource barSound;
    public AudioSource streetSong;
    public AudioSource silviaPunch;
    public AudioSource silviaGotHit;
    public AudioSource silviaBlock;
    public AudioSource copPunch;
    public AudioSource copGotHit;
    public AudioSource copBlock;

    public AudioSource outroSound;
    public AudioSource interactiveSoundBackground;
    public AudioSource creditSound;
    public AudioSource characterSelection;
    public AudioSource barBGMusic;



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
        if(src == streetSong)
        {
            src.DOKill();
        }
        if(src != null)
        {
            if (isFadein)
            {
                if (isPlay)
                {
                    src.volume = 0f;
                    src.Play();                  
                    src.DOFade(1f, 1f);
                    src.pitch = 1f;
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


    public void DoFadeTogglePause(AudioSource audio, bool isPause)
    {
        if (isPause)
        {
            audio.DOFade(0, 0.7f).OnComplete(() =>
            {
                audio.Pause();
            });
        }
        else
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }

            audio.DOFade(1f, 0.7f);
        }
    }


    public void DoPlayBG()
    {
    //   bgAudio.Play();
        PlaySound(bgAudio, 88.19f);
    }

    public void PlaySound(AudioSource audio, float startTime = 0f, float endTime = 0f, float volume = 1f, float pitch = 1f)
    {        
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
     
    public void StopThisSound(AudioSource src)
    {
        src.Stop();
    }


    public void DoWinPitch(float pitchTime = 15f)
    {
        streetSong.DOPitch(1.3f, pitchTime).SetEase(Ease.Linear);
    }

    public void DoLoosePitch(float pitchTime = 20f)
    {
        streetSong.DOPitch(0, pitchTime).SetEase(Ease.Linear);
    }


}
