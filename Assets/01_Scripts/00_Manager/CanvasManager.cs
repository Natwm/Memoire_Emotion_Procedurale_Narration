﻿using System.Collections;
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
        CharacterPanel.SetActive(false);
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

    public void SetSelectedPen()
    {
        for (int i = 0; i < PenPanel.transform.childCount; i++)
        {
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
        winIndicator.text = GameManager.instance.OrderCharacter.Count > 0 ? perso.CharacterName + " a survécu !\n C'est au tour de " + GameManager.instance.OrderCharacter[0].AssignedElement.CharacterName : "retouner à la base" ;
    }

    public void PlayerLooseTheGame(Character_SO perso)
    {
        SetActiveMoveButton(false);
        LoosePanel.SetActive(true);
        looseIndicator.text = perso.CharacterName + " est mort !\n Il ne vous reste plus que "+ CreationManager.instance.listOfCharacter.Count + " membres !";
    }

    #endregion

    public void SetUpGamePanel()
    {
        GamePanel.SetActive(true);
        CreatePanel.SetActive(false);
        GridManager.instance.ClearScene();
        EventGenerator.instance.GenerateGrid();
        grid.SetActive(true);
        LevelManager.instance.SpawnObject(PlayerManager.instance.Inventory);
        //SetUpCharacterInfo();

    }

    public void SetUpCharacterInfo()
    {
        print("eefefe");
        print(PlayerManager.instance.CharacterData);
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
