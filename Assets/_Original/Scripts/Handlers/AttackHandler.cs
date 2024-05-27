using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [ReadOnly, SerializeField] private CommonHandler commonHandler;
    [field: SerializeField, ReadOnly] public bool isAttacking { get; private set; }
    [field: ReadOnly,SerializeField] public CommonHandler currentTarget { get; private set; }

    public void InitializeHandler(CommonHandler cmnHandler)
    {
        commonHandler = cmnHandler;
    }

    public void Attack()
    {
        if (isAttacking) return;
        isAttacking = true;
        { // trigger animation
            commonHandler.UpdateAnimator(PlayerAnimationType.punch);
            Debug.Log("attack Anim");
          //  commonHandler.DoAttackAnimation();
        }

    }

    public void OnAttackDone()
    {
        isAttacking = false;
    }

    public void SetThisAsTarget(CommonHandler target)
    {
        currentTarget = target;
    }
}
