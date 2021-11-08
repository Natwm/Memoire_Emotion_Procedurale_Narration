using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DrawObject
{
    [SerializeField] private Object_SO.ObjectDraw cardToDraw;
    [SerializeField] private int amountOfCardToDraw;
}

[CreateAssetMenu(fileName = "New Cool Stuff", menuName = "New Scriptable Object/New Object")]
public class Object_SO : ScriptableObject
{
    #region Enum
    public enum ObjectCategory
    {
        NONE,
        ARME,
        USABLE
    }

    public enum ObjectTarget
    {
        NONE,
        ACTUAL_PLAYER,
        GROUP,
        WORLD
    }

    public enum ObjectAction
    {
        NONE,
        HEALTH,
        DRAW
    }

    public enum ObjectDraw
    {
        NONE,
        CURSE,
        EXPLORE,
        SHOOT,
        RELOAD,
        ATTACK
    }
    #endregion

    [Header("Statistique")]
    [SerializeField] private ObjectTarget m_Target;
    [SerializeField] private ObjectCategory m_Category;
    [SerializeField] private ObjectAction m_Action;
    [SerializeField] private List<DrawObject> m_DrawParam;

    [Space]
    [Header("Param")]
    [SerializeField] private string m_ObjectName;
    [SerializeField] private Sprite m_sprite;
    [SerializeField] private int m_AmountOfUse;
    [SerializeField] private int m_SizeInInventory;

    #region Getter && Setter

    public ObjectTarget Target { get => m_Target; set => m_Target = value; }
    public ObjectCategory Category { get => m_Category; set => m_Category = value; }
    public ObjectAction Action { get => m_Action; set => m_Action = value; }
    public List<DrawObject> DrawParam { get => m_DrawParam; set => m_DrawParam = value; }
    public string ObjectName { get => m_ObjectName; set => m_ObjectName = value; }
    public Sprite Sprite { get => m_sprite; set => m_sprite = value; }
    public int AmountOfUse { get => m_AmountOfUse; set => m_AmountOfUse = value; }
    public int SizeInInventory { get => m_SizeInInventory; set => m_SizeInInventory = value; }

    #endregion
}
