using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetSceneView : MonoBehaviour
{
    [SerializeField] private Transform stormeAndPolic;
    [SerializeField] private Transform outsideCam;
    [SerializeField] private CommonHandler player;
    [SerializeField] private List<Transform> cameraMovePosList;
    [SerializeField] private List<Transform> stromePosList;
    [SerializeField] private List<CharacterData> otherCharDataList;
    [SerializeField] private List<Transform> playerPosList;
    [SerializeField] private bool isJumpToFightScene;
    private void OnEnable()
    {
        Controller.self.cameraController.DoActiveVirtualCamera(Controller.self.cameraController.outsideCamera, false);
        if (!isJumpToFightScene)
        {
            StartCoroutine(OnStartSegment());
        }
        else
        {
            StartCoroutine(DirectJumpToFight());
        }
    }

    IEnumerator OtherCharSPeech()
    {
        
        yield return null;
        otherCharDataList[1].speechList[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);

        otherCharDataList[2].speechList[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(4.5f);

        otherCharDataList[3].speechList[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(4.5f);

        otherCharDataList[4].speechList[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(4.5f);

        otherCharDataList[5].speechList[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(4.5f);

        otherCharDataList[6].speechList[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(4.5f);

        otherCharDataList[7].speechList[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(4.5f);
    }

    IEnumerator OnStartSegment()
    {
        
        yield return null;
        player.transform.position = playerPosList[0].position;
        UIController.Instance.ShowLoadingAnimation(5f, 4f);

        SoundManager.Instance.DoPlayBGSound(SoundManager.Instance.streetSong, true, true);
        outsideCam.transform.position = cameraMovePosList[0].position;
        stormeAndPolic.transform.position = stromePosList[0].position;
        yield return new WaitForSeconds(8.5f);
        otherCharDataList[0].speechList[0].gameObject.SetActive(true);
       // yield return new WaitForSeconds(0.5f);
        StartCoroutine(OtherCharSPeech());
        yield return new WaitForSeconds(5f);
        outsideCam.transform.DOMove(cameraMovePosList[1].position, 25f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(25f);
        outsideCam.transform.DOMove(cameraMovePosList[2].position, 5f).SetEase(Ease.Linear);
        
        var cmnView = player.GetComponent<CommonHandler>();
        yield return new WaitForSeconds(1f);
        cmnView.BotControl(true);
        player.transform.DOMove(playerPosList[1].position, 6f).SetEase(Ease.Linear).OnComplete(() =>
        {
            cmnView.BotControl(false);
        });
        yield return new WaitForSeconds(7.5f);
        player.GetComponent<PlayerView>().playerPopUpCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        stormeAndPolic.gameObject.SetActive(false);
        UIController.Instance.ShowLoadingAnimation(1f);
        Controller.self.cameraController.DoActiveVirtualCamera(Controller.self.cameraController.fightCamera, false);
        yield return new WaitForSeconds(0.5f);

        Controller.self.inputController.EnableInput();
        //  Controller.self.playerController.StartSpawningEnemy();
        UIGamePlay.Instance.TogglePlayerHpPanel(true);

    }

    IEnumerator DirectJumpToFight()
    {
        stormeAndPolic.gameObject.SetActive(false);
        player.transform.position = playerPosList[1].position;
        Controller.self.cameraController.DoActiveVirtualCamera(Controller.self.cameraController.fightCamera, false);
        yield return new WaitForSeconds(0.45f);
        Controller.self.inputController.EnableInput();
        Controller.self.playerController.StartSpawningEnemy();
        UIGamePlay.Instance.TogglePlayerHpPanel(true);
    }


}
