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

    public void CreateObjectInventory()
    {
        for (int i = 0; i < InventoryManager.instance.InitialInventory.Count; i++)
        {
            UsableObject_SO tempObjectSO = InventoryManager.instance.InitialInventory[i];

            GameObject tempObject = Instantiate(InventoryManager.instance.ObjectPrefabs, InventoryManager.instance.gameObject.transform);
            tempObject.name += "_" + tempObjectSO.ObjectName;
            tempObject.GetComponent<UsableObject>().Data = tempObjectSO;

            CanvasManager.instance.CreateObjectButton(tempObject.GetComponent<UsableObject>());
            //GlobalInventory.Remove(tempObject);
            //CharacterList.Remove(tempCharacter);
        }
    }

    void RepartitionObjec()
    {
        foreach (var item in listOfCharacter)
        {
            CreatePlayerInventory(item);
        }
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

    public void SelectCharacter(Character_Button player)
    {
        selectedPlayer = player;
    }

    void CantApplyNegociation()
    {

    }

    void EndNegociation()
    {

    }

    public void CreateObjectListFromUsableObject()
    {
        List<UsableObject> tempList = new List<UsableObject>();

        for (int i = 0; i < InventoryManager.instance.GlobalInventoryObj.Count; i++)
        {
            print(i);
            //UsableObject tempObject = pulledObject.transform.GetChild(i).gameObject.GetComponent<UsableObject>();
            //CreateObjectButtonFromUsableObject(tempObject);

            //Destroy(tempObject.gameObject);
        }
    }

    public bool CreatePlayerInventory(Character player)
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

        for (int i = 0; i < player.InventorySize - player.InventoryObj.Count; i++)
        {
            int index = Random.Range(0, pullOfObject.Count);

            if (pullOfObject.Count > 0)
            {
                if (!player.InventoryObj.Contains(pullOfObject[index]))
                {
                    player.InventoryObj.Add(pullOfObject[index]);

                    player.InventoryObj.Add(pullOfObject[index]);

                    pullOfObject[index].gameObject.SetActive(false);
                }
                UsableObject obj = pullOfObject[index];
                pullOfObject.RemoveAll(item => item == obj);

                //obj.gameObject.transform.parent = pulledObject.transform;

                if (pullOfObject.Count <= 0)
                {
                    foreach (var item in player.InventoryObj)
                    {
                        InventoryManager.instance.GlobalInventoryObj.RemoveAll(objToRemove => objToRemove == item);
                    }
                    //player.SetUpInventoryUI();
                    return true;
                }
            }

        }
        foreach (var item in player.InventoryObj)
        {
            InventoryManager.instance.GlobalInventoryObj.RemoveAll(obj => obj == item);
        }
        //player.SetUpInventoryUI();
        return true;
    }

    /*private List<UsableObject> CreatePull(UsableObject ObjetToTake, Character player)
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
                    if (player == ObjetToTake.Stat.character)
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
    }*/

}
