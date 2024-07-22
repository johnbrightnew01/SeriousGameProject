using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private float maxWaitTimeToShowIdleVideo = 120f;
    [SerializeField,ReadOnly]private float timeDeltaCounter = 0;
    [SerializeField] private GameObject idleVideoPanel;
    [field: SerializeField, ReadOnly] public bool isInIdleMode { get; private set; }
    private void Awake()
    {
        idleVideoPanel.SetActive(false);
        OnInitial();
        isInIdleMode = false;
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

    private void Update()
    {
        if (IsTakingAnyInput())
        {
            timeDeltaCounter = 0;
            
        }

        if(timeDeltaCounter < maxWaitTimeToShowIdleVideo)
        {
            timeDeltaCounter += Time.deltaTime;
        }
        else
        {
            isInIdleMode = true;
            ToggleIdelVideoPanel(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isInIdleMode)
        {
            isInIdleMode = false;
            ToggleIdelVideoPanel(false);
            timeDeltaCounter = 0;
            SceneManager.LoadScene(0);
        }

    }

    private void ToggleIdelVideoPanel(bool isEnable)
    {
        idleVideoPanel.SetActive(isEnable);
        if (isEnable)
        {
            Controller.self.sequenceController.TurnOffEverySequence();
        }
    }

    private bool IsTakingAnyInput()
    {
        if(Controller.self.inputController.isTakingAnyInput)
        {
            Controller.self.inputController.isTakingAnyInput = false;
            timeDeltaCounter = 0;
            return true;
        }
        return false;
    }

    public void OnCancelVideoInput()
    {
        timeDeltaCounter = 0;
        // close the video;
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

        UIController.Instance.ShowUI(UIController.UiType.gameOverUI, true);
        GlobalData.isGameOver = true;
    }



 

   
}
