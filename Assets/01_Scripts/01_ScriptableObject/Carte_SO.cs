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

    public int Health { get => health; set => health = value; }
    public Status HealthEffect { get => healthEffect; set => healthEffect = value; }
    public Affect HealthAffect { get => healthAffect; set => healthAffect = value; }
    public Affect MovementAffect { get => movementAffect; set => movementAffect = value; }
    public Status MovementEffect { get => movementEffect; set => movementEffect = value; }
    public int Movement { get => movement; set => movement = value; }
}
