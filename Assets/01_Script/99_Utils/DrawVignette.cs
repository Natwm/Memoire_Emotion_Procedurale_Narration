using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DrawVignette 
{
    [SerializeField] private Vignette_Behaviours.VignetteCategories categoryToDraw;
    [SerializeField] private int amountOfCardToDraw;

    public int AmountOfCardToDraw { get => amountOfCardToDraw; set => amountOfCardToDraw = value; }
    public Vignette_Behaviours.VignetteCategories CategoryToDraw { get => categoryToDraw; set => categoryToDraw = value; }
}
