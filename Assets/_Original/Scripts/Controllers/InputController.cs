using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    [SerializeField]
    private bool isTakeInput;
    private bool isMoving;
    void FixedUpdate()
    {
        if (Controller.self.sequenceController.currentSequence != Sequence.bar_seq) return;
        if (!isTakeInput) return;
        isMoving = false;
        if (Input.GetKey("a"))
        {
            isMoving = true;
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.left);
        }
        if (Input.GetKey("d"))
        {
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.right);
            isMoving = true;
        }

        if (Input.GetKey("w"))
        {
            isMoving = true;
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.up);
        }
        if (Input.GetKey("s"))
        {
            isMoving = true;
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.down);
        }
        if (Input.GetKey("e"))
        {
            isMoving = false; ;
            Controller.self.playerController._playerCommonHandler.DoBlockAttack();
        }
        if (!isMoving)
        {
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.none);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Controller.self.playerController._playerCommonHandler._attackHandler.Attack();
        }

    }
}
