using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class FloatingUI : MonoBehaviour
{ 
    private Camera mainCamera;
    [ReadOnly, SerializeField]
    bool isActive = false;
    [SerializeField] public Transform target;
    [SerializeField] public Vector3 zDistance;
    public RectTransform homeIcon;
    public GameObject healBarPanel;
    public GameObject speechPanel;
    public TextMeshProUGUI speechText;
    public Image healthBar;
    private bool m_outOfScreen;
    [Space]
    [Range(0, 100)]
    public float m_edgeBuffer;
    public GameObject outOfscreenLogo;
    public bool isPointToTarget;
    private bool isAnimating;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void  SetFloatingUI(Transform trgt)
    {

        target = trgt;
        this.gameObject.transform.parent = UIController.Instance.floatingUICanvas.transform;
         this.transform.localScale = Vector3.one;
        speechPanel.transform.localScale = Vector3.zero;
        healBarPanel.transform.localScale = Vector3.zero;
        isActive = true;
      
    }

    public void ToggleUIType(bool isHealthBar)
    {
        healBarPanel.SetActive(isHealthBar);
        speechPanel.SetActive(!isHealthBar);
    }

    public void ShowSpeechCloud(string speech, float popUpTime = 1f)
    {
       // healBarPanel.SetActive(false);
      // speechPanel.SetActive(true);

        speechPanel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            speechText.text = speech;
            Debug.Log("speechIs hrere");
            Invoke("CloseSpeechCloud", popUpTime);

        });

    }

    public void CloseSpeechCloud()
    {
        speechPanel.transform.DOScale(Vector3.zero, 0.3f);
        speechText.text = "";

    }


    public void UpdateHp(float currentHealth)
    {
        healthBar.DOFillAmount(currentHealth, 0.4f);
    }

    public void ReActive()
    {
        this.gameObject.SetActive(true);
        isActive = true;
    }  

    public void OnDead()
    {
        this.gameObject.SetActive(false);
        isActive = false;
    }

    public void UpdateHealthBar(float fillAmount)
    {
      
        healthBar.fillAmount = fillAmount;
    }

    private void Update()
    {
        if (GlobalData.isGameOver)
        {
            this.gameObject.SetActive(false);
            return;
        }
        if (!isActive || target == null)
            return;
        Vector3 newPos = target.transform.position + zDistance;
        newPos = mainCamera.WorldToViewportPoint(newPos);
        //Simple check if the target object is out of the screen or inside
        if (newPos.x > 1 || newPos.y > 1 || newPos.x < 0 || newPos.y < 0)
            m_outOfScreen = true;
        else
            m_outOfScreen = false;
        if (newPos.z < 0)
        {
            newPos.x = 1f - newPos.x;
            newPos.y = 1f - newPos.y;
            newPos.z = 0;
            newPos = Vector3Maxamize(newPos);
        }
        newPos = mainCamera.ViewportToScreenPoint(newPos);
        newPos.x = Mathf.Clamp(newPos.x, m_edgeBuffer, Screen.width - m_edgeBuffer);
        newPos.y = Mathf.Clamp(newPos.y, m_edgeBuffer, Screen.height - m_edgeBuffer);
        newPos.z = 0;
        if (m_outOfScreen)
        {
            homeIcon.gameObject.SetActive(false);
         /*   outOfscreenLogo.gameObject.SetActive(true);
            if (isPointToTarget)
            {
                var targetPosLocal = mainCamera.transform.InverseTransformPoint(transform.position);
                var targetAngle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg - 90;
                //Apply rotation
                outOfscreenLogo.transform.eulerAngles = new Vector3(0, 0, targetAngle + 180f);
            }*/
        }
        else
        {
            homeIcon.gameObject.SetActive(true);
         //   outOfscreenLogo.gameObject.SetActive(false);
        }
        this.transform.position = newPos;
    }

    public Vector3 Vector3Maxamize(Vector3 vector)
    {
        Vector3 returnVector = vector;
        float max = 0;
        max = vector.x > max ? vector.x : max;
        max = vector.y > max ? vector.y : max;
        max = vector.z > max ? vector.z : max;
        returnVector /= max;
        return returnVector;
    }

}
