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
        if (!isTakeInput) return;
        isMoving = false;
        if (Input.GetKey("a"))
        {
            isMoving = true;
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.left);
        }
        else if (Input.GetKey("d"))
        {
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.right);
            isMoving = true;
        }

        if (Input.GetKey("w"))
        {
            isMoving = true;
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.up);
        }
        else if (Input.GetKey("s"))
        {
            isMoving = true;
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.down);
        }

        if (!isMoving)
        {
            Controller.self.playerController._playerCommonHandler.DoMove(PlayerDirection.none);
        }

    }
}
