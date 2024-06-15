using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class IntroSceneView : MonoBehaviour
{

    [SerializeField] private VideoPlayer introVideo;


    private void OnEnable()
    {
        OnStartTheScene();
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            CancelInvoke();
            Invoke("OnEndTheScene",0.01f);
        }
    }


    public void OnStartTheScene()
    {
        Invoke("OnEndTheScene",(float) introVideo.clip.length);
    }

    private void OnEndTheScene()
    {
        Controller.self.sequenceController.UpdateSequence(Sequence.intreactive_intro_seq);


        // Black screen animation

        this.gameObject.SetActive(false);
    }


}
