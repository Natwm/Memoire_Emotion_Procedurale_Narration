using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
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

    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randomIndex = Random.Range(0, ObjectManager.instance._HealPullOfObject.Count);
            UsableObject_SO newItem = ObjectManager.instance._HealPullOfObject[randomIndex];

            GameObject item = CanvasManager.instance.NewItemInLevelInventory(newItem);

            InventoryManager.instance.PageInventory.Add(item.GetComponent<UsableObject>());

            if (InventoryManager.instance.PageInventory.Count == InventoryManager.instance.amoutOfObjectBeforeTake)
            {
                /*foreach (var obj in InventoryManager.instance.PageInventory)
                {
                    Destroy(obj.gameObject);//.SetActive(false);
                }*/

                for (int i = 0; i < CanvasManager.instance.LevelInventoryPanel1.transform.childCount; i++)
                {
                    Destroy(CanvasManager.instance.LevelInventoryPanel1.transform.GetChild(i).gameObject);
                }

                InventoryManager.instance.PageInventory.Clear();
                CanvasManager.instance.SetUpLevelIndicator();
            }

            CanvasManager.instance.SetUpLevelIndicator();
        }
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

    public void ReduceMentalPlayer(int amountOfDamage)
    {
        MentalHealth -= amountOfDamage;

        if (IsDead())
            Death();
    }

    public void HealMentalPlayer(int heal)
    {
        MentalHealth += heal;

        if (MentalHealth > MaxMentalHealth)
            MentalHealth = MaxMentalHealth;
    }

    public void HealPlayer(int heal)
    {
        Life += heal;

        if (Life > MaxLife)
            Life = MaxLife;
    }

    public void GetDamage(int amountOfDamage)
    {
        Life -= amountOfDamage;
        if (IsDead())
            Death();
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public bool IsDead()
    {
        return Life <= 0 || MentalHealth <= 0;
    }

    public Character_SO AssignedElement { get => assignedElement; set => assignedElement = value; }
    public List<UsableObject> InventoryObj { get => m_InventoryObj; set => m_InventoryObj = value; }
    public int Life { get => m_Life; set => m_Life = value; }
    public int MaxLife { get => m_MaxLife; set => m_MaxLife = value; }
    public int MentalHealth { get => m_MentalHealth; set => m_MentalHealth = value; }
    public int MaxMentalHealth { get => m_MaxMentalHealth; set => m_MaxMentalHealth = value; }
    public Character_Button CharacterContener { get => characterContener; set => characterContener = value; }
    public int InventorySize { get => m_InventorySize; set => m_InventorySize = value; }
    public int MaxInventorySize { get => m_MaxInventorySize; set => m_MaxInventorySize = value; }
}
