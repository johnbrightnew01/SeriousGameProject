using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public enum PlayerDirection
    {
        forward,
        right,
        back,
        left
    }

    public enum PlayerAnimationType
    {
        idle,
        walk,
        jump
    }

    [SerializeField, ReadOnly]
    private Animator playerAnim;

    private void Awake()
    {
        playerAnim = GetComponentInChildren<Animator>();
    }


    public void PlayAnimation(PlayerDirection dir, PlayerAnimationType animType)
    {
        playerAnim.SetInteger("moveType", (int)animType);
        playerAnim.SetInteger("direction", (int)dir);
    }



}
