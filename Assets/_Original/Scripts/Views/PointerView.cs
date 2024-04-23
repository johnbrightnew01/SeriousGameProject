using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerView : MonoBehaviour
{
    public Transform pointerHolder;
    public Transform scalePointer;
    public Transform rotatorPointer;


    public void DoScalePointer(float scaleS)
    {
        pointerHolder.gameObject.SetActive(true);
        scalePointer.localScale = Vector3.one * scaleS;
    }

    public void ResetPointer()
    {
        pointerHolder.gameObject.SetActive(false);
        scalePointer.localScale = Vector3.one;
     //   rotatorPointer.localScale = Vector3.zero;
    }

}
