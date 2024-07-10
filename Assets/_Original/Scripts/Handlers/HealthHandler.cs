using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [ReadOnly, SerializeField] private CommonHandler commonHandler;
    [field: ReadOnly, SerializeField] public float remainHp { get; private set; }
    [SerializeField, ReadOnly] public FloatingUI hpUi;
    [SerializeField] private Transform uiTargetPos;
    [SerializeField] private SpriteRenderer spRender;
    private bool isEffectRunning = false;
 
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


    public void ReduceHealth(float reduceBy, CommonHandler reduceFrom)
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
        DoPlayGotHitSound();
        if (remainHp <= 0)
        {
            commonHandler.OnDead();
            if (hpUi != null)
            {
                hpUi.OnDead();
            }
        }
        else
        {
           
            HpReduceEffect();
            DoForceBackward(reduceFrom.transform.position.z - this.transform.position.z);
        }
    }

    public void DoPlayGotHitSound()
    {      
        {
            if (commonHandler.isPlayer)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.silviaGotHit);
            }
            else
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.copGotHit);
            }
        }
    }


    private void DoForceBackward(float dir)
    {
        commonHandler.ToggleGettingForce(true, dir);
    }

    private void HpReduceEffect()
    {
        if (isEffectRunning) return;
        isEffectRunning = true;
        spRender.DOFade(0f, 0.08f).OnComplete(() =>
        {
            spRender.DOFade(1f, 0.08f).OnComplete(() =>
            {
                isEffectRunning = false;
            });

        });
    }

}
