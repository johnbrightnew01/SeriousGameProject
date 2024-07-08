using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoToggleView : MonoBehaviour
{
    public VideoPlayer vPlayer;
    public GameObject playButton;
    public void TogglePlayPause()
    {
        if (vPlayer.isPlaying)
        {
            vPlayer.Pause();
            playButton.SetActive(true);
            SoundManager.Instance.DoFadeTogglePause(SoundManager.Instance.outroSound,false);
        }
        else
        {
            vPlayer.Play();
            playButton.SetActive(false);
            SoundManager.Instance.DoFadeTogglePause(SoundManager.Instance.outroSound, true);
        }
    }
}
