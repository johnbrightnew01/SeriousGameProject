using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [ReadOnly, SerializeField] private CommonHandler commonHandler;


    public void InitializeHandler(CommonHandler cmnHandler)
    {
        commonHandler = cmnHandler;
    }
}
