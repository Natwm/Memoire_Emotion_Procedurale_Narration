using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviours_Draw : ElementBehaviours
{
    [SerializeField] private int amountOfCardToDraw;
    public override void CollectElement(EventContener eventElt)
    {
        eventElt.Vignette += amountOfCardToDraw;
        //player.HandModifier(amountOfCardToDraw);
    }
}
