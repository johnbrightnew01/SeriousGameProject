using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [ReadOnly, SerializeField] private CommonHandler commonHandler;
    [SerializeField] private float hitDamage = 10f;
    [field: SerializeField, ReadOnly] public bool isAttacking { get; private set; }
    [field: ReadOnly,SerializeField] public CommonHandler currentTarget { get; private set; }
    [SerializeField] private Transform rayCastPos;
    [SerializeField] private float maxRayCastDist = 2f;
    [SerializeField] private LayerMask mask;
    private RaycastHit hit;
    [SerializeField] private Collider raycastCollider;

    bool isHitOther;
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
            DoPlayPunchSound();
            
           
          //  commonHandler.DoAttackAnimation();
        }
    }

    private void DoBlockAttack()
    {

    }

    private void Update()
    {
        if (commonHandler.isPlayer)
        {
            DoUpdateRayCastForTarget();
        }
    }


    public void DoUpdateRayCastForTarget()
    {
        return;

        if(Physics.BoxCast(raycastCollider.bounds.center, transform.localScale*0.5f, rayCastPos.forward, out hit,  rayCastPos.rotation, maxRayCastDist, mask))
        {
            isHitOther = true;
            currentTarget = hit.transform.GetComponent<CommonHandler>();
        }
        else
        {
            currentTarget = null;
            isHitOther = false;
        }
    }

 

    private void OnTriggerStay(Collider other)
    {
        if (!commonHandler.isPlayer) return;
        
        if( other.gameObject.layer == 7)
        {
            currentTarget = other.transform.GetComponent<CommonHandler>();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!commonHandler.isPlayer) return;

        if (other.gameObject.layer == 7)
        {
            currentTarget = null;
        }
    }

    public void DoPlayPunchSound()
    {
        if (currentTarget != null)
        {
            if (commonHandler.isPlayer)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.silviaPunch);
            }
            else
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.copPunch);
            }
        }
    }


    public void DoReduceOthersHP()
    {
        if (currentTarget == null) return;
      //  DoPlayPunchSound();
        currentTarget._healthHandler.ReduceHealth(hitDamage, commonHandler);
        currentTarget = null;
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
