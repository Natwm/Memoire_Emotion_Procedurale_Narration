using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character_Button : MonoBehaviour
{
    [SerializeField] private Character characterData;

    /*[Space]
    [Header("Sound Fmod Action")]
    FMOD.Studio.EventInstance characterSelectedEffect;
    [FMODUnity.EventRef] [SerializeField] private string characterSelectedSound;

    FMOD.Studio.EventInstance characterHurtEffect;
    [FMODUnity.EventRef] [SerializeField] private string characterHurtSound;*/

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

    public GameObject slotPanel;

    public List<GameObject> listOfObjButton = new List<GameObject>();

    public Character CharacterData { get => characterData; set => characterData = value; }
    /*public string CharacterSelectedSound { get => characterSelectedSound; set => characterSelectedSound = value; }
    public string CharacterHurtSound { get => characterHurtSound; set => characterHurtSound = value; }
    public EventInstance CharacterHurtEffect { get => characterHurtEffect; set => characterHurtEffect = value; }
    public EventInstance CharacterSelectedEffect { get => characterSelectedEffect; set => characterSelectedEffect = value; }*/
    public Image CharacterImage { get => m_CharacterImage; set => m_CharacterImage = value; }
    public GameObject InventoryPanel { get => m_InventoryPanel; set => m_InventoryPanel = value; }
    public Image Cadre { get => m_cadre; set => m_cadre = value; }
    public TMP_Text NameText { get => m_NameText; set => m_NameText = value; }
    public TMP_Text LifeText { get => m_LifeText; set => m_LifeText = value; }
    public TMP_Text EnduranceText { get => m_enduranceText; set => m_enduranceText = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void SetUpFmod()
    {
        CharacterSelectedEffect = FMODUnity.RuntimeManager.CreateInstance(CharacterSelectedSound);
        CharacterHurtEffect = FMODUnity.RuntimeManager.CreateInstance(CharacterHurtSound);
    }*/

    public void SetUpCharacterUI()
    {
        m_NameText.text = CharacterData.AssignedElement.CharacterName == " " || CharacterData.AssignedElement.CharacterName == string.Empty ? CharacterData.AssignedElement.name : CharacterData.AssignedElement.CharacterName;
        LifeText.text = CharacterData.Life + "/ " + CharacterData.MaxLife;
        EnduranceText.text = CharacterData.MentalHealth + " / " + CharacterData.MaxMentalHealth;
        SetUpInventoryUI();
    }

    public void SetUpInventoryUI()
    {
        List<GameObject> toRemove = new List<GameObject>();
        if (InventoryPanel.transform.childCount > 0)
        {
            for (int i = 0; i < InventoryPanel.transform.childCount; i++)
            {
                toRemove.Add(InventoryPanel.transform.GetChild(i).gameObject);
            }

            foreach (var item in toRemove)
            {
                Destroy(item);
            }
        }
        int index = -1;
        foreach (var item in CharacterData.InventoryObj)
        {
            index++;
            GameObject tempButton = Instantiate(m_ToolButtonPrefabs, InventoryPanel.transform);
            tempButton.AddComponent<UsableObject>();
            tempButton.GetComponent<UsableObject>().Data = item.Data;

            UsableObject eventButton = tempButton.GetComponent<UsableObject>();
            tempButton.GetComponent<Image>().sprite = item.Data.Sprite;

            if (CharacterData.InventoryObj[index].IsCurse)
            {
                tempButton.GetComponent<Image>().color = GameManager.instance.curseColor;
            }
            listOfObjButton.Add(tempButton);
        }
    }

    public void SetUpColor()
    {
        foreach (var item in FindObjectsOfType<Character_Button>())
        {
            item.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }

        transform.GetChild(0).GetComponent<Image>().color = CharacterData.AssignedElement.Color;
    }

    public void SelectThisPlayer()
    {
        NegociationManager.instance.SelectCharacter(this);
    }
}
