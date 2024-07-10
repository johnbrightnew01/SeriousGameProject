using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Sequence
{
    intro_seq,
    intreactive_intro_seq,
    bar_seq,
    character_selection,
    street_seq,
    outro_seq,
    none
}

public class SequenceController : MonoBehaviour
{
    public const string LastSequenceID = "LastSequenceID";
    public IntroSceneView introScene;
    public InteractiveIntroSceneView interactiveIntro;
    public BarSceneView barScene;
    public CharacterSelectionSceneView characterSelectionScene;
    public StreetSceneView streetScene;
    public OutroSceneView outroScene;
    [SerializeField] private bool isOverrideSequence = true;


    private void Awake()
    {
        if (!PlayerPrefs.HasKey(LastSequenceID))
        {
            PlayerPrefs.SetInt(LastSequenceID , 0);
        }
    }

    [field: SerializeField] public Sequence currentSequence { get; private set; } = Sequence.intro_seq;

    private void Start()
    {
        if (isOverrideSequence)
        {
            StartThisScene(currentSequence);
        }
        else
        {
            StartThisScene(GetTheSequenceEnum(PlayerPrefs.GetInt(LastSequenceID)));
        }
    }

    public void StartThisScene(Sequence scene)
    {
        characterSelectionScene.gameObject.SetActive((scene == Sequence.character_selection) ? true : false);
        introScene.gameObject.SetActive((scene == Sequence.intro_seq)?true: false);
        interactiveIntro.gameObject.SetActive((scene == Sequence.intreactive_intro_seq)?true: false);
        barScene.gameObject.SetActive((scene == Sequence.bar_seq) ? true : false);
        streetScene.gameObject.SetActive((scene == Sequence.street_seq) ? true : false);
        outroScene.gameObject.SetActive((scene == Sequence.outro_seq) ? true : false);
        UpdateSequence(scene);

        PlayerPrefs.SetInt(LastSequenceID, (int)scene);
    }

    public void UpdateSequence(Sequence setTo)
    {
        currentSequence = setTo;
        
    }

    public void GoToMenuScene()
    {
        StartThisScene(Sequence.intro_seq);
        Controller.self.sequenceController.introScene.ShowTheMenu();

    }


    private Sequence GetTheSequenceEnum(int indx)
    {
        if(indx == (int)Sequence.intro_seq)
        {
            return Sequence.intro_seq;
        }
        else if (indx == (int)Sequence.intreactive_intro_seq)
        {
            return Sequence.intreactive_intro_seq;
        }
        else if (indx == (int)Sequence.bar_seq)
        {
            return Sequence.bar_seq;
        }
        else if (indx == (int)Sequence.character_selection)
        {
            return Sequence.character_selection;
        }
        else if (indx == (int)Sequence.street_seq)
        {
            return Sequence.street_seq;
        }
        else if (indx == (int)Sequence.outro_seq)
        {
            return Sequence.outro_seq;
        }
        return Sequence.intro_seq;
    }






}
