using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviours_Stamina : ElementBehaviours
{
    [SerializeField] private int amountOfVignette;
    public override void CollectElement(PlayerManager player)
    {
        player.HandModifier(amountOfVignette);
    }
}
