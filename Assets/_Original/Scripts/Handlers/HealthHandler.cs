using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [ReadOnly, SerializeField] private CommonHandler commonHandler;
    [ReadOnly, SerializeField] private float remailHP;

    public void InitializeHandler(CommonHandler cmnHandler)
    {
        commonHandler = cmnHandler;
        remailHP = cmnHandler.totalHealth;
    }


    public void ReduceHealth(float reduceBy)
    {
        remailHP -= reduceBy;
        remailHP = Mathf.Clamp(remailHP, 0, 100f);
       
        if (commonHandler.isPlayer)
        {
            UIGamePlay.Instance.UpdatePlayerHP(  ( remailHP/ commonHandler.totalHealth));
            Debug.Log("HP"+  remailHP / commonHandler.totalHealth );
        }

        if(remailHP <= 0)
        {
            // dead
            commonHandler.OnDead();
        }


    }
}
