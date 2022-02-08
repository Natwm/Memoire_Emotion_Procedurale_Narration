using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;

    [SerializeField] private List<UsableObject_SO> m_InitialInventory;
    [SerializeField] private List<UsableObject> m_GlobalInventoryObj;

    public List<UsableObject> GlobalInventoryObj { get => m_GlobalInventoryObj; set => m_GlobalInventoryObj = value; }
    public List<UsableObject_SO> InitialInventory { get => m_InitialInventory; set => m_InitialInventory = value; }

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
