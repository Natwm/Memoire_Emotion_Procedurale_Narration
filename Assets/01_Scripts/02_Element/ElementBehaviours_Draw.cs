using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviours_Draw : ElementBehaviours, IModifier
{
    [SerializeField] private int amountOfCardToDraw;

    public int AmountOfCardToDraw { get => amountOfCardToDraw; set => amountOfCardToDraw = value; }

    public override void CollectElement(EventContener eventElt)
    {
        eventElt.CurrentAmountOfVignetteToDraw += AmountOfCardToDraw;
        print("okd");
    }
}
