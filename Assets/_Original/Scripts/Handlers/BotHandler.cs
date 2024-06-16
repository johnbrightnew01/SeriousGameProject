using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHandler : MonoBehaviour
{
    [ReadOnly, SerializeField] private FloatingUI floatingUI;
    [SerializeField] public Animator anim;
    [SerializeField, ReadOnly] private SpeechHandler speechHander;

    private void Awake()
    {
        floatingUI = GetComponentInChildren<FloatingUI>(); 
        speechHander = GetComponent<SpeechHandler>();
    }


    public void DoMoveToThisPos(Transform pos, float moveTime = 2f )
    {
      //  var targtPos = pos.position;
     //   targtPos.y = this.transform.position.y;
        if(this.transform.position.z - pos.position.z > 0)
        {
            this.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 180f, 0);
        }
        else
        {
            this.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        anim.SetBool("run", true);
        this.transform.DOMove(pos.position, moveTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            anim.SetBool("run", false);   
            
        });

    }
    public void DoMoveToThisPos(Vector3 pos, float moveTime = 2f)
    {
        //  var targtPos = pos.position;
        //   targtPos.y = this.transform.position.y;
        if (this.transform.position.z - pos.z > 0)
        {
            this.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 180f, 0);
        }
        else
        {
            this.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        anim.SetBool("run", true);
        this.transform.DOMove(pos, moveTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            anim.SetBool("run", false);

        });

    }

    public void ChangeDirection(bool isLeft)
    {
        if (isLeft)
        {
            this.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 180f, 0); 
        }
        else
        {
            this.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }


    public void ShowSpeech(int indx, float popUpTime = 2f)
    {
        speechHander.PopupSpeech(indx, popUpTime);
    }

  


}
