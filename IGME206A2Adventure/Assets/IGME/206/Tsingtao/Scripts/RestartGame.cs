using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    public Button restartButton;

    private void Start()
    {
        restartButton = GetComponent<Button>();
        restartButton.onClick.AddListener(RestartTheGame);
    }
    public void RestartTheGame() 
    {
        SceneManager.LoadScene(0);
        PlayerStatus.ResetStatus();
    }
}
