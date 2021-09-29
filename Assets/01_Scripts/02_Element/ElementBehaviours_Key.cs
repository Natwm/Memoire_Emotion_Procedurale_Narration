using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviours_Key : ElementBehaviours
{

    public override void CollectElement(PlayerManager player)
    {
        player.PickUpKey();
    }
}
