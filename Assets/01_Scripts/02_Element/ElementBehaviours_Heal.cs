using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviours_Heal: ElementBehaviours
{
    [SerializeField] private int amountOfHeal;

    public int AmountOfHeal { get => amountOfHeal; set => amountOfHeal = value; }

    public override void CollectElement(EventContener eventElt)
    {
        eventElt.Health += AmountOfHeal;
    }
}
