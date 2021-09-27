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

    [Header("Health Effects")]
    [SerializeField] private Status healthEffect;
    [SerializeField] private int health;

    public int Health { get => health; set => health = value; }
    public Status HealthEffect { get => healthEffect; set => healthEffect = value; }
}
