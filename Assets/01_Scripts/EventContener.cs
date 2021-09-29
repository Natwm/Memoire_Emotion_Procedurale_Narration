using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventContener : MonoBehaviour
{
    [Header("Health Effects")]
    [SerializeField] private int health;

    [Header("Movement Effects")]
    [SerializeField] private int movement;

    [Header("Hand Effect")]
    [SerializeField] private int vignette;

    [Header("key")]
    [SerializeField] private bool isKey;

    public int Health { get => health; set => health = value; }
    public int Movement { get => movement; set => movement = value; }
    public bool IsKey { get => isKey; set => isKey = value; }
    public int Vignette { get => vignette; set => vignette = value; }

    public void SetUpEvent(Carte_SO eventObj)
    {
        Health = eventObj.HealthEffect == Carte_SO.Status.BONUS ? eventObj.Health : -eventObj.Health;
        Movement = eventObj.MovementEffect == Carte_SO.Status.BONUS ? eventObj.Movement : -eventObj.Movement;
    }

    public void AffectByAModifier(IModifier modifer)
    {
        
    }
}
