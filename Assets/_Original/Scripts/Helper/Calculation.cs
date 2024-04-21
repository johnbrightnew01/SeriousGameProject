using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Calculation : MonoBehaviour
{
    public static float GetValueFrom2SetOfRange(float x, float x_min, float x_max, float Scale_min, float Scale_max)
    {        
        return (Scale_max - Scale_min) * ((x - x_min) / (x_max - x_min)) + Scale_min;
    }
}
