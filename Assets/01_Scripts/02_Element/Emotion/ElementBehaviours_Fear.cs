using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviours_Fear : MonoBehaviour, IModifier
{
    [SerializeField] private int amountOfAngryOrfear;

    public int AmountOfAngryOrfear { get => amountOfAngryOrfear; set => amountOfAngryOrfear = value; }

    public void CollectElement(EventContener eventElt)
    {
        eventElt.CurrentAngry_Fear += AmountOfAngryOrfear;
    }
}
