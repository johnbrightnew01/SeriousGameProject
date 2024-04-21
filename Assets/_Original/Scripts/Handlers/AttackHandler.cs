using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [ReadOnly, SerializeField] private CommonHandler commonHandler;


    public void InitializeHandler(CommonHandler cmnHandler)
    {
        commonHandler = cmnHandler;
    }
}
