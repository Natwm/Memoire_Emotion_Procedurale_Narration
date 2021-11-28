using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreationManager : MonoBehaviour
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

    public static CreationManager instance;

    //public List<Character> CharacterList;
    public List<Character_SO> PageCharacterList;
    [Space]
    [Header("Time")]
    [SerializeField] private int negociationTime = 100;

    [Space]
    [Header("Character Selected")]
    public Character_Button selectedPlayer;

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
    public List<Character_Button> listOfCharacter = new List<Character_Button>();
    public List<UsableObject> listOfObject = new List<UsableObject>();

    [Space]
    [Header("Global Character")]
    [SerializeField] private List<Character_SO> m_Crew;
    [SerializeField] private List<Character_SO> m_GlobalCrew;

    [Space]
    [Header("Global Inventory")]
    [SerializeField] private List<UsableObject> m_GlobalInventory;
    [SerializeField] private List<Object_SO> m_GlobalInventoryObj;

    [Space]
    [Header("Vignette Render")]
    [SerializeField] private List<Sprite> vignetteRender;
    //public Vector2 shape;

    [SerializeField] private m_PenStatus m_Pen;

    public GameObject pulledObject;

    [Space]
    [Header("Fmod")]
    [FMODUnity.EventRef] [SerializeField] private string Character1Sound;
    [FMODUnity.EventRef] [SerializeField] private string Character2Sound;
    [FMODUnity.EventRef] [SerializeField] private string Character3Sound;
    [FMODUnity.EventRef] [SerializeField] private string Character4Sound;

    [Space]
    [FMODUnity.EventRef] [SerializeField] private string CharacterHurt1Sound;
    [FMODUnity.EventRef] [SerializeField] private string CharacterHurt2Sound;
    [FMODUnity.EventRef] [SerializeField] private string CharacterHurt3Sound;
    [FMODUnity.EventRef] [SerializeField] private string CharacterHurt4Sound;

    #region Awake || Start || Update
    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : CreationManager");
        else
            instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        PageCharacterList = CreateCharacterList(2);
        CreateObjectList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //CreatePlayerInventory(PlayerManager.instance);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            CreateVignette();
        }
    }
    #endregion

    #region Create Button

    #region Create Character
    private void CreateCharacterButton(Character_SO tempCharacter, string musique, string hurt)
    {
        GameObject tempButton = Instantiate(baseButton, characterListHolder.transform);
        Character_Button buttonScript = tempButton.GetComponent<Character_Button>();

        buttonScript.CharacterSelectedSound = musique;
        buttonScript.CharacterHurtSound = hurt;

        buttonScript.SetUpFmod();

        buttonScript.AssignedElement = tempCharacter;
        buttonScript.CharacterImage.sprite = tempCharacter.Render;

        buttonScript.SetUpCharacter(tempCharacter);

        tempButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            SelectPlayer(tempButton.GetComponent<Character_Button>());
            buttonScript.PlaySelectedMusique();
        });

        listOfCharacter.Add(tempButton.GetComponent<Character_Button>());

    }

    public List<Character_SO> CreateCharacterList(int _charaAmount = 1)
    {
        List<Character_SO> tempList = new List<Character_SO>();
        foreach (var item in GlobalCrew)
        {
            tempList.Add(item);
        }


        for (int i = 0; i < _charaAmount; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            Character_SO tempCharacter = tempList[randomIndex];

            m_Crew.Add(tempCharacter);
            tempList.RemoveAt(randomIndex);

            string musique;
            string hurt;

            switch (i)
            {
                case 0:
                    musique = Character1Sound;
                    hurt = CharacterHurt1Sound;
                    break;
                case 1:
                    musique = Character2Sound;
                    hurt = CharacterHurt2Sound;
                    break;
                case 2:
                    musique = Character3Sound;
                    hurt = CharacterHurt3Sound;
                    break;
                case 3:
                    musique = Character4Sound;
                    hurt = CharacterHurt4Sound;
                    break;

                default:
                    musique = Character1Sound;
                    hurt = CharacterHurt1Sound;
                    break;
            }
            musique = Character1Sound;

            CreateCharacterButton(tempCharacter, musique, hurt);
        }
        return tempList == null ? null : tempList;
    }
    #endregion

    #region Create Object
    private void CreateObjectButtonFromUsableObject(UsableObject tempObject)
    {
        GameObject tempButton = Instantiate(ObjectButton, objectListHolder.transform);
        tempButton.AddComponent<UsableObject>();
        tempButton.GetComponent<UsableObject>().Data = tempObject.Data;
        tempButton.GetComponent<UsableObject>().IsCurse = tempObject.IsCurse;
        tempButton.GetComponent<UsableObject>().MyCurse = tempObject.MyCurse;

        UsableObject eventButton = tempButton.GetComponent<UsableObject>();

        tempButton.GetComponent<Image>().sprite = tempObject.Data.Sprite;
        if (tempButton.GetComponent<UsableObject>().IsCurse)
            tempButton.GetComponent<Image>().color = Color.red;
        //tempButton.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = tempObject.ObjectName;

        tempButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            eventButton.AffectByPlayer(tempButton.GetComponent<Button>(),selectedPlayer);
            UpdateDescriptionPanel(tempObject.Data);
        }
        );

        EventTrigger buttonEvent;

        if (tempButton.TryGetComponent<EventTrigger>(out buttonEvent))
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            EventTrigger.Entry exit = new EventTrigger.Entry();

            entry.eventID = EventTriggerType.PointerEnter;
            exit.eventID = EventTriggerType.PointerExit;

            entry.callback.AddListener((data) => { UpdateSliderValueOnEnter(eventButton); });
            entry.callback.AddListener((data) => { UpdateDescriptionPanel(tempObject.Data); });
            exit.callback.AddListener((data) => { UpdateSliderValueOnExit(eventButton); });
            exit.callback.AddListener((data) => { ResetDescriptionPanel(); });

            buttonEvent.triggers.Add(entry);
            buttonEvent.triggers.Add(exit);
        }

        listOfObject.Add(tempButton.GetComponent<UsableObject>());
    }

    private void CreateObjectButton(Object_SO tempObject)
    {
        GameObject tempButton = Instantiate(ObjectButton, objectListHolder.transform);
        tempButton.AddComponent<UsableObject>();
        tempButton.GetComponent<UsableObject>().Data = tempObject;
        tempButton.GetComponent<UsableObject>().IsCurse = false;
        tempButton.GetComponent<UsableObject>().MyCurse = null;

        UsableObject eventButton = tempButton.GetComponent<UsableObject>();

        tempButton.GetComponent<Image>().sprite = tempObject.Sprite;
        //tempButton.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = tempObject.ObjectName;

        tempButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            eventButton.AffectByPlayer(tempButton.GetComponent<Button>(), selectedPlayer);
            UpdateDescriptionPanel(tempObject);
        }
        );

        EventTrigger buttonEvent;

        if (tempButton.TryGetComponent<EventTrigger>(out buttonEvent))
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            EventTrigger.Entry exit = new EventTrigger.Entry();

            entry.eventID = EventTriggerType.PointerEnter;
            exit.eventID = EventTriggerType.PointerExit;

            entry.callback.AddListener((data) => { UpdateSliderValueOnEnter(eventButton); });
            entry.callback.AddListener((data) => { UpdateDescriptionPanel(tempObject); });
            exit.callback.AddListener((data) => { UpdateSliderValueOnExit(eventButton); });
            exit.callback.AddListener((data) => { ResetDescriptionPanel(); });

            buttonEvent.triggers.Add(entry);
            buttonEvent.triggers.Add(exit);
        }
        GlobalInventory.Add(eventButton);
        listOfObject.Add(tempButton.GetComponent<UsableObject>());
    }

    private void UpdateSliderValueOnEnter(UsableObject button)
    {
        switch (Pen)
        {
            case m_PenStatus.NONE:
                break;
            case m_PenStatus.CLAIM:
                CanvasManager.instance.UpdateInkSlider(-75);
                break;
            case m_PenStatus.WANT:
                CanvasManager.instance.UpdateInkSlider(-33);
                break;
            case m_PenStatus.REJECT:
                CanvasManager.instance.UpdateInkSlider(-33);
                break;
            case m_PenStatus.EXCLUDE:
                CanvasManager.instance.UpdateInkSlider(-75);
                break;
            case m_PenStatus.DRAW:
                break;
            default:
                break;
        }
    }

    private void UpdateSliderValueOnExit(UsableObject button)
    {
        switch (Pen)
        {
            case m_PenStatus.NONE:
                break;
            case m_PenStatus.CLAIM:
                CanvasManager.instance.UpdateInkSlider(75);
                break;
            case m_PenStatus.WANT:
                CanvasManager.instance.UpdateInkSlider(33);
                break;
            case m_PenStatus.REJECT:
                CanvasManager.instance.UpdateInkSlider(33);
                break;
            case m_PenStatus.EXCLUDE:
                CanvasManager.instance.UpdateInkSlider(75);
                break;
            case m_PenStatus.DRAW:
                break;
            default:
                break;
        }
        CanvasManager.instance.SetInkSlider();
    }

    private void UpdateDescriptionPanel(Object_SO data)
    {
        CanvasManager.instance.ObjectDescription.text = data.Description;
        CanvasManager.instance.ObjectTitle.text = data.ObjectName != " " || data.ObjectName != string.Empty ? data.ObjectName : data.name;

        CanvasManager.instance.ObjectImage.sprite = data.Sprite;
    }

    private void ResetDescriptionPanel()
    {
        CanvasManager.instance.ObjectDescription.text = "";
        CanvasManager.instance.ObjectTitle.text = "";

        CanvasManager.instance.ObjectImage.sprite = null;
    }

    public List<Object_SO> CreateObjectList()
    {
        List<Object_SO> tempList = new List<Object_SO>();

        for (int i = 0; i < GlobalInventoryObj.Count; i++)
        {
            Object_SO tempObject = GlobalInventoryObj[i];

            tempList.Add(tempObject);

            CreateObjectButton(tempObject);
            //GlobalInventory.Remove(tempObject);
            //CharacterList.Remove(tempCharacter);
        }
        return tempList == null ? null : tempList;
    }

    public List<UsableObject> CreateObjectListFromUsableObject()
    {
        List<UsableObject> tempList = new List<UsableObject>();

        for (int i = 0; i < pulledObject.transform.childCount; i++)
        {
            print(i);
            UsableObject tempObject = pulledObject.transform.GetChild(i).gameObject.GetComponent<UsableObject>();
            CreateObjectButtonFromUsableObject(tempObject);
            Destroy(tempObject.gameObject);
        }

        /*for (int i = 0; i < GlobalInventory.Count; i++)
        {
            UsableObject tempObject = GlobalInventory[i];

            tempList.Add(tempObject);

            CreateObjectButtonFromUsableObject(tempObject);
            //GlobalInventory.Remove(tempObject);
            //CharacterList.Remove(tempCharacter);
        }*/
        return tempList == null ? null : tempList;
    }
    #endregion

    #endregion
    public void PutAllObjectInInventory()
    {
        foreach (var item in listOfCharacter)
        {
            int index = -1;
            foreach (var obj in item.InventoryObj)
            {
                index++;
                obj.gameObject.transform.parent = pulledObject.transform;
                Destroy(item.InventoryPanel.transform.GetChild(index).gameObject);
            }
            item.Inventory.Clear();
            item.InventoryObj.Clear();
        }
    }

    public void RepartitionObject()
    {
        foreach (var item in listOfCharacter)
        {
            CreatePlayerInventory(item);
        }
        print("objectListHolder.transform.childCount" + objectListHolder.transform.childCount);

        foreach (var item in GlobalInventory)
        {
            item.transform.parent = pulledObject.transform;
            item.gameObject.SetActive(false);
        }
        GlobalInventory.Clear();
        GlobalInventoryObj.Clear();
    }
    public bool test()
    {
        List<UsableObject> pullOfObject = new List<UsableObject>();
        List<UsableObject> claimObject = new List<UsableObject>();
        foreach (var player in listOfCharacter)
        {
            pullOfObject = CreatePull();

            print("player.InventorySize - player.Inventory.Count " + (player.InventorySize - player.Inventory.Count));
            for (int i = 0; i < player.InventorySize - player.Inventory.Count; i++)
            {
                print(player.InventorySize);
                int index = Random.Range(0, pullOfObject.Count);

                if (!player.Inventory.Contains(pullOfObject[index].Data))
                {
                    player.Inventory.Add(pullOfObject[index].Data);
                    pullOfObject[index].gameObject.SetActive(false);
                }
                UsableObject obj = pullOfObject[index];
                pullOfObject.RemoveAll(item => item == obj);

                obj.gameObject.transform.parent = pulledObject.transform;

                if (pullOfObject.Count <= 0)
                {
                    foreach (var item in player.Inventory)
                    {
                        m_GlobalInventory.RemoveAll(objToRemove => objToRemove == item);
                    }
                    player.SetUpInventoryUI();
                    return true;
                }

            }
            foreach (var item in player.Inventory)
            {
                m_GlobalInventory.RemoveAll(obj => obj == item);
            }
            player.SetUpInventoryUI();
            
        }

        return true;

    }

    #region Create
    public void CreateVignette()
    {
        List<Character_Button> listOfCharacterWanted = new List<Character_Button>();
        List<Character_Button> listOfCharacterDontWanted = new List<Character_Button>();
        List<Character_Button> listOfCharacterFreeze = new List<Character_Button>();
        List<Character_Button> listOfCharacterNone = new List<Character_Button>();
        List<Character_Button> listOfSpawnable = new List<Character_Button>();


        for (int i = 0; i < characterListHolder.transform.childCount; i++)
        {
            Character_Button character = characterListHolder.transform.GetChild(i).GetComponent<Character_Button>();
            //print("<Color=green>"+character.assignedElement.characterName + "  " + character.Status+"</Color>");

            listOfCharacterNone.Add(character);
        }

        Character_SO[] tempCharacter_BehavioursDistribution = new Character_SO[listOfCharacterNone.Count];

        for (int i = 0; i < listOfCharacterNone.Count; i++)
        {
            tempCharacter_BehavioursDistribution[i] = listOfCharacterNone[i].AssignedElement;
        }

    }

    private List<UsableObject> CreatePull(UsableObject ObjetToTake, Character_Button player)
    {
        List<UsableObject> pullOfObject = new List<UsableObject>();
        switch (ObjetToTake.Status)
        {
            case UsableObject.ObjectStatus.NONE:
                pullOfObject.Add(ObjetToTake);
                pullOfObject.Add(ObjetToTake);
                break;
            case UsableObject.ObjectStatus.CLAIM:
                if (m_GlobalInventory.Contains(ObjetToTake))
                {
                    if(player == ObjetToTake.Stat.character)
                    {
                        player.Inventory.Add(ObjetToTake.Data);
                        player.InventoryObj.Add(ObjetToTake);
                        ObjetToTake.gameObject.SetActive(false);
                        m_GlobalInventory.Remove(ObjetToTake);
                    }
                }
                break;
            case UsableObject.ObjectStatus.WANT:
                pullOfObject.Add(ObjetToTake);
                pullOfObject.Add(ObjetToTake);
                pullOfObject.Add(ObjetToTake);
                break;
            case UsableObject.ObjectStatus.REJECT:
                pullOfObject.Add(ObjetToTake);
                break;
            case UsableObject.ObjectStatus.EXCLUDE:
                break;
            default:
                break;
        }
        return pullOfObject;
    }

    private List<UsableObject> CreatePull()
    {
        List<UsableObject> pullOfObject = new List<UsableObject>();
        foreach (var ObjetToTake in listOfObject)
        {
            switch (ObjetToTake.Status)
            {
                case UsableObject.ObjectStatus.NONE:
                    pullOfObject.Add(ObjetToTake);
                    pullOfObject.Add(ObjetToTake);
                    break;
                case UsableObject.ObjectStatus.CLAIM:
                    if (m_GlobalInventory.Contains(ObjetToTake))
                    {
                        ObjetToTake.Stat.character.Inventory.Add(ObjetToTake.Data);
                        ObjetToTake.gameObject.SetActive(false);
                        m_GlobalInventory.Remove(ObjetToTake);
                    }
                    break;
                case UsableObject.ObjectStatus.WANT:
                    pullOfObject.Add(ObjetToTake);
                    pullOfObject.Add(ObjetToTake);
                    pullOfObject.Add(ObjetToTake);
                    break;
                case UsableObject.ObjectStatus.REJECT:
                    pullOfObject.Add(ObjetToTake);
                    break;
                case UsableObject.ObjectStatus.EXCLUDE:
                    break;
                default:
                    break;
            }
        }
        return pullOfObject;
    }

    public bool CreatePlayerInventory(Character_Button player)
    {
        List<UsableObject> pullOfObject = new List<UsableObject>();
        List<UsableObject> claimObject = new List<UsableObject>();


        for (int i = 0; i < objectListHolder.transform.childCount; i++)
        {
            GameObject clickObject = objectListHolder.transform.GetChild(i).gameObject;

            UsableObject ObjetToTake = clickObject.GetComponent<UsableObject>();

            foreach (var item in CreatePull(ObjetToTake, player))
            {
                pullOfObject.Add(item);
            }
            
        }

        for (int i = 0; i < player.InventorySize - player.Inventory.Count; i++)
        {
            int index = Random.Range(0, pullOfObject.Count);

            if (!player.Inventory.Contains(pullOfObject[index].Data))
            {
                player.Inventory.Add(pullOfObject[index].Data);

                player.InventoryObj.Add(pullOfObject[index]);

                pullOfObject[index].gameObject.SetActive(false);
            }
            UsableObject obj = pullOfObject[index];
            pullOfObject.RemoveAll(item => item == obj);

            obj.gameObject.transform.parent = pulledObject.transform;

            if (pullOfObject.Count <= 0)
            {
                foreach (var item in player.Inventory)
                {
                    m_GlobalInventory.RemoveAll(objToRemove => objToRemove == item);
                }
                player.SetUpInventoryUI();
                return true;
            }

        }
        foreach (var item in player.Inventory)
        {
            m_GlobalInventory.RemoveAll(obj => obj == item);
        }
        player.SetUpInventoryUI();
        return true;
    }

    private void RemoveFromPullInventory(List<UsableObject> pull, UsableObject toRemove)
    {

    }
    #endregion

    public bool ReduceNegociationTime(int reduceValue)
    {
        if (negociationTime - reduceValue >= 0)
        {
            negociationTime -= reduceValue;
            return true;
        }
        return false;
    }

    public void IncreaseNegociationTime(int reduceValue)
    {
        if (negociationTime + reduceValue < CanvasManager.instance.InkSlider.maxValue)
        {
            negociationTime += reduceValue;
        }
        else
        {
            negociationTime = (int)CanvasManager.instance.InkSlider.maxValue;
        }
    }

    public void SelectPlayer(Character_Button player)
    {
        selectedPlayer = player;


        foreach (var item in FindObjectsOfType<PenObject>())
        {
            item.GetComponent<Button>().interactable = true;
            item.InitButton();
        }
    }

    public void LaunchGame()
    {
        PlayerManager.instance.CharacterData = GameManager.instance.OrderCharacter[0].AssignedElement;
        
        PlayerManager.instance.Inventory.Clear();
        PlayerManager.instance.InventoryObj.Clear();

        if (PlayerManager.instance.CharacterData != null)
        {
            foreach (var item in PlayerManager.instance.InventoryObj)
            {
                GlobalInventory.Add(item);
            }

            PlayerManager.instance.Inventory.Clear();

            PlayerManager.instance.SetUpCharacter(GameManager.instance.OrderCharacter[0]);
            CanvasManager.instance.SetUpGamePanel();
            GameManager.instance.OrderCharacter.RemoveAt(0);
            CanvasManager.instance.SetUpCharacterInfo();

        }

    }

    public void StartPull()
    {
        GameManager.instance.OrderCharacter = new List<Character_Button>();
        foreach (var item in listOfCharacter)
        {
            GameManager.instance.OrderCharacter.Add(item);
        }
        NextCharacter();
    }

    public void NextCharacter()
    {
        bool can = true;
        foreach (var item in listOfCharacter)
        {
            if (item.Inventory.Count == 0 && GlobalInventory.Count >= 0)
                can = false;
        }
        if (can) 
            LaunchGame();
        else
        {
            RepartitionObject();
            NextCharacter();
        }

    }

    #region To Delete
    public GameObject GetVignette(Vector2 caseShape)
    {
        string chemin = "Generation/Vignette/" + "case_" + caseShape.ToString();
        return Resources.Load<GameObject>(chemin);
    }

    public GameObject GetShape(Vector2 caseShape)
    {
        string chemin = "Generation/Vignette/" + "case_" + caseShape.ToString();
        return Resources.Load<GameObject>(chemin);
    }

    public GameObject GetVignetteShape(Vector2 caseShape)
    {
        string chemin = "Generation/Tile/" + "case_" + caseShape.x + "x" + caseShape.y;
        return Resources.Load<GameObject>(chemin);
    }

    public GameObject GetComp(Vector2 caseShape)
    {
        string chemin = "Generation/Composition/" + caseShape.ToString();
        return Resources.Load<GameObject>(chemin);
    }

    #endregion

    public void ChangePen(string usedPen)
    {
        CanvasManager.instance.SetSelectedPen();
        switch (usedPen)
        {
            case "claim":
                if (Pen != m_PenStatus.CLAIM)
                    Pen = m_PenStatus.CLAIM;
                else
                    Pen = m_PenStatus.NONE;
                break;

            case "want":
                if (Pen != m_PenStatus.WANT)
                    Pen = m_PenStatus.WANT;
                else
                    Pen = m_PenStatus.NONE;
                break;

            case "reject":
                if (Pen != m_PenStatus.REJECT)
                    Pen = m_PenStatus.REJECT;
                else
                    Pen = m_PenStatus.NONE;
                break;

            case "exclude":
                if (Pen != m_PenStatus.EXCLUDE)
                    Pen = m_PenStatus.EXCLUDE;
                else
                    Pen = m_PenStatus.NONE;
                break;

            case "draw":
                if (Pen != m_PenStatus.DRAW)
                    Pen = m_PenStatus.DRAW;
                else
                    Pen = m_PenStatus.NONE;
                break;

            case "none":
                Pen = m_PenStatus.NONE;
                break;

            default:
                Pen = m_PenStatus.NONE;
                break;
        }
    }

    private void PrintList(List<characterCreation> listOfCharacterFreeze, List<characterCreation> listOfCharacterDontWanted, List<characterCreation> listOfCharacterWanted, List<characterCreation> listOfCharacterNone)
    {
        print(" ------------------------------------------------");
        // print("ImageShape is : " + shape);

        print("My list est : listOfCharacterFreeze :");
        foreach (var item in listOfCharacterFreeze)
        {
            print(item.assignedElement.characterName);
        }

        print("My list est : listOfCharacterDontWanted :");
        foreach (var item in listOfCharacterDontWanted)
        {
            print(item.assignedElement.characterName);
        }

        print("My list est : listOfCharacterWanted :");
        foreach (var item in listOfCharacterWanted)
        {
            print(item.assignedElement.characterName);
        }

        print("My list est : listOfCharacterNone :");
        foreach (var item in listOfCharacterNone)
        {
            print(item.assignedElement.characterName);
        }
        print(" ------------------------------------------------");
    }


    public Sprite GetVignetteSprite(Vignette_Behaviours vignette)
    {
        switch (vignette.Categorie)
        {
            case Vignette_Behaviours.VignetteCategories.NEUTRE:
                return vignetteRender[0];
                break;
            case Vignette_Behaviours.VignetteCategories.EXPLORER:
                return vignetteRender[1];
                break;
            case Vignette_Behaviours.VignetteCategories.PRENDRE:
                return vignetteRender[2];
                break;
            case Vignette_Behaviours.VignetteCategories.COMBATTRE:
                return vignetteRender[3];
                break;
            case Vignette_Behaviours.VignetteCategories.UTILISER:
                return vignetteRender[4];
                break;
            case Vignette_Behaviours.VignetteCategories.PIEGE:
                return vignetteRender[5];
                break;
            case Vignette_Behaviours.VignetteCategories.CURSE:
                return vignetteRender[6];
                break;
            case Vignette_Behaviours.VignetteCategories.PERTE_OBJET:
                return vignetteRender[7];
                break;
            case Vignette_Behaviours.VignetteCategories.VENT_GLACIAL:
                return vignetteRender[8];
                break;
            case Vignette_Behaviours.VignetteCategories.SAVOIR_OCCULTE:
                return vignetteRender[9];
                break;
            default:
                return vignetteRender[0];
                break;
        }
    }

    public void ResetNegociationTime()
    {
        negociationTime = 100;
    }

    #region Getter && Setter

    public m_PenStatus Pen { get => m_Pen; set => m_Pen = value; }
    public int NegociationTime { get => negociationTime; set => negociationTime = value; }
    public List<UsableObject> GlobalInventory { get => m_GlobalInventory; set => m_GlobalInventory = value; }
    public List<Character_SO> GlobalCrew { get => m_GlobalCrew; set => m_GlobalCrew = value; }
    public List<Object_SO> GlobalInventoryObj { get => m_GlobalInventoryObj; set => m_GlobalInventoryObj = value; }

    #endregion
}
