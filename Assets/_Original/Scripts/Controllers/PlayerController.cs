using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[DefaultExecutionOrder(-98)]
public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnSequence
    {    

        public List<CommonHandler> policeSpawnList;
        public List<Transform> policeSpawnPosList;
        public List<float> spawnDelay;
        public float zStartPos;
        public float zEndPos;

    }

    public List<EnemySpawnSequence> policeSpawnSequenceList;

    [HideInInspector]
    public float timeCounter;
    public PlayerView _playerView;
    [SerializeField]private List<CommonHandler> orgPoliceList;
    public CommonHandler _playerCommonHandler;
    public List<CommonHandler> _enemyList;
  
    private int posCounter = 0;
    [SerializeField, ReadOnly] private int numberOfPoliceToSpawn = 5;
    [SerializeField] private float spawnDelta = 2f;
    [SerializeField, ReadOnly] private bool onStartSpawningEnemy = false;
    [SerializeField, ReadOnly] private int seqNo = 0;
    [SerializeField, ReadOnly] private int childNo = 0;
    private bool isWaveSpawnnerRunning = false;
    private void Awake()
    {
        onStartSpawningEnemy = false;
        isWaveSpawnnerRunning = false;
    }
    private void Start()
    {
      //  StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawningOverTime()
    {
        int counter = 0;
        while (numberOfPoliceToSpawn > counter)
        {
            counter++;
            DoSpawnPolice();   
            yield return new WaitForSeconds(spawnDelta);
        }
    }

    public void StartSpawningEnemy()
    {
        //  StartCoroutine(StartSpawningOverTime());
        onStartSpawningEnemy = true;
    }

    private void Update()
    {
        if (onStartSpawningEnemy)
        {
            if (_enemyList.Count == 0 || _enemyList[_enemyList.Count-1]._healthHandler.remainHp <= 10f)
            {
                if (!isWaveSpawnnerRunning)
                {
                    StartCoroutine(SpawnNewEnemyPolice());
                }
            }
        }
    }

    


    IEnumerator SpawnNewEnemyPolice()
    {
        isWaveSpawnnerRunning = true;
        if(seqNo <= policeSpawnSequenceList.Count - 1)
        {
            for (int i = 0; i < policeSpawnSequenceList[seqNo].policeSpawnList.Count; i++)
            {
                yield return new WaitForSeconds(policeSpawnSequenceList[seqNo].spawnDelay[i]);
                var plc = Instantiate(policeSpawnSequenceList[seqNo].policeSpawnList[i], policeSpawnSequenceList[seqNo].policeSpawnPosList[i].position, Quaternion.identity);
                _enemyList.Add(plc);
            }
            seqNo++;
        }
        isWaveSpawnnerRunning = false;
    }




    public void DoSpawnPolice(bool isSpawsRandomPos = false)
    {
        return;
   /*     var pos = policeSpawnPosList[Random.Range(0, policeSpawnPosList.Count)];
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
        _enemyList.Add(plc);*/
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
