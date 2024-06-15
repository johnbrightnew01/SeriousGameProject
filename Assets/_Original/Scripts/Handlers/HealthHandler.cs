using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [ReadOnly, SerializeField] private CommonHandler commonHandler;
    [ReadOnly, SerializeField] private float remailHP;
    [SerializeField, ReadOnly] public FloatingUI hpUi;
    [SerializeField] private Transform uiTargetPos;


    public void InitializeHandler(CommonHandler cmnHandler)
    {
        commonHandler = cmnHandler;
        remailHP = cmnHandler.totalHealth;
        hpUi = GetComponentInChildren<FloatingUI>();      
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        if (hpUi)
        {
            hpUi.SetFloatingUI(uiTargetPos);
        }
    }


    public void ReduceHealth(float reduceBy)
    {
        remailHP -= reduceBy;
        remailHP = Mathf.Clamp(remailHP, 0, 100f);
        if(hpUi != null)
        {
            hpUi.UpdateHp(remailHP/commonHandler.totalHealth);
        }
        if (commonHandler.isPlayer)
        {
            UIGamePlay.Instance.UpdatePlayerHP(  ( remailHP/ commonHandler.totalHealth));       
        }

        if(remailHP <= 0)
        {
            // dead
            commonHandler.OnDead();
            if (hpUi != null)
            {
                hpUi.OnDead();
            }
        }
    }
}
