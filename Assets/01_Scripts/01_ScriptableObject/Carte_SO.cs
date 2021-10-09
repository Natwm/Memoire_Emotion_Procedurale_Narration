using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Codex", menuName = "New Scriptable Object/New Card")]
public class Carte_SO : ScriptableObject
{
    public enum Affect
    {
        USE,
        UN_USE
    }

    [Header("Health Effects")]
    [SerializeField] private Affect happy_SadAffect;
    [SerializeField] private int happy_Sad;

    [Header("Movement Effects")]
    [SerializeField] private Affect angry_FearAffect;
    [SerializeField] private int angry_Fear;

    [Header("Draw Effects")]
    [SerializeField] private Affect vignetteAffect;
    [SerializeField] private int amountOfVignetteToDraw;

    [Header("Image")]
    [SerializeField] private Sprite cardSprite;

    public int Happy_Sad { get => happy_Sad; set => happy_Sad = value; }
    public Affect Happy_SadAffect { get => happy_SadAffect; set => happy_SadAffect = value; }
    public Affect Angry_FearAffect { get => angry_FearAffect; set => angry_FearAffect = value; }
    public int Angry_Fear { get => angry_Fear; set => angry_Fear = value; }
    public Sprite CardSprite { get => cardSprite; set => cardSprite = value; }
    public Affect VignetteAffect { get => vignetteAffect; set => vignetteAffect = value; }
    public int AmountOfVignetteToDraw { get => amountOfVignetteToDraw; set => amountOfVignetteToDraw = value; }
}
