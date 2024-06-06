using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-98)]
public class PlayerController : MonoBehaviour
{

    [HideInInspector]
    public float timeCounter;
    public PlayerView _playerView;
    public CommonHandler _playerCommonHandler;

    public List<CommonHandler> _enemyList;

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
