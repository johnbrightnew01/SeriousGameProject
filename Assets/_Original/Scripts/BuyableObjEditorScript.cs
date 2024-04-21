using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class BuyableObjEditorScript : MonoBehaviour
{
#if UNITY_EDITOR
    public bool isRun;
   

    private void OnEnable()
    {
        if (isRun)
        {
            RunIt();
        }
    }


    private void RunIt()
    {
      
    }


#endif
}
