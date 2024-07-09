using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private IntroSceneView intro;
    [SerializeField] private GameObject creditObj;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float moveTime;
    [SerializeField] private Transform lastClampPos;

    private void OnEnable()
    {
        creditObj.transform.position = startPos.position;
        intro.DoToggleFadeOutAudio(false);
        SoundManager.Instance.PlaySound(SoundManager.Instance.creditSound);
        DoMove();

    }

    private void OnDisable()
    {
        intro.DoToggleFadeOutAudio(true);
        creditObj.transform.DOKill();
        SoundManager.Instance.StopThisSound(SoundManager.Instance.creditSound);
    }

    private void DoMove()
    {
        creditObj.transform.DOMove(endPos.position, moveTime).SetEase(Ease.Linear).OnComplete(() =>
        {
           // OnMoveDone();
        });
    }

    private void OnMoveDone()
    {
        creditObj.transform.position = lastClampPos.position;
    }

    public void Return()
    {
        this.gameObject.SetActive(false);
    }

}
