using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] private GameObject playerHpPanel;
    [SerializeField] private Image playerHpBar;

    public static UIGamePlay Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        TogglePlayerHpPanel(false);
    }

    public void TogglePlayerHpPanel(bool isEnable)
    {
        playerHpPanel.SetActive(isEnable);
    }


    public void UpdatePlayerHP(float hpLeft)
    {
        playerHpBar.DOFillAmount( hpLeft, 0.5f);
    }


}
