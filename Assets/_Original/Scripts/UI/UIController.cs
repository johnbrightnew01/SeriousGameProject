using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
[DefaultExecutionOrder(-10)]
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
    public Canvas floatingUICanvas;
    public GameObject interactiveIntroUI;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    public void ToggleInteractiveIntroUI(bool isEanble)
    {
        interactiveIntroUI?.gameObject.SetActive(isEanble);
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

    public void ShowLoadingAnimation(float blackTime, float fadeTime = 0.5f)
    {
        return; // delete this
        StartCoroutine(ShowFadeoutLoading(blackTime, fadeTime));
   
    }

    IEnumerator ShowFadeoutLoading(float time, float fadeTime)
    {
        loadingImage.color = new Color(0, 0, 0, 1);
        loadingImage.gameObject.SetActive(true);
        loadingImage.DOFade(1f, fadeTime);
        yield return new WaitForSeconds(time);
        
        loadingImage.DOFade(0f, fadeTime);
        yield return new WaitForSeconds(fadeTime);

        loadingImage.gameObject.SetActive(false);
    }
   
}
