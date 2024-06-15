using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-98)]
public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public float timeCounter;
    public PlayerView _playerView;
    [SerializeField]private List<CommonHandler> orgPoliceList;
    public CommonHandler _playerCommonHandler;
    public List<CommonHandler> _enemyList;
    [SerializeField] private List<Transform> policeSpawnPosList;
    private int posCounter = 0;
    [SerializeField, ReadOnly] private int numberOfPoliceToSpawn = 5;
    [SerializeField] private float spawnDelta = 2f;


    private void Start()
    {
      //  StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        int counter = 0;
        while (numberOfPoliceToSpawn > counter)
        {
            counter++;
            DoSpawnPolice();   
            yield return new WaitForSeconds(spawnDelta);
        }
    }


    public void DoSpawnPolice(bool isSpawsRandomPos = false)
    {
        var pos = policeSpawnPosList[Random.Range(0, policeSpawnPosList.Count)];
        if (!isSpawsRandomPos)
        {
            posCounter++;
            if(posCounter >= policeSpawnPosList.Count)
            {
                posCounter = 0;
            }
            pos = policeSpawnPosList[posCounter];
        }
        var plc = Instantiate(orgPoliceList[Random.Range(0, orgPoliceList.Count)], pos.position, Quaternion.identity);
        _enemyList.Add(plc);
    }


    public CommonHandler GetTargetPlayer()
    {
        if (_playerCommonHandler.isDead) return null;
        return _playerCommonHandler;
    }


    public void RemoveThisEnemy(CommonHandler cmnH)
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            if (_enemyList[i] == cmnH)
            {
                _enemyList.RemoveAt(i);
                break;
            }
        }
    }

}
