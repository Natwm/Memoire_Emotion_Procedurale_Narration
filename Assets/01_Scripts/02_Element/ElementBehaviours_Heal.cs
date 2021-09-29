﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviours_Heal: ElementBehaviours
{
    [SerializeField] private int amountOfHeal;
    public override void CollectElement(PlayerManager player)
    {
        player.GainHeath(amountOfHeal);
    }
}
