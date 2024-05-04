using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-99)]
public class Controller : MonoBehaviour
{
    public static Controller self;
    public GameController gameController;    
    public CameraController cameraController;
    public LevelController levelController;
    public PlayerController playerController;
    public TimeLineController timeLineController;
    public InputController inputController;



    void Awake()
    {
        if(self == null)
        {
            self = this;
        }
    }
}
