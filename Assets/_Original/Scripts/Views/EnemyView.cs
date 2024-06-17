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
    [SerializeField, ReadOnly] private bool isRunningAway = false;

    private void Awake()
    {
        enemyCommonHandler = GetComponent<CommonHandler>();
    }


    private void Update()
    {
        if ( Controller.self.sequenceController.currentSequence != Sequence.street_seq) return;
        if (enemyCommonHandler == null) return;

        if (isRunningAway)
        {
            if(enemyCommonHandler._playerHolder.transform.localRotation.eulerAngles.y == 0)
            {

                enemyCommonHandler.transform.Translate(Vector3.forward * moveSpeed *10f* Time.deltaTime);
                enemyCommonHandler.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            else
            {          
                enemyCommonHandler.transform.Translate(Vector3.back * moveSpeed * 10f * Time.deltaTime);
                enemyCommonHandler.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            enemyCommonHandler.UpdateAnimator(PlayerAnimationType.walk);

            return;
        }

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
            if (IsFixedYMovement())
            {
                enemyCommonHandler.UpdateAnimator(PlayerAnimationType.idle);
                return true;
            }
           
        }
        else
        {
            enemyCommonHandler.transform.Translate((enemyCommonHandler._attackHandler.currentTarget.transform.position - this.transform.position) * moveSpeed * Time.deltaTime);
          
        }
        enemyCommonHandler.UpdateAnimator(PlayerAnimationType.walk);

        return false;
    }

    private bool IsFixedYMovement()
    {
        if(( enemyCommonHandler._attackHandler.currentTarget.transform.position.y  - this.transform.position.y) >=  0.2f)
        {
            enemyCommonHandler.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        else if ((enemyCommonHandler._attackHandler.currentTarget.transform.position.y - this.transform.position.y) <= -0.2f)
        {
            enemyCommonHandler.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        else
        {
            return true;
        }
        return false;
    }

    public void DoRunAway()
    {
        isRunningAway = true;
    }


}
