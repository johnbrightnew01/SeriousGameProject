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
    public GameObject menuScene;
    public GameObject gamePlayScene1;

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
        ShowIntroScene();
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

    public void StartGamePlayState(SceneList scn)
    {
        if(scn == SceneList.GamePlayScene1)
        {
            UIController.Instance.ShowLoadingAnimation();
            UIController.Instance.ShowUI(UIController.UiType.gamePlayUI, true);
            menuScene.SetActive(false);
            gamePlayScene1.SetActive(true);
          //  Controller.self.timeLineController.OnStartScene(scn);
            Controller.self.cameraController.DoActiveVirtualCamera(Controller.self.cameraController.playerCam, false);
        }
    }

    public void ShowIntroScene()
    {
        UIController.Instance.ShowUI( UIController.UiType.menuUI, true);
        

        menuScene.gameObject.SetActive(true);
        gamePlayScene1.gameObject.SetActive(false);
        Controller.self.cameraController.DoActiveVirtualCamera(Controller.self.cameraController.menuCam, false);
    }

    public void DoStartShooting()
    {
        Invoke("MoveToTheShootingCamera", 1f);

    }

    private void MoveToTheShootingCamera() // call from event(Invoke)
    {
        Controller.self.cameraController.DoActiveVirtualCamera(Controller.self.cameraController.shootCam, true);
        UIController.Instance.targetController.StartFight();
    }
   
}
