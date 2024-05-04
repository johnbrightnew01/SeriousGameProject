using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIController : MonoBehaviour
{
    public enum UiType
    {
        menuUI,
        gamePlayUI,
        gameOverUI
    }

    public static UIController Instance;
    [SerializeField]
    private UIMenu uiMenu;
    [SerializeField]
    private UIGamePlay uiGamePlay;
    [SerializeField]
    private UIGameOver uiGameOver;
    public Image loadingImage;
    public TargetController targetController;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void ShowUI(UiType type, bool isAnimation, float blendTime = 1f)
    {
        if (isAnimation)
        {

        }

        if (type == UiType.menuUI)
        {

        }
    }


    public void ShowMenu(bool isAnimation, float animTime = 0.7f)
    {
        
    }

    public void ShowLoadingAnimation()
    {
        
        loadingImage.gameObject.SetActive(true);
        loadingImage.transform.DOScale(1f, 0.5f).OnComplete(() =>
        {
            loadingImage.gameObject.SetActive(false);
        });
    }
   
}
