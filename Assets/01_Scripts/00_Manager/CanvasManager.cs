using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    [Header("Panel")]
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LoosePanel;
    [SerializeField] private GameObject QuitPanel;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject CreatePanel;
    [SerializeField] private GameObject LevelInventoryPanel;

    [Space]
    public GameObject SelectedCharacterPanel;
    public GameObject WaitingCharacterPanel;

    [Space]
    [Header("ButtonList")]
    [SerializeField] private GameObject PenPanel;

    [Space]
    [Header("Information Panel")]
    [SerializeField] private Image objectImage;
    [SerializeField] private TMP_Text objectTitle;
    [SerializeField] private TMP_Text objectDescription;

    [Space]
    [Header("Character Information")]
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private Image playerRender;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject inventoryButton;

    [Space]
    [Header("Slider")]
    [SerializeField] private Slider inkSlider;


    [Space]
    [Header("Text")]
    public TMP_Text logText;
    [Space]
    [SerializeField] private TMP_Text lifeText;
    [SerializeField] private TMP_Text staminaText;
    [SerializeField] private TMP_Text vignetteText;
    [SerializeField] private TMP_Text pageIndicator;
    [SerializeField] private TMP_Text winIndicator;
    [SerializeField] private TMP_Text looseIndicator;

    [Space]
    [Header("Button")]
    [SerializeField] private Button moveButton;

    [Space]
    [Header("Prefabs")]
    [SerializeField] private GameObject levelInventoryButtonPrefabs;

    [Space]
    Vector2 CharacterShifter = new Vector2(450, -450);
    int XshifterIndex = 0;
    int YshiterIndex = 0;
    int Xshifter = 0;
    int Yshifter = 0;

    [SerializeField] private GameObject grid;

    public Slider InkSlider { get => inkSlider; set => inkSlider = value; }
    public Image ObjectImage { get => objectImage; set => objectImage = value; }
    public TMP_Text ObjectTitle { get => objectTitle; set => objectTitle = value; }
    public TMP_Text ObjectDescription { get => objectDescription; set => objectDescription = value; }

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : CanvasManager");
        else
            instance = this;
    }
    private void Start()
    {
        SetActiveMoveButton(false);
        QuitPanel.SetActive(false);
        SelectedCharacterPanel.SetActive(false);
        WaitingCharacterPanel.SetActive(false);
        InkSlider.maxValue = CreationManager.instance.NegociationTime;
        InkSlider.value = InkSlider.maxValue;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitPanel.SetActive(!QuitPanel.activeSelf);
        }
    }

    public void SetActiveMoveButton(bool activeObject)
    {
        if (activeObject)
            moveButton.gameObject.GetComponent<Image>().color = Color.green;
        else
            moveButton.gameObject.GetComponent<Image>().color = Color.red;

        moveButton.interactable = activeObject;
    }

    public void SetSelectedPen()
    {
        for (int i = 0; i < PenPanel.transform.childCount; i++)
        {
            if(i!= 4)
                PenPanel.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
    }

    #region Update Information
    public void NewLogEntry(string content)
    {
        logText.text = content;
    }

    public void UpdateInformationText(int life, int stamina, int vignette)
    {
        Update_Happy_Sadness_Status(life);
        Update_Angry_Fear_Status(stamina);
        UpdateVignetteToDraw(vignette);
    }

    public void Update_Happy_Sadness_Status(int life)
    {
        lifeText.text = "Happy / Sad : " + life;
    }
    public void Update_Angry_Fear_Status(int stamina)
    {
        staminaText.text = "Angry / Fear : " + stamina;
    }

    public void UpdateVignetteToDraw(int amoutOfVignette)
    {
        vignetteText.text = "Next Vignette : " + amoutOfVignette;
    }

    public GameObject NewItemInLevelInventory(Object_SO item)
    {
        GameObject newItemInInventory = Instantiate(levelInventoryButtonPrefabs, LevelInventoryPanel.transform);
        newItemInInventory.GetComponent<Image>().sprite = item.Sprite;
        return newItemInInventory;
    }

    public void ClearLevelInventory()
    {
        for (int i = 0; i < LevelInventoryPanel.transform.childCount; i++)
        {
            Destroy(LevelInventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    public void RemoveObjInPlayerInventory(int index)
    {
        print(index);
        if(SelectedCharacterPanel.GetComponent<SelectedCharacter_GAMEUI>().InventoryPanel.transform.GetChild(index) != null)
            Destroy(SelectedCharacterPanel.GetComponent<SelectedCharacter_GAMEUI>().InventoryPanel.transform.GetChild(index).gameObject);
    }

    #endregion

    public void UpdatePageIndicator()
    {
        pageIndicator.text = "Nb page : " + LevelManager.instance.AmountOfpageDone;
    }

    public void UpdateInkSlider(float value)
    {
        InkSlider.value += value;
    }

    public void SetInkSlider()
    {
        InkSlider.value = CreationManager.instance.NegociationTime;
    }

    #region Win / Loose Panel

    public void PlayerWinTheGame(Character_SO perso)
    {
        SetActiveMoveButton(false);
        WinPanel.SetActive(true);
        winIndicator.text = GameManager.instance.OrderCharacter.Count > 0 ? perso.CharacterName + " a survécu !\n C'est au tour de " + GameManager.instance.OrderCharacter[0].AssignedElement.CharacterName : "retouner à la base";
        if(!(GameManager.instance.OrderCharacter.Count > 0))
        {
            GridManager.instance.ClearScene();
            //GamePanel.SetActive(false);
            grid.SetActive(false);
        }
        
    }

    public void PlayerLooseTheGame(Character_SO perso)
    {
        SetActiveMoveButton(false);
        LoosePanel.SetActive(true);
        looseIndicator.text = GameManager.instance.OrderCharacter.Count > 0 ? perso.CharacterName + " est mort !\n Il ne vous reste plus que " + CreationManager.instance.listOfCharacter.Count + " membres !" : perso.CharacterName + " est mort !\n retouner à la base. Il ne vous reste plus que " + CreationManager.instance.listOfCharacter.Count + " membres !";

        if (!(GameManager.instance.OrderCharacter.Count > 0))
        {
            GridManager.instance.ClearScene();
            //GamePanel.SetActive(false);
            grid.SetActive(false);
        }
    }

    #endregion

    public void SetUpGamePanel()
    {
        GamePanel.SetActive(true);
        CreatePanel.SetActive(false);
        SelectedCharacterPanel.SetActive(true);
        WaitingCharacterPanel.SetActive(true);
        GridManager.instance.ClearScene();
        EventGenerator.instance.GenerateGrid();
        grid.SetActive(true);
        //LevelManager.instance.SpawnObject(PlayerManager.instance.Inventory);
        //SetUpCharacterInfo();

    }

    public void SetUpCharacterInfo()
    {
        SelectedCharacterPanel.GetComponent<SelectedCharacter_GAMEUI>().SetUpUI();

        for (int i = 0; i < GameManager.instance.OrderCharacter.Count; i++)
        {
            WaitingCharacterPanel.transform.GetChild(i).GetComponent<WaitingCharacterPanel>().SetUpUI(GameManager.instance.OrderCharacter[i]);
        }

        for (int i = GameManager.instance.OrderCharacter.Count; i < WaitingCharacterPanel.transform.childCount; i++)
        {
            WaitingCharacterPanel.transform.GetChild(i).gameObject.SetActive(false);
        }

        /*print("eefefe");
        print(PlayerManager.instance.CharacterData);
        Character_SO toSet = PlayerManager.instance.CharacterData;

        //rt.offsetMin = rt.offsetMin = new Vector2(0,rt.offsetMin.y);
        
        playerName.text = toSet.CharacterName;

        playerRender.sprite = toSet.Render;

        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }

        foreach (var item in PlayerManager.instance.Inventory)
        {
            GameObject myButton = Instantiate(inventoryButton, inventoryPanel.transform);
            myButton.GetComponent<Image>().sprite = item.Sprite;
        }*/
    }

    public void SetUpCreationPanel()
    {
        /*foreach (var item in LevelManager.instance.PageInventory)
        {
            CreationManager.instance.GlobalInventory.Add(item.Data);
        }*/
        CreationManager.instance.PutAllObjectInInventory();
        CreationManager.instance.CreateObjectList();
        GamePanel.SetActive(false);
        CreatePanel.SetActive(true);
    }
}
