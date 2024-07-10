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
     
    }

    [System.Serializable]
    public class Wave
    {
        public List<EnemySpawnSequence> policeSpawnSequence;
        public float zStartPos;
        public float zEndPos;
    }

    public List<Wave> waveList;  

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
    [SerializeField, ReadOnly] private int waveNo = 0;
    [SerializeField, ReadOnly] private int seqNo = 0;

    [SerializeField, ReadOnly] private int childNo = 0;
    private bool isWaveSpawnnerRunning = false;
    [field: ReadOnly, SerializeField] public bool isGettingReadyForWave { get; private set; } = false;
    [SerializeField, ReadOnly] public bool isEnemyBossDead = false;

    public List<Wave> BackUpWavelist;
    private void Awake()
    {
        onStartSpawningEnemy = false;
        isWaveSpawnnerRunning = false;
        seqNo = 0;
        isGettingReadyForWave = false;
        isEnemyBossDead = false;
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
        StartCoroutine(CheckForGameOver());
    }

    public void GetReadyForWave(bool isGettingReady) 
    {
        isGettingReadyForWave = isGettingReady;
    }

    private void Update()
    {
        if (onStartSpawningEnemy)
        {
            if (_enemyList.Count == 0 /*|| _enemyList[_enemyList.Count-1]._healthHandler.remainHp <= 10f*/)
            {
                if (!isWaveSpawnnerRunning && waveNo < waveList.Count)
                {
                    StartCoroutine(SpawnNewEnemyPolice());
                }
            }
        }
    }

    IEnumerator CheckForGameOver()
    {
        bool isGameOver = false;
        yield return null;
        while (!isGameOver)
        {
            yield return new WaitForSeconds(0.5f);
            if (waveNo >= waveList.Count && _enemyList.Count == 0)
            {
                yield return new WaitForSeconds(2f);
                if (waveNo >= waveList.Count && _enemyList.Count == 0)
                {
                    isGameOver = true;
                    Controller.self.levelController.DoGameOver(true);
                }
            }
        }
    }

    


    IEnumerator SpawnNewEnemyPolice()
    {

      

        isWaveSpawnnerRunning = true;
        yield return null;
        _playerCommonHandler.UpdatePlayerWavePosition(waveList[waveNo].zStartPos, waveList[waveNo].zEndPos);
        yield return null;
        Controller.self.cameraController.DoSetWaveCam();
        
       
      //  UIController.Instance.ShowLoadingAnimation(1f);


        yield return new WaitUntil(() => !isGettingReadyForWave);
        UIGamePlay.Instance.ShowWaveText(waveNo + 1, 5f);

        Controller.self.cameraController.ResetCustomeThing();
        seqNo = 0;
        if (seqNo <= waveList[waveNo].policeSpawnSequence.Count - 1)
        {
            yield return null;
            for (int i = 0; i < waveList[waveNo].policeSpawnSequence[seqNo].policeSpawnList.Count; i++)
            {
                yield return new WaitForSeconds(waveList[waveNo].policeSpawnSequence[seqNo].spawnDelay[i]);
                var plc = Instantiate(waveList[waveNo].policeSpawnSequence[seqNo].policeSpawnList[i], waveList[waveNo].policeSpawnSequence[seqNo].policeSpawnPosList[i].position, Quaternion.identity);
                _enemyList.Add(plc);
                if (isEnemyBossDead || Controller.self.levelController.isGameOver)
                {
                    waveNo++;
                    isWaveSpawnnerRunning = false;
                    yield break;
                }
            }
            seqNo++;
        }
        waveNo++;
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
