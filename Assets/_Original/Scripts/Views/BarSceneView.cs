using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BarSceneView : MonoBehaviour
{
    [SerializeField,ReadOnly] private BotHandler policeBot_1;
    [SerializeField,ReadOnly] private BotHandler policeBot_2;
    [SerializeField, ReadOnly] private BotHandler stormeHander;

    [SerializeField] private GameObject police1;
    [SerializeField] private GameObject police2;
    [SerializeField] private GameObject storme;
    [Space(5)]

    [SerializeField] private Transform cameraInitialPos;
    [SerializeField] private Transform cameraMovePos_1;

    [Space(5)]
    [SerializeField] private Transform storme_initialPos;
    [SerializeField] private Transform police_1_initialPos;
    [SerializeField] private Transform police_2_initialPos;
    [SerializeField] private Transform police1_Pos_1;
    [SerializeField] private Transform police2_Pos_1;
    [Space(5)]
    [SerializeField] private Transform police1_Pos_2;
    [SerializeField] private Transform police2_Pos_2;
    [Space(5)]
    [SerializeField] private Transform police2_Pos_3;
    [Space(5)]
    [SerializeField] private Transform storme_Pos_1;
    [Space(5)]
    [SerializeField] private Transform storme_Pos_Left;
    [SerializeField] private Transform storme_Pos_right;
    [SerializeField, ReadOnly] private bool onPosition;
    [SerializeField, ReadOnly] private bool isMoveLeft = false;
    [SerializeField, ReadOnly] private bool isTakeInput = false;
    [SerializeField] private float maxDistToTrigger = 1f;
    [SerializeField, ReadOnly] private float currentDist;


    private void Awake()
    {
        policeBot_1 = police1.GetComponent<BotHandler>();
        policeBot_2 = police2.GetComponent<BotHandler>();
        stormeHander = storme.GetComponent<BotHandler>(); 
    }


    private void OnEnable()
    {
        storme.transform.position = storme_initialPos.position;
        police1.transform.position = police_1_initialPos.position;
        police2.transform.position = police_2_initialPos.position;
        Controller.self.cameraController.barFreeCamera.transform.position = cameraInitialPos.position;
        Controller.self.cameraController.DoActiveVirtualCamera(Controller.self.cameraController.barFreeCamera, false);
        StartCoroutine(DoStatTheSequence());
        onPosition = false;
    }


    private void Update()
    {
        
        if (!isTakeInput) return;
        if (Input.GetKey("a"))
        {
            stormeHander.gameObject.transform.Translate(Vector3.forward * 5f * Time.deltaTime);
            stormeHander.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0f, 0);
            stormeHander.anim.SetBool("run", true);
        }
        else if (Input.GetKey("d"))
        {
            stormeHander.gameObject.transform.Translate(Vector3.forward * -5f * Time.deltaTime);
            stormeHander.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 180, 0);
            stormeHander.anim.SetBool("run", true);
        }
        else
        {
            stormeHander.anim.SetBool("run", false);

        }
        var calPos = stormeHander.transform.position;
        calPos.z = Mathf.Clamp(calPos.z, storme_Pos_Left.position.z, storme_Pos_right.position.z);
        stormeHander.transform.position = calPos;

        if (!isMoveLeft)
        {
            currentDist = Vector3.Distance(stormeHander.transform.position, storme_Pos_Left.position);
            if (Vector3.Distance(stormeHander.transform.position, storme_Pos_Left.position) <= maxDistToTrigger)
            {
                onPosition = true;
            }
            else
            {
                onPosition = false;
            }
        }
        else
        {
            currentDist = Vector3.Distance(stormeHander.transform.position, storme_Pos_right.position);
            if (Vector3.Distance(stormeHander.transform.position, storme_Pos_right.position) <= maxDistToTrigger)
            {
                onPosition = true;
            }
            else
            {
                onPosition = false;
            }
        }
    }

    IEnumerator DoStatTheSequence()
    {
        yield return new WaitForSeconds(0.1f);
        policeBot_1.DoMoveToThisPos(police1_Pos_1, 2.5f);
        policeBot_2.DoMoveToThisPos(police2_Pos_1, 3f);
        yield return new WaitForSeconds(3.2f);
        policeBot_1.ShowSpeech(1); // Police! We’re taking the place!
        yield return new WaitForSeconds(2.5f);
        policeBot_1.DoMoveToThisPos(police1_Pos_2, 17f);
        yield return new WaitForSeconds(0.4f);
        policeBot_2.DoMoveToThisPos(police2_Pos_2, 17f);
        yield return new WaitForSeconds(5f);
        Controller.self.cameraController.barFreeCamera.transform.DOMove(cameraMovePos_1.position,19f);
        yield return new WaitForSeconds(18f);
        stormeHander.DoMoveToThisPos(storme_Pos_1, 7f);
        yield return new WaitForSeconds(7f);
     //   isTakeInput = true;

        stormeHander.ShowSpeech(0); //What? Again?

      /*  yield return new WaitUntil(() => onPosition);
        onPosition = false;
        isMoveLeft = !isMoveLeft;
        policeBot_2.ChangeDirection(false);
        policeBot_2.ShowSpeech(0); //Alright, young lady, let’s see your ID

        yield return new WaitForSeconds(0.4f);
        policeBot_2.anim.SetTrigger("push");
        yield return new WaitForSeconds(0.25f);
        stormeHander.anim.SetTrigger("trigger");
         

        yield return new WaitUntil(() => onPosition);
        onPosition = false;
        isMoveLeft = !isMoveLeft;
        stormeHander.ShowSpeech(1); // I showed you my ID last week!
    

        yield return new WaitUntil(() => onPosition);
        onPosition = false;
        isMoveLeft = !isMoveLeft;

        policeBot_1.ShowSpeech(1); // You know the drill. ID, or it’s pants off

        yield return new WaitForSeconds(0.4f);
        policeBot_1.anim.SetTrigger("push");
        yield return new WaitForSeconds(0.25f);
        stormeHander.anim.SetTrigger("trigger");

        yield return new WaitUntil(() => onPosition);
        onPosition = false;
        isMoveLeft = !isMoveLeft;



        stormeHander.ShowSpeech(2); // No

        yield return new WaitUntil(() => onPosition);
        onPosition = false;
        isMoveLeft = !isMoveLeft;

        policeBot_2.ShowSpeech(1); // Ladies’ clothes on ladies. That’s the law.

        yield return new WaitForSeconds(0.4f);
        policeBot_2.anim.SetTrigger("push");
        yield return new WaitForSeconds(0.25f);
        stormeHander.anim.SetTrigger("trigger");

        yield return new WaitUntil(() => onPosition);
        onPosition = false;
        isMoveLeft = !isMoveLeft;

        stormeHander.ShowSpeech(3); // Get oS me!

        yield return new WaitUntil(() => onPosition);
        onPosition = false;
        isMoveLeft = !isMoveLeft;

        policeBot_1.ShowSpeech(2); // That’s it. You’re coming with us. You’re under arrest.
        yield return new WaitForSeconds(0.4f);
        policeBot_2.anim.SetTrigger("push");
        yield return new WaitForSeconds(0.25f);
        stormeHander.anim.SetTrigger("trigger");
        isTakeInput = false;*/


    }



}
