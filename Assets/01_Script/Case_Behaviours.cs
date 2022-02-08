using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case_Behaviours : MonoBehaviour
{
    [SerializeField] private CaseContener_SO caseEffects;

    public CaseContener_SO CaseEffects { get => caseEffects; set => caseEffects = value; }
}
