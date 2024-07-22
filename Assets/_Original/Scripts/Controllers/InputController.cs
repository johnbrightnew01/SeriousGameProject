using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    [SerializeField]
    private bool isTakeInput;
    private bool isMoving;
    [SerializeField, ReadOnly] public bool isTakingAnyInput;

    private void Awake()
    {
        isTakeInput = false;
        isTakingAnyInput = false;
    }
    void FixedUpdate()
    {
        if (Controller.self.playerController.isGettingReadyForWave) return;
        if (Controller.self.sequenceController.currentSequence != Sequence.street_seq) return;
        if (!isTakeInput) return;
        isMoving = false;
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            isTakingAnyInput = true;
            isMoving = true;
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.left);
        }
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.right);
            isMoving = true;
            isTakingAnyInput = true;
        }

        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            isMoving = true;
            isTakingAnyInput = true;
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.up);
        }
        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            isMoving = true;
            isTakingAnyInput = true;
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.down);
        }
        if (Input.GetKey("e"))
        {
            isMoving = false; ;
            isTakingAnyInput = true;
            Controller.self.playerController._playerCommonHandler.DoBlockAttack();
        }
        if (Input.GetKeyUp("e"))
        {
            isTakingAnyInput = true;
            Controller.self.playerController._playerCommonHandler.RemoveBlock();
        }
        if (!isMoving)
        {
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.none);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            isTakingAnyInput = true;
            Controller.self.playerController._playerCommonHandler._attackHandler.Attack();
        }

    }

    public void EnableInput()
    {
        isTakeInput = true;
    }
}
