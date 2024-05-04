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
    public CinemachineVirtualCamera menuCam;
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera actionCam;
    public CinemachineVirtualCamera shootCam;
    


    public void DoActiveVirtualCamera(CinemachineVirtualCamera nextCam, bool doBlend , float blendTime = 1f)
    {
        return;
        if (doBlend)
        {
            camBrain.m_DefaultBlend.m_Time = blendTime;
        }
        else
        {
           // camBrain.m_DefaultBlend.m_Time = 0;
        }

        menuCam.Priority = 0;
        playerCam.Priority = 0;
        actionCam.Priority = 0;
        shootCam.Priority = 0;
        nextCam.Priority = 20;

    }
   

}
