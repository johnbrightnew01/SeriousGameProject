using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroSceneView : MonoBehaviour
{
    [SerializeField] private GameObject room1;
    [SerializeField] private GameObject room1Door;
    [Space(10)]
    [SerializeField] private GameObject room2;
    [SerializeField] private GameObject room2TriggerHolder;
    [SerializeField] private List<GameObject> room2TriggerList;
    [SerializeField] private GameObject room2_Video_page;
    [SerializeField] private GameObject room2_Info_page;
    [SerializeField] private List<GameObject> room2_info_picList;
    [Space(10)]
    [SerializeField] private GameObject room3;
    [SerializeField] private GameObject room3TriggerHolder;
    [SerializeField] private List<GameObject> room3InfoList;
    [SerializeField] private List<GameObject> room3TriggerList;

    [SerializeField] private Transform mouseCursor;
    [SerializeField] private Transform normalMouse;
    [SerializeField] private Transform interactiveMouse;
    [SerializeField, ReadOnly] private int currentRoomNo = 1;


    private void OnEnable()
    {
        currentRoomNo = 1;
        interactiveMouse.gameObject.SetActive(false);
    }

    private void Update()
    {
        var pointerPos = Input.mousePosition;
        mouseCursor.transform.position = new Vector3(pointerPos.x, pointerPos.y, 0);
    }

    public void OnPointerEnter()
    {
        interactiveMouse.gameObject.SetActive(true);
        normalMouse.gameObject.SetActive(false);
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit()
    {
        interactiveMouse.gameObject.SetActive(false);
        normalMouse.gameObject.SetActive(true);
        Debug.Log("Mouse exit");
    }

    public void OnMuseumEnter()
    {
        room1.SetActive(false);
        room2TriggerHolder.gameObject.SetActive(true);
        room3TriggerHolder.gameObject.SetActive(false);

        room1Door.gameObject.SetActive(false);
        normalMouse.gameObject.SetActive(true);
        interactiveMouse.gameObject.SetActive(false);
        ToggleInteractiveTrigger(room2TriggerList, true);
        room2.SetActive(true);
        room3.SetActive(false);
    }
    
    public void OpenRoom3()
    {
        room1.SetActive(false);
        room2TriggerHolder.gameObject.SetActive(false);
        room3TriggerHolder.gameObject.SetActive(true);


        ToggleInteractiveTrigger(room2TriggerList, false);
        ToggleInteractiveTrigger(room1Door, false);

        room2.SetActive(false);
        room3.SetActive(true);
    }

    public void Room3Info(int infoPanelNumber)
    {
        for (int i = 0; i < room3InfoList.Count; i++)
        {
            room3InfoList[i].gameObject.SetActive(false);
        }
        room3InfoList[infoPanelNumber].gameObject.SetActive(true);
    }

    public void ReturenToRoom3()
    {
        room1.SetActive(false);
        room3TriggerHolder.gameObject.SetActive(true);
        ToggleInteractiveTrigger(room3TriggerList, true);
        for (int i = 0; i < room3InfoList.Count; i++)
        {
            room3InfoList[i].gameObject.SetActive(false);
        }
        

        room2.SetActive(false);
        room3.SetActive(true);
    }


    public void Room2VideoPage_OnMouseClick()
    {
        room2_Video_page.gameObject.SetActive(true);
        room2_Info_page.gameObject.SetActive(false);       

    }   

    public void Room2PosterPage_OnMouseClick()
    {
        room2_Video_page.gameObject.SetActive(false);
        room2_Info_page.gameObject.SetActive(true);
    }

    public void ReturnToRoom2()
    {
        room2_Video_page.gameObject.SetActive(false);
        room2_Info_page.gameObject.SetActive(false);
        ToggleInteractiveTrigger(room2TriggerList, true);
    }

    private void ToggleInteractiveTrigger(List<GameObject> list, bool isEnable)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].gameObject.SetActive(isEnable);
        }

        if (!isEnable)
        {
            normalMouse.gameObject.SetActive(true);
            interactiveMouse.gameObject.SetActive(false);
        }
    }


    private void ToggleInteractiveTrigger(GameObject obj, bool isEnable)
    {
        obj.gameObject.SetActive(isEnable);

        if (!isEnable)
        {
            normalMouse.gameObject.SetActive(true);
            interactiveMouse.gameObject.SetActive(false);
        }
    }

}
