using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public Button restartButton, nextLevelButton, nextButton, previousButton;
    public DialogDisplay DialogDisplay;

    public TMP_Text StageText;
    public enum LevelStage
    {
        TrashTalk,
        Fight,
        Inventory,
        Loot,
        Explore
    }
    public LevelStage levelStage;
    void Start()
    {
        DialogDisplay = transform.GetComponent<DialogDisplay>();
        restartButton.onClick.AddListener(RestartScene);
        nextLevelButton.onClick.AddListener(NextLevel);
        nextButton.onClick.AddListener(NextEvent);
        previousButton.onClick.AddListener(LastDialog);

        nextLevelButton.gameObject.SetActive(false);
    }
    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void NextEvent()
    {
        switch (levelStage)
        {
            case LevelStage.TrashTalk:
                NextDialog(); break;
            case LevelStage.Fight:
                Fight(); break;
            case LevelStage.Inventory:
                Inventory(); break;
            case LevelStage.Loot:
                Loot(); break;
            case LevelStage.Explore:
                Explore(); break;
        } 
    }

    private void NextDialog()
    {
        StageText.text = "Trash Talk";
        bool trashTalkEnd = DialogDisplay.NextDialogLine();
        if (trashTalkEnd)
        {
            levelStage = LevelStage.Fight;
        }
    }
    private void Fight()
    {
        StageText.text = "Fight";
        Debug.Log("Fight");
        levelStage = LevelStage.Inventory;
    }
    private void Inventory() 
    {
        StageText.text = "Inventory";
        Debug.Log("Inventory");
        levelStage = LevelStage.Explore;
    }
    private void Loot()
    {
        StageText.text = "Loot";
        Debug.Log("Loot");
        levelStage = LevelStage.Explore;
    }
    private void Explore()
    {
        StageText.text = "Explore";
        Debug.Log("Explore");
    }
    private void LastDialog()
    {
        DialogDisplay.LastDialogLine();
    }
}
