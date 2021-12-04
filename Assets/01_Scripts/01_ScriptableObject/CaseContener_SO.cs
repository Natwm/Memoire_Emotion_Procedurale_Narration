﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cool Stuff", menuName = "New Scriptable Object/New CaseContener")]
public class CaseContener_SO : ScriptableObject
{
    [SerializeField] private string caseName;

    [SerializeField] private bool anyVignette;

    [SerializeField] private bool useSpecifiqueObject;

    [SerializeField] private bool isEchecResult;

    [SerializeField] private List<Vignette_Behaviours.VignetteCategories> objectsRequired;

    [SerializeField] private Object_SO specifiqueObject;

    [SerializeField] private Vignette_Behaviours.VignetteCategories result;

    [SerializeField] private Vignette_Behaviours.VignetteCategories echecResult;

    public List<Vignette_Behaviours.VignetteCategories> ObjectsRequired { get => objectsRequired; set => objectsRequired = value; }
    public Vignette_Behaviours.VignetteCategories CaseResult { get => result; set => result = value; }
    public bool AnyVignette { get => anyVignette; set => anyVignette = value; }
    public Vignette_Behaviours.VignetteCategories EchecResult { get => echecResult; set => echecResult = value; }
    public Object_SO SpecifiqueObject { get => specifiqueObject; set => specifiqueObject = value; }
    public bool IsEchecResult { get => isEchecResult; set => isEchecResult = value; }
}
