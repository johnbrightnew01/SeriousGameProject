using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class CharacterData
{
    public GameObject character;
    public List<Transform> speechList;
}
public class BarSceneView : MonoBehaviour
{
    [SerializeField] private List<CharacterData> charList;

    [SerializeField,ReadOnly] private BotHandler policeBot_1;
    [SerializeField,ReadOnly] private BotHandler policeBot_2;
    [SerializeField, ReadOnly] private BotHandler stormeHander;
    [SerializeField] private GameObject introText;

    [SerializeField] private GameObject police1;
    [SerializeField] private GameObject police2;
    [SerializeField] private GameObject storme;
    [Space(5)]

    [SerializeField] private Transform cameraInitialPos;
    [SerializeField] private Transform cameraMovePos_1;
    [SerializeField] private Transform cameraMovePos_2;

    [Space(5)]
    [SerializeField] private Transform storme_initialPos;
    [SerializeField] private Transform police_1_initialPos;
    [SerializeField] private Transform police_2_initialPos;

    [SerializeField] private List<Transform> police_1_posList;
    [SerializeField] private List<Transform> police_2_posList;

    [SerializeField] private Transform police1_Pos_1;
    [SerializeField] private Transform police2_Pos_1;
    [Space(5)]
    [SerializeField] private Transform police1_Pos_2;
    [SerializeField] private Transform police2_Pos_2;
    [Space(5)]
    [SerializeField] private Transform police2_Pos_3;
    [Space(5)]
    [SerializeField] private List<Transform> storme_Pos_List;
    [Space(5)]
    [SerializeField] private Transform storme_Pos_Left;
    [SerializeField] private Transform storme_Pos_right;
    [SerializeField, ReadOnly] private bool onPosition;
    [SerializeField, ReadOnly] private bool isMoveLeft = false;
    [SerializeField, ReadOnly] private bool isTakeInput = false;
    [SerializeField] private float maxDistToTrigger = 1f;
    [SerializeField, ReadOnly] private float currentDist;

    [SerializeField] private Canvas canvas;
    [SerializeField] private Image fadeImage;



    private void Awake()
    {
        policeBot_1 = police1.GetComponent<BotHandler>();
        policeBot_2 = police2.GetComponent<BotHandler>();
        stormeHander = storme.GetComponent<BotHandler>(); 
     
    }


    private void OnEnable()
    {
        SoundManager.Instance.StopThisSound(SoundManager.Instance.interactiveSoundBackground);
        storme.transform.position = storme_initialPos.position;
        police1.transform.position = police_1_initialPos.position;
        police2.transform.position = police_2_initialPos.position;
        Controller.self.cameraController.barFreeCamera.transform.position = cameraInitialPos.position;
        UIController.Instance.ShowLoadingAnimation(2, 2);
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


    IEnumerator BackgroundCharSpeech()
    {
      //  ShowBGSpeech(0, 0);
        charList[0].speechList[0].gameObject.SetActive(true);
        Debug.Log("speech;");
        yield return new WaitForSeconds(3f);
        charList[1].speechList[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        charList[2].speechList[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        charList[3].speechList[0].gameObject.SetActive(true);

    }

    IEnumerator DoStatTheSequence()
    {
        yield return new WaitForSeconds(0.1f);
       
        SoundManager.Instance.ToggleBarSound(true);
        yield return new WaitForSeconds(10f);
        policeBot_1.DoMoveToThisPos(police1_Pos_1, 2.5f);
        policeBot_2.DoMoveToThisPos(police2_Pos_1, 3f);
        yield return new WaitForSeconds(3.2f);
      //  policeBot_1.ShowSpeech(0); // Police! We’re taking the place!
        introText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        SoundManager.Instance.ToggleBarSound(false);
        yield return new WaitForSeconds(2.5f);
        policeBot_1.DoMoveToThisPos(police1_Pos_2, 14f);
        yield return new WaitForSeconds(0.4f);
        policeBot_2.DoMoveToThisPos(police2_Pos_2, 14f);
        yield return new WaitForSeconds(3.5f);
        StartCoroutine(BackgroundCharSpeech());
        Controller.self.cameraController.barFreeCamera.transform.DOMove(cameraMovePos_1.position, 11f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(9f);
        Controller.self.cameraController.barFreeCamera.transform.DOMove(cameraMovePos_2.position, 3f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(5.5f);
        
        policeBot_1.ShowSpeech(0,3.5f); // you come over here
        yield return new WaitForSeconds(3f);
         stormeHander.ShowSpeech(0, 3f); //What? Again?        
        yield return new WaitForSeconds(2.5f);
        stormeHander.DoMoveToThisPos(storme_Pos_List[1], 3f);
        yield return new WaitForSeconds(3f);
        stormeHander.DoMoveToThisPos(storme_Pos_List[2], 5f);
        yield return new WaitForSeconds(2f);
        policeBot_2.DoMoveToThisPos(police_2_posList[3], 5f);
        yield return new WaitForSeconds(6.5f);

        { // jitter
            Vector3 currentPos = stormeHander.transform.position;
            currentPos.z = -5.14f;
            stormeHander.DoMoveToThisPos(currentPos, 1f);            
            yield return new WaitForSeconds(1.3f);
            currentPos.z = -3.59f;
            stormeHander.DoMoveToThisPos(currentPos, 1.5f);
            yield return new WaitForSeconds(1.65f);
            currentPos.z = -5.71f;
            stormeHander.DoMoveToThisPos(currentPos, 2.5f);
            yield return new WaitForSeconds(2.5f);
            //policeBot_2.ChangeDirection(true);
            policeBot_2.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
            policeBot_2.anim.SetTrigger("push");
            yield return new WaitForSeconds(0.2f);
            stormeHander.anim.SetTrigger("trigger");
         
            policeBot_2.ShowSpeech(0, 4);// alright you lady lets see your id
            yield return new WaitForSeconds(3f);
            stormeHander.ShowSpeech(1, 3);
            yield return new WaitForSeconds(4f);

            Debug.Log("Segment 1 done");

            currentPos.z = -3.61f;
            stormeHander.DoMoveToThisPos(currentPos, 2.5f);
            yield return new WaitForSeconds(2.8f);
            currentPos.z = -3f;
            stormeHander.DoMoveToThisPos(currentPos, 0.7f);
            yield return new WaitForSeconds(1.3f);
            policeBot_1.anim.SetTrigger("push");
            yield return new WaitForSeconds(0.22f);
            stormeHander.anim.SetTrigger("trigger");
            policeBot_1.ShowSpeech(1,4);
            yield return new WaitForSeconds(2.5f);
            stormeHander.ShowSpeech(2,1.5f); //NO
            yield return new WaitForSeconds(5f);
            Debug.Log("Segment 2 done");

            currentPos.z = -5.71f;
            stormeHander.DoMoveToThisPos(currentPos, 4f);
            yield return new WaitForSeconds(4f);
            policeBot_2.anim.SetTrigger("push");
            yield return new WaitForSeconds(0.22f);
            stormeHander.anim.SetTrigger("trigger");
            policeBot_2.ShowSpeech(1,4);
            yield return new WaitForSeconds(2f);
            stormeHander.ShowSpeech(3,3);//get off me
            yield return new WaitForSeconds(5);
            policeBot_2.ShowSpeech(2, 4);
            yield return new WaitForSeconds(8f);

            UIController.Instance.ShowLoadingAnimation(5);
        }

        Controller.self.sequenceController.StartThisScene(Sequence.street_seq);

  
    }

    

 




}
