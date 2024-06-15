using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] private Image playerHpBar;

    public static UIGamePlay Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    public void UpdatePlayerHP(float hpLeft)
    {
        playerHpBar.DOFillAmount(1f - hpLeft, 0.5f);
    }


}
