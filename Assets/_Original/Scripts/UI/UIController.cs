using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public UIMenu uiMenu;
    public UIGamePlay uiGamePlay;
    public UIGameOver uiGameOver;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
   
}
