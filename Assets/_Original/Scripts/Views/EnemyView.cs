using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField, ReadOnly] private CommonHandler enemyCommonHandler;    
    [Range(0.1f,3f), SerializeField] private float attackFrequency = 0.5f;
    [SerializeField] private float minAttackDist = 2f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField,ReadOnly]private float deltaCounter = 0;
    

    private void Awake()
    {
        enemyCommonHandler = GetComponent<CommonHandler>();
    }


    private void Update()
    {
        if (Controller.self.sequenceController.currentSequence != Sequence.bar_seq && Controller.self.sequenceController.currentSequence != Sequence.street_seq) return;
        if (enemyCommonHandler == null) return;
        if (enemyCommonHandler.isDead) return;

        if(enemyCommonHandler._attackHandler.currentTarget == null)
        {
            enemyCommonHandler._attackHandler.SetThisAsTarget(Controller.self.playerController.GetTargetPlayer());
            return;
        }
        if (enemyCommonHandler._attackHandler.isAttacking) return;

        if (IsMovedToTarget())
        {
            if (deltaCounter < attackFrequency)
            {
                deltaCounter += Time.deltaTime;
            }
            else
            {
                deltaCounter = 0;
                AttackNow();
            }
          
          
        }
        else
        {

            if (deltaCounter < attackFrequency)
            {
                deltaCounter += Time.deltaTime;
            }
            else
            {
                deltaCounter = 0;
            }
        }
    }

    


    private void AttackNow()
    {   
        enemyCommonHandler._attackHandler.Attack();
    }

    private bool IsMovedToTarget()
    {
        if(enemyCommonHandler._playerHolder.transform.position.z - enemyCommonHandler._attackHandler.currentTarget.transform.position.z < 0)
        {
            enemyCommonHandler._playerHolder.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            enemyCommonHandler._playerHolder.transform.rotation = Quaternion.Euler(0, 180f, 0);
        }

        if(Vector3.Distance(this.transform.position, enemyCommonHandler._attackHandler.currentTarget.transform.position) <= minAttackDist)
        {

            enemyCommonHandler.UpdateAnimator( PlayerAnimationType.idle);
            return true;
        }

        enemyCommonHandler.transform.Translate((enemyCommonHandler._attackHandler.currentTarget.transform.position - this.transform.position) * moveSpeed * Time.deltaTime);
        enemyCommonHandler.UpdateAnimator(PlayerAnimationType.walk);
        return false;
    }


}
