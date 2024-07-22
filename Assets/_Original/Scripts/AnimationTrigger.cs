using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField, ReadOnly] private CommonHandler commonH;
    [SerializeField] private bool isPlayFootStepSound;

    private void Awake()
    {
        commonH = GetComponentInParent<CommonHandler>();
    }

    public void OnAttackAnimationDone()
    {
       // commonH._attackHandler.OnAttackDone();
    }

    public void OnAttackHit()
    {
        commonH._attackHandler.DoReduceOthersHP();
    }

    public void FootStep()
    {
        if (!isPlayFootStepSound) return;
        SoundManager.Instance.SpawnAndPlayNewSound(SoundManager.Instance.footStep);
    }

    public void StormyFootStep()
    {
        if (!isPlayFootStepSound) return;
        SoundManager.Instance.SpawnAndPlayNewSound(SoundManager.Instance.footStepStormy);
    }
    
}
