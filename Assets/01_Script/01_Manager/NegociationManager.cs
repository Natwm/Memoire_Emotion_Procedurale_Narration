using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NegociationManager : MonoBehaviour
{
    public enum m_PenStatus
    {
        NONE,
        CLAIM,
        WANT,
        REJECT,
        EXCLUDE,
        DRAW
    }


    public static NegociationManager instance;

    [Space]
    [Header("Negociation Value")]
    [SerializeField] private int reclamerValue;
    [SerializeField] private int prendreValue;
    [SerializeField] private int declinerValue;
    [SerializeField] private int refuserValue;

    [Space]
    [Header("Time")]
    [SerializeField] private int negociationTime = 100;
    [SerializeField] private int currentNegociationTime = 100;
    [SerializeField] private int maxNegociationTime = 100;

    [Space]
    [Header("Character Selected")]
    public Character_Button selectedPlayer;

    [Space]
    [Header("Object List")]
    public List<Character_Button> listOfCharacter = new List<Character_Button>();

    [Space]
    [Header("UI")]
    public GameObject characterListHolder;
    public GameObject objectListHolder;

    [Space]
    [Header("Prefabs")]
    public GameObject baseButton;
    public GameObject ObjectButton;

    [Space]
    [Header("Object List")]
    /*public List<Character> listOfCharacter = new List<Character>();
    public List<UsableObject> listOfObject = new List<UsableObject>();
    */
    [SerializeField] private m_PenStatus m_Pen;

    public int ReclamerValue { get => reclamerValue; set => reclamerValue = value; }
    public int PrendreValue { get => prendreValue; set => prendreValue = value; }
    public int DeclinerValue { get => declinerValue; set => declinerValue = value; }
    public int RefuserValue { get => refuserValue; set => refuserValue = value; }
    public m_PenStatus Pen { get => m_Pen; set => m_Pen = value; }
    public int CurrentNegociationTime { get => currentNegociationTime; set => currentNegociationTime = value; }
    public int NegociationTime { get => negociationTime; set => negociationTime = value; }

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : NegociationManager");
        else
            instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpNegociation()
    {
        CreateObjectInventory();

        listOfCharacter[0].GetComponent<Button>().onClick.Invoke();
        listOfCharacter[0].SetUpColor();

        foreach (var item in FindObjectsOfType<PenObject>())
        {
            item.GetComponent<Button>().interactable = true;
            item.InitButton();
        }

        //RoomGenerator.instance.InitialiseGame();

    }

    public void CreateCharacterButton(Character tempCharacter, string musique = "", string hurt = "")
    {
        GameObject tempButton = Instantiate(baseButton, characterListHolder.transform);
        Character_Button buttonScript = tempButton.GetComponent<Character_Button>();

        /*buttonScript.CharacterSelectedSound = musique;
        buttonScript.CharacterHurtSound = hurt;

        buttonScript.SetUpFmod();*/

        buttonScript.CharacterData = tempCharacter;
        buttonScript.CharacterImage.sprite = tempCharacter.AssignedElement.Render;

        buttonScript.SetUpCharacterUI();

        tempButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            SelectCharacter(tempButton.GetComponent<Character_Button>());
            /*if (muteFirstSound)
                buttonScript.PlaySelectedMusique();*/
        });

        listOfCharacter.Add(tempButton.GetComponent<Character_Button>());

    }

    public void CreateObjectInventory()
    {
        List<UsableObject_SO> tempList = new List<UsableObject_SO>();

        for (int i = 0; i < InventoryManager.instance.InitialInventory.Count; i++)
        {
            UsableObject_SO tempObject = InventoryManager.instance.InitialInventory[i];

            tempList.Add(tempObject);

            //CreateObjectButton(tempObject);
            //GlobalInventory.Remove(tempObject);
            //CharacterList.Remove(tempCharacter);
        }
    }

    private void CreateObjectButton(UsableObject tempObject)
    {
        GameObject tempButton = Instantiate(ObjectButton, objectListHolder.transform);
        GameObject tempdata = Instantiate(ObjectButton, objectListHolder.transform);
        tempButton.GetComponent<Object_Button>().Data = tempObject;
        tempdata.GetComponent<UsableObject>().IsCurse = false;
        tempdata.GetComponent<UsableObject>().MyCurse = null;

        Object_Button eventButton = tempButton.GetComponent<Object_Button>();

        tempButton.GetComponent<Image>().sprite = tempObject.Data.Sprite;
        //tempButton.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = tempObject.ObjectName;

        tempButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            eventButton.AffectByPlayer(tempButton.GetComponent<Button>(), selectedPlayer);
            UpdateDescriptionPanel(tempObject.Data, tempButton.GetComponent<UsableObject>().MyCurse);
            //tempObject.PlaySound();
        }
        );

        EventTrigger buttonEvent;

        if (tempButton.TryGetComponent<EventTrigger>(out buttonEvent))
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            EventTrigger.Entry exit = new EventTrigger.Entry();

            entry.eventID = EventTriggerType.PointerEnter;
            exit.eventID = EventTriggerType.PointerExit;

            /*entry.callback.AddListener((data) => { UpdateSliderValueOnEnter(eventButton); });
            entry.callback.AddListener((data) => { UpdateDescriptionPanel(tempObject, tempButton.GetComponent<UsableObject>().MyCurse); });
            exit.callback.AddListener((data) => { UpdateSliderValueOnExit(eventButton); });*/
            //exit.callback.AddListener((data) => { ResetDescriptionPanel(); });

            buttonEvent.triggers.Add(entry);
            buttonEvent.triggers.Add(exit);
        }
        InventoryManager.instance.GlobalInventoryObj.Add(eventButton.Data);
        //listOfObject.Add(tempButton.GetComponent<UsableObject>());
    }

    void RepartitionObjec()
    {

    }

    public bool ReduceNegociationTime(int reduceValue)
    {
        if (NegociationTime - reduceValue >= 0)
        {
            NegociationTime -= reduceValue;
            //CanvasManager.instance.NegociationModificationText.text = "-" + reduceValue;
            return true;
        }
        return false;
    }

    public void IncreaseNegociationTime(int reduceValue)
    {
        NegociationTime += reduceValue;
    }

    public void ResetNegociationTime()
    {
        NegociationTime = maxNegociationTime;
    }

    void SelectCharacter(Character_Button player)
    {
        selectedPlayer = player;
    }

    void ChangePen()
    {

    }

    void CantApplyNegociation()
    {

    }

    void EndNegociation()
    {

    }

    private void UpdateDescriptionPanel(UsableObject_SO data, CurseBehaviours isCurse)
    {
        string curseText = "";
        /*if (isCurse != null)
        {
            curseText = GetCurseName(isCurse);
        }
        CanvasManager.instance.ObjectDescription.text = data.Description + curseText;
        CanvasManager.instance.ObjectTitle.text = data.ObjectName != " " || data.ObjectName != string.Empty ? data.ObjectName : data.name;
        
        CanvasManager.instance.ObjectImage.sprite = data.Sprite;*/
    }

    public void CreateObjectListFromUsableObject()
    {
        /*List<UsableObject> tempList = new List<UsableObject>();

        for (int i = 0; i < pulledObject.transform.childCount; i++)
        {
            print(i);
            UsableObject tempObject = pulledObject.transform.GetChild(i).gameObject.GetComponent<UsableObject>();
            CreateObjectButtonFromUsableObject(tempObject);

            Destroy(tempObject.gameObject);
        }*/
    }

}
