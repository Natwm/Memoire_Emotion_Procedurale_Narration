using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Palette", menuName = "New Scriptable Object/New Palette")]
public class Palette_SO : ScriptableObject
{
    [SerializeField] private Color[] m_colorPalette;
    public Color[] colorPalette { get => m_colorPalette; set => m_colorPalette = value; }
}
