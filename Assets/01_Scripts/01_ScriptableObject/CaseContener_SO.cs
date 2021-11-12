using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cool Stuff", menuName = "New Scriptable Object/New CaseContener")]
public class CaseContener_SO : ScriptableObject
{
    [SerializeField] private string caseName;

    [SerializeField] private List<Object_SO> objectsRequired;

    [SerializeField] private List<Object_SO> caseResult;

    public List<Object_SO> ObjectsRequired { get => objectsRequired; set => objectsRequired = value; }
    public List<Object_SO> CaseResult { get => caseResult; set => caseResult = value; }
}
