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
           
          //  commonHandler.DoAttackAnimation();
        }
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

        if(Physics.BoxCast(raycastCollider.bounds.center, transform.localScale*0.5f, rayCastPos.forward, out hit,  rayCastPos.rotation, maxRayCastDist, mask))
        {
          
            currentTarget = hit.transform.GetComponent<CommonHandler>();
        }

       /* {
            if (Physics.Raycast(rayCastPos.position, rayCastPos.transform.forward, out hit, maxRayCastDist, mask))
            {
                Debug.DrawRay(rayCastPos.position, rayCastPos.transform.forward * hit.distance, Color.yellow);
                if (hit.transform.TryGetComponent<CommonHandler>(out CommonHandler cmn))
                {
                    if (!cmn.isPlayer)
                    {
                        Debug.Log("Found the nemey");
                    }
                }
            }
            else
            {
                Debug.DrawRay(rayCastPos.position, rayCastPos.transform.forward * maxRayCastDist, Color.red);
            }
        }*/
       
    }

  




    public void DoReduceOthersHP()
    {
        if (currentTarget == null) return;
        currentTarget._healthHandler.ReduceHealth(hitDamage);
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
