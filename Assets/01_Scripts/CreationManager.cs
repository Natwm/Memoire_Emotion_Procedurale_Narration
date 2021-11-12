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
    [SerializeField] private List<Object_SO> m_GlobalInventory;

    [Space]
    [Header("Vignette Render")]
    [SerializeField] private List<Sprite> vignetteRender;
    //public Vector2 shape;

    [SerializeField] private m_PenStatus m_Pen;

    

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
        PageCharacterList = CreateCharacterList(4);
        CreateObjectList(3);
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
    private void CreateCharacterButton(Character_SO tempCharacter)
    {
        GameObject tempButton = Instantiate(baseButton, characterListHolder.transform);
        Character_Button buttonScript = tempButton.GetComponent<Character_Button>();

        buttonScript.AssignedElement = tempCharacter;
        buttonScript.CharacterRender.sprite = tempCharacter.Render;

        tempButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            SelectPlayer(tempButton.GetComponent<Character_Button>());
        });

        listOfCharacter.Add(tempButton.GetComponent<Character_Button>());

    }

    public List<Character_SO> CreateCharacterList(int _charaAmount = 1)
    {
        List<Character_SO> tempList = new List<Character_SO>();
        foreach (var item in m_GlobalCrew)
        {
            tempList.Add(item);
        }


        for (int i = 0; i < _charaAmount; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            Character_SO tempCharacter = tempList[randomIndex];

            m_Crew.Add(tempCharacter);
            tempList.RemoveAt(randomIndex);

            CreateCharacterButton(tempCharacter);
        }
        return tempList == null ? null : tempList;
    }
    #endregion

    #region Create Object
    private void CreateObjectButton(Object_SO tempObject)
    {
        GameObject tempButton = Instantiate(ObjectButton, objectListHolder.transform);
        tempButton.AddComponent<UsableObject>();
        tempButton.GetComponent<UsableObject>().Data = tempObject;

        UsableObject eventButton = tempButton.GetComponent<UsableObject>();
        tempButton.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = tempObject.ObjectName;

        tempButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            eventButton.AffectByPlayer(tempButton.GetComponent<Button>());
        }
        );

        EventTrigger buttonEvent;

        if(tempButton.TryGetComponent<EventTrigger>(out buttonEvent)){
            EventTrigger.Entry entry = new EventTrigger.Entry();
            EventTrigger.Entry exit = new EventTrigger.Entry();

            entry.eventID = EventTriggerType.PointerEnter;
            exit.eventID = EventTriggerType.PointerExit;

            entry.callback.AddListener((data) => { UpdateSliderValueOnEnter(eventButton); });
            exit.callback.AddListener((data) => { UpdateSliderValueOnExit(eventButton); });

            buttonEvent.triggers.Add(entry);
            buttonEvent.triggers.Add(exit);
        }

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


    public List<Object_SO> CreateObjectList(int amoutOfObject = 1)
    {
        List<Object_SO> tempList = new List<Object_SO>();

        for (int i = 0; i < amoutOfObject; i++)
        {
            int randomObject = Random.Range(0, m_GlobalInventory.Count);

            Object_SO tempObject = m_GlobalInventory[randomObject];

            tempList.Add(tempObject);

            CreateObjectButton(tempObject);
            m_GlobalInventory.Remove(tempObject);
            //CharacterList.Remove(tempCharacter);
        }
        return tempList == null ? null : tempList;
    }
    #endregion

    #endregion

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

    public void CreatePlayerInventory(PlayerManager player)
    {
        player.SetUpCharacter(selectedPlayer.AssignedElement);
        List<UsableObject> pullOfObject = new List<UsableObject>();
        List<UsableObject> claimObject = new List<UsableObject>();

        for (int i = 0; i < objectListHolder.transform.childCount; i++)
        {   GameObject clickObject = objectListHolder.transform.GetChild(i).gameObject;
            
            UsableObject ObjetToTake = clickObject.GetComponent<UsableObject>();
            
            switch (ObjetToTake.Status)
            {
                case UsableObject.ObjectStatus.NONE:
                    pullOfObject.Add(ObjetToTake);
                    pullOfObject.Add(ObjetToTake);
                    break;
                case UsableObject.ObjectStatus.CLAIM:
                    claimObject.Add(ObjetToTake);
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

        for (int i = 0; i < player.InventorySize; i++)
        {
            int index = Random.Range(0, pullOfObject.Count);

            if (!player.Inventory.Contains(pullOfObject[index].Data))
            {
                player.Inventory.Add(pullOfObject[index].Data);
            }
            UsableObject obj = pullOfObject[index];
            pullOfObject.Remove(obj);

            obj.gameObject.SetActive(false);
            if (pullOfObject.Count <= 0)
                break;
        }
    }
    #endregion

    public bool ReduceNegociationTime(int reduceValue)
    {
        if(negociationTime - reduceValue >= 0)
        {
            negociationTime -= reduceValue;
            return true;
        }
        return false;
    }

    public void SelectPlayer( Character_Button player)
    {
        selectedPlayer = player;
    }

    public void LaunchGame()
    {
        if(selectedPlayer != null)
        {
            CreatePlayerInventory(PlayerManager.instance);
            CanvasManager.instance.SetUpCharacterInfo();
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
            case Vignette_Behaviours.VignetteCategories.ALEATOIRE:
                return vignetteRender[5];
                break;
            case Vignette_Behaviours.VignetteCategories.TREBUCHER:
                return vignetteRender[6];
                break;
            case Vignette_Behaviours.VignetteCategories.PERTE_OBJET:
                return vignetteRender[7];
                break;
            case Vignette_Behaviours.VignetteCategories.CURSE:
                return vignetteRender[8];
                break;
            default:
                return vignetteRender[0];
                break;
        }
    }

    #region Getter && Setter

    public m_PenStatus Pen { get => m_Pen; set => m_Pen = value; }
    public int NegociationTime { get => negociationTime; set => negociationTime = value; }

    #endregion
}
