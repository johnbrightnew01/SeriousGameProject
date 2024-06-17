using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
 

    [SerializeField, ReadOnly]
    private Animator playerAnim;
    [SerializeField]
    private float moveSpeed = 2f;
    public Canvas playerPopUpCanvas;


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
