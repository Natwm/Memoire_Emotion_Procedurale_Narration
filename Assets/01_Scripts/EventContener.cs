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
        print("okokokokokokoekfsokfoskfosekfos");
        switch (eventObj.HealthEffect)
        {
            case Carte_SO.Status.BONUS:
                Health = eventObj.Health;
                break;
            case Carte_SO.Status.MALUS:
                Health = -eventObj.Health;
                break;
            default:
                break;
        }

        switch (eventObj.MovementEffect)
        {
            case Carte_SO.Status.BONUS:
                Movement = eventObj.Movement;
                break;
            case Carte_SO.Status.MALUS:
                Movement = -eventObj.Movement;
                break;
            default:
                break;
        }

        switch (eventObj.VignetteEffect)
        {
            case Carte_SO.Status.BONUS:
                Vignette = eventObj.AmountOfVignetteToDraw;
                break;
            case Carte_SO.Status.MALUS:
                Vignette = -eventObj.AmountOfVignetteToDraw;
                break;
            default:
                break;
        }

    }

    public void AffectByAModifier(IModifier modifer)
    {
        
    }
}
