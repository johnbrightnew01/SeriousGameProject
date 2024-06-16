using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerView : MonoBehaviour
{
    [SerializeField] private float minUpPos = 0f;
    [SerializeField] private float maxUpPos = 1.3f;
    [SerializeField] private int maxLayer = 100;
    [SerializeField] private float minLayer = 5;

    [SerializeField] private SpriteRenderer sprite;


    private void Awake()
    {
        if(sprite == null)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if (sprite != null)
        {
            sprite.sortingOrder = (int)GetTheValue(this.transform.position.y);
        }
    }

    private float GetTheValue(float posY)
    {
        var x = (minLayer - maxLayer) * ((posY - minUpPos) / (maxUpPos - minUpPos)) + maxLayer;
        return x;
    }



}
