using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class IntroSceneView : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private VideoPlayer introVideo;

    [SerializeField] private float thunderSoundTimeBefore = 20f;
    [SerializeField] private float thunderSoundTimeAfter = 10f;
    [SerializeField] private Transform angryPixelLogo;
    [SerializeField] private Transform theButtonPanel;
    [SerializeField] private Transform grid;


    private void OnEnable()
    {
        // SoundManager.Instance.PlaySound(SoundManager.Instance.thunderSound, 0.5f);
        //SoundManager.Instance.PlaySound(SoundManager.Instance.bgAudio, 88.19f);
       // SoundManager.Instance.DoPlayBG();
        canvas.gameObject.SetActive(false);
        grid.gameObject.SetActive(false);

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
        Invoke("OnEndTheScene",(float) 89.5f);
        Invoke("DoFadeWithBgSound", 87.2f);

    }
    private void DoFadeWithBgSound() 
    {
        SoundManager.Instance.DoPlayBG();
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
        yield return new WaitForSeconds(0.5f);
        theButtonPanel.gameObject.SetActive(true);
        grid.gameObject.SetActive(true);
    }


}
