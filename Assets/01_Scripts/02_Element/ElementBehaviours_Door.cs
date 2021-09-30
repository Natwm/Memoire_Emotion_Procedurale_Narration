using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviours_Door : ElementBehaviours
{
    PlayerManager player;
    private void Start()
    {
        player = FindObjectOfType<PlayerManager>();
        
    }

    public override void CollectElement(EventContener eventElt)
    {
        player.HaveKey = true;
        FindObjectOfType<Porte>().SetDoor(true);
    }
}
