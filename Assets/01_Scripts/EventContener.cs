using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventContener : MonoBehaviour
{
    [Header("Health Effects")]
    [SerializeField] private int happy_Sad;

    [Header("Movement Effects")]
    [SerializeField] private int angry_Fear;

    [Header("Hand Effect")]
    [SerializeField] private int vignette;

    [Header("key")]
    [SerializeField] private bool isKey;

    public int Happy_Sad { get => happy_Sad; set => happy_Sad = value; }
    public int Angry_Fear { get => angry_Fear; set => angry_Fear = value; }
    public bool IsKey { get => isKey; set => isKey = value; }
    public int Vignette { get => vignette; set => vignette = value; }

    public void SetUpEvent(Carte_SO eventObj)
    {
        Happy_Sad = eventObj.Happy_Sad;

        Angry_Fear = eventObj.Angry_Fear;

        Vignette = eventObj.AmountOfVignetteToDraw;
    }

    public void AffectByAModifier(IModifier modifer)
    {

    }
}
