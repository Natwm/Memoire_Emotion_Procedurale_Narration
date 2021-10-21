using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventContener : MonoBehaviour
{
    [Header("Health Effects")]
    [SerializeField] private int happy_Sad;
    [SerializeField] private int currentHappy_Sad;

    [Header("Movement Effects")]
    [SerializeField] private int angry_Fear;
    [SerializeField] private int currentAngry_Fear;

    [Header("Hand Effect")]
    [SerializeField] private int amountOfVignetteToDraw;
    [SerializeField] private int currentAmountOfVignetteToDraw;

    [Header("key")]
    [SerializeField] private bool isKey;
    [SerializeField] private bool currentKeyState;

    public void SetUp(int happySad_Value = 0, int angryFear_Value = 0, int amountofVignetteToDraw_Value = 0, bool isKey = false)
    {
        happy_Sad = happySad_Value;
        angry_Fear = angryFear_Value;
        amountOfVignetteToDraw = amountofVignetteToDraw_Value;
        this.isKey = isKey;
    }

    public void UpdateCharacterFace()
    {
        if (GetComponent<Vignette_Behaviours>().assignedVignette != null)
        {
            GetComponent<Vignette_Behaviours>().assignedVignette.CharacterFeedback();
            foreach (Character item in GetComponent<Vignette_Behaviours>().assignedVignette.inVignetteCharacter)
        {
            if (item != null)
            {
                 
                GetComponent<Vignette_Behaviours>().assignedVignette.UpdateCharactersState(-currentHappy_Sad, -currentAngry_Fear, item);
            }
            
        }
        }
    }

    public void SetUpWithModifier(int happySad_Value = 0, int angryFear_Value = 0, int amountofVignetteToDraw_Value = 0, bool isKey = false)
    {
        happy_Sad += happySad_Value;
        angry_Fear += angryFear_Value;
        amountOfVignetteToDraw += amountofVignetteToDraw_Value;
        
        this.isKey = isKey;
    }

    public void ResetEvent()
    {
        CurrentHappy_Sad = happy_Sad;
        CurrentAngry_Fear = angry_Fear;
        CurrentAmountOfVignetteToDraw = amountOfVignetteToDraw;
        CurrentKeyState = isKey;
        
       UpdateCharacterFace();
        
        
    }

    public int Happy_Sad { get => happy_Sad; set => happy_Sad = value; }
    public int Angry_Fear { get => angry_Fear; set => angry_Fear = value; }
    public bool IsKey { get => isKey; set => isKey = value; }
    public int AmountOfVignetteToDraw { get => amountOfVignetteToDraw; set => amountOfVignetteToDraw = value; }
    public int CurrentHappy_Sad { get => currentHappy_Sad; set => currentHappy_Sad = value; }
    public int CurrentAngry_Fear { get => currentAngry_Fear; set => currentAngry_Fear = value; }
    public int CurrentAmountOfVignetteToDraw { get => currentAmountOfVignetteToDraw; set => currentAmountOfVignetteToDraw = value; }
    public bool CurrentKeyState { get => currentKeyState; set => currentKeyState = value; }
}
