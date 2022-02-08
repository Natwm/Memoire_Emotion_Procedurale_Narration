using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cool Stuff", menuName = "New Scriptable Object/New Object")]
public class UsableObject_SO : ScriptableObject
{
    [Header("Statistique")]
    [SerializeField] private List<DrawVignette> m_DrawParam;

    [Space]
    [Header("Param")]
    [SerializeField] private string m_ObjectName;
    [SerializeField] private Sprite m_sprite;
    [SerializeField] private int m_AmountOfUse;
    [SerializeField] private int m_SizeInInventory;

    [Space]
    [Header("UI")]
    [TextArea(10, 50)]
    [SerializeField] private string m_Description;
    [SerializeField] private string m_NarrationName;

    #region Getter && Setter

    public string NarrationName { get => m_NarrationName; set => m_NarrationName = value; }
    public List<DrawVignette> DrawParam { get => m_DrawParam; set => m_DrawParam = value; }
    public string ObjectName { get => m_ObjectName; set => m_ObjectName = value; }
    public Sprite Sprite { get => m_sprite; set => m_sprite = value; }
    public int AmountOfUse { get => m_AmountOfUse; set => m_AmountOfUse = value; }
    public int SizeInInventory { get => m_SizeInInventory; set => m_SizeInInventory = value; }
    public string Description { get => m_Description; set => m_Description = value; }

    #endregion
}
