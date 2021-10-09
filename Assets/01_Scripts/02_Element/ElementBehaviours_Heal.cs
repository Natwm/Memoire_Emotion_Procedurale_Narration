using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviours_Heal: ElementBehaviours,IModifier
{
    [SerializeField] private int amountOfHappyOrSad;

    public int AmountOfHappyOrSad { get => amountOfHappyOrSad; set => amountOfHappyOrSad = value; }

    public override void CollectElement(EventContener eventElt)
    {
        eventElt.CurrentHappy_Sad += AmountOfHappyOrSad;
    }
}
