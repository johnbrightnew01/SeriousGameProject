using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject loosePanel;


    private void OnEnable()
    {
        if (Controller.self.levelController.IsLevelSuccess())
        {
            ShowWinPanel();
        }
        else
        {
            ShowLoosePanel();
        }
    }



    private void ShowWinPanel()
    {
        winPanel.SetActive(true);
        loosePanel.SetActive(false);
        StartCoroutine(ShowTheOutro());
    }

    private void ShowLoosePanel()
    {
        loosePanel.SetActive(true);
        winPanel.SetActive(false);
    }

    IEnumerator ShowTheOutro()
    {
        yield return new WaitForSeconds(3f);
        Controller.self.sequenceController.StartThisScene(Sequence.outro_seq);
    }

    public void TryAgain() // call from ui
    {
        Controller.self.sequenceController.StartThisScene(Sequence.street_seq);
        SceneManager.LoadScene(0);
    }
}
