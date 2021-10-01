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
    [SerializeField] private Affect healthAffect;
    [SerializeField] private int health;

    [Header("Movement Effects")]
    [SerializeField] private Affect movementAffect;
    [SerializeField] private int movement;

    [Header("Draw Effects")]
    [SerializeField] private Affect vignetteAffect;
    [SerializeField] private int amountOfVignetteToDraw;

    [Header("Image")]
    [SerializeField] private Sprite cardSprite;

    public int Health { get => health; set => health = value; }
    public Affect HealthAffect { get => healthAffect; set => healthAffect = value; }
    public Affect MovementAffect { get => movementAffect; set => movementAffect = value; }
    public int Movement { get => movement; set => movement = value; }
    public Sprite CardSprite { get => cardSprite; set => cardSprite = value; }
    public Affect VignetteAffect { get => vignetteAffect; set => vignetteAffect = value; }
    public int AmountOfVignetteToDraw { get => amountOfVignetteToDraw; set => amountOfVignetteToDraw = value; }
}
