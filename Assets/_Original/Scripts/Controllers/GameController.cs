using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public enum SceneList
{
    GameMenu,
    GamePlayScene1,
    GamePlayScene2,
    GamePlayScene3,
    none
}

public class GameController : MonoBehaviour
{


    private void Awake()
    {
        OnInitial();
    }

    public void OnInitial()
    {
        GlobalData.isGameOver = false;
        GlobalData.isInGameMenu = false;
        GlobalData.isLevelStart = false;
        GlobalData.isInGameMenu = true;
   
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);

    }


    public void OnGamePlayStart()
    {
        GlobalData.isGameOver = false;
        GlobalData.isInGameMenu = false;
        GlobalData.isLevelStart = false;

      //  UI.instance.ShowUI(UI.instance.uiGamePlay, false);
        GlobalData.isLevelStart = true;
    }

    public void OnGameOver()
    {
        GlobalData.isGameOver = false;
        GlobalData.isInGameMenu = false;
        GlobalData.isLevelStart = false;

       // UI.instance.ShowUI(UI.instance.uiGameOver, false);
        GlobalData.isGameOver = true;
    }



 

   
}
