using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Assign")]
    public Button nextLevelButton, nextButton, attackButton, inventoryButton, closeInventoryButton, lootButton, equipButton, unequipButton;
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
    [SerializeField] List<GameObject> itemButtons = new List<GameObject>();

    public int initialItemAmount = 0;
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
        //Debug
        //for (int i = 0; i < initialItemAmount; i++)
        //{
        //    PlayerStatus.PlayerItems.Add(new Item());
        //}
        //Done

        Debug.Log(PlayerStatus.LevelID);
        PlayerStatus.NextLevel();
        dialogDisplay = transform.GetComponent<DialogDisplay>();
        nextLevelButton.onClick.AddListener(NextLevel);
        nextButton.onClick.AddListener(NextEvent);

        //Combat
        attackButton.onClick.AddListener(Attack);
        inventoryButton.onClick.AddListener(OpenInventory);
        closeInventoryButton.onClick.AddListener(CloseInventory);

        nextLevelButton.gameObject.SetActive(false);
        attackButton.gameObject.SetActive(false);
        inventoryButton.gameObject.SetActive(false);
        closeInventoryButton.gameObject.SetActive(false);
        lootButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        unequipButton.gameObject.SetActive(false);

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
    private void ShiftToNextStage()
    {
        levelStage++;
        nextButton.GetComponentInChildren<TMP_Text>().text = levelStage.ToString();
    }
    private void TrashTalk()
    {
        stageText.text = "Trash Talk";

        bool trashTalkEnd;
        (trashTalkEnd, infoText.text) = dialogDisplay.NextDialogLine();

        if (trashTalkEnd)
        {
            ShiftToNextStage();
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

        if(lootItem == null)
        {
            infoText.text = "Nothing valuable found from this enemy";
        }
        else
        {
            infoText.text = PlayerStatus.GetItem(lootItem);
        }

        ShiftToNextStage();
    }
    private void Explore()
    {
        stageText.text = "Explore";
        Debug.Log("Explore");
    }

    //Below are detailed methods in each stage
    private void Attack()
    {
        string textBuffer = infoText.text;
        infoText.text = "";

        if (enemy == null) return;

        infoText.text += enemy.TakeDamage(PlayerStatus.PlayerAttackValue);
        infoText.text += "\n";

        if (!enemy.Alive)
        {
            attackButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
            lootItem = enemy.item;
            enemy.Die();

            ShiftToNextStage();
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

    private void OpenInventory()
    {
        stageText.text = "Inventory";
        infoTextBuffer = infoText.text;

        int itemAmount;
        (infoText.text, itemAmount) = PlayerStatus.DisplayInventory(infoText.transform.position);

        DisplayInventoryUI(true);

        attackButton.gameObject.SetActive(false);        
    }

    private void CloseInventory()
    {
        stageText.text = levelStage.ToString();
        infoText.text = infoTextBuffer;
        infoTextBuffer = "";
        inventoryButton.gameObject.SetActive(true);
        attackButton.gameObject.SetActive(true);

        DisplayInventoryUI(false);
        
        if (enemy == null || enemy.Alive == false)
        {
            attackButton.gameObject.SetActive(false);
        }
    }

    private void DisplayInventoryUI(bool display)
    {
        closeInventoryButton.gameObject.SetActive(display);
        inventoryButton.gameObject.SetActive(!display);

        if (display)
        {
            for (int i = 0; i < PlayerStatus.PlayerItems.Count; i++)
            {
                Item item = PlayerStatus.PlayerItems[i];

                GameObject equipButtonGO = Instantiate(equipButton.gameObject);
                GameObject unequipButtonGO = Instantiate(unequipButton.gameObject);
                equipButtonGO.transform.SetParent(transform);
                unequipButtonGO.transform.SetParent(transform);
                equipButtonGO.SetActive(true);
                unequipButtonGO.SetActive(true);

                //Code
                itemButtons.Add(equipButtonGO);
                itemButtons.Add(unequipButtonGO);
                equipButtonGO.GetComponent<Button>().onClick.AddListener(item.EquipItem);
                unequipButtonGO.GetComponent<Button>().onClick.AddListener(item.UnequipItem);
                

                //Position
                Vector3 buttonStartPosition = infoText.transform.position + new Vector3(-20, -80 - 124 * i, 0);

                equipButtonGO.transform.position = buttonStartPosition;
                unequipButton.transform.position = buttonStartPosition + new Vector3(200, 0, 0);
            }
        }
        else
        {
            foreach (GameObject buttonGO in itemButtons)
            {
                Destroy(buttonGO);
            }
            itemButtons.Clear();
        }
    }
}
