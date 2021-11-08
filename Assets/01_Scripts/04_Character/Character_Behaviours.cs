using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Behaviours : MonoBehaviour, IDamageable
{
    #region Param
    private bool m_IsSelected = false;
    private Character assignedElement;

    [Header("Player State")]
    [SerializeField] private int m_Life;
    [Min(1)]
    [SerializeField] private int m_MaxLife = 1;

    [Space]

    [SerializeField] private int m_Endurance;
    [Min(1)]
    [SerializeField] private int m_MaxEndurance = 1;

    [Space]

    [SerializeField] private int m_InventorySize;
    [Min(1)]
    [SerializeField] private int m_MaxInventorySize = 1;

    [Space]

    [SerializeField] private List<UsableObject> m_Inventory;

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        m_InventorySize = MaxInventorySize;
        m_Life = m_MaxLife;
        m_Endurance = m_MaxEndurance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPlayer()
    {
        foreach (var item in FindObjectsOfType<Character_Behaviours>())
        {
            item.GetComponent<Button>().image.color = Color.white;
            item.IsSelected = false;
        }
        IsSelected = true;
        GetComponent<Button>().image.color = Color.red;
    }

    #region Interfaces
    public void GetDamage()
    {
        throw new System.NotImplementedException();
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public bool IsDead()
    {
        throw new System.NotImplementedException();
    }

    #endregion


    #region Getter && Setter

    public bool IsSelected { get => m_IsSelected; set => m_IsSelected = value; }
    public Character AssignedElement { get => assignedElement; set => assignedElement = value; }
    public int Life { get => m_Life; set => m_Life = value; }
    public int MaxLife { get => m_MaxLife; set => m_MaxLife = value; }
    public int Endurance { get => m_Endurance; set => m_Endurance = value; }
    public int MaxEndurance { get => m_MaxEndurance; set => m_MaxEndurance = value; }
    public int InventorySize { get => m_InventorySize; set => m_InventorySize = value; }
    public int MaxInventorySize { get => m_MaxInventorySize; set => m_MaxInventorySize = value; }
    public List<UsableObject> Inventory { get => m_Inventory; set => m_Inventory = value; }

    #endregion

}
