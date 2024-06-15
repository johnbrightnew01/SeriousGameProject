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
    [SerializeField] private TextMeshProUGUI introText;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField, ReadOnly] private bool initalIntroDone;
    [SerializeField, ReadOnly] private bool isTextDone;
    [SerializeField, ReadOnly] private bool isMouseButtonClicked;
    private int counter = 0;
    private void OnEnable()
    {
        UIController.Instance.ToggleInteractiveIntroUI(true);
        StartCoroutine(OnStartSequence());
    }

    private void OnDisable()
    {
        UIController.Instance.ToggleInteractiveIntroUI(false);
        
    }



    IEnumerator OnStartSequence()
    {
        StartTheIntroShit();
        yield return new WaitUntil(() => initalIntroDone);
        for (int i = 0; i < textList.Count; i++)
        {
            isTextDone = false;
            var newCo = StartCoroutine(ShowThisText(i));
            yield return new WaitUntil(() => isMouseButtonClicked);
            isMouseButtonClicked = false;           
            StopCoroutine(newCo);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitUntil(() => isMouseButtonClicked);
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


    private void StartTheIntroShit()
    {
        var Targtclr = introText.color;
        var starCol = new Color(Targtclr.r, Targtclr.g, Targtclr.b, 0);
        introText.color = starCol;
        introText.DOColor(Targtclr, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            initalIntroDone = true;
        });
    }

}
