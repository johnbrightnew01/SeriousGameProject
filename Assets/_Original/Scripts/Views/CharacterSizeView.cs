using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSizeView : MonoBehaviour
{
    [SerializeField] private float minSizeOfCharacter = 1.0884f;
    [SerializeField] private float maxSizeOfCharacter = 1.16f;

    [SerializeField] private float maxUpPos = 0.62f;
    [SerializeField] private float maxDownPos = -1.39f;
    
    void Update()
    {

        this.transform.localScale = (GetTheValue(this.transform.position.y)) * Vector3.one;
        
    }

    private float GetTheValue(float posY)
    {
        var x = (maxSizeOfCharacter - minSizeOfCharacter) * ((posY - maxDownPos) / (maxUpPos - maxDownPos)) + minSizeOfCharacter;
        return x;
    }
}
