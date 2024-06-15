using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechHandler : MonoBehaviour
{
    [SerializeField, ReadOnly] private CommonHandler commonHandler;
    [SerializeField] private List<string> speechList;
    [SerializeField,ReadOnly] private FloatingUI floatingUI;
    [SerializeField] private Transform uiTargetPos;

    void Start()
    {
        //  commonHandler = GetComponent<CommonHandler>();
        floatingUI = GetComponentInChildren<FloatingUI>();
        floatingUI.SetFloatingUI(uiTargetPos);
        floatingUI.ToggleUIType(false);
    }

    public void PopupSpeech(int speechLine)
    {
        Debug.Log("Signal Receive " + speechLine);

        floatingUI.ShowSpeechCloud(speechList[speechLine]);
    }


    
}
