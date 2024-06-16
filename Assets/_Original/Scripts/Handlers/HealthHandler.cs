using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [ReadOnly, SerializeField] private CommonHandler commonHandler;
    [ReadOnly, SerializeField] private float remainHp;
    [SerializeField, ReadOnly] public FloatingUI hpUi;
    [SerializeField] private Transform uiTargetPos;


    public void InitializeHandler(CommonHandler cmnHandler)
    {
        commonHandler = cmnHandler;
        remainHp = cmnHandler.totalHealth;
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
  
        if (commonHandler.isBlocking) return;
  
        remainHp -= reduceBy;
        remainHp = Mathf.Clamp(remainHp, 0, commonHandler.totalHealth);
        if(hpUi != null)
        {
            hpUi.UpdateHp(remainHp/commonHandler.totalHealth);
        }
        if (commonHandler.isPlayer)
        {
           
            UIGamePlay.Instance.UpdatePlayerHP(  ( remainHp/ commonHandler.totalHealth));       
        }

        if(remainHp <= 0)
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
