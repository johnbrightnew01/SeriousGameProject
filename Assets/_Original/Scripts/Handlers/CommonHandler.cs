using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [HideInInspector] public int _runHash = Animator.StringToHash("run");
    [HideInInspector] public int _deadHash = Animator.StringToHash("dead");


    private void Awake()
    {
        _attackHandler = GetComponent<AttackHandler>();
        _attackHandler.InitializeHandler(this);

        _healthHandler = GetComponent<HealthHandler>();
        _healthHandler.InitializeHandler(this);

        _uiHandler = GetComponent<UIHandler>();
        _uiHandler.InitializeHandler(this);


    }
}
