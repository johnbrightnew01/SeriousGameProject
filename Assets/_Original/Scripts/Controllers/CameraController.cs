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
    public CinemachineVirtualCamera outsideCamera;
    public CinemachineVirtualCamera fightCamera;
    [SerializeField] private float normalDeadZone = 0.7f;
    [SerializeField] private float waveInbetweenlDeadZone = 0.0f;
    Coroutine camDeadZOneCoroutine;
    


    public void DoActiveVirtualCamera(CinemachineVirtualCamera nextCam, bool doBlend , float blendTime = 1f)
    {
        playerCam.Priority = 0;
        barFreeCamera.Priority = 0;
        outsideCamera.Priority = 0;
        fightCamera.Priority = 0;
        nextCam.Priority = 20;
      
    }


    public void DoSetWaveCam()
    {
        camDeadZOneCoroutine = StartCoroutine(CustomThing());
    }

    public IEnumerator CustomThing()
    {
        var composer = fightCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
     
        float timeElaps = 0;
        float from = composer.m_DeadZoneWidth;
        float to = waveInbetweenlDeadZone;
        while (timeElaps < 0.5f)
        {
            timeElaps += Time.deltaTime;
            composer.m_DeadZoneWidth = Mathf.Lerp(from, to, timeElaps / 0.5f);
            yield return null;

        }
        
    }

    public void ResetCustomeThing()
    {
        StopCoroutine(camDeadZOneCoroutine);
        var composer = fightCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        composer.m_DeadZoneWidth = normalDeadZone;

        Debug.Log("reseting the thing;");
    }




   

}
