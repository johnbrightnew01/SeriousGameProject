using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject creditObj;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float moveTime;
    [SerializeField] private Transform lastClampPos;

    private void OnEnable()
    {
        creditObj.transform.position = startPos.position;
        DoMove();
    }

    private void DoMove()
    {
        creditObj.transform.DOMove(endPos.position, moveTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            OnMoveDone();
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
