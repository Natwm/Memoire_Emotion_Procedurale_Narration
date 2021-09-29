using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Codex", menuName = "New Scriptable Object/New Card")]
public class Carte_SO : ScriptableObject
{
    public enum Status
    {
        BONUS,
        MALUS
    }

    public enum Affect
    {
        USE,
        UN_USE
    }

    [Header("Health Effects")]
    [SerializeField] private Affect healthAffect;
    [SerializeField] private Status healthEffect;
    [SerializeField] private int health;

    [Header("Movement Effects")]
    [SerializeField] private Affect movementAffect;
    [SerializeField] private Status movementEffect;
    [SerializeField] private int movement;

    [Header("Draw Effects")]
    [SerializeField] private Affect vignetteAffect;
    [SerializeField] private Status vignetteEffect;
    [SerializeField] private int amountOfVignetteToDraw;

    [Header("Image")]
    [SerializeField] private Sprite cardSprite;

    public int Health { get => health; set => health = value; }
    public Status HealthEffect { get => healthEffect; set => healthEffect = value; }
    public Affect HealthAffect { get => healthAffect; set => healthAffect = value; }
    public Affect MovementAffect { get => movementAffect; set => movementAffect = value; }
    public Status MovementEffect { get => movementEffect; set => movementEffect = value; }
    public int Movement { get => movement; set => movement = value; }
    public Sprite CardSprite { get => cardSprite; set => cardSprite = value; }
    public Affect VignetteAffect { get => vignetteAffect; set => vignetteAffect = value; }
    public Status VignetteEffect { get => vignetteEffect; set => vignetteEffect = value; }
    public int AmountOfVignetteToDraw { get => amountOfVignetteToDraw; set => amountOfVignetteToDraw = value; }
}
