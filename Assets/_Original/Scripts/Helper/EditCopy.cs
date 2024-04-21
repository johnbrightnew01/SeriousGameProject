using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;
using System;

[ExecuteInEditMode]
public class EditCopy : MonoBehaviour
{
#if UNITY_EDITOR
    public bool isRun;
    Coroutine updater;
    public float distance = 0;
    public GameObject A;
    public GameObject B;
    
    private void OnEnable()
    {
        if (isRun)
        {
            CalculateDistance();
        }
    }

    private void CalculateDistance()
    {
        if(updater != null)
        {
            StopCoroutine(updater);
        }
        updater =  StartCoroutine(DistUpdater());
        
    }

    IEnumerator DistUpdater()
    {
        while (isRun)
        {
            
            if(A == null || B == null)
            {
                yield break;
            }
            distance = Vector3.Distance(A.transform.position, B.transform.position);
            yield return new WaitForSeconds(0.2f);
        }
    }




#endif
}
