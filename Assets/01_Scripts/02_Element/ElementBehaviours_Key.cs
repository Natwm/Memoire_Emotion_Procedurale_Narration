using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviours_Key : MonoBehaviour, IModifier
{

    public void CollectElement(EventContener eventElt)
    {
        eventElt.IsKey = true;
    }
}
