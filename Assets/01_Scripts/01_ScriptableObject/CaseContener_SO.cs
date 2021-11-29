using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cool Stuff", menuName = "New Scriptable Object/New CaseContener")]
public class CaseContener_SO : ScriptableObject
{
    [SerializeField] private string caseName;

    [SerializeField] private bool anyVignette;

    [SerializeField] private List<Vignette_Behaviours.VignetteCategories> objectsRequired;

    [SerializeField] private Vignette_Behaviours.VignetteCategories result;

    public List<Vignette_Behaviours.VignetteCategories> ObjectsRequired { get => objectsRequired; set => objectsRequired = value; }
    public Vignette_Behaviours.VignetteCategories CaseResult { get => result; set => result = value; }
    public bool AnyVignette { get => anyVignette; set => anyVignette = value; }
}
