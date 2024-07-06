using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureToggleView : MonoBehaviour
{
    [SerializeField, ReadOnly] private OutroSceneView outroView;
    [SerializeField] private List<GameObject> pictureList;
    [SerializeField, ReadOnly] private int imageNum = 0;

    private void OnEnable()
    {
        if (pictureList.Count == 0) return;
        
        for (int i = 0; i < pictureList.Count; i++)
        {            
            pictureList[i].SetActive(false);
        }
        pictureList[imageNum].SetActive(true);
    }


    public void OnTogglePicture()
    {
        imageNum++;
        if(imageNum > pictureList.Count-1)
        {
            imageNum = 0;
        }
        for (int i = 0; i < pictureList.Count; i++)
        {
            pictureList[i].SetActive(false);
        }
        pictureList[imageNum].SetActive(true);
    }

}
