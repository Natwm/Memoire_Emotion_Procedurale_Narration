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
    [Header("Time")]
    [SerializeField] private int negociationTime = 100;

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
            CreatePlayerInventory(PlayerManager.instance);
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
        tempButton.AddComponent<UsableObject>();
        tempButton.GetComponent<UsableObject>().Data = tempObject;

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
        List<Character_Behaviours> listOfCharacterWanted = new List<Character_Behaviours>();
        List<Character_Behaviours> listOfCharacterDontWanted = new List<Character_Behaviours>();
        List<Character_Behaviours> listOfCharacterFreeze = new List<Character_Behaviours>();
        List<Character_Behaviours> listOfCharacterNone = new List<Character_Behaviours>();
        List<Character_Behaviours> listOfSpawnable = new List<Character_Behaviours>();


        for (int i = 0; i < characterListHolder.transform.childCount; i++)
        {
            Character_Behaviours character = characterListHolder.transform.GetChild(i).GetComponent<Character_Behaviours>();
            //print("<Color=green>"+character.assignedElement.characterName + "  " + character.Status+"</Color>");
          
                    listOfCharacterNone.Add(character);

        }



        GameObject card = Instantiate(GetVignetteShape(shape));

        //print(card.name);
        //        print(Bd_Component.bd_instance.name);
        Character[] tempCharacter_BehavioursDistribution = new Character[listOfCharacterNone.Count];

        for (int i = 0; i < listOfCharacterNone.Count; i++)
        {
            tempCharacter_BehavioursDistribution[i] = listOfCharacterNone[i].AssignedElement;
        }

        Bd_Component.bd_instance.SetVignetteToObjectCreate(card, tempCharacter_BehavioursDistribution);

        //Vignette tempVignette = new Vignette(shape.ToString(), GetVignette(shape), null, null, GetComp(shape));

        //PrintList(listOfCharacterFreeze, listOfCharacterDontWanted, listOfCharacterWanted, listOfCharacterNone);
    }

    public void CreatePlayerInventory(PlayerManager player)
    {
        player.CharacterData = selectedPlayer.AssignedElement;
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
        negociationTime -= reduceValue;

        return negociationTime >= 0;
    }

    public void SelectPlayer( Character_Behaviours player)
    {
        selectedPlayer = player;
    }

    public void LaunchGame()
    {
        if(selectedPlayer !=null)
            CreatePlayerInventory(PlayerManager.instance);
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
    public int NegociationTime { get => negociationTime; set => negociationTime = value; }

    #endregion
}
