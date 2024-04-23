using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector timeLine;


    public void OnStartScene(SceneList scn)
    {
        if(scn == SceneList.GamePlayScene1)
        {
            timeLine.Play();
        }
    }

    public void DoPause()
    {
        timeLine.Pause();
    }

    public void DoPlayTimeLine()
    {
        timeLine.Play();
    }

    public void StopPlayerAnimation()
    {
        Controller.self.playerController.playerView.PlayAnimation(PlayerView.PlayerDirection.right, PlayerView.PlayerAnimationType.idle);
        Controller.self.gameController.DoStartShooting();
    }



}
