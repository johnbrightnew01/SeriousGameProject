using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField, ReadOnly] private CommonHandler commonH;

    private void Awake()
    {
        commonH = GetComponentInParent<CommonHandler>();
    }

    public void OnAttackAnimationDone()
    {
       // commonH._attackHandler.OnAttackDone();
    }
    
}
