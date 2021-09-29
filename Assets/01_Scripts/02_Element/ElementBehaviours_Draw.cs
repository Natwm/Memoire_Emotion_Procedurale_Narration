using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviours_Draw : ElementBehaviours
{
    [SerializeField] private int amountOfCardToDraw;
    public override void CollectElement(PlayerManager player)
    {
        player.HandModifier(amountOfCardToDraw);
    }
}
