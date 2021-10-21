using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviours_Sad : MonoBehaviour, IModifier
{
    [SerializeField] private int amountOfJoySad;

    public int AmountOfJoySad { get => amountOfJoySad; set => amountOfJoySad = value; }

    public void CollectElement(EventContener eventElt)
    {
        eventElt.CurrentHappy_Sad += AmountOfJoySad;
        eventElt.UpdateCharacterFace();
    }
}
