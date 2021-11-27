using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Simaisnon", menuName = "New Scriptable Object/New Character")]
public class Character_SO : ScriptableObject
{
    [Space]
    [Header("Render")]
    [SerializeField] private Sprite m_render;
    [SerializeField] private string m_CharacterName;

    [Space]
    [Header("Health")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int health;


    [Space]
    [Header("Endurance")]
    [SerializeField] private int m_Endurance;
    [Min(1)]
    [SerializeField] private int m_MaxEndurance = 1;

    [Space]
    [Header("Mental Health")]
    [SerializeField] private int m_MentalHealth;
    [Min(1)]
    [SerializeField] private int m_MaxMentalHealth = 1;

    [Space]
    [Header("Inventory")]
    [SerializeField] private int m_InventorySize;
    [Min(1)]
    [SerializeField] private int m_MaxInventorySize = 1;

    [Space]

    [SerializeField] private List<Object_SO> m_Inventory;

    [Space]
    [Header("Base Hand")]
    [SerializeField] private List<DrawVignette> m_BaseHand;

    [Space]
    [Header("Color")]
    [SerializeField] private Color m_Color;

    #region Getter && Setter

    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Health { get => health; set => health = value; }
    public int Endurance { get => m_Endurance; set => m_Endurance = value; }
    public int MaxEndurance { get => m_MaxEndurance; set => m_MaxEndurance = value; }
    public int InventorySize { get => m_InventorySize; set => m_InventorySize = value; }
    public int MaxInventorySize { get => m_MaxInventorySize; set => m_MaxInventorySize = value; }
    public List<Object_SO> Inventory { get => m_Inventory; set => m_Inventory = value; }
    public Sprite Render { get => m_render; set => m_render = value; }
    public string CharacterName { get => m_CharacterName; set => m_CharacterName = value; }
    public List<DrawVignette> BaseHand { get => m_BaseHand; set => m_BaseHand = value; }
    public Color Color { get => m_Color; set => m_Color = value; }
    public int MentalHealth { get => m_MentalHealth; set => m_MentalHealth = value; }
    public int MaxMentalHealth { get => m_MaxMentalHealth; set => m_MaxMentalHealth = value; }

    #endregion
}
