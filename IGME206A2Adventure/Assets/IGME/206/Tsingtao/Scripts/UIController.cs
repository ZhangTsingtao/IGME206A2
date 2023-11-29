using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button restartButton, nextLevelButton, nextDialogButton, lastDialogButton;
    public DialogDisplay playerDialogDisplay;
    void Start()
    {
        restartButton.onClick.AddListener(RestartScene);
        nextLevelButton.onClick.AddListener(NextLevel);
        nextDialogButton.onClick.AddListener(NextDialog);
        lastDialogButton.onClick.AddListener(LastDialog);
    }
    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void NextDialog()
    {
        playerDialogDisplay.NextDialogLine();
    }
    private void LastDialog()
    {
        playerDialogDisplay.LastDialogLine();
    }
}
