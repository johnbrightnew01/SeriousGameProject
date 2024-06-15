using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    public Camera mainCam;
    public Transform cameraParent;
    public CinemachineBrain camBrain;
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera barFreeCamera;
 
    


    public void DoActiveVirtualCamera(CinemachineVirtualCamera nextCam, bool doBlend , float blendTime = 1f)
    {
        playerCam.Priority = 0;
        barFreeCamera.Priority = 0;

        nextCam.Priority = 20;

    }
   

}
