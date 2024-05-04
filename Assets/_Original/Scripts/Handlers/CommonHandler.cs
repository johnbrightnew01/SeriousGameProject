using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerDirection
{
    up,
    right,
    down,
    left,
    none
}

public enum AttackType
{
    punch,
    baseballBat,
    none
}

public enum PlayerAnimationType
{
    idle,
    walk,
    jump,
    punch,
    die
}
[RequireComponent(typeof(AttackHandler))]
[RequireComponent(typeof(HealthHandler))]
[RequireComponent(typeof(UIHandler))]


public class CommonHandler : MonoBehaviour
{
    [ReadOnly] public AttackHandler _attackHandler;
    [ReadOnly] public HealthHandler _healthHandler;
    [ReadOnly] public UIHandler _uiHandler;
    [ReadOnly] public CharacterController _charController;
    [ReadOnly, SerializeField] private Animator anim;
    [SerializeField] private Transform playerHolder;

    [HideInInspector] public int _runHash = Animator.StringToHash("run");
    [HideInInspector] public int _attackHash = Animator.StringToHash("attack");
    [HideInInspector] public int _deadHash = Animator.StringToHash("dead");

    [SerializeField, ReadOnly] private Rigidbody playerRb;
    [SerializeField] private float moveSpeed;



    private void Awake()
    {
        _attackHandler = GetComponent<AttackHandler>();
        _attackHandler.InitializeHandler(this);

        _healthHandler = GetComponent<HealthHandler>();
        _healthHandler.InitializeHandler(this);

        _uiHandler = GetComponent<UIHandler>();
        _uiHandler.InitializeHandler(this);

        anim = GetComponentInChildren<Animator>();
        playerRb = GetComponent<Rigidbody>();
    }

    public void DoMove(PlayerDirection dir)
    {
        if(dir == PlayerDirection.left)
        {
            playerRb.MovePosition(playerRb.position + Vector3.forward * moveSpeed * Time.deltaTime);
            // playerRb.Move(Vector3.forward * moveSpeed * Time.deltaTime, Quaternion.identity);
            
            playerHolder.transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else if(dir == PlayerDirection.right)
        {
            // playerRb.Move(Vector3.back * -1f *moveSpeed * Time.deltaTime, Quaternion.identity);
            playerRb.MovePosition(playerRb.position + Vector3.back * moveSpeed * Time.deltaTime);
            playerHolder.transform.rotation = Quaternion.identity;
        }
      //  Debug.Log(dir);
        if(dir == PlayerDirection.up)
        {
            playerRb.MovePosition(playerRb.position + Vector3.right * moveSpeed * Time.deltaTime);
            // playerRb.Move(Vector3.right * moveSpeed * Time.deltaTime, Quaternion.identity);
        }
        else if (dir == PlayerDirection.down)
        {
            //  playerRb.Move(Vector3.left * moveSpeed * Time.deltaTime, Quaternion.identity);
            playerRb.MovePosition(playerRb.position + Vector3.left * moveSpeed * Time.deltaTime);
        }

        if(dir == PlayerDirection.none)
        {
            UpdateAnimator(PlayerAnimationType.idle);
        }
    }



    public void UpdateAnimator(PlayerAnimationType animType)
    {
        {
            anim.SetBool(_runHash, false);
            anim.SetBool(_attackHash, false);            
        }
        if(animType == PlayerAnimationType.punch)
        {
            anim.SetBool(_attackHash, true);
            Invoke("CancellAttackAnim", 1f);
        }
        else if(animType == PlayerAnimationType.walk)
        {
            if (!anim.GetBool(_runHash)) anim.SetBool(_runHash, true);
        }
        else
        {
            anim.SetBool(_runHash, false);
        }


    }

    private void CancellAttackAnim()
    {
        anim.SetBool(_attackHash, false);
    }


    public void DoAttack()
    {

    }
}
