using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character_Button : MonoBehaviour,IPointerDownHandler
{
    #region Param
    private bool m_IsSelected = false;
    [SerializeField] private Character_SO assignedElement;

    [Header("Player State")]
    [SerializeField] private int m_Life;
    [Min(1)]
    [SerializeField] private int m_MaxLife = 1;

    [Space]

    [SerializeField] private int m_Endurance;
    [Min(1)]
    [SerializeField] private int m_MaxEndurance = 1;

    [Space]

    [SerializeField] private int m_MentalHealth;
    [Min(1)]
    [SerializeField] private int m_MaxMentalHealth = 1;

    [Space]

    [SerializeField] private int m_InventorySize;
    [Min(1)]
    [SerializeField] private int m_MaxInventorySize = 1;

    [Space]

    [SerializeField] private List<Object_SO> m_Inventory;
    [SerializeField] private List<UsableObject> m_InventoryObj = new List<UsableObject>();

    [Header("Text")]
    [Header("UI")]
    [SerializeField] private TMPro.TMP_Text m_NameText;
    [SerializeField] private TMPro.TMP_Text m_LifeText;
    [SerializeField] private TMPro.TMP_Text m_enduranceText;

    [Header("Image")]
    [SerializeField] private Image m_CharacterImage;

    [Header("Inventory Panel")]
    [SerializeField] private GameObject m_InventoryPanel;

    [Header("Prefabs")]
    [SerializeField] private GameObject m_ToolButtonPrefabs;

    [SerializeField] private Image m_cadre;

    [Space]
    [Header("Sound Fmod Action")]
    FMOD.Studio.EventInstance characterSelectedEffect;
    [FMODUnity.EventRef] [SerializeField] private string characterSelectedSound;

    FMOD.Studio.EventInstance characterHurtEffect;
    [FMODUnity.EventRef] [SerializeField] private string characterHurtSound;


    #endregion



    // Start is called before the first frame update
    void Start()
    {
        /*m_InventorySize = MaxInventorySize;
        m_Life = m_MaxLife;
        m_Endurance = m_MaxEndurance;*/
        SetUpFmod();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUpCharacter()
    {
        m_MaxLife = assignedElement.MaxHealth;
        m_Life = assignedElement.Health;

        m_MaxEndurance = assignedElement.MaxEndurance;
        m_Endurance = assignedElement.Endurance;

        MaxMentalHealth = assignedElement.MaxMentalHealth;
        MentalHealth = assignedElement.MentalHealth;

        m_MaxInventorySize = assignedElement.MaxInventorySize;
        InventorySize = assignedElement.InventorySize;

        Inventory = new List<Object_SO>();
        foreach (var item in assignedElement.Inventory)
        {
            Inventory.Add(item);
        }
    }
    public void SetUpCharacter(Character_SO data)
    {
        m_MaxLife = data.MaxHealth;
        m_Life = data.Health;

        m_LifeText.text = m_Life + " / " + m_MaxLife;

        m_MaxEndurance = data.MaxEndurance;
        m_Endurance = data.Endurance;

        MaxMentalHealth = data.MaxMentalHealth;
        MentalHealth = data.MentalHealth;

        m_enduranceText.text = MentalHealth + " / " + MaxMentalHealth;

        m_MaxInventorySize = data.MaxInventorySize;
        InventorySize = data.InventorySize;

        Inventory = new List<Object_SO>();
        foreach (var item in data.Inventory)
        {
            Inventory.Add(item);
            Instantiate(m_ToolButtonPrefabs, m_InventoryPanel.transform);
        }
        SetUpCharacterUI();
    }

    public void SetUpCharacterUI()
    {
        SetUpInventoryUI();
        m_NameText.text = assignedElement.CharacterName == " " || assignedElement.CharacterName == string.Empty ? assignedElement.name : assignedElement.CharacterName;
        LifeText.text = m_Life + "/ " + m_MaxLife;
        EnduranceText.text = MentalHealth + " / " + MaxMentalHealth;
    }

    public void SetUpInventoryUI()
    {
        List<GameObject> toRemove = new List<GameObject>();
        for (int i = 0; i < m_InventoryPanel.transform.childCount; i++)
        {
            toRemove.Add(m_InventoryPanel.transform.GetChild(i).gameObject);
        }

        foreach (var item in toRemove)
        {
            Destroy(item);
        }

        foreach (var item in m_Inventory)
        {
            GameObject tempButton = Instantiate(m_ToolButtonPrefabs, m_InventoryPanel.transform);
            tempButton.AddComponent<UsableObject>();
            tempButton.GetComponent<UsableObject>().Data = item;

            UsableObject eventButton = tempButton.GetComponent<UsableObject>();
            tempButton.GetComponent<Image>().sprite = item.Sprite;
            //tempButton.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = item.ObjectName;
        }
    }

    public void SetUpFmod()
    {
        CharacterSelectedEffect = FMODUnity.RuntimeManager.CreateInstance(CharacterSelectedSound);
        CharacterHurtEffect = FMODUnity.RuntimeManager.CreateInstance(CharacterHurtSound);
    }

   /* public void SelectPlayer()
    {
        foreach (var item in FindObjectsOfType<Character_Button>())
        {
            item.GetComponent<Button>().image.color = Color.white;
            item.IsSelected = false;
        }
        IsSelected = true;
        GetComponent<Button>().image.color = Color.red;
    }*/

    public void SelectPlayer()
    {
        m_cadre.color = Color.blue;
    }
    public void UnSelectPlayer()
    {
        m_cadre.color = Color.white;
    }

    public void PlaySelectedMusique()
    {
        CharacterSelectedEffect.start();
    }
    public void PlayDamageMusique()
    {
        CharacterHurtEffect.start();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        foreach (var item in FindObjectsOfType<Character_Button>())
        {
            item.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }

        transform.GetChild(0).GetComponent<Image>().color = assignedElement.Color;
    }


    #region Getter && Setter

    public bool IsSelected { get => m_IsSelected; set => m_IsSelected = value; }
    public Character_SO AssignedElement { get => assignedElement; set => assignedElement = value; }
    public int InventorySize { get => m_InventorySize; set => m_InventorySize = value; }
    public List<Object_SO> Inventory { get => m_Inventory; set => m_Inventory = value; }
    public Image CharacterImage { get => m_CharacterImage; set => m_CharacterImage = value; }
    public TMP_Text EnduranceText { get => m_enduranceText; set => m_enduranceText = value; }
    public TMP_Text LifeText { get => m_LifeText; set => m_LifeText = value; }
    public TMP_Text NameText { get => m_NameText; set => m_NameText = value; }
    public int Life { get => m_Life; set => m_Life = value; }
    public int MaxLife { get => m_MaxLife; set => m_MaxLife = value; }
    public int Endurance { get => m_Endurance; set => m_Endurance = value; }
    public int MaxEndurance { get => m_MaxEndurance; set => m_MaxEndurance = value; }
    public int MaxInventorySize { get => m_MaxInventorySize; set => m_MaxInventorySize = value; }
    public int MentalHealth { get => m_MentalHealth; set => m_MentalHealth = value; }
    public int MaxMentalHealth { get => m_MaxMentalHealth; set => m_MaxMentalHealth = value; }
    public EventInstance CharacterSelectedEffect { get => characterSelectedEffect; set => characterSelectedEffect = value; }
    public EventInstance CharacterHurtEffect { get => characterHurtEffect; set => characterHurtEffect = value; }
    public string CharacterSelectedSound { get => characterSelectedSound; set => characterSelectedSound = value; }
    public string CharacterHurtSound { get => CharacterHurtSound1; set => CharacterHurtSound1 = value; }
    public string CharacterHurtSound1 { get => characterHurtSound; set => characterHurtSound = value; }
    public List<UsableObject> InventoryObj { get => m_InventoryObj; set => m_InventoryObj = value; }
    #endregion

}
