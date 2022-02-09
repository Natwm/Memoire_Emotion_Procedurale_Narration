using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Param
    private bool m_IsSelected = false;
    [SerializeField] private Character_SO assignedElement;

    [Header("Player State")]
    [SerializeField] private int m_Life;
    [Min(1)]
    [SerializeField] private int m_MaxLife = 1;

    [Space]
    [SerializeField] private int m_MentalHealth;
    [Min(1)]
    [SerializeField] private int m_MaxMentalHealth = 1;

    [Space]

    [SerializeField] private int m_InventorySize;
    [Min(1)]
    [SerializeField] private int m_MaxInventorySize = 1;

    [Space]

    [SerializeField] private List<UsableObject> m_InventoryObj = new List<UsableObject>();

    private Character_Button characterContener;

    public Character_SO AssignedElement { get => assignedElement; set => assignedElement = value; }
    public List<UsableObject> InventoryObj { get => m_InventoryObj; set => m_InventoryObj = value; }
    public int Life { get => m_Life; set => m_Life = value; }
    public int MaxLife { get => m_MaxLife; set => m_MaxLife = value; }
    public int MentalHealth { get => m_MentalHealth; set => m_MentalHealth = value; }
    public int MaxMentalHealth { get => m_MaxMentalHealth; set => m_MaxMentalHealth = value; }
    public Character_Button CharacterContener { get => characterContener; set => characterContener = value; }
    public int InventorySize { get => m_InventorySize; set => m_InventorySize = value; }
    public int MaxInventorySize { get => m_MaxInventorySize; set => m_MaxInventorySize = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUpCharacter(Character_SO data)
    {
        assignedElement = data;
        m_Life = data.Health;
        m_MaxLife = data.MaxHealth;

        m_MentalHealth = data.MentalHealth;
        m_MaxMentalHealth = data.MaxMentalHealth;

        InventorySize = data.InventorySize;

        this.gameObject.name += "_" + data.CharacterName;
    }

}
