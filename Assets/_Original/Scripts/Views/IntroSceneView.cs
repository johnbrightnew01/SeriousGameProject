using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class IntroSceneView : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private VideoPlayer introVideo;

    [SerializeField] private float thunderSoundTimeBefore = 20f;
    [SerializeField] private float thunderSoundTimeAfter = 10f;
    [SerializeField] private float afterVGATime = 5f;
    [SerializeField] private Transform angryPixelLogo;
    [SerializeField] private Transform theButtonPanel;
    [SerializeField] private Transform grid;
    [SerializeField] private GameObject animatedPage;


    private void OnEnable()
    {
        // SoundManager.Instance.PlaySound(SoundManager.Instance.thunderSound, 0.5f);
        //SoundManager.Instance.PlaySound(SoundManager.Instance.bgAudio, 88.19f);
       // SoundManager.Instance.DoPlayBG();
        canvas.gameObject.SetActive(false);
        grid.gameObject.SetActive(false);
        animatedPage.gameObject.SetActive(true);
        angryPixelLogo.gameObject.SetActive(false);
        theButtonPanel.gameObject.SetActive(false);
        OnStartTheScene();
     
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            CancelInvoke();
            Invoke("OnEndTheScene",0.01f);
        }
    }
    public void OnStartTheScene()
    {
        introVideo.Play();
        introVideo.isLooping = true;
        Invoke("OnEndTheScene",(float) 89.5f);
        Invoke("DoFadeWithBgSound", 87.2f);

    }

    public void ShowTheMenu()
    {
        CancelInvoke("OnEndTheScene");
        CancelInvoke("DoFadeWithBgSound");
        canvas.gameObject.SetActive(true);
    }

    private void DoFadeWithBgSound() 
    {
        SoundManager.Instance.DoPlayBG();
    }

    private void OnDisable()
    {
        CancelInvoke("OnEndTheScene");
        CancelInvoke("DoFadeWithBgSound");
    }

    private void OnEndTheScene()
    {

       // SoundManager.Instance.DoPlayBG();
        canvas.gameObject.SetActive(true);
        StartCoroutine(StartTheSequence());

        // Black screen animation

      //  this.gameObject.SetActive(false);

    }

    IEnumerator StartTheSequence()
    {     
        SoundManager.Instance.PlaySound(SoundManager.Instance.thunderSound, 4.20f);
        yield return new WaitForSeconds(thunderSoundTimeBefore);
        angryPixelLogo.gameObject.SetActive(true);
        yield return new WaitForSeconds(thunderSoundTimeAfter);
        SoundManager.Instance.thunderSound.Stop();
        angryPixelLogo.gameObject.SetActive(false);
        yield return new WaitForSeconds(afterVGATime);
        theButtonPanel.gameObject.SetActive(true);
        animatedPage.gameObject.SetActive(false);
        grid.gameObject.SetActive(true);
    }

    public void DoToggleFadeOutAudio(bool isPlay)
    {

        if (isPlay)
        {
            StartCoroutine(DoFade(1f, 0));
        }
        else
        {
            StartCoroutine(DoFade(0, 1f));
        }
    }
    
    IEnumerator DoFade(float target, float startFrom)
    {
        float timeELaps = 0;
        float vol = startFrom;
        while (0.5f > timeELaps)
        {
            timeELaps += Time.deltaTime;
            vol = Mathf.Lerp(startFrom, target, timeELaps / 0.5f);
            introVideo.SetDirectAudioVolume(0, vol);
            yield return null;

        }
    }

}
