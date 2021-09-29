using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviours_Door : ElementBehaviours
{

    public override void CollectElement(PlayerManager player)
    {
        if (player.HaveKey)
            print("yes you win");

    }
}
