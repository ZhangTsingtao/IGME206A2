using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Assign")]
    public Button restartButton, nextLevelButton, nextButton, attackButton, inventoryButton, closeInventoryButton, lootButton;
    [Header("Assign")]
    public NPC enemy;
    [Header("Assign")]
    public TMP_Text stageText, infoText, playerStatusHeaderText, playerStatusText;
    [Header("Assign")]
    public GameObject drop;
    [Header("Assign")]
    public GameObject gameOverUI;

    [Header("No Assign")]
    [SerializeField] private Item lootItem;
    private DialogDisplay dialogDisplay;
    private string infoTextBuffer;
    public enum LevelStage
    {
        TrashTalk,
        Combat,
        Loot,
        Explore
    }
    public LevelStage levelStage;
    void Start()
    {
        Debug.Log(PlayerStatus.LevelID);
        PlayerStatus.NextLevel();
        dialogDisplay = transform.GetComponent<DialogDisplay>();
        restartButton.onClick.AddListener(RestartLevel);
        nextLevelButton.onClick.AddListener(NextLevel);
        nextButton.onClick.AddListener(NextEvent);

        //Combat
        attackButton.onClick.AddListener(Attack);
        inventoryButton.onClick.AddListener(Inventory);
        closeInventoryButton.onClick.AddListener(CloseInventory);

        nextLevelButton.gameObject.SetActive(false);
        attackButton.gameObject.SetActive(false);
        inventoryButton.gameObject.SetActive(false);
        closeInventoryButton.gameObject.SetActive(false);
        lootButton.gameObject.SetActive(false);
        playerStatusHeaderText.gameObject.SetActive(false);
        playerStatusText.gameObject.SetActive(false);
        drop.SetActive(false);
        gameOverUI.SetActive(false);

        stageText.text = "";
        infoText.text = "";

        NextEvent();
    }
    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void NextEvent()
    {
        switch (levelStage)
        {
            case LevelStage.TrashTalk:
                TrashTalk(); break;
            case LevelStage.Combat:
                Combat(); break;
            case LevelStage.Loot:
                Loot(); break;
            case LevelStage.Explore:
                Explore(); break;

            default: 
                Debug.Log("!!!BUG!!!"); break;
        } 
    }

    private void TrashTalk()
    {
        stageText.text = "Trash Talk";

        bool trashTalkEnd;
        (trashTalkEnd, infoText.text) = dialogDisplay.NextDialogLine();

        if (trashTalkEnd)
        {
            levelStage = LevelStage.Combat;
        }
    }
    private void Combat()
    {
        if(enemy == null)
        {
            Debug.LogWarning("No enemy attached to UIController");
            levelStage = LevelStage.Explore;
            NextEvent();
        }
        //Texts
        stageText.text = "Combat";
        infoText.text = "";
        playerStatusHeaderText.gameObject.SetActive(true);
        playerStatusText.gameObject.SetActive(true);
        drop.SetActive(true);

        playerStatusText.text = PlayerStatus.DisplayStatus();

        //Buttons
        nextButton.gameObject.SetActive(false);
        attackButton.gameObject.SetActive(true);
        inventoryButton.gameObject.SetActive(true);
    }

    

    private void Loot()
    {
        stageText.text = "Loot";
        Debug.Log("Loot");

        if(enemy.TryGetComponent<Item>(out Item item))
        {
            PlayerStatus.GetItem(item);
        }
        levelStage = LevelStage.Explore;
    }
    private void Explore()
    {
        stageText.text = "Explore";
        Debug.Log("Explore");
    }

    //Below are detailed methods in each stage
    private void Attack()
    {
        infoText.text = "";
        if (enemy == null) return;
        infoText.text += enemy.TakeDamage(PlayerStatus.PlayerAttackValue);
        infoText.text += "\n";

        if (!enemy.Alive)
        {
            attackButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
            levelStage = LevelStage.Loot;
            lootItem = enemy.item;
            enemy.Die();
            return;
        }

        bool playerAlive;
        string playerFightInfo;
        (playerAlive, playerFightInfo) = PlayerStatus.TakeDamage(enemy.attackValue);
        infoText.text += "\n" + playerFightInfo;
        playerStatusText.text = PlayerStatus.DisplayStatus();

        if (!playerAlive)
        {
            Debug.Log("Player died");
            gameOverUI.SetActive(true);
        }
    }

    private void Inventory()
    {
        stageText.text = "Inventory";
        infoTextBuffer = infoText.text;
        infoText.text = "";
        inventoryButton.gameObject.SetActive(false);
        attackButton.gameObject.SetActive(false);
        closeInventoryButton.gameObject.SetActive(true);
    }
    private void CloseInventory()
    {
        stageText.text = "Combat";
        infoText.text = infoTextBuffer;
        infoTextBuffer = "";
        inventoryButton.gameObject.SetActive(true);
        attackButton.gameObject.SetActive(true);
        closeInventoryButton.gameObject.SetActive(false);
    }
}
