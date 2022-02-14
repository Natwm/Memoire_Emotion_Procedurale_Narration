using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CanvasManager : MonoBehaviour
{
    public enum m_NegociationActions
    {
        NONE,
        CLAIM,
        WANT,
        REJECT,
        EXCLUDE,
        DRAW
    }

    public static CanvasManager instance;

    [Header("Panel")]
    [SerializeField] private GameObject QuitPanel;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject CreatePanel;
    [SerializeField] private GameObject LevelInventoryPanel;
    [SerializeField] private GameObject GameOverPanel;

    [Space]
    [Header("Negociation")]
    [SerializeField] private GameObject m_InventoryPanel;
    [SerializeField] private GameObject characterListHolder;
    [SerializeField] private GameObject objectInventoryListHolder;

    [Space]
    [Header("RoomPanel")]
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Image roomPicture;

    [Header("Intro")]
    [SerializeField] private GameObject intro;
    [SerializeField] private TMP_Text introText;

    [Space]
    [Header("Outro")]
    [SerializeField] private GameObject outro;
    [SerializeField] private TMP_Text outroText;
    [SerializeField] private GameObject baseOutroButton;
    [SerializeField] private GameObject layoutGroupButton;

    [Space]
    public GameObject SelectedCharacterPanel;
    public GameObject WaitingCharacterPanel;

    [Space]
    [Header("ButtonList")]
    [SerializeField] private GameObject PenPanel;
    [SerializeField] private GameObject drawButton;

    [Space]
    [Header("Information Panel")]
    [SerializeField] private Image objectImage;
    [SerializeField] private TMP_Text objectTitle;
    [SerializeField] private TMP_Text objectDescription;

    [Space]
    [Header("Character Information")]
    [SerializeField] private GameObject characterInventoryPanel;
    [SerializeField] private GameObject inventoryButton;

    [Space]
    [Header("Slider")]
    [SerializeField] private TMP_Text negociationText;
    [SerializeField] private TMP_Text negociationModificationText;
    [Space]
    [SerializeField] private int currentNegociationTime;
    [SerializeField] private int negociationTime;
    [SerializeField] private int initNegociationTime;


    [Space]
    [Header("Text")]
    [SerializeField] private TMP_Text levelInfo;

    [Space]
    [Header("Button")]
    [SerializeField] private Button moveButton;
    [SerializeField] private Button distribuerButton;

    [Space]
    [Header("Prefabs")]
    [SerializeField] private GameObject levelInventoryButtonPrefabs;
    [SerializeField] private GameObject characterButton;
    [SerializeField] private GameObject ObjectNegociationButton;

    [Space]
    [SerializeField] private m_NegociationActions m_Action;
    Vector2 CharacterShifter = new Vector2(450, -450);
    int XshifterIndex = 0;
    int YshiterIndex = 0;
    int Xshifter = 0;
    int Yshifter = 0;

    [SerializeField] private GameObject grid;
    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : CanvasManager");
        else
            instance = this;
    }
    private void Start()
    {
    /*    SetActiveMoveButton(false);
        QuitPanel.SetActive(false);
        //SelectedCharacterPanel.SetActive(false);
        //WaitingCharacterPanel.SetActive(false);
        negociationTime = currentNegociationTime = initNegociationTime;
        SetUpLevelIndicator();
        SetInkSlider();*/
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
            MoveButton.gameObject.GetComponent<Image>().color = Color.green;
        else
            MoveButton.gameObject.GetComponent<Image>().color = Color.red;

        MoveButton.interactable = activeObject;
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

    public GameObject NewItemInLevelInventory(UsableObject_SO item)
    {
        GameObject newItemInInventory = Instantiate(levelInventoryButtonPrefabs, LevelInventoryPanel1.transform);
        newItemInInventory.GetComponent<Image>().sprite = item.Sprite;
        return newItemInInventory;
    }

    /*public void ClearLevelInventory()
    {
        for (int i = 0; i < LevelInventoryPanel1.transform.childCount; i++)
        {
            LevelInventoryPanel1.transform.GetChild(i).gameObject.SetActive(false);
            LevelInventoryPanel1.transform.GetChild(i).transform.parent = NegociationManager.instance.pulledObject.transform;
        }
    }*/

    public void RemoveObjInLevelInventory(int index)
    {
        print(index);
        if (LevelInventoryPanel1.transform.GetChild(index) != null)
            Destroy(LevelInventoryPanel1.transform.GetChild(index).gameObject);
    }

    public void RemoveObjInPlayerInventory(int index)
    {
        print(index);
        if(SelectedCharacterPanel.GetComponent<SelectedCharacter_GAMEUI>().InventoryPanel.transform.childCount > index )
            if(SelectedCharacterPanel.GetComponent<SelectedCharacter_GAMEUI>().InventoryPanel.transform.GetChild(index) != null)
                Destroy(SelectedCharacterPanel.GetComponent<SelectedCharacter_GAMEUI>().InventoryPanel.transform.GetChild(index).gameObject);
    }

    #endregion


    public void SetInkSlider()
    {
        //InkSlider.value = CreationManager.instance.NegociationTime;
        currentNegociationTime = negociationTime;
        NegociationText.text = NegociationManager.instance.NegociationTime.ToString();
    }

    
    public void DisablePenObj()
    {
        foreach (var item in FindObjectsOfType<PenObject>())
        {
            item.GetComponent<Button>().interactable = false;
            item.DisableInteraction();
        }
    }

    public void EnablePenObj()
    {
        foreach (var item in FindObjectsOfType<PenObject>())
        {
            item.GetComponent<Button>().interactable = true;
            item.EnableInteraction();
        }
    }

    public void SetUpLevelIndicator()
    {
        levelInfo.text = InventoryManager.instance.PageInventory.Count +" / " + GameManager.instance.AmountOfObjToSendCamp;
    }
    public void SetUpGamePanel()
    {
        GamePanel1.SetActive(true);
        CreatePanel1.SetActive(false);
        SelectedCharacterPanel.SetActive(true);
        WaitingCharacterPanel.SetActive(true);

       // GridManager.instance.ClearScene();
        //EventGenerator.instance.GenerateGrid();

        grid.SetActive(true);
        //LevelManager.instance.SpawnObject(PlayerManager.instance.Inventory);
        //SetUpCharacterInfo();

    }

    public void SetUpCharacterInfo()
    {
        SelectedCharacterPanel.GetComponent<SelectedCharacter_GAMEUI>().SetUpUI();
        if (GameManager.instance.OrderCharacter.Count > 0)
        {
            for (int i = 0; i < GameManager.instance.OrderCharacter.Count; i++)
            {
                WaitingCharacterPanel.transform.GetChild(i).GetComponent<WaitingCharacterPanel>().SetUpUI(GameManager.instance.OrderCharacter[i]);
                WaitingCharacterPanel.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        for (int i = GameManager.instance.OrderCharacter.Count; i < WaitingCharacterPanel.transform.childCount; i++)
        {
            WaitingCharacterPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void SetUpWaitingCharacterInfo()
    {
        SelectedCharacterPanel.GetComponent<SelectedCharacter_GAMEUI>().SetUpUI();

        for (int i = 0; i < GameManager.instance.OrderCharacter.Count; i++)
        {
            WaitingCharacterPanel.transform.GetChild(i).GetComponent<WaitingCharacterPanel>().SetUpUI(GameManager.instance.OrderCharacter[i]);
        }

        for (int i = GameManager.instance.OrderCharacter.Count; i < WaitingCharacterPanel.transform.childCount; i++)
        {
            WaitingCharacterPanel.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void SetUpCreationPanel()
    {
        /*foreach (var item in LevelManager.instance.PageInventory)
        {
            CreationManager.instance.GlobalInventory.Add(item.Data);
        }*/
        GamePanel.SetActive(false);
        PutAllObjectInInventory();
        NegociationManager.instance.CreateObjectListFromUsableObject();
        SetUpAllCharacter();
        CreatePanel1.SetActive(true);
        distribuerButton.gameObject.SetActive(true);
        EnablePenObj();

    }

    public void UpdateSelectedCharacterPanel()
    {
        SelectedCharacter_GAMEUI playerUI = SelectedCharacterPanel.GetComponent<SelectedCharacter_GAMEUI>();

        playerUI.LifeText.text = "<sprite=0> " + GameManager.instance.CurrentCharacter.Life.ToString();
        playerUI.MentalLifeText.text = "<sprite=2> " + GameManager.instance.CurrentCharacter.MentalHealth.ToString();
    }

    public void ResetGamePanelUI()
    {
        LevelInventoryPanel.SetActive(true);
        moveButton.interactable = true;
        drawButton.SetActive(true);
    }

    private void PutAllObjectInInventory()
    {
        foreach (var item in GameManager.instance.Crew)
        {
            int index = -1;
            if (item.InventoryObj.Count > 0)
            {
                foreach (var obj in item.InventoryObj)
                {
                    index++;
                    Destroy(item.CharacterContener.InventoryPanel.transform.GetChild(index).gameObject);
                }
            }
            item.InventoryObj.Clear();
        }
    }
    public void SetUpAllCharacter()
    {
        foreach (var item in NegociationManager.instance.ListOfCharacter)
        {
            item.SetUpCharacterUI();
        }
    }

    #region Room Manager UI

    public void SetIntro()
    {
        Intro.SetActive(true);
        Outro.SetActive(false);
    }

    public void SetOutro()
    {
        Debug.Log("Outro");
        Outro.SetActive(true);
        Intro.SetActive(false);

        GridManager.instance.ClearAllList();

        LevelManager.instance.ClearVignette();
    }

    public void FadeIn()
    {
        RoomPanel.GetComponent<Animator>().SetTrigger("Reveal");
        Intro.SetActive(true);
        Outro.SetActive(true);
        TitleText.gameObject.SetActive(true);
        IntroText.gameObject.SetActive(true);
        //SoundManager.instance.PlaySound_Woosh();
    }

    public void FadeOut()
    {
        GamePanel.SetActive(true);

        RoomGenerator.instance.GenerateRoom(RoomGenerator.instance.CurrentRoom);
        RoomPanel.GetComponent<Animator>().SetTrigger("FadeOut");
        TitleText.gameObject.SetActive(false);
        Intro.SetActive(false);
        Outro.SetActive(false);
        IntroText.gameObject.SetActive(false);
        
        SetUpCharacterInfo();
        //SoundManager.instance.PlaySound_Woosh();
    }

    public void InitialiseRoomToUi(Room_SO Room)
    {
        TitleText.text = Room.RoomName;
        RoomPicture.sprite = Room.RoomPicture;
        IntroText.text = Room.Room_IntroText;
        OutroText.text = Room.Room_OutroText;
        if (layoutGroupButton.transform.transform.childCount > 0)
        {
            foreach (Transform child in layoutGroupButton.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < Room.PossibleExits.Length; i++)
        {
            CreateExitButton(Room.PossibleExits[i]);
        }

    }

    public void CreateExitButton(RoomExit Exit)
    {
        GameObject ExitButton = GameObject.Instantiate(BaseOutroButton);
        if (Exit.Exit_Text != "")
        {
            ExitButton.GetComponent<TMP_Text>().text = Exit.Exit_Text;
        }
        else
        {
            ExitButton.GetComponent<TMP_Text>().text = "Aller à " + Exit.Exit_To_Room.name;
        }

        ExitButton.GetComponent<RoomExitButton>().RoomToExitTo = Exit;
        ExitButton.transform.parent = layoutGroupButton.transform;
    }

    public void TransitionAnim()
    {
        RoomPanel.GetComponent<Animator>().SetTrigger("RoomTransition");
        //SoundManager.instance.PlaySound_Woosh();
    }

    #endregion

    public void CreateCharacterButton(Character tempCharacter, string musique = "", string hurt = "")
    {
        GameObject tempButton = Instantiate(characterButton, characterListHolder.transform);
        Character_Button buttonScript = tempButton.GetComponent<Character_Button>();

        /*buttonScript.CharacterSelectedSound = musique;
        buttonScript.CharacterHurtSound = hurt;

        buttonScript.SetUpFmod();*/

        tempCharacter.CharacterContener = buttonScript;
        buttonScript.CharacterData = tempCharacter;
        buttonScript.CharacterImage.sprite = tempCharacter.AssignedElement.Render;

        buttonScript.SetUpCharacterUI();

        tempButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            NegociationManager.instance.SelectCharacter(tempButton.GetComponent<Character_Button>());
            /*if (muteFirstSound)
                buttonScript.PlaySelectedMusique();*/
        });

        NegociationManager.instance.ListOfCharacter.Add(tempButton.GetComponent<Character_Button>());
    }

    public void CreateObjectButton(UsableObject tempObject)
    {
        GameObject tempButton = Instantiate(ObjectNegociationButton, objectInventoryListHolder.transform);
        tempObject.ButtonDisplay = tempButton;
        tempButton.GetComponent<Object_Button>().Data = tempObject;

        Object_Button eventButton = tempButton.GetComponent<Object_Button>();

        tempButton.GetComponent<Image>().sprite = tempObject.Data.Sprite;
        //tempButton.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = tempObject.ObjectName;

        tempButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            eventButton.AffectByPlayer(tempButton.GetComponent<Button>(), NegociationManager.instance.selectedPlayer);
            UpdateDescriptionPanel(tempObject.Data, tempObject.MyCurse);
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

    private void UpdateDescriptionPanel(UsableObject_SO data, CurseBehaviours isCurse)
    {
        string curseText = "";
        if (isCurse != null)
        {
            curseText = isCurse.GetCurseName();
        }
        CanvasManager.instance.ObjectDescription.text = data.Description + curseText;
        CanvasManager.instance.ObjectTitle.text = data.ObjectName != " " || data.ObjectName != string.Empty ? data.ObjectName : data.name;
        
        CanvasManager.instance.ObjectImage.sprite = data.Sprite;
    }

    private void UpdateSliderValueOnEnter(UsableObject button)
    {
        switch (Action)
        {
            case m_NegociationActions.NONE:
                break;
            case m_NegociationActions.CLAIM:
                CanvasManager.instance.NegociationText.text = NegociationTime.ToString();
                CanvasManager.instance.NegociationModificationText.text = "-" + NegociationManager.instance.PrendreValue.ToString();
                break;
            case m_NegociationActions.WANT:
                CanvasManager.instance.NegociationText.text = NegociationTime.ToString();
                CanvasManager.instance.NegociationModificationText.text = "-" + NegociationManager.instance.ReclamerValue.ToString();
                break;
            case m_NegociationActions.REJECT:
                CanvasManager.instance.NegociationText.text = NegociationTime.ToString();
                CanvasManager.instance.NegociationModificationText.text = "-" + NegociationManager.instance.DeclinerValue.ToString();
                break;
            case m_NegociationActions.EXCLUDE:
                CanvasManager.instance.NegociationText.text = NegociationTime.ToString();
                CanvasManager.instance.NegociationModificationText.text = "-" + NegociationManager.instance.RefuserValue.ToString();
                break;
            case m_NegociationActions.DRAW:
                break;
            default:
                break;
        }
    }

    private void UpdateSliderValueOnExit(UsableObject button)
    {
        switch (Action)
        {
            case m_NegociationActions.NONE:
                break;
            case m_NegociationActions.CLAIM:
                CanvasManager.instance.NegociationText.text = NegociationTime.ToString();
                CanvasManager.instance.NegociationModificationText.text = "";
                break;
            case m_NegociationActions.WANT:
                CanvasManager.instance.NegociationText.text = NegociationTime.ToString();
                CanvasManager.instance.NegociationModificationText.text = "";
                break;
            case m_NegociationActions.REJECT:
                CanvasManager.instance.NegociationText.text = NegociationTime.ToString();
                CanvasManager.instance.NegociationModificationText.text = "";
                break;
            case m_NegociationActions.EXCLUDE:
                CanvasManager.instance.NegociationText.text = NegociationTime.ToString();
                CanvasManager.instance.NegociationModificationText.text = "";
                break;
            case m_NegociationActions.DRAW:
                break;
            default:
                break;
        }
        CanvasManager.instance.SetInkSlider();
    }

    public void ChangeAction(string usedPen)
    {
        CanvasManager.instance.SetSelectedPen();
        switch (usedPen)
        {
            case "claim":
                if (Action != m_NegociationActions.CLAIM)
                    Action = m_NegociationActions.CLAIM;
                else
                    Action = m_NegociationActions.NONE;
                break;

            case "want":
                if (Action != m_NegociationActions.WANT)
                    Action = m_NegociationActions.WANT;
                else
                    Action = m_NegociationActions.NONE;
                break;

            case "reject":
                if (Action != m_NegociationActions.REJECT)
                    Action = m_NegociationActions.REJECT;
                else
                    Action = m_NegociationActions.NONE;
                break;

            case "exclude":
                if (Action != m_NegociationActions.EXCLUDE)
                    Action = m_NegociationActions.EXCLUDE;
                else
                    Action = m_NegociationActions.NONE;
                break;

            case "none":
                Action = m_NegociationActions.NONE;
                break;

            default:
                Action = m_NegociationActions.NONE;
                break;
        }
    }


    public Image ObjectImage { get => objectImage; set => objectImage = value; }
    public TMP_Text ObjectTitle { get => objectTitle; set => objectTitle = value; }
    public TMP_Text ObjectDescription { get => objectDescription; set => objectDescription = value; }
    public GameObject CharacterInventoryPanel { get => characterInventoryPanel; set => characterInventoryPanel = value; }
    public GameObject GamePanel1 { get => GamePanel; set => GamePanel = value; }
    public GameObject CreatePanel1 { get => CreatePanel; set => CreatePanel = value; }
    public GameObject LevelInventoryPanel1 { get => LevelInventoryPanel; set => LevelInventoryPanel = value; }
    public int NegociationTime { get => negociationTime; set => negociationTime = value; }
    public int CurrentNegociationTime { get => currentNegociationTime; set => currentNegociationTime = value; }
    public TMP_Text NegociationText { get => negociationText; set => negociationText = value; }
    public TMP_Text NegociationModificationText { get => negociationModificationText; set => negociationModificationText = value; }
    public Button MoveButton { get => moveButton; set => moveButton = value; }
    public GameObject Intro { get => intro; set => intro = value; }
    public TMP_Text IntroText { get => introText; set => introText = value; }
    public Image RoomPicture { get => roomPicture; set => roomPicture = value; }
    public TMP_Text TitleText { get => titleText; set => titleText = value; }
    public GameObject RoomPanel { get => roomPanel; set => roomPanel = value; }
    public GameObject Outro { get => outro; set => outro = value; }
    public TMP_Text OutroText { get => outroText; set => outroText = value; }
    public GameObject BaseOutroButton { get => baseOutroButton; set => baseOutroButton = value; }
    public GameObject LayoutGroupButton { get => layoutGroupButton; set => layoutGroupButton = value; }
    public m_NegociationActions Action { get => m_Action; set => m_Action = value; }
    public GameObject InventoryPanel { get => m_InventoryPanel; set => m_InventoryPanel = value; }
}
