using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
   

    public void StartTheGame()
    {
        Controller.self.gameController.StartGamePlayState(SceneList.GamePlayScene1);
    }
}
