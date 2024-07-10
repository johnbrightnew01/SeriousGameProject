using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionSceneView : MonoBehaviour
{
    [SerializeField] private Color nonSelectedCharColor;

    [SerializeField] private bool isLockedChar_1 = true;
    [SerializeField] private GameObject char1;
    [SerializeField] private GameObject locker_1;
    [SerializeField] private Image char_1Icon;
    [Space(10)]
    [SerializeField] private bool isLockedChar_2 = false;
    [SerializeField] private GameObject char2;
    [SerializeField] private GameObject locker_2;
    [SerializeField] private Image char_2Icon;
    [Space(10)]
    [SerializeField] private bool isLockedChar_3 = false;
    [SerializeField] private GameObject char3;
    [SerializeField] private GameObject locker_3;
    [SerializeField] private Image char_3Icon;
    [SerializeField, ReadOnly] private int currentSelectedChar;


    private void OnEnable()
    {
        locker_1.gameObject.SetActive(isLockedChar_1 ? true : false);
        locker_2.gameObject.SetActive(isLockedChar_2 ? true : false);
        locker_3.gameObject.SetActive(isLockedChar_3 ? true : false);
        char_1Icon.color = (isLockedChar_1 ? nonSelectedCharColor : Color.white);
        char_2Icon.color = (isLockedChar_2 ? nonSelectedCharColor : Color.white);
        char_3Icon.color = (isLockedChar_3 ? nonSelectedCharColor : Color.white);
        SelectChar_1();
        SoundManager.Instance.PlaySound(SoundManager.Instance.characterSelection);
    }

    private void OnDisable()
    {
        SoundManager.Instance.StopThisSound(SoundManager.Instance.characterSelection);
    }

    public void SelectChar_1() // call from Ui
    {
        char1.SetActive(true);
        char2.SetActive(false);
        char3.SetActive(false);
        currentSelectedChar = 1;
        char_1Icon.color = Color.white;
        char_2Icon.color = nonSelectedCharColor;
        char_3Icon.color = nonSelectedCharColor;

    }

    public void SelectChar_2()// call from Ui
    {
        char1.SetActive(false);
        char2.SetActive(true);
        char3.SetActive(false);
        currentSelectedChar = 2;
        char_1Icon.color = nonSelectedCharColor;
        char_2Icon.color = Color.white;
        char_3Icon.color = nonSelectedCharColor;
    }

    public void SelectChar_3()// call from Ui
    {
        char1.SetActive(false);
        char2.SetActive(false);
        char3.SetActive(true);
        currentSelectedChar = 3;
        char_1Icon.color = nonSelectedCharColor;
        char_2Icon.color = nonSelectedCharColor;
        char_3Icon.color = Color.white;
    }

    public void SelectThisChar()
    {
        UIController.Instance.ShowLoadingAnimation(0.5f);
        Controller.self.sequenceController.StartThisScene(Sequence.street_seq);
        Debug.Log("SelectThisCHar");
        if(currentSelectedChar == 1)
        {

        }

    }
    

}
