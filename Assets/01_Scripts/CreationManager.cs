using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public List<Character> PageCharacterList;

    [Space]
    [Header("Character Selected")]
    public Character_Behaviours selectedPlayer;

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
    public List<Character_Behaviours> listOfCharacter = new List<Character_Behaviours>();
    public List<UsableObject> listOfObject = new List<UsableObject>();

    [Space]
    [Header("Global Inventory")]
    [SerializeField] private List<Object_SO> m_GlobalInventory;

    [Space]
    public Vector2 shape;

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
            CreatePlayerInventory(listOfCharacter[0]);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            CreateVignette();
        }
    }
    #endregion

    #region Create Button

    #region Create Character
    private void CreateCharacterButton(Character tempCharacter)
    {
        GameObject tempButton = Instantiate(baseButton, characterListHolder.transform);

        tempButton.GetComponent<Character_Behaviours>().AssignedElement = tempCharacter;
        tempCharacter.CreateFaceUI(tempButton);

        tempButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            SelectPlayer(tempButton.GetComponent<Character_Behaviours>());
        });

        listOfCharacter.Add(tempButton.GetComponent<Character_Behaviours>());

        //tempButton.GetComponent<characterCreation>().assignedElement = tempCharacter;

    }

    public List<Character> CreateCharacterList(int _charaAmount = 1)
    {
        List<Character> tempList = new List<Character>();

        for (int i = 0; i < _charaAmount; i++)
        {
            Character tempCharacter = CastingManager.instance.getRandomUniqueCharacter(tempList.ToArray());

            tempList.Add(tempCharacter);

            CreateCharacterButton(tempCharacter);

            //CharacterList.Remove(tempCharacter);
        }
        return tempList == null ? null : tempList;
    }
    #endregion

    #region Create Object
    private void CreateObjectButton(Object_SO tempObject)
    {
        GameObject tempButton = Instantiate(ObjectButton, objectListHolder.transform);

        switch (tempObject.Category)
        {
            case Object_SO.ObjectCategory.NONE:
                break;
            case Object_SO.ObjectCategory.ARME:
                tempButton.AddComponent<Armes_Behaviours>();
                tempButton.GetComponent<Armes_Behaviours>().Data = tempObject;
                break;
            case Object_SO.ObjectCategory.USABLE:

                switch (tempObject.Action)
                {
                    case Object_SO.ObjectAction.NONE:
                        break;
                    case Object_SO.ObjectAction.HEALTH:
                        tempButton.AddComponent<HealObject_Behaviours>();
                        tempButton.GetComponent<HealObject_Behaviours>().Data = tempObject;
                        break;
                    case Object_SO.ObjectAction.DRAW:
                        tempButton.AddComponent<DrawObject_Behaviours>();
                        tempButton.GetComponent<DrawObject_Behaviours>().Data = tempObject;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }

        UsableObject eventButton = tempButton.GetComponent<UsableObject>();
        tempButton.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = tempObject.ObjectName;

        tempButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            eventButton.AffectByPlayer(tempButton.GetComponent<Button>());
        }
        );

        listOfObject.Add(tempButton.GetComponent<UsableObject>());

        //tempButton.GetComponent<characterCreation>().assignedElement = tempCharacter;

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
        List<characterCreation> listOfCharacterWanted = new List<characterCreation>();
        List<characterCreation> listOfCharacterDontWanted = new List<characterCreation>();
        List<characterCreation> listOfCharacterFreeze = new List<characterCreation>();
        List<characterCreation> listOfCharacterNone = new List<characterCreation>();
        List<characterCreation> listOfSpawnable = new List<characterCreation>();


        for (int i = 0; i < characterListHolder.transform.childCount; i++)
        {
            characterCreation character = characterListHolder.transform.GetChild(i).GetComponent<characterCreation>();
            //print("<Color=green>"+character.assignedElement.characterName + "  " + character.Status+"</Color>");
            switch (character.Status)
            {
                case characterCreation.CharacterStatus.FREEZE:
                    listOfCharacterFreeze.Add(character);
                    break;
                case characterCreation.CharacterStatus.WANT:
                    listOfCharacterWanted.Add(character);
                    break;
                case characterCreation.CharacterStatus.DONT_WANT:
                    listOfCharacterDontWanted.Add(character);
                    break;
                case characterCreation.CharacterStatus.NONE:
                    listOfCharacterNone.Add(character);
                    break;
                default:
                    break;
            }
        }

        listOfSpawnable.AddRange(listOfCharacterWanted);
        listOfSpawnable.AddRange(listOfCharacterNone);

        List<Character> test = new List<Character>();

        foreach (var item in listOfSpawnable)
        {
            test.Add(item.assignedElement);
        }

        GameObject card = Instantiate(GetVignetteShape(shape));

        //print(card.name);
        //        print(Bd_Component.bd_instance.name);
        Bd_Component.bd_instance.SetVignetteToObjectCreate(card, test.ToArray());

        //Vignette tempVignette = new Vignette(shape.ToString(), GetVignette(shape), null, null, GetComp(shape));

        //PrintList(listOfCharacterFreeze, listOfCharacterDontWanted, listOfCharacterWanted, listOfCharacterNone);
    }

    public void CreatePlayerInventory(Character_Behaviours player)
    {
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

            if (!player.Inventory.Contains(pullOfObject[index]))
            {
                player.Inventory.Add(pullOfObject[index]);
            }
            
            pullOfObject.Remove(pullOfObject[index]);
            if (pullOfObject.Count <= 0)
                break;
        }
    }
    #endregion

    public void SelectPlayer( Character_Behaviours player)
    {
        selectedPlayer = player;
    }

    public void LaunchGame()
    {
        if(selectedPlayer !=null)
            CreatePlayerInventory(selectedPlayer);
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
        print("ImageShape is : " + shape);

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


    #region Getter && Setter

    public m_PenStatus Pen { get => m_Pen; set => m_Pen = value; }

    #endregion
}
