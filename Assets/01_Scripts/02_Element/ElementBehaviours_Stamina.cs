using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviours_Stamina : MonoBehaviour, IModifier
{
    [SerializeField] private int amountOfStamina;
    public void CollectElement(EventContener eventElt)
    {
        eventElt.Movement += amountOfStamina;
    }
}
