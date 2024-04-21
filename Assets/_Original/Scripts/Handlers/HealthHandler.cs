using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [ReadOnly, SerializeField] private CommonHandler commonHandler;


    public void InitializeHandler(CommonHandler cmnHandler)
    {
        commonHandler = cmnHandler;
    }
}
