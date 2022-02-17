using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;
    [Space]
    public int amoutOfObjectBeforeTake;
    [Space]

    [SerializeField] private List<UsableObject_SO> m_InitialInventory;
    [SerializeField] private List<UsableObject> m_GlobalInventoryObj;
    [SerializeField] private List<UsableObject> pageInventory = new List<UsableObject>();

    [Space]
    [SerializeField] private GameObject objectPrefabs;

    public List<UsableObject> GlobalInventoryObj { get => m_GlobalInventoryObj; set => m_GlobalInventoryObj = value; }
    public List<UsableObject_SO> InitialInventory { get => m_InitialInventory; set => m_InitialInventory = value; }
    public List<UsableObject> PageInventory { get => pageInventory; set => pageInventory = value; }
    public GameObject ObjectPrefabs { get => objectPrefabs; set => objectPrefabs = value; }

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : InventoryManager");
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
}
