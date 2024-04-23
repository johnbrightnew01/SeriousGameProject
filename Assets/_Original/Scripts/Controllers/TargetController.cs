using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public List<Transform> thorwPointList;
    [SerializeField]
    private float minTargetTime = 1f;
    [SerializeField]
    private float maxTargetTime = 3f;
    [SerializeField, ReadOnly]
    private Transform currentTargetPoint;
    [SerializeField]
    private PointerView pView;
    [SerializeField, ReadOnly]
    private float currentTargetTime;
    [SerializeField, ReadOnly]
    private float timeDeltaCounter;
    [SerializeField, ReadOnly]
    private bool isFightStart = false;

    private void Start()
    {
        isFightStart = false;
        pView.ResetPointer();
    }


    private void Update()
    {
        if (!isFightStart) return;
        if(currentTargetPoint == null)
        {
            currentTargetPoint = GetRandomPointToThorw();
            currentTargetTime = Random.Range(minTargetTime, maxTargetTime);
        }
        if (currentTargetPoint == null) return;
        if(timeDeltaCounter < currentTargetTime)
        {
            pView.transform.position = currentTargetPoint.position;
            pView.DoScalePointer(1f - (timeDeltaCounter / currentTargetTime));
            timeDeltaCounter += Time.deltaTime;
        }
        else
        {
            pView.ResetPointer();
            currentTargetPoint = null;
            timeDeltaCounter = 0;
        }

    }

    public void StartFight()
    {
        Invoke("WaitAndStartFight", 1f);
    }

    public void WaitAndStartFight()
    {
        isFightStart = true;
    }

    private Transform GetRandomPointToThorw()
    {
        return thorwPointList[Random.Range(0, thorwPointList.Count)];
    }
}
