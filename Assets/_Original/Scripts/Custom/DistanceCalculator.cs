using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{

    public Transform objA;
    public Transform objB;
    public float distance;

    void Update()
    {
        if(objA && objB)
        {
            distance = Vector3.Distance(objA.position, objB.position);
        }
    }
}
