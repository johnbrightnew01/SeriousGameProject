using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractiveIntroSceneView : MonoBehaviour
{
   
    [SerializeField] private AudioSource typeSound;
    [SerializeField] private List<string> textList;
    [Range(0.01f, 1.5f)]
    [SerializeField] private float textSpeed;
    [SerializeField] private CanvasGroup introTextCanvasGroup;
    [SerializeField] private GameObject bodyTextObj;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField, ReadOnly] private bool initalIntroDone;
    [SerializeField, ReadOnly] private bool isTextDone;
    [SerializeField, ReadOnly] private bool isMouseButtonClicked;
    [SerializeField] private GameObject arrow;
    private int counter = 0;

    private void OnEnable()
    {
        UIController.Instance.ToggleInteractiveIntroUI(true);
        StartCoroutine(OnStartSequence());
        SoundManager.Instance.PlaySound(SoundManager.Instance.interactiveSoundBackground, 0,0, 0.671f);
    }

    private void OnDisable()
    {
        UIController.Instance.ToggleInteractiveIntroUI(false);        
    }



    IEnumerator OnStartSequence()
    {
        bodyTextObj.SetActive(false);
        ToggleIntoData(true);
        yield return new WaitUntil(() => initalIntroDone);
        yield return new WaitForSeconds(3f);
        ToggleIntoData(false);

        yield return new WaitUntil(() => initalIntroDone);
        introTextCanvasGroup.gameObject.SetActive(false);

        bodyTextObj.SetActive(true);
        for (int i = 0; i < textList.Count; i++)
        {
            
            arrow.gameObject.SetActive(textList.Count - 1 != i);
            
            isTextDone = false;
            var newCo = StartCoroutine(ShowThisText(i));
            yield return new WaitUntil(() => isMouseButtonClicked);
            isMouseButtonClicked = false;           
            StopCoroutine(newCo);
            bodyText.text = "";
            yield return new WaitForEndOfFrame();
        }
       // yield return new WaitUntil(() => isMouseButtonClicked);
        Controller.self.sequenceController.StartThisScene(Sequence.bar_seq);

    }

    private void DoPlayTypeSound()
    {
        /*if (!typeSound.isPlaying)
        {
            typeSound.Play();
        }*/
        counter++;
        if(counter %4 == 0)
        {
            AddAndPlaySound();
        }
    }

    private void AddAndPlaySound()
    {
        var sound = Instantiate(typeSound);
        sound.Play();
        Destroy(sound.gameObject, 0.5f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTextDone)
        {
            isMouseButtonClicked = true;
        }
    }

    IEnumerator ShowThisText(int indx)
    {
        for (int i = 0; i < textList[indx].Length; i++)
        {           
            bodyText.text += textList[indx][i];
            DoPlayTypeSound();
            yield return new WaitForSeconds(textSpeed);
        }
        bodyText.text += "\n";
        isTextDone = true;
    }


    private void ToggleIntoData(bool isShow, float time = 0.5f)
    {
        if (isShow)
        {
            initalIntroDone = false;
            introTextCanvasGroup.alpha = 0;
            introTextCanvasGroup.DOFade(1f, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                initalIntroDone = true;

            });
        }
        else
        {
            initalIntroDone = false;
            introTextCanvasGroup.alpha = 1;
            introTextCanvasGroup.DOFade(0, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                initalIntroDone = true;

            });
        }
       
    }

}
