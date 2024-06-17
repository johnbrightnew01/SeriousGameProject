using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Sequence
{
    intro_seq,
    intreactive_intro_seq,
    bar_seq,
    street_seq,
    outro_seq,
    none
}

public class SequenceController : MonoBehaviour
{
    public IntroSceneView introScene;
    public InteractiveIntroSceneView interactiveIntro;
    public BarSceneView barScene;
    public StreetSceneView streetScene;
    public OutroSceneView outroScene;
    [SerializeField] private bool isStartSequenceFromBeginning = true;

    [field: SerializeField] public Sequence currentSequence { get; private set; } = Sequence.intro_seq;

    private void Start()
    {
        if (isStartSequenceFromBeginning)
        {
            StartThisScene(currentSequence);
        }
    }

    public void StartThisScene(Sequence scene)
    {
        introScene.gameObject.SetActive((scene == Sequence.intro_seq)?true: false);
        interactiveIntro.gameObject.SetActive((scene == Sequence.intreactive_intro_seq)?true: false);
        barScene.gameObject.SetActive((scene == Sequence.bar_seq) ? true : false);
        streetScene.gameObject.SetActive((scene == Sequence.street_seq) ? true : false);
        outroScene.gameObject.SetActive((scene == Sequence.outro_seq) ? true : false);
        UpdateSequence(scene);
    }

    public void UpdateSequence(Sequence setTo)
    {
        currentSequence = setTo;
        
    }







}
