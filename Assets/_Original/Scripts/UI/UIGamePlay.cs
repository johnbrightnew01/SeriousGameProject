using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] private GameObject playerHpPanel;
    [SerializeField] private Image playerHpBar;
    [SerializeField] private TextMeshProUGUI waveText; 
    public static UIGamePlay Instance;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        TogglePlayerHpPanel(false);
        waveText.DOFade(0, 0).SetEase(Ease.Linear);
    }

    public void TogglePlayerHpPanel(bool isEnable)
    {
        playerHpPanel.SetActive(isEnable);
    }


    public void UpdatePlayerHP(float hpLeft)
    {
        playerHpBar.DOFillAmount( hpLeft, 0.5f);
    }


    public void ShowWaveText(int waveNum, float showTime)
    {
        if(waveNum == 4)
        {
            waveText.text = "BOSS";
        }
        else
        {
            waveText.text = "WAVE " + waveNum.ToString();
        }
        waveText.DOFade(1f, 0);
        waveText.transform.DOScale(1f, showTime).OnComplete(() =>
        {
            waveText.DOFade(0f, 0).SetEase(Ease.Linear);
        });
    }


}
