using System.Collections;
using System.Collections.Generic;
using System.Timers;
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
    [field: SerializeField] public bool isPlayer;
    [field: SerializeField] public float totalHealth = 100f;
    [ReadOnly] public AttackHandler _attackHandler;
    [ReadOnly] public HealthHandler _healthHandler;
    [ReadOnly] public UIHandler _uiHandler;
    [ReadOnly] public CharacterController _charController;
    [ReadOnly, SerializeField] private Animator anim;
    public Transform _playerHolder;

    [HideInInspector] public int _runHash = Animator.StringToHash("run");
    [HideInInspector] public int _attackHash = Animator.StringToHash("attack");
    [HideInInspector] public int _deadHash = Animator.StringToHash("dead");

    [SerializeField, ReadOnly] public Rigidbody _playerRb;
    [SerializeField] private float moveSpeed;
    [field: SerializeField, ReadOnly] public bool isDead { get; private set; }
    [SerializeField] float blockDeltaTime = 2f;
    [SerializeField] float blockTime = 1f;
    [field: SerializeField, ReadOnly] public bool isBlocking { get; private set; }

    private void Awake()
    {
        _attackHandler = GetComponent<AttackHandler>();
        _attackHandler.InitializeHandler(this);

        _healthHandler = GetComponent<HealthHandler>();
        _healthHandler.InitializeHandler(this);

        _uiHandler = GetComponent<UIHandler>();
        _uiHandler.InitializeHandler(this);

        anim = GetComponentInChildren<Animator>();
        _playerRb = GetComponent<Rigidbody>();
    }

    public void DoBlockAttack()
    {
        if (isBlocking) return;
        isBlocking = true;
        anim.SetBool("block", true);
        Invoke("RemoveBlock", blockTime);
    }

    private void RemoveBlock()
    {
        anim.SetBool("block", false);
        isBlocking = false;
    }



    public void DoMove(PlayerDirection dir)
    {
        if (isBlocking && dir != PlayerDirection.none)
        {
            RemoveBlock();
        }
        if (!isPlayer) return;
        if (_attackHandler.isAttacking || isDead)
        {
            _playerRb.velocity = Vector3.zero;
            return;
        }

        Vector3 customDir = Vector3.zero;
        if(dir == PlayerDirection.left)
        {
            customDir += Vector3.forward;
            _playerRb.MovePosition(_playerRb.position + Vector3.forward * moveSpeed * Time.deltaTime);
            _playerHolder.transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        if(dir == PlayerDirection.right)
        {
            customDir += Vector3.back;
            _playerRb.MovePosition(_playerRb.position + Vector3.back * moveSpeed * Time.deltaTime);
            _playerHolder.transform.rotation = Quaternion.identity;
        }
        if(dir == PlayerDirection.up)
        {
            customDir += Vector3.right;
            _playerRb.MovePosition(_playerRb.position + Vector3.up * moveSpeed * Time.deltaTime);            
        }
        if (dir == PlayerDirection.down)
        {
            customDir += Vector3.left;
            _playerRb.MovePosition(_playerRb.position + Vector3.down * moveSpeed * Time.deltaTime);
        }      
        if(dir == PlayerDirection.none)
        {
           // _playerRb.velocity = Vector3.zero;
            UpdateAnimator(PlayerAnimationType.idle);
        }
        else
        {
            UpdateAnimator(PlayerAnimationType.walk);
        }

        var pp = _playerRb.position;
        pp.y = Mathf.Clamp(pp.y, -0.59f, 1.36f);
        pp.z = Mathf.Clamp(pp.z, -24f, -6f);
        _playerRb.position = pp;

        


    }

    public void DoAttackAnimation()
    {
        if (isDead) return;
        anim.SetBool(_runHash, false);
        anim.SetBool(_attackHash, true);

    }


    public void OnDead()
    {
        if (isDead) return;
        isDead = true;
        // do effects
        if (!isPlayer)
        {
            var plc = GetComponent<EnemyView>();
            var col = GetComponent<Collider>();
            col.enabled = false;
            plc.DoRunAway();
            Controller.self.playerController.RemoveThisEnemy(this);
            anim.speed = 3f;
            Destroy(this.gameObject, 5f);
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
            
            Invoke("CancellAttackAnim", GetAttackAnimationTime());
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


    private void CancellAttackAnim() // call from Invoke
    {
        anim.SetBool(_attackHash, false);
        _attackHandler.OnAttackDone();
    }

   
    public void BotControl(bool isRun)
    {
        anim.SetBool("run" , isRun);
    }


    private float GetAttackAnimationTime()
    {
        var clipsList = anim.runtimeAnimatorController.animationClips;
        for (int i = 0; i < clipsList.Length; i++)
        {
            if (clipsList[i].name == "Attack")
            {
                return clipsList[i].length;
            }
        }
        return 0;
    }


   
}
