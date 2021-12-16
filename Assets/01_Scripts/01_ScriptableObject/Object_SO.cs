using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]


public class DrawVignette
{
    [SerializeField] private Vignette_Behaviours.VignetteCategories categoryToDraw;
    [SerializeField] private int amountOfCardToDraw;

    public int AmountOfCardToDraw { get => amountOfCardToDraw; set => amountOfCardToDraw = value; }
    public Vignette_Behaviours.VignetteCategories CategoryToDraw { get => categoryToDraw; set => categoryToDraw = value; }
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
    #endregion

    [Header("Statistique")]
    [SerializeField] private ObjectTarget m_Target;
    [SerializeField] private ObjectCategory m_Category;
    [SerializeField] private ObjectAction m_Action;
    [SerializeField] private List<DrawVignette> m_DrawParam;

    [Space]
    [Header("Param")]
    [SerializeField] private string m_ObjectName;
    [SerializeField] private Sprite m_sprite;
    [SerializeField] private int m_AmountOfUse;
    [SerializeField] private int m_SizeInInventory;

    [Space]
    [Header("UI")]
    [TextArea(10,50)]
    [SerializeField] private string m_Description;
    [SerializeField] private string m_NarrationName;

    public FMOD.Studio.EventInstance SelectedEffect;
    [FMODUnity.EventRef] [SerializeField] private string SelectedSound;


    public void PlaySound()
    {
        SelectedEffect = FMODUnity.RuntimeManager.CreateInstance(SelectedSound);
        SelectedEffect.start();
    }

    #region Getter && Setter

    public string NarrationName { get => m_NarrationName; set => m_NarrationName=value; }
    public ObjectTarget Target { get => m_Target; set => m_Target = value; }
    public ObjectCategory Category { get => m_Category; set => m_Category = value; }
    public ObjectAction Action { get => m_Action; set => m_Action = value; }
    public List<DrawVignette> DrawParam { get => m_DrawParam; set => m_DrawParam = value; }
    public string ObjectName { get => m_ObjectName; set => m_ObjectName = value; }
    public Sprite Sprite { get => m_sprite; set => m_sprite = value; }
    public int AmountOfUse { get => m_AmountOfUse; set => m_AmountOfUse = value; }
    public int SizeInInventory { get => m_SizeInInventory; set => m_SizeInInventory = value; }
    public string Description { get => m_Description; set => m_Description = value; }

    #endregion
}
