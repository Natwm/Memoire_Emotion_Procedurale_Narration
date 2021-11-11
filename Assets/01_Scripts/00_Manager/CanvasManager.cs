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

    [Space]
    public GameObject CharacterPanel;
    public GameObject CharacterReaderPrefab;

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

    [Space]
    [Header("Button")]
    [SerializeField] private Button moveButton;

    [Space]
    Vector2 CharacterShifter = new Vector2(450, -450);
    int XshifterIndex = 0;
    int YshiterIndex = 0;
    int Xshifter = 0;
    int Yshifter = 0;


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
        CharacterPanel.SetActive(false);
        inkSlider.maxValue = CreationManager.instance.NegociationTime;
        inkSlider.value = inkSlider.maxValue;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetUpCharacterInfo();
            //QuitPanel.SetActive(!QuitPanel.activeSelf);
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

    public void OpenCharacterPanel(bool Open)
    {
        if (Open)
        {
            CharacterPanel.SetActive(true);
        }
        else
        {
            CharacterPanel.SetActive(false);
        }
    }

    public void InitialiseCharactersPanel()
    {
        for (int i = 0; i < CastingManager.instance.AllCharacters.Length; i++)
        {
            GameObject newReader = Instantiate(CharacterReaderPrefab, CharacterPanel.transform);
            RectTransform newT = newReader.GetComponent<RectTransform>();
            Vector3 newPos = new Vector3(newT.anchoredPosition.x + CharacterShifter.x * Xshifter, newT.anchoredPosition.y + CharacterShifter.y * Yshifter, 0);

            newT.anchoredPosition = newPos;
            if (Xshifter < 2)
            {
                Xshifter++;
                
            }
            else
            {
                Xshifter = 0;
                Yshifter++;
            }
            newReader.GetComponent<CharacterReader>().assignedCharacter = CastingManager.instance.AllCharacters[i];
            newReader.GetComponent<CharacterReader>().InitialiseUi();
            newReader.GetComponent<CharacterReader>().ReadCharacter();

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

    #endregion

    public void UpdatePageIndicator()
    {
        pageIndicator.text = "Nb page : " + LevelManager.instance.AmountOfpageDone;
    }

    public void UpdateInkSlider(float value)
    {
        inkSlider.value += value;
    }

    public void SetInkSlider()
    {
        inkSlider.value = CreationManager.instance.NegociationTime;
    }

    #region Win / Loose Panel

    public void PlayerWinTheGame()
    {
        SetActiveMoveButton(false);
        WinPanel.SetActive(true);
    }

    public void PlayerLooseTheGame()
    {
        SetActiveMoveButton(false);
        LoosePanel.SetActive(true);
    }

    #endregion

    public void SetUpGamePanel()
    {
        GamePanel.SetActive(true);
        CreatePanel.SetActive(false);
        LevelManager.instance.SpawnObject(PlayerManager.instance.AmountOfCardToDraw);
    }

    public void SetUpCharacterInfo()
    {
        Character_SO toSet = PlayerManager.instance.CharacterData;

        //rt.offsetMin = rt.offsetMin = new Vector2(0,rt.offsetMin.y);
        
        playerName.text = toSet.CharacterName;

        playerRender.sprite = toSet.Render;

        foreach (var item in PlayerManager.instance.Inventory)
        {
            GameObject myButton = Instantiate(inventoryButton, inventoryPanel.transform);
            myButton.GetComponent<Image>().sprite = item.Sprite;
        }
    }

    public void SetUpCreationPanel()
    {
        GamePanel.SetActive(false);
        CreatePanel.SetActive(true);
    }
}
