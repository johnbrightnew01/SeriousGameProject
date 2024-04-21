using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToPull : MonoBehaviour
{
    public float waitTime;


    private void OnEnable()
    {
        CancelInvoke();   
        Invoke("ToPull", waitTime);
    }

    private void ToPull()
    {
        Puller.Instance.RestockToPull(this.gameObject);
    }

    
    IEnumerator WaitAndAddToPull()
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.transform.parent = null;
        Puller.Instance.RestockToPull(this.gameObject);
    }

    private void OnDisable()
    {
       
    }

}
